namespace RogueGambit.Handlers.Interface;

public interface IMoveHandler
{
    PieceModel SelectedPiece { get; }

    void SelectPiece(PieceModel piece);
    void DeselectPiece();
    void ToggleSelectedPiece(PieceModel piece);
    void MovePiece(PieceModel piece, Vector2 targetPosition);
}