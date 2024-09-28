namespace RogueGambit.Logic.Interfaces;

public interface IMoveLogic
{
    HashSet<Vector2> GetValidMoves(PieceModel piece);
    void Initialize();
}