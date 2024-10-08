using RogueGambit.Handlers.Interface;
using RogueGambit.Logic;
using Piece = RogueGambit.Models.Piece;

namespace RogueGambit.Handlers;

public partial class InputHandler : Node2D, IInputHandler
{
    [Inject] private IBoardHandler _boardHandler;
    [Inject] private IGameStateHandler _gameStateHandler;
    [Inject] private IMoveHandler _moveHandler;
    [Inject] private IPieceHandler _pieceHandler;
    public Vector2 MousePosition { get; private set; }
    public ClickMode ClickMode { get; set; } = ClickMode.PieceOnly;

    public void ConnectSignals()
    {
        var allPieces = _pieceHandler.GetPieceNodes();
        foreach (var piece in allPieces)
        {
            piece.Connect("PieceClicked", new Callable(this, nameof(OnPieceClicked)));
            piece.Connect("PieceMouseEntered", new Callable(this, nameof(OnPieceMouseEntered)));
            piece.Connect("PieceMouseExited", new Callable(this, nameof(OnPieceMouseExited)));
        }

        var allSquares = _boardHandler.GetBoardSquareNodes();
        foreach (var square in allSquares)
        {
            square.Connect("SquareClicked", new Callable(this, nameof(OnBoardSquareClicked)));
            square.Connect("SquareMouseEntered", new Callable(this, nameof(OnBoardSquareMouseEntered)));
            square.Connect("SquareMouseExited", new Callable(this, nameof(OnBoardSquareMouseExited)));
        }
    }

    public void Initialize()
    {
        InjectDependencies(this);
        ConnectSignals();
    }

    public override void _Ready()
    {
        GD.Print("...InputHandler ready.");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is not InputEventMouseMotion mouseMotion) return;
        MousePosition = mouseMotion.Position;
    }

    // Pieces
    private void OnPieceClicked(Piece clickedPiece)
    {
        ClickMode = InputLogic.GetClickMode(_gameStateHandler.PlayerStatus);
        if (ClickMode is ClickMode.None or ClickMode.UiOnly or ClickMode.BoardOnly) return;

        if (_gameStateHandler.PlayerStatus == SelectingPiece)
        {
            _gameStateHandler.SelectPiece(clickedPiece.PieceModel);
            _gameStateHandler.PlayerStatus = MovingPiece;
        }

        GetViewport().SetInputAsHandled();
    }

    private void OnPieceMouseEntered(Piece piece)
    {
        piece.OverlaySprite.Visible = true;
    }

    private void OnPieceMouseExited(Piece piece)
    {
        piece.OverlaySprite.Visible = false;
    }

    // BoardSquares
    private void OnBoardSquareClicked(BoardSquare clickedSquare)
    {
        ClickMode = InputLogic.GetClickMode(_gameStateHandler.PlayerStatus);
        if (ClickMode is ClickMode.None or ClickMode.UiOnly or ClickMode.PieceOnly) return;


        if (InputLogic.IsNormalMove(clickedSquare.BoardSquareModel))
        {
            _gameStateHandler.MovePiece(_moveHandler.SelectedPiece, clickedSquare.GridPosition);
            _gameStateHandler.PlayerStatus = SelectingPiece;
            GetViewport().SetInputAsHandled();
            return;
        }

        if (InputLogic.IsAttackMove(clickedSquare.BoardSquareModel, _moveHandler.SelectedPiece,
                                    _gameStateHandler.GameState))
        {
            _gameStateHandler.CapturePiece(_moveHandler.SelectedPiece,
                                           _gameStateHandler.GameState.GetPieceAtPosition(clickedSquare.GridPosition));
            _gameStateHandler.PlayerStatus = SelectingPiece;
        }


        GetViewport().SetInputAsHandled();
    }

    private static void OnBoardSquareMouseEntered(BoardSquare square)
    {
        if (!square.IsOccupied) square.OverlaySprite.Visible = true;
    }

    private static void OnBoardSquareMouseExited(BoardSquare square)
    {
        square.OverlaySprite.Visible = false;
    }
}