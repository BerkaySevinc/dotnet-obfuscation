
namespace Assembly.Obfuscation;


// TODO: Improve EventArgs by adding a ResultName property and a parent property for certain EventArgs types.
// TODO: Add an ObfuscationType property (name, value, junk, etc.) to events so the logger can identify the obfuscation type.
// TODO: Handle special cases such as IsRuntimeSpecialName for NameGenerators, ValueModifiers, and MemberGenerators.
// TODO: Implement namespace obfuscation (hidden namespaces) and junk namespace generation.




public class Obfuscator : AssemblyModifier
{
    public Obfuscator(string inputAssemblyFile) : base(inputAssemblyFile) { }


    public event EventHandler<NameChangedEventArgs>? NameChanged;
    public event EventHandler<ValueModifiedEventArgs>? ValueModified;
    public event EventHandler<MemberGeneratedEventArgs>? MemberGenerated;


    protected virtual void OnNameChanged(NameChangedEventArgs e) =>
        NameChanged?.Invoke(this, e);
    protected virtual void OnValueModified(ValueModifiedEventArgs e) =>
        ValueModified?.Invoke(this, e);
    protected virtual void OnMemberGenerated(MemberGeneratedEventArgs e) =>
        MemberGenerated?.Invoke(this, e);


    public void Obfuscate(ObfuscatorOptions? options = null)
    {
        // Creates default options if given is null.
        options ??= new ObfuscatorOptions();

        // Generates junk.
        GenerateJunks(options);

        // Obfuscates values.
        ObfuscateValues(options);

        // Obfuscates names.
        ObfuscateNames(options);
    }

    private void GenerateJunks(ObfuscatorOptions options)
    {
        // Gets and resets the name generator.
        NameGenerator junkNameGenerator = options.JunkNameGenerator;
        junkNameGenerator.Reset();

        // Creates junk generator.
        var junkGenerator = new JunkGenerator(Assembly, junkNameGenerator);
        junkGenerator.MemberGenerated += MemberGenerated;

        // Generates junk fields.
        junkGenerator.GenerateField(options.JunkFieldCount);
    }

    private void ObfuscateNames(ObfuscatorOptions options)
    {
        // Gets and resets the name generator.
        NameGenerator obfuscatedNameGenerator = options.ObfuscatedNameGenerator;
        obfuscatedNameGenerator.Reset();

        // Creates renamer.
        var renamer = new NameObfuscator(Assembly, obfuscatedNameGenerator);
        renamer.NameChanged += NameChanged;

        // Obfuscates assembly name.
        if (options.ObfuscateAssemblyName)
            renamer.ObfuscateAssemblyName();

        // Obfuscates module names.
        if (options.ObfuscateModuleNames)
            renamer.ObfuscateModuleNames();

        // Obfuscates type names.
        if (options.ObfuscateTypeNames)
            renamer.ObfuscateTypeNames();

        // Obfuscates method names.
        if (options.ObfuscateMethodNames)
            renamer.ObfuscateMethodNames();

        // Obfuscates field names.
        if (options.ObfuscateFieldNames)
            renamer.ObfuscateFieldNames();

        // Obfuscates property names.
        if (options.ObfuscatePropertyNames)
            renamer.ObfuscatePropertyNames();

        // Obfuscates event names.
        if (options.ObfuscateEventNames)
            renamer.ObfuscateEventNames();

        // Obfuscates parameter names.
        if (options.ObfuscateParameterNames)
            renamer.ObfuscateParameterNames();

        // Resets name generator.
        obfuscatedNameGenerator.Reset();
    }

    private void ObfuscateValues(ObfuscatorOptions options)
    {
        // Creates value modifier.
        var valueModifier = new ValueObfuscator(Assembly);
        valueModifier.ValueModified += ValueModified;

        // Obfuscates string values.
        if (options.ObfuscateStringValues)
            valueModifier.EncodeStringValues();
    }
}
