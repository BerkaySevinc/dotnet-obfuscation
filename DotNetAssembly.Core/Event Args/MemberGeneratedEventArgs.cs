using dnlib.DotNet;


namespace Assembly;


public class MemberGeneratedEventArgs : EventArgs
{
    public MemberObjectType ObjectType { get; }
    public List<IMemberDef> Objects { get; }

    public MemberObjectType ParentObjectType { get; }
    public IDnlibDef ParentObject { get; }


    public MemberGeneratedEventArgs(MemberObjectType parentObjectType, IDnlibDef parentObject, MemberObjectType objectType, List<IMemberDef> objects)
    {
        ParentObjectType = parentObjectType;
        ParentObject = parentObject;

        ObjectType = objectType;
        Objects = objects;
    }
}