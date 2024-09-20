namespace RogueGambit.Static.Constants;

public static class FenMap
{
    public static Dictionary<char, (PieceType, PieceColor)> PieceTypeMap { get; set; } = new()
    {
        { 'p', (PieceType.Pawn, PieceColor.Black) },
        { 'r', (PieceType.Rook, PieceColor.Black) },
        { 'n', (PieceType.Knight, PieceColor.Black) },
        { 'b', (PieceType.Bishop, PieceColor.Black) },
        { 'q', (PieceType.Queen, PieceColor.Black) },
        { 'k', (PieceType.King, PieceColor.Black) },
        { 'P', (PieceType.Pawn, PieceColor.White) },
        { 'R', (PieceType.Rook, PieceColor.White) },
        { 'N', (PieceType.Knight, PieceColor.White) },
        { 'B', (PieceType.Bishop, PieceColor.White) },
        { 'Q', (PieceType.Queen, PieceColor.White) },
        { 'K', (PieceType.King, PieceColor.White) }
    };
}