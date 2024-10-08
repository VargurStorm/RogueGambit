using System;

namespace RogueGambit.Handlers.Interface;

public interface IInputHandler
{
    void ConnectSignals();

    void Initialize();

    static void OnPieceClicked(Piece piece)
    {
        throw new NotImplementedException();
    }

    static void OnPieceMouseEntered(Piece piece)
    {
        throw new NotImplementedException();
    }

    static void OnPieceMouseExited(Piece piece)
    {
        throw new NotImplementedException();
    }

    static void OnBoardSquareClicked(BoardSquare square)
    {
        throw new NotImplementedException();
    }

    static void OnBoardSquareMouseEntered(BoardSquare square)
    {
        throw new NotImplementedException();
    }

    static void OnBoardSquareMouseExited(BoardSquare square)
    {
        throw new NotImplementedException();
    }
}