using Microsoft.Extensions.DependencyInjection;
using AspNetCore.Interface;
using AspNetCore.Service;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Autofac.Configuration;

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
        /// 这个方法还是用到了具体类（注册映射时），还没完全脱离细节
        /// </summary>
        public static void ServiceCollection()
        {

            IServiceCollection serviceDescriptors = new ServiceCollection();//初始化出来容器
            //2.注册构造对象间的关系(这一步就是在构造抽象类和实现类间的关系)
            serviceDescriptors.AddTransient(typeof(IPower), typeof(Power));//AddTransient方法中第一个参数是抽象类，第二个参数是实现类
            serviceDescriptors.AddTransient(typeof(IMicrophone), typeof(Microphone));
            //3.可以理解为将映射关系生成出来
            ServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();//
            IMicrophone microphone = serviceProvider.GetService<IMicrophone>();
            IPower power = serviceProvider.GetService<IPower>();//Power构造函数注入
        }

        /// <summary>
        /// 第三方容器AutoFac
        /// 需要Nuget引入AutoFac
        /// 这个方法还是用到了具体类（注册映射时），还没完全脱离细节
        /// </summary>
        public static void AutoFac()
        {
            {//普通构造函数注入
                System.Console.WriteLine("----------------------------普通构造函数注入");
                ContainerBuilder containerBuilder = new ContainerBuilder();
                containerBuilder.RegisterType<Microphone>().As<IMicrophone>();
                containerBuilder.RegisterType<Power>().As<IPower>();
                containerBuilder.RegisterType<Applephone>().As<IPhone>();
                //containerBuilder.RegisterType<Androidphone>().As<IPhone>();
                containerBuilder.RegisterType<Headphone>().As<IHeadphone>();
                IContainer container = containerBuilder.Build();
                IMicrophone microphone = container.Resolve<IMicrophone>();
                IPhone iphone = container.Resolve<IPhone>();//问题：会按Androidphone构造，而不按Applephone，为什么？
            }

            {//属性注入 
                //实现方式，在注册时加上.PropertiesAutowired()
                System.Console.WriteLine("----------------------普通构造函数注入+属性注入");
                ContainerBuilder containerBuilder = new ContainerBuilder();
                containerBuilder.RegisterType<Microphone>().As<IMicrophone>();
                containerBuilder.RegisterType<Power>().As<IPower>();
                containerBuilder.RegisterType<Applephone>().As<IPhone>().PropertiesAutowired();//属性注入
                containerBuilder.RegisterType<Headphone>().As<IHeadphone>();
                IContainer container = containerBuilder.Build();
                IMicrophone microphone = container.Resolve<IMicrophone>();
                IPhone iphone = container.Resolve<IPhone>();
            }
        }

        /// <summary>
        /// 利用配置文件解决抽象注册时依赖细节的问题
        /// 增加读取配置文件的Nuget：
        /// </summary>
        public static void AutoFacOne()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();//创建IOC容器建造者
            IConfigurationBuilder configuration = new ConfigurationBuilder();//创建配置文件建造者
            IConfigurationSource configurationSource = new JsonConfigurationSource()//读出配置文件
            {
                Path = "AutoFac.json",
                Optional=false,//默认是false，可不写
                ReloadOnChange= true//默认是true，可不写
            };
            configuration.Add(configurationSource);//给建造者添加建造工作
            var module = new ConfigurationModule(configuration.Build());//建造出配置文件映射关系，并生成模型
            containerBuilder.RegisterModule(module);//IOC容器建造者浏览映射关系模型
            IContainer container = containerBuilder.Build();//生成出真正的IOC容器

            IMicrophone microphone = container.Resolve<IMicrophone>();//测试生成的IOC容器
        }
    }
}
