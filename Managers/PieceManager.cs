using Piece = RogueGambit.Models.Piece;

namespace RogueGambit.Managers;

public partial class PieceManager : Node2D
{
	private PackedScene _pieceScene;


	public override void _Ready()
	{
		GD.Print("...PieceManager ready.");
	}

	public void LoadScenes()
	{
		_pieceScene = GD.Load<PackedScene>(PathConstants.PieceScenePath);
		GD.Print("...Loaded piece scene.");
	}

	public void PlacePieceNodes()
	{
		if (_pieceScene is null)
		{
			GD.PrintErr("Piece scene could not be loaded.");
			return;
		}

		for (var x = 0; x < BoardConstants.BoardSize; x++)
		{
			PlacePieceNode(new Vector2(x, 1), PieceColor.Black, PieceType.Pawn);
			PlacePieceNode(new Vector2(x, 6), PieceColor.White, PieceType.Pawn);
		}

		PlacePieceNode(new Vector2(0, 0), PieceColor.Black, PieceType.Rook);
		PlacePieceNode(new Vector2(7, 0), PieceColor.Black, PieceType.Rook);
		PlacePieceNode(new Vector2(1, 0), PieceColor.Black, PieceType.Knight);
		PlacePieceNode(new Vector2(6, 0), PieceColor.Black, PieceType.Knight);
		PlacePieceNode(new Vector2(2, 0), PieceColor.Black, PieceType.Bishop);
		PlacePieceNode(new Vector2(5, 0), PieceColor.Black, PieceType.Bishop);
		PlacePieceNode(new Vector2(3, 0), PieceColor.Black, PieceType.Queen);
		PlacePieceNode(new Vector2(4, 0), PieceColor.Black, PieceType.King);

		PlacePieceNode(new Vector2(1, 7), PieceColor.White, PieceType.Knight);
		PlacePieceNode(new Vector2(6, 7), PieceColor.White, PieceType.Knight);
		PlacePieceNode(new Vector2(0, 7), PieceColor.White, PieceType.Rook);
		PlacePieceNode(new Vector2(7, 7), PieceColor.White, PieceType.Rook);
		PlacePieceNode(new Vector2(2, 7), PieceColor.White, PieceType.Bishop);
		PlacePieceNode(new Vector2(5, 7), PieceColor.White, PieceType.Bishop);
		PlacePieceNode(new Vector2(3, 7), PieceColor.White, PieceType.Queen);
		PlacePieceNode(new Vector2(4, 7), PieceColor.White, PieceType.King);
	}

	private void PlacePieceNode(Vector2 boardPosition, PieceColor color, PieceType type)
	{
		var pieceInstance = _pieceScene.Instantiate() as Piece;
		if (pieceInstance is null)
		{
			GD.PrintErr("Failed to instantiate piece.");
			return;
		}

		pieceInstance.PieceColor = color;
		pieceInstance.PieceType = type;
		pieceInstance.GridPosition = boardPosition;
		AddChild(pieceInstance);
	}

	public List<Piece> GetPiecesOnBoard()
	{
		var pieces = new List<Piece>();

		foreach (var child in GetChildren())
			if (child is Piece piece)
				pieces.Add(piece);

		return pieces;
	}
}
