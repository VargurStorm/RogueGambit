using System;
using RogueGambit.Models.State.Interfaces;

namespace RogueGambit.Managers.Interface;

public interface IBoardManager
{
    Node2D CreateNoteForModel(INodeModel model);
    void LoadScenes();

    static Dictionary<Vector2, BoardSquareModel> BuildBoardSquareModels(int boardStart,
                                                                        Vector2 boardShape,
                                                                        List<List<int>> boardMask = null)
    {
        throw new NotImplementedException();
    }

    List<BoardSquare> GetBoardSquareNodes();
}