using Piece = RogueGambit.Models.Piece;
using static RogueGambit.Logic.InputLogic;

namespace RogueGambit.Managers;

public partial class InputManager : Node2D
{
    private PieceManager PieceManager => GetNode<PieceManager>(PathConstants.PieceManager);
    private BoardManager BoardManager => GetNode<BoardManager>(PathConstants.BoardManager);
    private GameStateManager GameStateManager => GetNode<GameStateManager>(PathConstants.GameStateManager);
    private MoveManager MoveManager => GetNode<MoveManager>(PathConstants.MoveManager);
    public Vector2 MousePosition { get; private set; }

    public override void _Ready()
    {
        GD.Print("...InputManager ready.");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is not InputEventMouseMotion mouseMotion) return;
        MousePosition = mouseMotion.Position;
    }

    // Signals
    public void ConnectSignals()
    {
        var allPieces = PieceManager.GetPiecesOnBoard();
        foreach (var piece in allPieces)
        {
            piece.Connect("PieceClicked", new Callable(this, nameof(OnPieceClicked)));
            piece.Connect("PieceMouseEntered", new Callable(this, nameof(OnPieceMouseEntered)));
            piece.Connect("PieceMouseExited", new Callable(this, nameof(OnPieceMouseExited)));
        }

        var allSquares = BoardManager.GetBoardSquares();
        foreach (var square in allSquares)
        {
            square.Connect("SquareClicked", new Callable(this, nameof(OnBoardSquareClicked)));
            square.Connect("SquareMouseEntered", new Callable(this, nameof(OnBoardSquareMouseEntered)));
            square.Connect("SquareMouseExited", new Callable(this, nameof(OnBoardSquareMouseExited)));
        }
    }

    // Pieces
    private void OnPieceClicked(Piece piece)
    {
        GetViewport().SetInputAsHandled();
        if (IsMyPiece(piece.PieceModel, PieceOwner.Player) && IsMyTurn(GameStateManager.GameState, PieceOwner.Player) &&
            MoveManager.SelectedPiece is null)
            GameStateManager.ToggleSelectedPiece(piece.PieceModel);
    }

    private static void OnPieceMouseEntered(Piece piece)
    {
        piece.OverlaySprite.Visible = true;
    }

    private static void OnPieceMouseExited(Piece piece)
    {
        piece.OverlaySprite.Visible = false;
    }

    // BoardSquares
    private void OnBoardSquareClicked(BoardSquare square)
    {
        if (MoveManager.SelectedPiece is null) return;
        if (IsMyTurn(GameStateManager.GameState, PieceOwner.Player))
        {
            GameStateManager.MovePiece(MoveManager.SelectedPiece, square.GridPosition);
            MoveManager.DeselectPiece();
        }
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