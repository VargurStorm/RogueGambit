namespace RogueGambit.Managers.Interface;

public interface IGameStateManager
{
    GameState GameState { get; set; }
    void InitializeGameState();
    void LoadScenes();
    void PlaceBoard(int boardStart, Vector2 boardShape, List<List<int>> boardMask = null);
    void PlacePieces();
    void ConnectSignals();
    void AssignColorToOwner(PieceColor color, PieceOwner owner);
    void UpdateGameState();
    void SetTurn(PieceOwner owner);
}