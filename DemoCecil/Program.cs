using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using System;
using System.Linq;

namespace DemoCecil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var assemblyDefinition= AssemblyDefinition.ReadAssembly("DemoCecil.exe");
            var module= assemblyDefinition.MainModule;

            var type = module.GetType("DemoCecil.Test");

            var setName = type.GetMethods().First(x=>x.Name == "set_Name");
            var il = setName.Body.GetILProcessor();
            string valTrim =String.Empty;
            il.Emit(OpCodes.Localloc,4);


            var ins = setName.Body.Instructions;
 
            assemblyDefinition.Write("DemoCecil2.exe");

            var o = new Test
            {
                Name = "yzn"
            };

        }
    }

    public class Test
    {
        public string Name { get; set; }
    }
}
