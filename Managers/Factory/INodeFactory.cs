using RogueGambit.Models.State.Interfaces;

namespace RogueGambit.Managers.Factory;

public interface INodeFactory
{
    Node2D CreateNoteForModel(INodeModel model);
}