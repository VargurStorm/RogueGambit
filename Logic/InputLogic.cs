namespace RogueGambit.Logic;

public static class InputLogic
{
    public static bool IsMyTurn(GameState gameState, PieceOwner player)
    {
        return gameState.CurrentTurn == player;
    }

    public static bool IsMyPiece(PieceModel piece, PieceOwner player)
    {
        return piece.Owner == player;
    }
}