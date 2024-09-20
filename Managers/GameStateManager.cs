namespace RogueGambit.Managers;

public partial class GameStateManager : Node, IGameStateManager
{
    [Inject] private IBoardManager _boardManager;
    [Inject] private IInputManager _inputManager;
    [Inject] private IMoveManager _moveManager;
    [Inject] private IPieceManager _pieceManager;
    [Inject] private ITurnManager _turnManager;


    public GameState GameState { get; set; }

    public void InitializeGameState()
    {
        GameState = new GameState();
    }

    public void LoadScenes()
    {
        _boardManager.LoadScenes();
        _pieceManager.LoadScenes();
    }

    public void PlaceBoard(int boardStart, Vector2 boardShape, List<List<int>> boardMask = null)
    {
        GameState.BoardSquares = BoardManager.BuildBoardSquareModels(boardStart, boardShape, boardMask);
        GameState.BoardSquares.Values.ToList().ForEach(square => square.UpdateNode(true));
        GameState.BoardShape = boardShape;
        GameState.BoardMask = boardMask;
    }

    public void PlacePieces()
    {
        GameState.Pieces = PieceManager.CreatePieceModelsDefault();
        GameState.Pieces.Values.ToList().ForEach(piece => piece.UpdateNode(true));
    }

    public void ConnectSignals()
    {
        _inputManager.ConnectSignals();
    }

    public void UpdateGameState()
    {
        GameState.FindOccupiedSquares();
        GameState.UpdateBoardNodes();
        GameState.UpdatePieceNodes();
    }

    public void AssignColorToOwner(PieceColor color, PieceOwner owner)
    {
        foreach (var piece in GameState.Pieces.Values.Where(piece => piece.Color == color)) piece.Owner = owner;
    }

    public void SetTurn(PieceOwner owner)
    {
        GameState.CurrentTurn = owner;
        _turnManager.SetTurn(owner);
    }

    public override void _Ready()
    {
        GD.Print("...GameStateManager ready.");
        InjectDependencies(this);
        InitializeGameState();
        LoadScenes();

        PlaceBoard(BoardConstants.BoardStartX, new Vector2(8, 8));
        PlacePieces();
        ConnectSignals();

        AssignColorToOwner(PieceColor.White, PieceOwner.Player);
        AssignColorToOwner(PieceColor.Black, PieceOwner.Player);

        UpdateGameState();
        SetTurn(PieceOwner.Player);
    }


    public void AdvanceTurn()
    {
        _turnManager.AdvanceTurn();
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