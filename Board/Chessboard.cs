using System.Collections.Generic;
using Godot;
using RogueGambit.Static;

public partial class Chessboard : Node2D
{
	private static readonly int BoardSize = BoardConstants.BoardSize;
	private static readonly int SquareSize = BoardConstants.SquareSize;
	private PackedScene _boardSquareScene;

	public override void _Ready()
	{
		GD.Print("BoardManager ready.");
		LoadScenes();
		PlaceBoardSquares();
		GD.Print("Board squares placed.");
	}

	private void LoadScenes()
	{
		_boardSquareScene = GD.Load<PackedScene>(PathConstants.BoardScenePath);
	}

	private void PlaceBoardSquares()
	{
		if (_boardSquareScene is null)
		{
			GD.PrintErr("Board square scene could not be loaded.");
			return;
		}

		for (var x = 0; x < BoardSize; x++)
		for (var y = 0; y < BoardSize; y++)
		{
			var boardPosition = new Vector2(x, y);
			var color = (x + y) % 2 == 0 ? BoardColor.Light : BoardColor.Dark;
			PlaceBoardSquare(boardPosition, color);
		}
	}

	private void PlaceBoardSquare(Vector2 boardPosition, BoardColor color)
	{
		if (_boardSquareScene is null)
		{
			GD.PrintErr("Board square scene could not be loaded.");
			return;
		}

		var boardSquare = _boardSquareScene.Instantiate() as BoardSquare;
		if (boardSquare is null)
		{
			GD.PrintErr("Board square could not be instantiated.");
			return;
		}

		boardSquare.Position = boardPosition * SquareSize;
		boardSquare.SquareColor = color;
		boardSquare.GridPosition = boardPosition;
		AddChild(boardSquare);
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
