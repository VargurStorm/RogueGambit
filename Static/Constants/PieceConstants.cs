namespace RogueGambit.Static.Constants;

public static class PieceConstants
{
	public enum PieceColor
	{
		White,
		Black
	}

	public enum PieceOwner
	{
		Player,
		Ai,
		None
	}

	public enum PieceType
	{
		Pawn,
		Rook,
		Knight,
		Bishop,
		Queen,
		King
	}

	public static Dictionary<PieceType, MoveSet> MoveSetMap { get; set; }
}
