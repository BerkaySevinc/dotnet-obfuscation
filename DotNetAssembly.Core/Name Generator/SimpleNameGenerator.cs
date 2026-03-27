using dnlib.DotNet;

namespace Assembly;

// TODO: SimpleNameGenerator is incomplete.
// FIXME: Does not work when a property, field, or method shares the same name.


public class SimpleNameGenerator : NameGenerator
{

    private Dictionary<string, int> generatedNameCounts = new();
    public override string GenerateName(IDnlibDef? target)
    {
        // Gets type name prefix.
        string? typeName = target is null ? null : GetTypeName(target);
        string prefix = CreatePrefix(target, typeName);

        // Gets count suffix.
        generatedNameCounts.TryGetValue(prefix, out int generatedCount);
        generatedCount++;

        string generatedName = prefix + generatedCount;

        generatedNameCounts[prefix] = generatedCount;

        return generatedName;
    }

    private static string CreatePrefix(IDnlibDef? target, string? typeName)
    {
        string prefix = typeName switch
        {
            "Int32" => "int",
            "Int64" => "long",
            "Int16" => "short",
            "Boolean" => "bool",

            null => target switch
            {
                AssemblyDef => "assembly",
                ModuleDef => "module",
                FileDef => "file",
                TypeDef => "type",

                _ => "variable"
            },

            _ => typeName
        };

        string caseOrganizedPrefix = target switch
        {
            PropertyDef or MethodDef or TypeDef or ModuleDef or AssemblyDef or EventDef or FileDef =>
                char.ToUpperInvariant(prefix[0]) + prefix[1..],

            _ => char.ToLowerInvariant(prefix[0]) + prefix[1..],
        };

        return caseOrganizedPrefix;
    }

    private static string? GetTypeName(IDnlibDef target)
    {
        string? typeName = target switch
        {
            FieldDef field => field.FieldType.GetName(),
            PropertyDef property => property.Type.ToString(),
            MethodDef method => method.ReturnType.GetName(),

            _ => null
        };

        return typeName;
    }


    public override void Reset()
    {
        generatedNameCounts.Clear();
    }
}
