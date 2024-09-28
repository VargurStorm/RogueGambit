namespace RogueGambit.Logic;

public class AttackLogic
{
    public static bool IsPieceAttackable(PieceModel attacker, PieceModel target)
    {
        if (FriendlyFire) return true;
        return attacker.Owner != target.Owner;
    }
}