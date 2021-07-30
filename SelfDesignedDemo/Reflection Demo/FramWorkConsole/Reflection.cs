using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FramWorkConsole
{
    public class Reflection
    {
        //public object ExecuteFunction(string assemblyName, string spaceName, string className, string functionName, Dictionary<string, object> parameterValues)
        public object ExecuteFunction(string assemblyName, string spaceName, string className, string functionName, Dictionary<string, object> pairs)
        {
            if (assemblyName.ToLower().EndsWith(".dll"))
            {
                assemblyName = assemblyName.Substring(0, assemblyName.Length - 4);
            }
            //DLL和本方法所在DLL再同一个文件夹直接写名字就行
            Assembly asm = Assembly.Load(assemblyName);
            //注意写法：程序集.类名
            Type type = asm.GetType(spaceName + "." + className);//ignore case 忽略大小写  
            if (type == null)
                throw new TypeLoadException(string.Format("Unable to load the type {0} from the assembly {1}.", className, assemblyName));
            //创建获得的对象实例
            object instance = Activator.CreateInstance(type);
            //获取方法
            MethodInfo methodInfo = type.GetMethod(functionName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            if (methodInfo == null)
                throw new TypeLoadException(string.Format("Unable to find the function named {0} in {1}.", functionName, type.FullName));
            //获取方法的参数
            ParameterInfo[] parameterInfos = methodInfo.GetParameters();
            List<object> parameters = new List<object>();
            //foreach (ParameterInfo paramInfo in parameterInfos)
            //{
            //    if (parameterValues.ContainsKey(paramInfo.Name))
            //    {
            //        parameters.Add(parameterValues[paramInfo.Name]);
            //    }
            //    else
            //    {
            //        parameters.Add(null);
            //    }
            //}
            foreach (var item in pairs)
            {
                var type2 =Type.GetType(item.Key.ToString());
                parameters.Add(Convert.ChangeType(item.Value, type2));
            }

            //执行方法，并添加参数
            object result = methodInfo.Invoke(instance, parameters.ToArray<object>());
            return result;
        }

    }
}
