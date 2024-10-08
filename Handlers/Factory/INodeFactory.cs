using RogueGambit.Models.State.Interfaces;

namespace RogueGambit.Handlers.Factory;

public interface INodeFactory
{
    Node2D CreateNoteForModel(INodeModel model);
}