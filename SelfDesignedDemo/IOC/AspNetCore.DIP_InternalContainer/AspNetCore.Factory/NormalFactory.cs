using Microsoft.Extensions.DependencyInjection;
using AspNetCore.Interface;
using AspNetCore.Service;
using Autofac;

namespace AspNetCore.Factory
{
    public class NormalFactory
    {
        //IOC就是用来构造对象的工厂
        //DI依赖注入：就是IOC在构造对象A时，若对象A依赖于对象B（B是A的参数），则会自动构造出对象B，并传递给对象A，实现依赖注入
        //IOC是想要的效果（），DI是实现IOC的一个技术手段

        //DI依赖注入三种方式：
        //1.	构造函数注入
        //2.	属性注入
        //3.	方法注入

        /// <summary>
        /// AspNetCore内置容器ServiceCollection
        /// 网上说这个方式只适合构造函数注入
        /// 需要Nuget引入：Microsoft.Extensions.DependencyInjection
        /// </summary>
        public static void ServiceCollection()
        {
            
            IServiceCollection serviceDescriptors = new ServiceCollection();//初始化出来容器
            //2.注册构造对象间的关系(这一步就是在构造抽象类和实现类间的关系)
            serviceDescriptors.AddTransient(typeof(IPower),typeof(Power));//AddTransient方法中第一个参数是抽象类，第二个参数是实现类
            serviceDescriptors.AddTransient(typeof(IMicrophone), typeof(Microphone));
            //3.可以理解为将映射关系生成出来
            ServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();//
            IMicrophone microphone= serviceProvider.GetService<IMicrophone>();
            IPower power= serviceProvider.GetService<IPower>();//Power构造函数注入
        }

        /// <summary>
        /// 第三方容器AutoFac
        /// 需要Nuget引入AutoFac
        /// </summary>
        public static void AutoFac()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<Microphone>().As<IMicrophone>();
        }
    }
}
