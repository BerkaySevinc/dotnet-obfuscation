using dnlib.DotNet;


namespace Assembly;


public class NameChangedEventArgs : EventArgs
{
    public MemberObjectType ObjectType { get; }
    public IDnlibDef? Object { get; }

    public string InitialName { get; }

    public NameChangedEventArgs(MemberObjectType objectType, IDnlibDef? @object, string initialName)
    {
        ObjectType = objectType;
        Object = @object;

        InitialName = initialName;
    }
}