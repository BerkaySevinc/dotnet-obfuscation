using dnlib.DotNet;

namespace Assembly.Obfuscation;


// TODO: Generate junk for other member types (methods, properties, namespaces, etc.) with various value types (string, int, short, bool, etc.) and values.


public class JunkGenerator : MemberGenerator
{
    public JunkGenerator(AssemblyDef assembly, NameGenerator nameGenerator) : base(assembly, nameGenerator) { }   

    public void GenerateField(int junkCount)
    {
        if (junkCount is 0) return;

        foreach (var module in Assembly.Modules)
            foreach (var type in module.Types)
            {
                if (type.IsGlobalModuleType || type.IsRuntimeSpecialName || type.IsSpecialName || type.IsWindowsRuntime || type.IsInterface) continue;

                NameGenerator.Reset();

                var fields = new List<IMemberDef>();
                for (int i = 0; i < junkCount; i++)
                {
                    string name = NameGenerator.GenerateName();

                    var fieldSignature = new FieldSig(module.CorLibTypes.Int32);
                    var field = new FieldDefUser(name, fieldSignature);

                    type.Fields.Add(field);
                    fields.Add(field);
                }
                var args = new MemberGeneratedEventArgs(MemberObjectType.Type, type, MemberObjectType.Field, fields);
                OnMemberGenerated(args);
            }

        NameGenerator.Reset();
    }

}
