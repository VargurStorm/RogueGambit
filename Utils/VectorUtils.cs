namespace RogueGambit.Utils;

public static class VectorUtils
{
    public static Vector2 GetGridSize(List<Vector2> gridPositions)
    {
        var maxX = gridPositions.Max(v => v.X);
        var maxY = gridPositions.Max(v => v.Y);
        return new Vector2(maxX, maxY);
    }

    public static List<Vector2> GetGridMasks(Vector2 gridSize, List<Vector2> gridPositions)
    {
        var missingVectors = new List<Vector2>();
        for (var x = 0; x < gridSize.X; x++)
        for (var y = 0; y < gridSize.Y; y++)
        {
            var gridPosition = new Vector2(x, y);
            if (!gridPositions.Contains(gridPosition))
                missingVectors.Add(gridPosition);
        }

        return missingVectors;
    }
}