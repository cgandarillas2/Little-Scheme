namespace LittleScheme.Components;

public static class ComponentBuilder {
    
    public static Component Build(string code) 
    {
        string[] tokens = SplitCodeIntoTokens(code);
        Component component = new Component(tokens[0]);
        for (int i = 1; i < tokens.Length; i++)
        {
            try {
                var _ = int.Parse(tokens[i]);
                component.AddChild(new Component(tokens[i]));
            } catch (Exception) {
                component.AddChild(Build(tokens[i]));
            }
        }
        return component;
    }

    private static string[] SplitCodeIntoTokens(string code)
    {
        int openCounter = 0;
        string temp = "";
        List<string> result = new List<string>();
        foreach (char key in code.Substring(1,code.Length-2))
        {
            if (key == ' ' && openCounter == 0)
            {
                result.Add(temp.Trim());
                temp = "";
            }
            else if (key == '(')
                openCounter++;
            else if (key == ')')
                openCounter--;

            temp += key;
        }
        result.Add(temp.Trim());

        return [.. result];
    }
}