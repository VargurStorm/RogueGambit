namespace RogueGambit.Utils;

using static MoveAttrib;

public static class BuildMoveSets
{
    private static readonly Dictionary<PieceType, MoveSet> DefaultMoveSets;

    static BuildMoveSets()
    {
        DefaultMoveSets = new Dictionary<PieceType, MoveSet>
        {
            { PieceType.Pawn, CreatePawnMoveSet() },
            { PieceType.Rook, CreateRookMoveSet() },
            { PieceType.Knight, CreateKnightMoveSet() },
            { PieceType.Bishop, CreateBishopMoveSet() },
            { PieceType.Queen, CreateQueenMoveSet() },
            { PieceType.King, CreateKingMoveSet() }
        };
    }

    public static Dictionary<PieceType, MoveSet> GetDefault()
    {
        return DefaultMoveSets;
    }

    private static MoveSet CreatePawnMoveSet()
    {
        var pawnMoveSet = new MoveSet();
        pawnMoveSet.AddMove(new Vector2(0, 1), 1, default, Slide, MoveOnly);
        pawnMoveSet.AddMove(new Vector2(0, 1), 2, default, Slide, MoveOnly, FirstMove);
        pawnMoveSet.AddMove(new Vector2(1, 1), 1, default, Slide, AttackOnly);
        pawnMoveSet.AddMove(new Vector2(-1, 1), 1, default, Slide, AttackOnly);
        return pawnMoveSet;
    }

    private static MoveSet CreateRookMoveSet()
    {
        var rookMoveSet = new MoveSet();
        rookMoveSet.AddMove(new Vector2(0, 1), 8, default, Slide, AttackAndMove);
        rookMoveSet.AddMove(new Vector2(0, -1), 8, default, Slide, AttackAndMove);
        rookMoveSet.AddMove(new Vector2(1, 0), 8, default, Slide, AttackAndMove);
        rookMoveSet.AddMove(new Vector2(-1, 0), 8, default, Slide, AttackAndMove);
        rookMoveSet.AddMove(new Vector2(1, 0), 2, (1, 1), Slide, ChainedMove);
        rookMoveSet.AddMove(new Vector2(0, -1), 4, (1, 2), Slide);
        return rookMoveSet;
    }

    private static MoveSet CreateKnightMoveSet()
    {
        var knightMoveSet = new MoveSet();
        knightMoveSet.AddMove(new Vector2(1, 2), 1, default, Jump, AttackAndMove);
        knightMoveSet.AddMove(new Vector2(2, 1), 1, default, Jump, AttackAndMove);
        knightMoveSet.AddMove(new Vector2(2, -1), 1, default, Jump, AttackAndMove);
        knightMoveSet.AddMove(new Vector2(1, -2), 1, default, Jump, AttackAndMove);
        knightMoveSet.AddMove(new Vector2(-1, -2), 1, default, Jump, AttackAndMove);
        knightMoveSet.AddMove(new Vector2(-2, -1), 1, default, Jump, AttackAndMove);
        knightMoveSet.AddMove(new Vector2(-2, 1), 1, default, Jump, AttackAndMove);
        knightMoveSet.AddMove(new Vector2(-1, 2), 1, default, Jump, AttackAndMove);
        return knightMoveSet;
    }

    private static MoveSet CreateBishopMoveSet()
    {
        var bishopMoveSet = new MoveSet();
        bishopMoveSet.AddMove(new Vector2(1, 1), 8, default, Slide, AttackAndMove);
        bishopMoveSet.AddMove(new Vector2(1, -1), 8, default, Slide, AttackAndMove);
        bishopMoveSet.AddMove(new Vector2(-1, 1), 8, default, Slide, AttackAndMove);
        bishopMoveSet.AddMove(new Vector2(-1, -1), 8, default, Slide, AttackAndMove);
        return bishopMoveSet;
    }

    private static MoveSet CreateQueenMoveSet()
    {
        var queenMoveSet = new MoveSet();
        queenMoveSet.AddMove(new Vector2(0, 1), 8, default, Slide, AttackAndMove);
        queenMoveSet.AddMove(new Vector2(0, -1), 8, default, Slide, AttackAndMove);
        queenMoveSet.AddMove(new Vector2(1, 0), 8, default, Slide, AttackAndMove);
        queenMoveSet.AddMove(new Vector2(-1, 0), 8, default, Slide, AttackAndMove);
        queenMoveSet.AddMove(new Vector2(1, 1), 8, default, Slide, AttackAndMove);
        queenMoveSet.AddMove(new Vector2(1, -1), 8, default, Slide, AttackAndMove);
        queenMoveSet.AddMove(new Vector2(-1, 1), 8, default, Slide, AttackAndMove);
        queenMoveSet.AddMove(new Vector2(-1, -1), 8, default, Slide, AttackAndMove);
        return queenMoveSet;
    }

    private static MoveSet CreateKingMoveSet()
    {
        var kingMoveSet = new MoveSet();
        kingMoveSet.AddMove(new Vector2(0, 1), 1, default, Slide, AttackAndMove);
        kingMoveSet.AddMove(new Vector2(0, -1), 1, default, Slide, AttackAndMove);
        kingMoveSet.AddMove(new Vector2(1, 0), 1, default, Slide, AttackAndMove);
        kingMoveSet.AddMove(new Vector2(-1, 0), 1, default, Slide, AttackAndMove);
        kingMoveSet.AddMove(new Vector2(1, 1), 1, default, Slide, AttackAndMove);
        kingMoveSet.AddMove(new Vector2(1, -1), 1, default, Slide, AttackAndMove);
        kingMoveSet.AddMove(new Vector2(-1, 1), 1, default, Slide, AttackAndMove);
        kingMoveSet.AddMove(new Vector2(-1, -1), 1, default, Slide, AttackAndMove);
        return kingMoveSet;
    }
}