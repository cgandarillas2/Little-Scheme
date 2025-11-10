using LittleScheme.Composite.OperatorNodes;

namespace LittleScheme.Visitors;

public class RedundantAbsVisitor : Visitor
{
    private int _counter = 0;

    public override int GetTotal()
        => _counter;

    public override void Visit(AbsNode node) 
    {
        if (node.Nodes[0] is AbsNode)
            _counter++;
        base.Visit(node);
    }
}