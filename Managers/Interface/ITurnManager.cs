namespace RogueGambit.Managers.Interface;

public interface ITurnManager
{
    void UpdateTurn();
    void AdvanceTurn();
    void SetTurn(PieceOwner owner);
    void UpdateTurnSprite();
    void BuildTextureMap();
}