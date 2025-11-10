using LittleScheme.Components;
using LittleScheme.Composite;
using LittleScheme.Composite.OperatorNodes;
using LittleScheme.Exceptions;
using LittleScheme.Visitors;

namespace LittleScheme.Tests;

public class LittleSchemeTests
{
    [Theory]
    [InlineData("(+ 1 2)",3)]
    [InlineData("(- 1 1)",0)]
    [InlineData("(abs -1)",1)]
    [InlineData("(abs 1)",1)]
    [InlineData("(+ (abs -2) (- (+ 1 (- (abs 1) 2)) (abs (+ 1 -4))))",-1)]
    public void PartA_Composite_CodeShouldReturnTheRightOutput(string code, int expectedOutput)
    {
        AssertCompositeEvaluation(code, expectedOutput);
    }

    private void AssertCompositeEvaluation(string code, int expectedOutput)
    {
        Component component = ComponentBuilder.Build(code);
        INode tree = Controller.BuildAST(component);
        Assert.Equal(expectedOutput, tree.Evaluate());
    }

    [Theory]
    [InlineData("(abs (- 1 2))", 0)]
    [InlineData("(abs (- (abs (abs 1)) (abs (abs -2))))", 2)]
    [InlineData("(abs (abs -1))", 1)]
    public void PartA_Visitor_RedundantAbsShouldBeDetected(string code, int expectedOutput)
    {
        Visitor visitor = new RedundantAbsVisitor();

        AssertVisitorEvaluation(code, expectedOutput, visitor); 
    }

    private void AssertVisitorEvaluation(string code, int expectedOutput, Visitor visitor)
    {
        Component component = ComponentBuilder.Build(code);
        INode tree = Controller.BuildAST(component);
        
        int result = Controller.GetVisitorTotal(tree, visitor);
        
        Assert.Equal(expectedOutput, result);
    }

    [Theory]
    [InlineData("(if0 0 1 2)",1)]
    [InlineData("(if0 2 1 2)",2)]
    [InlineData("(if0 (+ -1 1) (abs -1) 4)",1)]
    [InlineData("(if0 0 (+ (abs (- 1 2)) (+ 3 5)) 2)",9)]
    public void PartB_Composite_If0ShouldWorkCorrectly(string code, int expectedOutput)
    {
        AssertCompositeEvaluation(code, expectedOutput);
    }

    [Theory]
    [InlineData("(fibonacci 1 2 3)",5)]
    [InlineData("(fibonacci 1 2 0)",1)]
    [InlineData("(fibonacci 1 2 1)",2)]
    [InlineData("(fibonacci -1 -1 2)",-2)]
    [InlineData("(fibonacci 1 2 20)",17711)]
    [InlineData("(fibonacci (- 15 0) (- 1 1) (+ 1 (abs (- 1 4))))",30)]
    public void PartB_Composite_ValidFibonacciShouldWorkCorrectly(string code, int expectedOutput)
    {
        AssertCompositeEvaluation(code, expectedOutput);
    }
    
    [Theory]
    [InlineData("(fibonacci 1 2 -10)")]
    [InlineData("(fibonacci (- 15 0) (- 1 1) (- 1 (abs (- 1 4))))")]
    public void PartB_Composite_InvalidFibonacciShouldThrowAnException(string code)
    {
        Component component = ComponentBuilder.Build(code);
        INode tree = Controller.BuildAST(component);
        Assert.Throws<LittleSchemeException>(() => tree.Evaluate());
    }

    [Theory]
    [InlineData("(if0 0 1 2)", 1)]
    [InlineData("(if0 (abs 0) 1 2)", 0)]
    [InlineData("(if0 0 (if0 1 1 2) (if0 0 1 1))", 3)]
    [InlineData("(if0 (if0 1 1 2) (if0 1 1 2) (if0 0 1 1))", 3)]
    [InlineData("(+ 1 (- 1 (if0 1 1 2)))", 1)]
    [InlineData("(fibonacci 0 0 3)",0)]
    public void PartB_Visitor_RedundantIf0ShouldBeDetected(string code, int expectedOutput)
    {
        Visitor visitor = Controller.GetRedundantIf0Visitor();
        
        AssertVisitorEvaluation(code, expectedOutput, visitor); 
    }

    [Theory]
    [InlineData("(fibonacci 0 0 3)",1)]
    [InlineData("(fibonacci -4 3 3)",0)]
    [InlineData("(fibonacci -4 3 -3)",1)]
    [InlineData("(fibonacci (+ 0 0) (abs 0) 2)",0)]
    [InlineData("(fibonacci 0 0 0)",1)]
    [InlineData("(fibonacci 1 1 0)",1)]
    [InlineData("(fibonacci 0 1 (- 1 1))",0)]
    [InlineData("(fibonacci 1 0 (- 1 1))",0)]
    [InlineData("(fibonacci 1 1 (fibonacci (fibonacci 0 0 0) (fibonacci 1 1 5) 0))",2)]
    [InlineData("(fibonacci 1 1 (fibonacci (fibonacci 0 0 0) (fibonacci 1 1 5) (+ 1 (fibonacci 1 1 (+ 1 (abs 5))))))",1)]
    [InlineData("(if0 0 1 2)", 0)]
    public void PartB_Visitor_RedundantFibonacciShouldBeDetected(string code, int expectedOutput)
    {
        Visitor visitor = Controller.GetRedundantFibonacciVisitor();

        AssertVisitorEvaluation(code, expectedOutput, visitor); 
    }
    
    [Theory]
    [InlineData("(+ 1 2)", 3)]
    [InlineData("(- 1 1)", 0)]
    [InlineData("(abs -42)", 42)]
    [InlineData("(abs 42)", 42)]
    [InlineData("(+ (abs -2) (- (+ 1 (- (abs 1) 2)) (abs (+ 1 -4))))", -1)]
    [InlineData("(abs (- 1 90))", 89)]
    [InlineData("(abs (- (abs (abs 1)) (abs (abs -2))))", 1)]
    [InlineData("(abs (abs -1))", 1)]
    [InlineData("(if0 0 1 2)", 1)]
    [InlineData("(if0 2 1 3)", 3)]
    [InlineData("(if0 (+ -1 1) (abs -5) 4)", 5)]
    [InlineData("(if0 0 (+ (abs (- 1 2)) (+ 3 5)) 2)", 9)]
    [InlineData("(fibonacci 0 0 1)", 0)]
    [InlineData("(fibonacci (+ 0 0) (abs 0) 1)", 0)]
    [InlineData("(fibonacci 4 1 (- 1 1))", 4)]
    [InlineData("(fibonacci 1 1 (fibonacci (fibonacci 0 0 0) (fibonacci 1 1 5) (+ 1 (fibonacci 1 1 (+ 1 (abs 2))))))", 75025)]
    public void PartC_ClonedTreeShouldGetTheCorrectResult(string code, int expectedOutput)
    {
        Component component = ComponentBuilder.Build(code);
        INode tree = Controller.BuildAST(component);
        INode clonedTree = Controller.GetASTClone(tree);
        
        Assert.Equal(expectedOutput, clonedTree.Evaluate());
    }

    [Theory]
    [InlineData("(+ 1 2)")]
    [InlineData("(- 1 1)")]
    [InlineData("(abs -1)")]
    [InlineData("(abs 1)")]
    [InlineData("(+ (abs -2) (- (+ 1 (- (abs 1) 2)) (abs (+ 1 -4))))")]
    [InlineData("(abs (- 1 2))")]
    [InlineData("(abs (- (abs (abs 1)) (abs (abs -2))))")]
    [InlineData("(abs (abs -1))")]
    [InlineData("(if0 0 1 2)")]
    [InlineData("(if0 2 1 2)")]
    [InlineData("(if0 (+ -1 1) (abs -1) 4)")]
    [InlineData("(if0 0 (+ (abs (- 1 2)) (+ 3 5)) 2)")]
    [InlineData("(fibonacci 0 0 1)")]
    [InlineData("(fibonacci (+ 0 0) (abs 0) 1)")]
    [InlineData("(fibonacci 0 0 0)")]
    [InlineData("(fibonacci 1 1 0)")]
    [InlineData("(fibonacci 1 1 (- 1 1))")]
    [InlineData("(fibonacci 1 1 (fibonacci (fibonacci 0 0 0) (fibonacci 1 1 5) 0))")]
    [InlineData("(fibonacci 1 1 (fibonacci (fibonacci 0 0 0) (fibonacci 1 1 5) (+ 1 (fibonacci 1 1 (+ 1 (abs 5))))))")]
    public void PartC_ClonedTreeShouldBeADifferentInstance(string code)
    {
        Component component = ComponentBuilder.Build(code);
        INode tree = Controller.BuildAST(component);
        INode clonedTree = Controller.GetASTClone(tree);
        
        AssertClone(tree, clonedTree);
    }

    private void AssertClone(INode tree, INode clonedTree)
    {
        Assert.NotSame(tree, clonedTree);
        if (tree is OperatorNode operatorNode)
        {
            Assert.True(clonedTree is OperatorNode);
            var clonedOperatorNode = (OperatorNode) clonedTree;
            Assert.Equal(operatorNode.Nodes.Count(), clonedOperatorNode.Nodes.Count());
            for (int i = 0; i < operatorNode.Nodes.Count(); i++)
                AssertClone(operatorNode.Nodes[i], clonedOperatorNode.Nodes[i]);
        }
    }
}