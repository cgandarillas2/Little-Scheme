using LittleScheme.Visitors;

namespace LittleScheme.Composite.OperatorNodes;

public class AdditionNode(INode leftNode, INode rightNode) : OperatorNode([leftNode, rightNode])
{
    public override int Evaluate()
        => Nodes[0].Evaluate() + Nodes[1].Evaluate();
    
    public override void Accept(Visitor visitor)
        => visitor.Visit(this);
}