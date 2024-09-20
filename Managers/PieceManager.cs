using RogueGambit.Managers.Factory;
using Piece = RogueGambit.Models.Piece;

namespace RogueGambit.Managers;

public partial class PieceManager : Node2D, INodeFactory, IPieceManager
{
	private PackedScene _pieceScene;

	public Node2D CreateNoteForModel(INodeModel model)
	{
		if (model is not PieceModel pieceModel)
		{
			GD.PrintErr("Piece manager can only place piece models.");
			return null;
		}

		if (_pieceScene is null)
		{
			GD.PrintErr("Piece scene could not be loaded.");
			return null;
		}

		if (_pieceScene.Instantiate() is not Piece piece)
		{
			GD.PrintErr("Piece could not be instantiated.");
			return null;
		}

		piece.PieceColor = pieceModel.Color;
		piece.PieceType = pieceModel.Type;
		piece.GridPosition = pieceModel.GridPosition;
		AddChild(piece);
		return piece;
	}

	public override void _Ready()
	{
		GD.Print("...PieceManager ready.");
	}

	public void LoadScenes()
	{
		_pieceScene = GD.Load<PackedScene>(PathConstants.PieceScenePath);
		GD.Print("...Loaded piece scene.");
	}

	public static Dictionary<Vector2, PieceModel> CreatePieceModelsDefault()
	{
		var pieceDictionary = new Dictionary<Vector2, PieceModel>();

		for (var x = 0; x < BoardConstants.BoardSize; x++)
		{
			pieceDictionary.Add(new Vector2(x, 1), new PieceModel(new Vector2(x, 1), PieceColor.Black, PieceType.Pawn));
			pieceDictionary.Add(new Vector2(x, 6), new PieceModel(new Vector2(x, 6), PieceColor.White, PieceType.Pawn));
		}

		pieceDictionary.Add(new Vector2(0, 0), new PieceModel(new Vector2(0, 0), PieceColor.Black, PieceType.Rook));
		pieceDictionary.Add(new Vector2(7, 0), new PieceModel(new Vector2(7, 0), PieceColor.Black, PieceType.Rook));
		pieceDictionary.Add(new Vector2(1, 0), new PieceModel(new Vector2(1, 0), PieceColor.Black, PieceType.Knight));
		pieceDictionary.Add(new Vector2(6, 0), new PieceModel(new Vector2(6, 0), PieceColor.Black, PieceType.Knight));
		pieceDictionary.Add(new Vector2(2, 0), new PieceModel(new Vector2(2, 0), PieceColor.Black, PieceType.Bishop));
		pieceDictionary.Add(new Vector2(5, 0), new PieceModel(new Vector2(5, 0), PieceColor.Black, PieceType.Bishop));
		pieceDictionary.Add(new Vector2(3, 0), new PieceModel(new Vector2(3, 0), PieceColor.Black, PieceType.Queen));
		pieceDictionary.Add(new Vector2(4, 0), new PieceModel(new Vector2(4, 0), PieceColor.Black, PieceType.King));

		pieceDictionary.Add(new Vector2(1, 7), new PieceModel(new Vector2(1, 7), PieceColor.White, PieceType.Knight));
		pieceDictionary.Add(new Vector2(6, 7), new PieceModel(new Vector2(6, 7), PieceColor.White, PieceType.Knight));
		pieceDictionary.Add(new Vector2(0, 7), new PieceModel(new Vector2(0, 7), PieceColor.White, PieceType.Rook));
		pieceDictionary.Add(new Vector2(7, 7), new PieceModel(new Vector2(7, 7), PieceColor.White, PieceType.Rook));
		pieceDictionary.Add(new Vector2(2, 7), new PieceModel(new Vector2(2, 7), PieceColor.White, PieceType.Bishop));
		pieceDictionary.Add(new Vector2(5, 7), new PieceModel(new Vector2(5, 7), PieceColor.White, PieceType.Bishop));
		pieceDictionary.Add(new Vector2(3, 7), new PieceModel(new Vector2(3, 7), PieceColor.White, PieceType.Queen));
		pieceDictionary.Add(new Vector2(4, 7), new PieceModel(new Vector2(4, 7), PieceColor.White, PieceType.King));

		return pieceDictionary;
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

	public List<Piece> GetPieceNodes()
	{
		var pieces = new List<Piece>();

		foreach (var child in GetChildren())
			if (child is Piece piece)
				pieces.Add(piece);

		return pieces;
	}
}
