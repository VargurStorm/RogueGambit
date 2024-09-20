namespace RogueGambit.Managers.Interface;

public interface IMoveManager
{
    void SelectPiece(PieceModel piece);
    void DeselectPiece();
    void ToggleSelectedPiece(PieceModel piece);
    void MovePiece(PieceModel piece, Vector2 targetPosition);
}