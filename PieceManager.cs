using System.Collections.Generic;
using Godot;
using RogueGambit.Scenes.Pieces;
using RogueGambit.Static;

public partial class PieceManager : Node2D
{
	private PackedScene _pieceScene;


	public override void _Ready()
	{
		GD.Print("PieceManager ready.");
		LoadScenes();
		PlacePieces();
		GD.Print("Pieces placed.");
	}

	private void LoadScenes()
	{
		_pieceScene = GD.Load<PackedScene>(PathConstants.PieceScenePath);
	}

	private void PlacePieces()
	{
		if (_pieceScene is null)
		{
			GD.PrintErr("Piece scene could not be loaded.");
			return;
		}

		for (var x = 0; x < BoardConstants.BoardSize; x++)
		{
			PlacePiece(new Vector2(x, 1), PieceColor.White, PieceType.Pawn);
			PlacePiece(new Vector2(x, 6), PieceColor.Black, PieceType.Pawn);
		}

		PlacePiece(new Vector2(0, 0), PieceColor.White, PieceType.Rook);
		PlacePiece(new Vector2(7, 0), PieceColor.White, PieceType.Rook);
		PlacePiece(new Vector2(1, 0), PieceColor.White, PieceType.Knight);
		PlacePiece(new Vector2(6, 0), PieceColor.White, PieceType.Knight);
		PlacePiece(new Vector2(2, 0), PieceColor.White, PieceType.Bishop);
		PlacePiece(new Vector2(5, 0), PieceColor.White, PieceType.Bishop);
		PlacePiece(new Vector2(3, 0), PieceColor.White, PieceType.Queen);
		PlacePiece(new Vector2(4, 0), PieceColor.White, PieceType.King);

		PlacePiece(new Vector2(1, 7), PieceColor.Black, PieceType.Knight);
		PlacePiece(new Vector2(6, 7), PieceColor.Black, PieceType.Knight);
		PlacePiece(new Vector2(0, 7), PieceColor.Black, PieceType.Rook);
		PlacePiece(new Vector2(7, 7), PieceColor.Black, PieceType.Rook);
		PlacePiece(new Vector2(2, 7), PieceColor.Black, PieceType.Bishop);
		PlacePiece(new Vector2(5, 7), PieceColor.Black, PieceType.Bishop);
		PlacePiece(new Vector2(3, 7), PieceColor.Black, PieceType.Queen);
		PlacePiece(new Vector2(4, 7), PieceColor.Black, PieceType.King);
	}

	private void PlacePiece(Vector2 boardPosition, PieceColor color, PieceType type)
	{
		var pieceInstance = _pieceScene.Instantiate() as Piece;
		if (pieceInstance is null)
		{
			GD.PrintErr("Failed to instantiate piece.");
			return;
		}

		pieceInstance.Position = new Vector2(boardPosition.X * BoardConstants.SquareSize,
			boardPosition.Y * BoardConstants.SquareSize);
		pieceInstance.PieceColor = color;
		pieceInstance.PieceType = type;
		pieceInstance.GridPosition = boardPosition;
		AddChild(pieceInstance);
	}

	public List<Piece> GetPiecesOnBoard()
	{
		var pieces = new List<Piece>();
		var pieceManager = GetNode<PieceManager>("/root/MainScene/PieceManager");

		foreach (var child in pieceManager.GetChildren())
			if (child is Piece piece)
				pieces.Add(piece);

		return pieces;
	}
}
