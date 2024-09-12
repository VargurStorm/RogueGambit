using System.Collections.Generic;
using Godot;
using RogueGambit.Static.Constants;
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

    public void PlacePieces()
    {
        if (_pieceScene is null)
        {
            GD.PrintErr("Piece scene could not be loaded.");
            return;
        }

        for (var x = 0; x < BoardConstants.BoardSize; x++)
        {
            PlacePiece(new Vector2(x, 1), PieceConstants.PieceColor.White, PieceConstants.PieceType.Pawn);
            PlacePiece(new Vector2(x, 6), PieceConstants.PieceColor.Black, PieceConstants.PieceType.Pawn);
        }

        PlacePiece(new Vector2(0, 0), PieceConstants.PieceColor.White, PieceConstants.PieceType.Rook);
        PlacePiece(new Vector2(7, 0), PieceConstants.PieceColor.White, PieceConstants.PieceType.Rook);
        PlacePiece(new Vector2(1, 0), PieceConstants.PieceColor.White, PieceConstants.PieceType.Knight);
        PlacePiece(new Vector2(6, 0), PieceConstants.PieceColor.White, PieceConstants.PieceType.Knight);
        PlacePiece(new Vector2(2, 0), PieceConstants.PieceColor.White, PieceConstants.PieceType.Bishop);
        PlacePiece(new Vector2(5, 0), PieceConstants.PieceColor.White, PieceConstants.PieceType.Bishop);
        PlacePiece(new Vector2(3, 0), PieceConstants.PieceColor.White, PieceConstants.PieceType.Queen);
        PlacePiece(new Vector2(4, 0), PieceConstants.PieceColor.White, PieceConstants.PieceType.King);

        PlacePiece(new Vector2(1, 7), PieceConstants.PieceColor.Black, PieceConstants.PieceType.Knight);
        PlacePiece(new Vector2(6, 7), PieceConstants.PieceColor.Black, PieceConstants.PieceType.Knight);
        PlacePiece(new Vector2(0, 7), PieceConstants.PieceColor.Black, PieceConstants.PieceType.Rook);
        PlacePiece(new Vector2(7, 7), PieceConstants.PieceColor.Black, PieceConstants.PieceType.Rook);
        PlacePiece(new Vector2(2, 7), PieceConstants.PieceColor.Black, PieceConstants.PieceType.Bishop);
        PlacePiece(new Vector2(5, 7), PieceConstants.PieceColor.Black, PieceConstants.PieceType.Bishop);
        PlacePiece(new Vector2(3, 7), PieceConstants.PieceColor.Black, PieceConstants.PieceType.Queen);
        PlacePiece(new Vector2(4, 7), PieceConstants.PieceColor.Black, PieceConstants.PieceType.King);
    }

    private void PlacePiece(Vector2 boardPosition, PieceConstants.PieceColor color, PieceConstants.PieceType type)
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

        foreach (var child in GetChildren())
            if (child is Piece piece)
                pieces.Add(piece);

        return pieces;
    }
}