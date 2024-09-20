namespace RogueGambit.Managers.Factory;

public interface INodeFactory
{
    Node2D CreateNoteForModel(INodeModel model);
}