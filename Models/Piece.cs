namespace RogueGambit.Models;

public partial class Piece : Node2D
{
    [Signal]
    public delegate void PieceClickedEventHandler(Piece piece);

    [Signal]
    public delegate void PieceMouseEnteredEventHandler(Piece piece);

    [Signal]
    public delegate void PieceMouseExitedEventHandler(Piece piece);

    private static readonly Dictionary<(PieceType, PieceColor), string> TextureMap = new()
    {
        { (PieceType.Pawn, PieceColor.White), "res://Assets/Pieces/white_pawn.png" },
        { (PieceType.Pawn, PieceColor.Black), "res://Assets/Pieces/black_pawn.png" },
        { (PieceType.Knight, PieceColor.White), "res://Assets/Pieces/white_knight.png" },
        { (PieceType.Knight, PieceColor.Black), "res://Assets/Pieces/black_knight.png" },
        { (PieceType.Rook, PieceColor.White), "res://Assets/Pieces/white_rook.png" },
        { (PieceType.Rook, PieceColor.Black), "res://Assets/Pieces/black_rook.png" },
        { (PieceType.Bishop, PieceColor.White), "res://Assets/Pieces/white_bishop.png" },
        { (PieceType.Bishop, PieceColor.Black), "res://Assets/Pieces/black_bishop.png" },
        { (PieceType.Queen, PieceColor.White), "res://Assets/Pieces/white_queen.png" },
        { (PieceType.Queen, PieceColor.Black), "res://Assets/Pieces/black_queen.png" },
        { (PieceType.King, PieceColor.White), "res://Assets/Pieces/white_king.png" },
        { (PieceType.King, PieceColor.Black), "res://Assets/Pieces/black_king.png" }
    };

    private Vector2 _gridPos;
    private Area2D _pieceArea2D;
    private CollisionShape2D _pieceCollision2D;
    private Sprite2D _pieceSprite;
    public PieceModel PieceModel { get; set; }

    [Export] public PieceType PieceType { get; set; }
    [Export] public PieceColor PieceColor { get; set; }

    [Export]
    public Vector2 GridPosition
    {
        get => _gridPos;
        set
        {
            _gridPos = value;
            Position = value * BoardConstants.SquareSize;
        }
    }

    [Export] public Sprite2D OverlaySprite { get; set; }
    [Export] public Sprite2D SelectedSprite { get; set; }

    public override void _Ready()
    {
        InitializeNodes();
        ConnectSignals();
        UpdateSprite();
    }

    private void InitializeNodes()
    {
        _pieceSprite = GetNode<Sprite2D>("pieceSprite2D");
        _pieceArea2D = GetNode<Area2D>("pieceArea2D");
        _pieceCollision2D = GetNode<CollisionShape2D>("pieceArea2D/pieceCollision2D");
        OverlaySprite = GetNode<Sprite2D>("pieceSprite2D/pieceOverlay2D");
        OverlaySprite.Visible = false;
        SelectedSprite = GetNode<Sprite2D>("pieceSprite2D/pieceSelected2D");
        SelectedSprite.Visible = false;
    }

    private void ConnectSignals()
    {
        SetProcessInput(true);
        _pieceArea2D.Connect("input_event", new Callable(this, nameof(OnAreaInputEvent)));
        _pieceArea2D.Connect("mouse_entered", new Callable(this, nameof(OnMouseEntered)));
        _pieceArea2D.Connect("mouse_exited", new Callable(this, nameof(OnMouseExited)));
    }

    private void OnAreaInputEvent(Node viewport, InputEvent @event, int shapeIdx)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.ButtonIndex == MouseButton.Left &&
            mouseEvent.IsPressed()) EmitSignal(nameof(PieceClicked), this);
    }

    private void OnMouseEntered()
    {
        EmitSignal(nameof(PieceMouseEntered), this);
    }

    private void OnMouseExited()
    {
        EmitSignal(nameof(PieceMouseExited), this);
    }

    private void UpdateSprite()
    {
        if (TextureMap.TryGetValue((PieceType, PieceColor), out var texturePath))
        {
            var texture = GD.Load<Texture2D>(texturePath);
            if (texture != null)
                _pieceSprite.Texture = texture;
            else
                GD.PrintErr($"Failed to load texture at {texturePath}");
        }
        else
        {
            GD.PrintErr($"No texture path found for {PieceType} and {PieceColor}");
        }
    }
}