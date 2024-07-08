using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace DemoConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var lst = typeof(OpCodes).GetFields()
            //                        .Select(x => x.GetValue(null))
            //                        .OfType<OpCode>()
            //                        .ToList();

            //IL_Sum99();
            // BuildAdderType();
            //IL_Mod();
            //DataGroup.Do();
            //LongestIncreasingSubsequence.Do();
            //KMeansClustering.Do();
            //MinimizeAdjacentDifference.Do();
            YznGroup.Do();
            //YznGroup yznGroup= new YznGroup();
            //var json = File.ReadAllText("c:\\1.json");
           
            //var lst = JsonConvert.DeserializeObject<List<MyGroup.AnalysisSource>>(json);
            //Console.WriteLine(string.Join(",", lst.Select(x => x.No)));
            //Console.WriteLine("-------------------------");
           
            //yznGroup.ReOrderAnalysisSourceByNo(lst);
            //Console.WriteLine(string.Join(",", lst.Select(x => x.No)));
            Console.Read();


        }

        public static Type BuildAdderType()
        {
            const string ModuleName = "AdderExceptionMod";
            AppDomain myDomain = Thread.GetDomain();
            AssemblyName myAsmName = new AssemblyName();
            myAsmName.Name = ModuleName;
            AssemblyBuilder myAsmBldr = myDomain.DefineDynamicAssembly(myAsmName,
                                         AssemblyBuilderAccess.RunAndSave);

            ModuleBuilder myModBldr = myAsmBldr.DefineDynamicModule(ModuleName, ModuleName + ".dll");

            TypeBuilder myTypeBldr = myModBldr.DefineType("Adder");

            Type[] adderParams = new Type[] { typeof(int), typeof(int) };

            // This method will add two numbers which are 100 or less. If either of the
            // passed integer vales are greater than 100, it will return the value of -1.

            MethodBuilder adderBldr = myTypeBldr.DefineMethod("DoAdd",
                                    MethodAttributes.Public |
                                    MethodAttributes.Static,
                                    typeof(int),
                                    adderParams);
            ILGenerator adderIL = adderBldr.GetILGenerator();

            // In order to successfully branch, we need to create labels
            // representing the offset IL instruction block to branch to.
            // These labels, when the MarkLabel(Label) method is invoked,
            // will specify the IL instruction to branch to.

            Label failed = adderIL.DefineLabel();
            Label endOfMthd = adderIL.DefineLabel();

            // First, load argument 0 and the integer value of "100" onto the
            // stack. If arg0 > 100, branch to the label "failed", which is marked
            // as the address of the block that loads -1 onto the stack, bypassing
            // the addition.

            adderIL.Emit(OpCodes.Ldarg_0);
            adderIL.Emit(OpCodes.Ldc_I4_S, 100);
            adderIL.Emit(OpCodes.Bgt_S, failed);

            // Now, check to see if argument 1 was greater than 100. If it was,
            // branch to "failed." Otherwise, fall through and perform the addition,
            // branching unconditionally to the instruction at the label "endOfMthd".

            adderIL.Emit(OpCodes.Ldarg_1);
            adderIL.Emit(OpCodes.Ldc_I4_S, 100);
            adderIL.Emit(OpCodes.Bgt_S, failed);

            adderIL.Emit(OpCodes.Ldarg_0);
            adderIL.Emit(OpCodes.Ldarg_1);
            adderIL.Emit(OpCodes.Add_Ovf_Un);
            adderIL.Emit(OpCodes.Br_S, endOfMthd);

            // If this label is branched to (the failure case), load -1 onto the stack
            // and fall through to the return opcode.
            adderIL.MarkLabel(failed);
            adderIL.Emit(OpCodes.Ldc_I4_M1);

            // The end of the method. If both values were less than 100, the
            // correct result will return. If one of the arguments was greater
            // than 100, the result will be -1. 

            adderIL.MarkLabel(endOfMthd);
            adderIL.Emit(OpCodes.Ret);
            myAsmBldr.Save(ModuleName + ".dll");
            return myTypeBldr.CreateType();
        }

        private static void IL_Mod()
        {
            const string ModuleName = "FooBar";
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(ModuleName), AssemblyBuilderAccess.RunAndSave);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(ModuleName, ModuleName + ".dll");

            var typeBuilder = moduleBuilder.DefineType("Foo", TypeAttributes.Public);
            //object result = DefSum(typeBuilder);
            //Console.WriteLine(result);
            var result = DefChangeSet(typeBuilder);
            Console.WriteLine(result);

            assemblyBuilder.Save(ModuleName + ".dll");
        }

        private static object DefSum(TypeBuilder typeBuilder)
        {
            var sum = typeBuilder.DefineMethod("Sum", MethodAttributes.Public, typeof(int), new[] { typeof(int), typeof(int) });

            var il = sum.GetILGenerator();
            Label failed = il.DefineLabel();
            Label endOfMthd = il.DefineLabel();

            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ldc_I4_S, 100);
            il.Emit(OpCodes.Cgt);
            il.Emit(OpCodes.Brfalse_S, failed);
            il.Emit(OpCodes.Ldarg_2);
            il.Emit(OpCodes.Ldc_I4_5);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Starg_S, 2);
            il.MarkLabel(failed);

            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ldarg_2);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Ret);

            var fooType = typeBuilder.CreateType();
            var instane = Activator.CreateInstance(fooType);

            var result = fooType.GetMethod("Sum").Invoke(instane, new object[] { 1, 5 });
            return result;
        }

        private static object DefChangeSet(TypeBuilder typeBuilder)
        {
            var sum = typeBuilder.DefineMethod("ChnageSet", MethodAttributes.Public, typeof(void), new[] { typeof(string), typeof(string) });

            var trimMod = typeof(string).GetMethod("Trim", new Type[] { });
            var opInequalityMod = (typeof(string).GetMethod("op_Inequality"));
            var il = sum.GetILGenerator();
            Label lbl007 = il.DefineLabel();
            Label lbl00d = il.DefineLabel();
            Label lbl001f= il.DefineLabel();
            il.DeclareLocal(typeof(int));
            il.DeclareLocal(typeof(bool));

            il.Emit(OpCodes.Ldarg_2);
            il.Emit(OpCodes.Brtrue_S, lbl007); //非空跳转
            il.Emit(OpCodes.Ldnull);
            il.Emit(OpCodes.Br_S, lbl00d);  //无条件
            il.MarkLabel(lbl007);
            il.Emit(OpCodes.Ldarg_2);
            il.Emit(OpCodes.Call, trimMod );//trim
            il.MarkLabel(lbl00d); 
            il.Emit(OpCodes.Starg_S,2);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ldarg_2);
            il.Emit(OpCodes.Call, opInequalityMod );  //不等比较
            il.Emit(OpCodes.Brfalse_S, lbl001f);   // 相等退出
            il.Emit(OpCodes.Ldarg_2);             //加载第二个参数
            il.Emit(OpCodes.Starg_S,1);       //赋值给第一个参数
            il.MarkLabel(lbl001f);
            il.Emit(OpCodes.Ret);

            var fooType = typeBuilder.CreateType();
            var instane = Activator.CreateInstance(fooType);

            var result = fooType.GetMethod("ChnageSet").Invoke(instane, new object[] { "s", "v" });
            return result;
        }

        private static void IL_Sum99()
        {
            var dm = new DynamicMethod("sum99", typeof(int), new[] { typeof(int), typeof(int) });
            var il = dm.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);  //x
            il.Emit(OpCodes.Ldarg_1); //[x,y]
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Ldc_I4, 99);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Ret);
            var sum = (Func<int, int, int>)dm.CreateDelegate(typeof(Func<int, int, int>));
            Console.WriteLine(sum(10, 20));
        }
    }

 

    public class Test
    {
        public int Sum(int x, int y)
        {
            if (x > 100)
            {
               y+=5;
            }
            return x + y;
        }

        public  void ChangeSet(string x, string v)
        {
            v = v?.Trim();
            if (x != v)
            {
                x = v;
            }
        }
    }
}
