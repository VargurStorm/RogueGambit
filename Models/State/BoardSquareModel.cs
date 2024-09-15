namespace RogueGambit.Models.State;

public class BoardSquareModel
{
    public Vector2 GridPosition { get; set; }
    public Color SquareColor { get; set; }
    public bool IsOccupied { get; set; }
    public BoardSquare Instance { get; set; }

    public void UpdateNode(bool create = false)
    {
        if (create && Instance is null) Instance = new BoardSquare();
        Instance.GridPosition = GridPosition;
        Instance.SquareColor = SquareColor;
        Instance.IsOccupied = IsOccupied;
        Instance.UpdateColor(SquareColor);
        Instance.BoardSquareModel = this;
    }

    public void ReadNode()
    {
        if (Instance is null) return;
        GridPosition = Instance.GridPosition;
        SquareColor = Instance.SquareColor;
        IsOccupied = Instance.IsOccupied;
    }

    public void DestroyNode()
    {
        Instance?.QueueFree();
    }
}