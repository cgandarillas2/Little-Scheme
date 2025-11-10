namespace LittleScheme.Components;

public class Component(string value)
{
    public readonly string Value = value;
    public List<Component> Children = new();

    public void AddChild(Component child)
        => Children.Add(child);
}