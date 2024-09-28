using System;

namespace RogueGambit.Managers;

public partial class GameStateManager : Node, IGameStateManager
{
	[Inject] private IBoardManager _boardManager;
	[Inject] private IInputManager _inputManager;
	[Inject] private IMoveLogic _moveLogic;
	[Inject] private IMoveManager _moveManager;
	[Inject] private IPieceManager _pieceManager;
	[Inject] private ITurnManager _turnManager;

	public PlayerStatus PlayerStatus
	{
		get => GameState.PlayerStatus;
		set => GameState.PlayerStatus = value;
	}


	public GameState GameState { get; set; }

	public void InitializeGameState()
	{
		GameState = new GameState();
	}

	public void LoadScenes()
	{
		_boardManager.LoadScenes();
		_pieceManager.LoadScenes();
	}

	public void PlaceBoard(int boardStart, Vector2 boardShape, List<List<int>> boardMask = null)
	{
		GameState.BoardSquares = BoardManager.BuildBoardSquareModels(boardStart, boardShape, boardMask);
		GameState.BoardSquares.Values.ToList().ForEach(square => square.UpdateNode(true));
		GameState.BoardShape = boardShape;
		GameState.BoardMask = boardMask;
	}

	public void PlacePieces()
	{
		GameState.Pieces = PieceManager.CreatePieceModelsDefault();
		GameState.Pieces.Values.ToList().ForEach(piece => piece.UpdateNode(true));
	}

	public void UpdateGameState()
	{
		GameState.FindOccupiedSquares();
		GameState.UpdateBoardNodes();
		GameState.UpdatePieceNodes();
	}

	public void AssignColorToOwner(PieceColor color, PieceOwner owner)
	{
		foreach (var piece in GameState.Pieces.Values.Where(piece => piece.Color == color)) piece.Owner = owner;
	}

	public void SetTurn(PieceOwner owner)
	{
		GameState.CurrentTurn = owner;
		_turnManager.SetTurn(owner);
	}


	public void MovePiece(PieceModel piece, Vector2 targetPosition)
	{
		_moveManager.MovePiece(piece, targetPosition);
		DeselectPiece();
		UpdateGameState();
		AdvanceTurn();
	}

	public void ToggleSelectedPiece(PieceModel piece)
	{
		_moveManager.ToggleSelectedPiece(piece);
	}

	public void CapturePiece(PieceModel attacker, PieceModel targetPiece)
	{
		GameState.Graveyard.Add(targetPiece);
		GameState.Pieces.Remove(targetPiece.GridPosition);
		targetPiece.Instance.QueueFree();
		MovePiece(attacker, targetPiece.GridPosition);
	}

	public void SelectPiece(PieceModel piece)
	{
		_moveManager.SelectPiece(piece);
		var validMoves = _moveLogic.GetValidMoves(piece);
		foreach (var move in validMoves)
		{
			var square = GameState.BoardSquares[move];
			square.Instance.TargetSprite.Visible = true;
		}
	}

	public void PromotePiece(PieceModel piece, PieceType newType)
	{
		throw new NotImplementedException();
	}


	public override void _Ready()
	{
		GD.Print("...GameStateManager ready.");
		InjectDependencies(this);
		InitializeGameState();
		LoadScenes();

		PlaceBoard(BoardStartX, new Vector2(8, 8));
		PlacePieces();
		_pieceManager.SetDefaultMoveSets();
		_inputManager.Initialize();
		_moveLogic.Initialize();

		AssignColorToOwner(PieceColor.White, PieceOwner.Player);
		AssignColorToOwner(PieceColor.Black, PieceOwner.Player);

		UpdateGameState();
		SetTurn(PieceOwner.Player);
	}


	public void AdvanceTurn()
	{
		_turnManager.AdvanceTurn();
	}

	public void DeselectPiece()
	{
		_moveManager.DeselectPiece();
		foreach (var square in GameState.BoardSquares.Values) square.Instance.TargetSprite.Visible = false;
	}
}
