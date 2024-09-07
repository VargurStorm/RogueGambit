namespace RogueGambit.Static;

public static class BoardConstants
{
    public static int BoardSize { get; set; } = 8;
    public static int SquareSize { get; set; } = 64;
    public static int BoardStartX { get; set; } = 0;
    public static int BoardStartY { get; set; } = 0;

    public static int BoardEndX => BoardStartX + BoardSize * SquareSize;
    public static int BoardEndY => BoardStartY + BoardSize * SquareSize;

    public static string LightSquareColor { get; set; } = "D7B899";
    public static string DarkSquareColor { get; set; } = "8B4513";
}

public enum BoardColor
{
    Light,
    Dark
}