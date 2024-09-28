using RogueGambit.Logic;
using Piece = RogueGambit.Models.Piece;

namespace RogueGambit.Managers;

public partial class InputManager : Node2D, IInputManager
{
	[Inject] private IBoardManager _boardManager;
	[Inject] private IGameStateManager _gameStateManager;
	[Inject] private IMoveManager _moveManager;
	[Inject] private IPieceManager _pieceManager;
	public Vector2 MousePosition { get; private set; }
	public ClickMode ClickMode { get; set; } = ClickMode.PieceOnly;

	public void ConnectSignals()
	{
		var allPieces = _pieceManager.GetPieceNodes();
		foreach (var piece in allPieces)
		{
			piece.Connect("PieceClicked", new Callable(this, nameof(OnPieceClicked)));
			piece.Connect("PieceMouseEntered", new Callable(this, nameof(OnPieceMouseEntered)));
			piece.Connect("PieceMouseExited", new Callable(this, nameof(OnPieceMouseExited)));
		}

		var allSquares = _boardManager.GetBoardSquareNodes();
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
		GD.Print("...InputManager ready.");
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is not InputEventMouseMotion mouseMotion) return;
		MousePosition = mouseMotion.Position;
	}

	// Pieces
	private void OnPieceClicked(Piece clickedPiece)
	{
		ClickMode = InputLogic.GetClickMode(_gameStateManager.PlayerStatus);
		if (ClickMode is ClickMode.None or ClickMode.UiOnly or ClickMode.BoardOnly) return;

		if (_gameStateManager.PlayerStatus == SelectingPiece)
		{
			_gameStateManager.SelectPiece(clickedPiece.PieceModel);
			_gameStateManager.PlayerStatus = MovingPiece;
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
		ClickMode = InputLogic.GetClickMode(_gameStateManager.PlayerStatus);
		if (ClickMode is ClickMode.None or ClickMode.UiOnly or ClickMode.PieceOnly) return;


		if (InputLogic.IsNormalMove(clickedSquare.BoardSquareModel))
		{
			_gameStateManager.MovePiece(_moveManager.SelectedPiece, clickedSquare.GridPosition);
			_gameStateManager.PlayerStatus = SelectingPiece;
			GetViewport().SetInputAsHandled();
			return;
		}

		if (InputLogic.IsAttackMove(clickedSquare.BoardSquareModel, _moveManager.SelectedPiece,
									_gameStateManager.GameState))
		{
			_gameStateManager.CapturePiece(_moveManager.SelectedPiece,
										   _gameStateManager.GameState.GetPieceAtPosition(clickedSquare.GridPosition));
			_gameStateManager.PlayerStatus = SelectingPiece;
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
