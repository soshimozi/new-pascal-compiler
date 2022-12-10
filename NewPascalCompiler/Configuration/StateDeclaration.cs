namespace NewPascalCompiler.Configuration;

public class StateDeclarationYML
{
    public string name { get; set; }
    public string handlerPath { get; set; }
}

public class StateDeclaration
{
    public string Name { get; set; }
    public List<string> VariableReferences { get; set; }

    public StateHandlerDeclaration Handler { get; set; }
}