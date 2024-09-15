namespace RogueGambit.Managers;

public partial class BoardManager : Node2D
{
	private PackedScene _boardSquareScene;

	public override void _Ready()
	{
		GD.Print("...BoardManager ready.");
	}

	public void LoadScenes()
	{
		_boardSquareScene = GD.Load<PackedScene>(PathConstants.BoardSquareScenePath);
		GD.Print("...Loaded board square scene.");
	}

	public void PlaceBoardSquares(int boardStart, Vector2 boardShape, List<Vector2> boardMasks = null)
	{
		if (_boardSquareScene is null)
		{
			GD.PrintErr("Board square scene could not be loaded.");
			return;
		}

		for (var x = boardStart; x < boardShape.X; x++)
		for (var y = boardStart; y < boardShape.Y; y++)
		{
			var boardPosition = new Vector2(x, y);
			var color = (x + y) % 2 == 0 ? BoardConstants.LightSquareColor : BoardConstants.DarkSquareColor;

			if (boardMasks != null && boardMasks.Contains(boardPosition))
				continue;

			PlaceBoardSquare(boardPosition, color);
		}
	}

	private void PlaceBoardSquare(Vector2 boardPosition, Color color)
	{
		if (_boardSquareScene is null)
		{
			GD.PrintErr("Board square scene could not be loaded.");
			return;
		}

		if (_boardSquareScene.Instantiate() is not BoardSquare boardSquare)
		{
			GD.PrintErr("Board square could not be instantiated.");
			return;
		}

		boardSquare.SquareColor = color;
		boardSquare.GridPosition = boardPosition;
		AddChild(boardSquare);
	}

	private void DeleteBoardSquare(Vector2 squarePosition)
	{
		var boardSquare = GetNode<BoardSquare>($"Square_{squarePosition.ToString()}");
		if (boardSquare is null)
		{
			GD.PrintErr("Board square could not be found.");
			return;
		}

		boardSquare.QueueFree();
	}

	public List<BoardSquare> GetBoardSquares()
	{
		var boardSquares = new List<BoardSquare>();
		foreach (var child in GetChildren())
			if (child is BoardSquare square)
				boardSquares.Add(square);

		return boardSquares;
	}
}
