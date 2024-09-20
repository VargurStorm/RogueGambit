namespace RogueGambit.Static.Constants;

public static class BoardConstants
{
	public static int BoardSize { get; set; } = 8;
	public static int SquareSize { get; set; } = 64;
	public static int BoardStartX { get; set; } = 0;
	public static int BoardStartY { get; set; } = 0;
	public static int BoardEndX => BoardStartX + BoardSize * SquareSize;
	public static int BoardEndY => BoardStartY + BoardSize * SquareSize;
	public static Vector2 SquareSizeVector => new(SquareSize, SquareSize);
	public static Color LightSquareColor => new("D7B899");
	public static Color DarkSquareColor => new("8B4513");
	public static Color MaskedSquareColor => new("FF0000");
	public static string StandardFen => "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
}
