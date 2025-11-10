using LittleScheme.Visitors;

namespace LittleScheme.Composite.OperatorNodes;

public abstract class OperatorNode(INode[] nodes) : INode
{
    public readonly INode[] Nodes = nodes;

    public abstract int Evaluate();

    public abstract void Accept(Visitor visitor);
}