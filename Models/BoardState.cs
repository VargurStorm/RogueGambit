using System.Collections.Generic;
using Godot;
using RogueGambit.Managers;
using RogueGambit.Static.Constants;

namespace RogueGambit.Models;

public class BoardState
{
    public BoardState()
    {
        BoardSquares = new List<BoardSquareModel>();
        Pieces = new List<PieceModel>();
        GD.Print("...BoardState object ready.");
    }

    public List<BoardSquareModel> BoardSquares { get; set; }
    public List<PieceModel> Pieces { get; set; }

    public void FindBoardState(BoardManager boardManager, PieceManager pieceManager)
    {
        var boardSquares = boardManager.GetBoardSquares();
        var pieces = pieceManager.GetPiecesOnBoard();

        foreach (var square in boardSquares)
            BoardSquares.Add(new BoardSquareModel
            {
                GridPosition = square.GridPosition,
                SquareColor = square.SquareColor,
                IsOccupied = square.IsOccupied,
                Instance = square
            });

        foreach (var piece in pieces)
            Pieces.Add(new PieceModel
            {
                GridPosition = piece.GridPosition,
                Color = piece.PieceColor,
                Type = piece.PieceType,
                Instance = piece
            });
    }

    public void FindOccupiedSquares()
    {
        foreach (var square in BoardSquares)
        {
            square.IsOccupied = false;
            foreach (var piece in Pieces)
                if (piece.GridPosition == square.GridPosition)
                    square.IsOccupied = true;
        }
    }

    public string GetPieceList()
    {
        var pieceList = "";
        foreach (var piece in Pieces) pieceList += $"Piece: {piece.Type} at {piece.GridPosition}\n";

        return pieceList;
    }

    public void UpdateBoardState()
    {
        foreach (var square in BoardSquares) square.UpdateNode();
        foreach (var piece in Pieces) piece.UpdateNode();
    }


    public class BoardSquareModel
    {
        public Vector2 GridPosition { get; set; }
        public Color SquareColor { get; set; }
        public bool IsOccupied { get; set; }
        public BoardSquare Instance { get; set; }

        public void UpdateNode()
        {
            Instance.GridPosition = GridPosition;
            Instance.SquareColor = SquareColor;
            Instance.IsOccupied = IsOccupied;
            Instance.UpdateColor(SquareColor);
        }
    }

    public class PieceModel
    {
        public Vector2 GridPosition { get; set; }
        public PieceConstants.PieceColor Color { get; set; }
        public PieceConstants.PieceType Type { get; set; }
        public Piece Instance { get; set; }

        public void UpdateNode()
        {
            Instance.GridPosition = GridPosition;
            Instance.PieceType = Type;
            Instance.PieceColor = Color;
        }
    }
}