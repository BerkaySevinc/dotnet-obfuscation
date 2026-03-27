using dnlib.DotNet;


namespace Assembly;


public abstract class NameGenerator
{
    public NameGenerator() { }

    public abstract string GenerateName(IDnlibDef? target);
    public virtual string GenerateName() => GenerateName(null);

    public abstract void Reset();
}
