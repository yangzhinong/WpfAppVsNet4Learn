using System;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace ModbusDemo
{
    internal class Class2
    {
        public static void CodeDomTest()
        {
            CodeCompileUnit unit = new CodeCompileUnit();
            CodeNamespace ns = new CodeNamespace("Sample");
            ns.Imports.Add(new CodeNamespaceImport(nameof(System)));
            unit.Namespaces.Add(ns);

            CodeTypeDeclaration t1 = new CodeTypeDeclaration("Dog");
            ns.Types.Add(t1);

            CodeParameterDeclarationExpression p = new CodeParameterDeclarationExpression(typeof(string), "dogName");

            CodeConstructor ctor = new CodeConstructor();
            ctor.Parameters.Add(p);
            t1.Members.Add(ctor);


            CodeMemberProperty prty = new CodeMemberProperty()
            {
                Name = "Age",
                Type = new CodeTypeReference(typeof(int)),
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                HasGet = true,
                HasSet = true,
            };
            t1.Members.Add(prty);

            CodeDomProvider provider = CodeDomProvider.CreateProvider("cs");
            provider.GenerateCodeFromCompileUnit(unit, Console.Out, null);
            
        }
    }
}
