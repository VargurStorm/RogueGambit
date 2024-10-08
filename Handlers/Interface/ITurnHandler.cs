namespace RogueGambit.Handlers.Interface;

public interface ITurnHandler
{
    void UpdateTurn();
    void AdvanceTurn();
    void SetTurn(PieceOwner owner);
    void UpdateTurnSprite();
    void BuildTextureMap();
}