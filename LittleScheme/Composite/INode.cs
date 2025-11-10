using LittleScheme.Visitors;

namespace LittleScheme.Composite;

public interface INode
{
    int Evaluate();
    void Accept(Visitor visitor);
}