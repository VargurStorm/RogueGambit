using Godot;
using RogueGambit.Models;
using RogueGambit.Static.Constants;
using Piece = RogueGambit.Models.Piece;

namespace RogueGambit.Managers;

public partial class InputManager : Node2D
{
    private Vector2 FromPosition { get; set; }
    private Vector2 ToPosition { get; set; }
    private bool IsPieceSelected { get; set; }
    private PieceManager PieceManager => GetNode<PieceManager>(PathConstants.PieceManager);
    private BoardManager BoardManager => GetNode<BoardManager>(PathConstants.BoardManager);
    public Vector2 MousePosition { get; private set; }

    public override void _Ready()
    {
        GD.Print("...InputManager ready.");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is not InputEventMouseMotion mouseMotion) return;
        MousePosition = mouseMotion.Position;
    }

    // Signals
    public void ConnectSignals()
    {
        var allPieces = PieceManager.GetPiecesOnBoard();
        foreach (var piece in allPieces)
        {
            piece.Connect("PieceClicked", new Callable(this, nameof(OnPieceClicked)));
            piece.Connect("PieceMouseEntered", new Callable(this, nameof(OnPieceMouseEntered)));
            piece.Connect("PieceMouseExited", new Callable(this, nameof(OnPieceMouseExited)));
        }

        var allSquares = BoardManager.GetBoardSquares();
        foreach (var square in allSquares)
        {
            square.Connect("SquareClicked", new Callable(this, nameof(OnBoardSquareClicked)));
            square.Connect("SquareMouseEntered", new Callable(this, nameof(OnBoardSquareMouseEntered)));
            square.Connect("SquareMouseExited", new Callable(this, nameof(OnBoardSquareMouseExited)));
        }
    }

    // Pieces
    private void OnPieceClicked(Piece piece)
    {
        FromPosition = piece.GridPosition;
        IsPieceSelected = true;
    }

    private static void OnPieceMouseEntered(Piece piece)
    {
        piece.OverlaySprite.Visible = true;
    }

    private static void OnPieceMouseExited(Piece piece)
    {
        piece.OverlaySprite.Visible = false;
    }

    // BoardSquares
    private void OnBoardSquareClicked(BoardSquare square)
    {
        if (FromPosition == Vector2.Zero) return;
        if (!IsPieceSelected) return;
        GD.Print("Moving piece from: " + FromPosition + " to: " + square.GridPosition);
        ToPosition = square.GridPosition;
        GameStateManager.MovePiece(FromPosition, ToPosition);
        ToPosition = FromPosition = Vector2.Zero;
        IsPieceSelected = false;
    }

    private static void OnBoardSquareMouseEntered(BoardSquare square)
    {
        if (!square.IsOccupied) square.OverlaySprite.Visible = true;
    }

    private static void OnBoardSquareMouseExited(BoardSquare square)
    {
        square.OverlaySprite.Visible = false;
    }
}