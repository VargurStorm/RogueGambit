using Godot;
using RogueGambit.Models;

namespace RogueGambit.Managers;

public partial class GameStateManager : Node
{
    private BoardManager _boardManager;
    private GameStateManager _gameStateManager;
    private InputManager _inputManager;
    private PieceManager _pieceManager;

    public static BoardState BoardState { get; set; }

    public override void _Ready()
    {
        GD.Print("...GameStateManager ready.");
        InitializeNodes();
        LoadScenes();
        PlaceBoard();
        PlacePieces();
        ConnectSignals();
        InitializeBoardState();
        GD.Print("Game state setup complete!");

        // temp for tests
        var myPieces = BoardState.GetPieceList();
        GD.Print("...Board state piece list:");
        GD.Print(myPieces);
        BoardState.FindOccupiedSquares();
        BoardState.UpdateBoardState();
    }

    public static void MovePiece(Vector2 from, Vector2 to)
    {
        var piece = BoardState.Pieces.Find(p => p.GridPosition == from);
        piece.GridPosition = to;
        BoardState.FindOccupiedSquares();
        BoardState.UpdateBoardState();
    }

    private void InitializeBoardState()
    {
        BoardState = new BoardState();
        BoardState.FindBoardState(_boardManager, _pieceManager);
        GD.Print("...Board state initialized!");
    }

    private void InitializeNodes()
    {
        _gameStateManager = this;
        _pieceManager = GetNode<PieceManager>("/root/MainScene/PieceManager");
        _boardManager = GetNode<BoardManager>("/root/MainScene/BoardManager");
        _inputManager = GetNode<InputManager>("/root/MainScene/InputManager");
        GD.Print("...Game state nodes initialized!");
    }

    private void LoadScenes()
    {
        _boardManager.LoadScenes();
        _pieceManager.LoadScenes();
        GD.Print("...All scenes loaded!");
    }

    private void PlaceBoard()
    {
        _boardManager.PlaceBoardSquares();
        GD.Print("...All board squares placed!");
    }

    private void PlacePieces()
    {
        _pieceManager.PlacePieces();
        GD.Print("...All pieces placed!");
    }

    private void ConnectSignals()
    {
        _inputManager.ConnectSignals();
        GD.Print("...All signals connected!");
    }
}