using LittleScheme.Visitors;

namespace LittleScheme.Composite.OperatorNodes;

public class AbsNode(INode node) : OperatorNode([node])
{
    public override int Evaluate()
        => Math.Abs(Nodes[0].Evaluate());

    public override void Accept(Visitor visitor)
        => visitor.Visit(this);
}