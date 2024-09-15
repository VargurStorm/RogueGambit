using RogueGambit.Utils;

namespace RogueGambit.Models.State;

public class GameState
{
    public GameState()
    {
        BoardSquares = new List<BoardSquareModel>();
        Pieces = new List<PieceModel>();

        GD.Print("...BoardState object ready.");
    }

    public List<BoardSquareModel> BoardSquares { get; set; }
    public List<PieceModel> Pieces { get; set; }
    public Vector2 BoardShape { get; set; }
    public List<Vector2> BoardMasks { get; set; }
    public PieceOwner CurrentTurn { get; set; }

    public void ReadGameStateFromNodes(BoardManager boardManager, PieceManager pieceManager)
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

    public void ReadBoardShape()
    {
        var boardSquares = BoardSquares.Select(square => square.GridPosition).ToList();
        var boardShape = VectorUtils.GetGridSize(boardSquares);
        var boardMasks = VectorUtils.GetGridMasks(boardShape, boardSquares);
        BoardShape = boardShape;
        BoardMasks = boardMasks;
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

    public void UpdateBoardNodes()
    {
        foreach (var square in BoardSquares) square.UpdateNode();
    }

    public void UpdatePieceNodes()
    {
        foreach (var piece in Pieces) piece.UpdateNode();
    }
}