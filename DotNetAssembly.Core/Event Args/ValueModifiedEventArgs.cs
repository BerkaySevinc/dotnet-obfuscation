

namespace Assembly;


public class ValueModifiedEventArgs : EventArgs
{
    public ValueObjectType ObjectType { get; }

    public string InitialValue { get; }

    public ValueModifiedEventArgs(ValueObjectType objectType, string initialValue)
    {
        ObjectType = objectType;

        InitialValue = initialValue;
    }
}