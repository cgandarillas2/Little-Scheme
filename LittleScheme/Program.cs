// Agradecemos a Juan Pablo Sandoval por su ayuda en la elaboración de esta pregunta

using LittleScheme;
using LittleScheme.Components;
using LittleScheme.Composite;


Component component = ComponentBuilder.Build("(fibonacci 1 1 (fibonacci (fibonacci 0 0 0) (fibonacci 1 1 5) 0))");
INode tree = Controller.BuildAST(component);
var visitor = Controller.GetRedundantFibonacciVisitor();

Console.WriteLine(Controller.GetVisitorTotal(tree, visitor));