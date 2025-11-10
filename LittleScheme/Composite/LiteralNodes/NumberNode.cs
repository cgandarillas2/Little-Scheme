using LittleScheme.Visitors;

namespace LittleScheme.Composite.LiteralNodes;

public class NumberNode(int value) : INode
{
    public void Accept(Visitor visitor)
        => visitor.Visit(this);

    public int Evaluate()
        => value;
}