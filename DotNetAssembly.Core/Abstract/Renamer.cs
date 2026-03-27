using dnlib.DotNet;


namespace Assembly;


public abstract class Renamer
{
    public AssemblyDef Assembly { get; }
    public NameGenerator NameGenerator { get; set; }


    public Renamer(AssemblyDef assembly, NameGenerator nameGenerator) =>
        (Assembly, NameGenerator) = (assembly, nameGenerator);


    public event EventHandler<NameChangedEventArgs>? NameChanged;

    protected virtual void OnNameChanged(NameChangedEventArgs e) =>
        NameChanged?.Invoke(this, e);
}
