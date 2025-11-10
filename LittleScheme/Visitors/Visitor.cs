using LittleScheme.Composite.LiteralNodes;
using LittleScheme.Composite.OperatorNodes;

namespace LittleScheme.Visitors;

public abstract class Visitor
{
    public abstract int GetTotal();
    
    public void Visit(NumberNode node){}
    
    public virtual void Visit(AbsNode node)
        => Visit((OperatorNode)node);

    public void Visit(OperatorNode node)
    {
        foreach (var child in node.Nodes)
            child.Accept(this);
    }
}