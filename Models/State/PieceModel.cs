namespace RogueGambit.Models.State;

public class PieceModel
{
    public Vector2 GridPosition { get; set; }
    public PieceColor Color { get; set; }
    public PieceType Type { get; set; }
    public Piece Instance { get; set; }
    public PieceOwner Owner { get; set; } = PieceOwner.None;

    public void UpdateNode(bool create = false)
    {
        if (create && Instance is null) Instance = new Piece();
        Instance.GridPosition = GridPosition;
        Instance.PieceType = Type;
        Instance.PieceColor = Color;
        Instance.PieceModel = this;
    }

    public void ReadNode()
    {
        if (Instance is null) return;
        GridPosition = Instance.GridPosition;
        Type = Instance.PieceType;
        Color = Instance.PieceColor;
    }

    public void DestroyNode()
    {
        Instance?.QueueFree();
    }

    public override string ToString()
    {
        return $"{Owner}'s {Color} {Type} at {GridPosition}";
    }
}