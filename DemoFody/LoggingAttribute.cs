using Newtonsoft.Json;
using Rougamo;
using Rougamo.Context;
using System;

namespace DemoFody;
[AttributeUsage(AttributeTargets.Method)]
public class LoggingAttribute: MoAttribute
{
    public override void OnEntry(MethodContext context)
    {
        Console.WriteLine("执行方法 {0}() 开始, 参数：{1}.",
            context.Method.Name, JsonConvert.SerializeObject(context.Arguments));
    }

    public override void OnException(MethodContext context)
    {
        Console.WriteLine("执行方法 {0}() 异常, {1}.", context.Method.Name, context.Exception.Message);
    }

    public override void OnExit(MethodContext context)
    {
        Console.WriteLine("执行方法 {0}() 结束.", context.Method.Name);
    }

    public override void OnSuccess(MethodContext context)
    {
        Console.WriteLine("执行方法 {0}() 成功.", context.Method.Name);
    }
}
