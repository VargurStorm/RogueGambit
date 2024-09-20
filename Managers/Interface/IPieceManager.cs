using System;

namespace RogueGambit.Managers.Interface;

public interface IPieceManager
{
    Node2D CreateNoteForModel(INodeModel model);
    void LoadScenes();

    static Dictionary<Vector2, PieceModel> CreatePieceModelsDefault()
    {
        throw new NotImplementedException();
    }

    List<Piece> GetPieceNodes();
}