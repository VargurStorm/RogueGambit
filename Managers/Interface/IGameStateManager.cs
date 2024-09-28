namespace RogueGambit.Managers.Interface;

public interface IGameStateManager
{
    GameState GameState { get; set; }
    PlayerStatus PlayerStatus { get; set; }
    void SelectPiece(PieceModel piece);
    void InitializeGameState();
    void LoadScenes();
    void PlaceBoard(int boardStart, Vector2 boardShape, List<List<int>> boardMask = null);
    void PlacePieces();
    void AssignColorToOwner(PieceColor color, PieceOwner owner);
    void UpdateGameState();
    void SetTurn(PieceOwner owner);
    void ToggleSelectedPiece(PieceModel piece);
    void MovePiece(PieceModel piece, Vector2 targetPosition);
    void CapturePiece(PieceModel attacker, PieceModel targetPiece);
}