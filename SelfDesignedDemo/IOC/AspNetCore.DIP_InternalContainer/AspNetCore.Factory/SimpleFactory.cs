using System;
using AspNetCore.Interface;
using System.Reflection;
using Microsoft.Extensions.Configuration;//nuget包Microsoft.Extensions.Configuration.Json

namespace AspNetCore.Factory
{
    public class SimpleFactory
    {
        public static IMicrophone ServiceMicrophoneCreateInstance()
        {
            string str = CustomConfigManager.GetConfig("IMicrophoneServiceAssembly");

            Assembly assembly = Assembly.LoadFrom(str.Split(',')[0]);//DLL名称
            Type type = assembly.GetType(str.Split(',')[1]);//类型全名称=命名空间+类名
            object obj = Activator.CreateInstance(type);
            return (IMicrophone)obj;//强制转换成对应的抽象类型
        }

        /// <summary>
        /// 每个生成的方法都要配置程序集的配置文件的名称，我们想要实现连这个配置都自动获取到
        /// </summary>
        /// <returns></returns>
        public static IPower ServicePowerCreateInstance()
        {
            string str=CustomConfigManager.GetConfig("IPowerServiceAssembly");
            
            Assembly assembly = Assembly.LoadFrom(str.Split(',')[0]);//DLL名称
            Type type = assembly.GetType(str.Split(',')[1]);//类型全名称=命名空间+类名
            object obj = Activator.CreateInstance(type);//去执行无参数构造函数（power没有无参构造函数），但Power有个有参数构造函数故这个地方会报错
            return (IPower)obj;//强制转换成对应的抽象类型
        }

        //
        public static IPower ServicePowerCreateInstanceArr()
        {
            string str = CustomConfigManager.GetConfig("IPowerServiceAssembly");
            IMicrophone microphone = ServiceMicrophoneCreateInstance();
            Assembly assembly = Assembly.LoadFrom(str.Split(',')[0]);//DLL名称
            Type type = assembly.GetType(str.Split(',')[1]);//类型全名称=命名空间+类名
            object obj = Activator.CreateInstance(type,new object[] { microphone });//拼凑出对象，使这个地方不报错，但完全违背优秀的编程思想
            return (IPower)obj;//强制转换成对应的抽象类型
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
