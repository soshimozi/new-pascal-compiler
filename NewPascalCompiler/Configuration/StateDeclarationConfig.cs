using YamlDotNet.Serialization;

namespace NewPascalCompiler.Configuration;

public class StateVariable
{
    public string Name { get; set; }
    [YamlIgnore]
    public object Value { get; set; }
    public string TypeName { get; set; }
}

public class StateDeclarationConfig
{
    public List<StateVariable> Variables;
    public List<StateDeclaration> Declarations;
}