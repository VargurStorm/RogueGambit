using Godot;
using RogueGambit.Static;

public partial class BoardSquare : Node2D
{
	[Signal]
	public delegate void SquareClickedEventHandler(BoardSquare square);

	private Vector2 _gridPos;
	private Area2D _squareArea2D;
	private CollisionShape2D _squareCollision2D;

	[Export] public BoardColor SquareColor { get; set; }
	[Export] public Vector2 SquareSize { get; set; } = new(BoardConstants.SquareSize, BoardConstants.SquareSize);

	[Export]
	public Vector2 GridPosition
	{
		get => _gridPos;
		set
		{
			_gridPos = value;
			Position = value * SquareSize;
		}
	}

	public override void _Ready()
	{
		CreateSquare();

		_squareArea2D = GetNode<Area2D>("boardSquareArea2D");
		_squareCollision2D = GetNode<CollisionShape2D>("boardSquareArea2D/boardSquareCollision2D");
		_squareArea2D.Connect("input_event", new Callable(this, nameof(OnAreaInputEvent)));
	}

	private void OnAreaInputEvent(Node viewport, InputEvent @event, int shapeIdx)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.ButtonIndex == MouseButton.Left &&
			mouseEvent.IsPressed())
			EmitSignal(nameof(SquareClicked), this);
	}

	private void CreateSquare()
	{
		var square = new ColorRect
		{
			Color = SquareColor == BoardColor.Light
				? new Color(BoardConstants.LightSquareColor)
				: new Color(BoardConstants.DarkSquareColor),
			Size = SquareSize,
			Position = new Vector2(0, 0),
			MouseFilter = Control.MouseFilterEnum.Ignore
		};

		AddChild(square);
	}

	public void DeleteColorRect()
	{
		foreach (var child in GetChildren())
			if (child is ColorRect)
				RemoveChild(child);
	}

	public void UpdateColor(Color color)
	{
		foreach (var child in GetChildren())
			if (child is ColorRect rect)
				rect.Color = color;
	}
}
