namespace RogueGambit.Managers;

public partial class GameStateManager : Node
{
    private BoardManager _boardManager;
    private GameStateManager _gameStateManager;
    private InputManager _inputManager;
    private MoveManager _moveManager;
    private PieceManager _pieceManager;
    private TurnManager _turnManager;

    public GameState GameState { get; set; }

    public override void _Ready()
    {
        GD.Print("...GameStateManager ready.");
        InitializeGameState();
        InitializeNodes();
        LoadScenes();
        PlaceBoard(BoardConstants.BoardStartX, new Vector2(8, 8));
        PlacePieces();
        ConnectSignals();

        GameState.ReadGameStateFromNodes(_boardManager, _pieceManager);
        AssignColorToOwner(PieceColor.White, PieceOwner.Player);
        AssignColorToOwner(PieceColor.Black, PieceOwner.Ai);
        UpdateGameState();
        SetTurn(PieceOwner.Player);
        GD.Print("Game state setup complete!");
    }

    private void InitializeNodes()
    {
        _gameStateManager = this;
        _pieceManager = GetNode<PieceManager>("/root/MainScene/PieceManager");
        _boardManager = GetNode<BoardManager>("/root/MainScene/BoardManager");
        _inputManager = GetNode<InputManager>("/root/MainScene/InputManager");
        _turnManager = GetNode<TurnManager>("/root/MainScene/TurnManager");
        _moveManager = GetNode<MoveManager>("/root/MainScene/MoveManager");
        GD.Print("...Game state nodes initialized!");
    }

    private void InitializeGameState()
    {
        GameState = new GameState();
        GD.Print("...Game state initialized!");
    }

    private void LoadScenes()
    {
        _boardManager.LoadScenes();
        _pieceManager.LoadScenes();
        GD.Print("...All scenes loaded!");
    }

    private void PlaceBoard(int boardStart, Vector2 boardShape, List<Vector2> boardMasks = null)
    {
        _boardManager.PlaceBoardSquares(boardStart, boardShape, boardMasks);
        GameState.BoardShape = boardShape;
        GameState.BoardMasks = boardMasks;
        GD.Print("...All board squares placed!");
    }

    private void PlacePieces()
    {
        _pieceManager.PlacePieceNodes();
        GD.Print("...All pieces placed!");
    }

    private void ConnectSignals()
    {
        _inputManager.ConnectSignals();
        GD.Print("...All signals connected!");
    }

    public void UpdateGameState()
    {
        GameState.FindOccupiedSquares();
        GameState.UpdateBoardNodes();
        GameState.UpdatePieceNodes();
    }

    public void AssignColorToOwner(PieceColor color, PieceOwner owner)
    {
        foreach (var piece in GameState.Pieces.Where(piece => piece.Color == color)) piece.Owner = owner;
    }

    public void SetTurn(PieceOwner owner)
    {
        GameState.CurrentTurn = owner;
        _turnManager.SetTurn(owner);
    }

    public void AdvanceTurn()
    {
        _turnManager.AdvanceTurn();
    }

    public void SelectPiece(PieceModel piece)
    {
        _moveManager.SelectPiece(piece);
    }

    public void DeselectPiece()
    {
        _moveManager.DeselectPiece();
    }

    public void ToggleSelectedPiece(PieceModel piece)
    {
        _moveManager.ToggleSelectedPiece(piece);
    }

    public void MovePiece(PieceModel piece, Vector2 targetPosition)
    {
        _moveManager.MovePiece(piece, targetPosition);
        UpdateGameState();
        AdvanceTurn();
    }
}