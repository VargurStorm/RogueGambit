namespace RogueGambit.Logic;

public static class InputLogic
{
    public static bool IsMyTurn(GameState gameState, PieceOwner player)
    {
        return true;
        return gameState.CurrentTurn == player;
    }

    public static bool IsMyPiece(PieceModel piece, PieceOwner player)
    {
        return true;
        return piece.Owner == player;
    }
}