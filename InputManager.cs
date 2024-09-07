using Godot;
using RogueGambit.Scenes.Pieces;

public partial class InputManager : Node2D
{
	private PieceManager _pieceManager => GetNode<PieceManager>("/root/MainScene/PieceManager");
	private Chessboard _chessboard => GetNode<Chessboard>("/root/MainScene/BoardManager");

	public override void _Ready()
	{
		GD.Print("InputManager ready.");

		var allPieces = _pieceManager.GetPiecesOnBoard();
		GD.Print("Pieces on board:", allPieces.Count);
		foreach (var piece in allPieces) piece.Connect("PieceClicked", new Callable(this, nameof(OnPieceClicked)));

		var allSquares = _chessboard.GetBoardSquares();
		GD.Print("Squares on board:", allSquares.Count);
		foreach (var square in allSquares)
			square.Connect("SquareClicked", new Callable(this, nameof(OnBoardSquareClicked)));
	}

	private void OnPieceClicked(Piece piece)
	{
		GD.Print($"{piece.PieceColor} {piece.PieceType} at {piece.GridPosition} was clicked.");
	}

	private void OnBoardSquareClicked(BoardSquare square)
	{
		GD.Print($"{square.SquareColor} Square at {square.GridPosition} was clicked.");
	}
}
