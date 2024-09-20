namespace RogueGambit.Models.State;

public interface INodeModel
{
    void UpdateNode(bool create = false);
    void ReadNode();
    void DestroyNode();
}