using RogueGambit.Managers.Factory;

namespace RogueGambit.Models.State;

public class PieceModel : INodeModel
{
    private readonly INodeFactory _nodeFactory;

    public PieceModel(Piece piece)
    {
        GridPosition = piece.GridPosition;
        Color = piece.PieceColor;
        Type = piece.PieceType;
        Instance = piece;
        _nodeFactory = ServiceLocator.GetNodeFactory<Piece>();
    }

    public PieceModel(Vector2 gridPosition, PieceColor color, PieceType type)
    {
        GridPosition = gridPosition;
        Color = color;
        Type = type;
        _nodeFactory = ServiceLocator.GetNodeFactory<Piece>();
    }

    public Vector2 GridPosition { get; set; }
    public PieceColor Color { get; set; }
    public PieceType Type { get; set; }
    public Piece Instance { get; set; }
    public PieceOwner Owner { get; set; } = PieceOwner.None;

    public void UpdateNode(bool create = false)
    {
        if (create || Instance is null) Instance = (Piece)_nodeFactory.CreateNoteForModel(this);

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