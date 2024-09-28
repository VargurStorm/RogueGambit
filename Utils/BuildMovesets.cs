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
        pawnMoveSet.AddMove(new Vector2(0, 1), 1, Slide, MoveOnly);
        pawnMoveSet.AddMove(new Vector2(0, 1), 2, Slide, MoveOnly, FirstMove);
        pawnMoveSet.AddMove(new Vector2(1, 1), 1, Slide, AttackOnly);
        pawnMoveSet.AddMove(new Vector2(-1, 1), 1, Slide, AttackOnly);
        return pawnMoveSet;
    }

    private static MoveSet CreateRookMoveSet()
    {
        var rookMoveSet = new MoveSet();
        rookMoveSet.AddMove(new Vector2(0, 1), 8, Slide, AttackAndMove);
        rookMoveSet.AddMove(new Vector2(0, -1), 8, Slide, AttackAndMove);
        rookMoveSet.AddMove(new Vector2(1, 0), 8, Slide, AttackAndMove);
        rookMoveSet.AddMove(new Vector2(-1, 0), 8, Slide, AttackAndMove);
        return rookMoveSet;
    }

    private static MoveSet CreateKnightMoveSet()
    {
        var knightMoveSet = new MoveSet();
        knightMoveSet.AddMove(new Vector2(1, 2), 1, Jump, AttackAndMove);
        knightMoveSet.AddMove(new Vector2(2, 1), 1, Jump, AttackAndMove);
        knightMoveSet.AddMove(new Vector2(2, -1), 1, Jump, AttackAndMove);
        knightMoveSet.AddMove(new Vector2(1, -2), 1, Jump, AttackAndMove);
        knightMoveSet.AddMove(new Vector2(-1, -2), 1, Jump, AttackAndMove);
        knightMoveSet.AddMove(new Vector2(-2, -1), 1, Jump, AttackAndMove);
        knightMoveSet.AddMove(new Vector2(-2, 1), 1, Jump, AttackAndMove);
        knightMoveSet.AddMove(new Vector2(-1, 2), 1, Jump, AttackAndMove);
        return knightMoveSet;
    }

    private static MoveSet CreateBishopMoveSet()
    {
        var bishopMoveSet = new MoveSet();
        bishopMoveSet.AddMove(new Vector2(1, 1), 8, Slide, AttackAndMove);
        bishopMoveSet.AddMove(new Vector2(1, -1), 8, Slide, AttackAndMove);
        bishopMoveSet.AddMove(new Vector2(-1, 1), 8, Slide, AttackAndMove);
        bishopMoveSet.AddMove(new Vector2(-1, -1), 8, Slide, AttackAndMove);
        return bishopMoveSet;
    }

    private static MoveSet CreateQueenMoveSet()
    {
        var queenMoveSet = new MoveSet();
        queenMoveSet.AddMove(new Vector2(0, 1), 8, Slide, AttackAndMove);
        queenMoveSet.AddMove(new Vector2(0, -1), 8, Slide, AttackAndMove);
        queenMoveSet.AddMove(new Vector2(1, 0), 8, Slide, AttackAndMove);
        queenMoveSet.AddMove(new Vector2(-1, 0), 8, Slide, AttackAndMove);
        queenMoveSet.AddMove(new Vector2(1, 1), 8, Slide, AttackAndMove);
        queenMoveSet.AddMove(new Vector2(1, -1), 8, Slide, AttackAndMove);
        queenMoveSet.AddMove(new Vector2(-1, 1), 8, Slide, AttackAndMove);
        queenMoveSet.AddMove(new Vector2(-1, -1), 8, Slide, AttackAndMove);
        return queenMoveSet;
    }

    private static MoveSet CreateKingMoveSet()
    {
        var kingMoveSet = new MoveSet();
        kingMoveSet.AddMove(new Vector2(0, 1), 1, Slide, AttackAndMove);
        kingMoveSet.AddMove(new Vector2(0, -1), 1, Slide, AttackAndMove);
        kingMoveSet.AddMove(new Vector2(1, 0), 1, Slide, AttackAndMove);
        kingMoveSet.AddMove(new Vector2(-1, 0), 1, Slide, AttackAndMove);
        kingMoveSet.AddMove(new Vector2(1, 1), 1, Slide, AttackAndMove);
        kingMoveSet.AddMove(new Vector2(1, -1), 1, Slide, AttackAndMove);
        kingMoveSet.AddMove(new Vector2(-1, 1), 1, Slide, AttackAndMove);
        kingMoveSet.AddMove(new Vector2(-1, -1), 1, Slide, AttackAndMove);
        return kingMoveSet;
    }
}