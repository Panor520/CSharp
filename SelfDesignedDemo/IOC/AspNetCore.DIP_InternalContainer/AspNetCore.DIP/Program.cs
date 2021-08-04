using System;
using AspNetCore.Interface;
using AspNetCore.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.DIP
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 利用自定义的SimpleFactory生成对象，同时发现传参问题
            {
                IMicrophone microphone = SimpleFactory.ServiceMicrophoneCreateInstance();
            }
            {
                //下面这个会报错，因为利用SimpleFactory生成实例时Power有有参构造，无无参构造。
                //IPower power = SimpleFactory.ServicePowerCreateInstance();
                //在SimpleFactory中拼凑出Power的参数，让其不报错，但违背我们的编程思想
                //这时候发现一个问题①：如果我们要创建的对象中有参数需要传递，而这些参数也是类对象的话就会出现层层依赖，这样就需要自动生成这些参数
                IPower power1 = SimpleFactory.ServicePowerCreateInstanceArr();
            }
            #endregion

            #region 利用NormalFactory中的容器解决上面的问题①
            {//把这块的实现放到NormalFactory中
                Console.WriteLine("------------------------ASPNETCORE内置容器ServiceCollection-----------------------------");
                NormalFactory.ServiceCollection();
                Console.WriteLine("------------------------第三方容器AutoFac-----------------------------");
                NormalFactory.AutoFac();
                Console.WriteLine("------------------------第三方容器AutoFac+配置文件（完全脱离细节）-----------------------------");
                NormalFactory.AutoFacOne();
            }
            #endregion

            #region 未解决问题
            //方法注入
            //生命周期问题
            #endregion
            Console.ReadLine();
        }
    }
}
