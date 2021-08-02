using System;
using AspNetCore.IBLL;
using System.Reflection;
using Microsoft.Extensions.Configuration;//nuget包Microsoft.Extensions.Configuration.Json

namespace AspNetCore.Factory
{
    public class SimpleFactory
    {
        /// <summary>
        /// 初步利用反射实现创建对象并返回
        /// 也就是在这里面实现抽象和实际对象的联系
        /// 断开了对实际对象内部细节的依赖，只通过类名即可联系
        /// </summary>
        /// <returns></returns>
        public static IStudentServiceGeneric StudentServiceGenericCreateInstance()
        {
            Assembly assembly = Assembly.LoadFrom("AspNetCore.BLL.dll");//DLL名称
            Type type = assembly.GetType("AspNetCore.BLL.StudentServiceGeneric");//类型全名称=命名空间+类名
            object obj = Activator.CreateInstance(type);
            return (IStudentServiceGeneric)obj;//强制转换成对应的抽象类型
        }

        /// <summary>
        /// 利用配置文件进一步优化利用反射创建对象问题
        /// 利用配置文件更进一步的让代码远离细节，修改内容时只需修改配置文件即可
        /// 但是getconfig时仍然会使用配置文件的名字，还是有部分细节
        /// 我们的最终目标是完全舍弃细节
        /// </summary>
        /// <returns></returns>
        public static IStudentServiceGeneric StudentServiceGenericCreateInstanceOne()
        {
            string str=CustomConfigManager.GetConfig("IStudentServiceAssembly");
            
            Assembly assembly = Assembly.LoadFrom(str.Split(',')[0]);//DLL名称
            Type type = assembly.GetType(str.Split(',')[1]);//类型全名称=命名空间+类名
            object obj = Activator.CreateInstance(type);
            return (IStudentServiceGeneric)obj;//强制转换成对应的抽象类型
        }
    }

    public class CustomConfigManager
    {
        public static string GetConfig(string key)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();
            string configValue = configuration.GetSection(key).Value;
            return configValue;
        }
    }
}
