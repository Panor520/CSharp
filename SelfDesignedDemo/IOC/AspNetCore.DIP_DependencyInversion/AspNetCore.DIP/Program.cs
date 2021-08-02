using System;
using AspNetCore.IBLL;
using AspNetCore.DAL;
using AspNetCore.BLL;
using AspNetCore.Factory;

namespace AspNetCore.DIP
{
    class Program
    {
        static void Main(string[] args)
        {
            //如果不依赖倒置某一层发生变动，其它层都可能要变动，这也是为什么程序员加班的原因
            Console.WriteLine("依赖倒置：也叫做面向抽象编程");//接口就是抽象
            //DIP-IBLL-BLL-IDAL-DAL 
            //DAL发生变化不直接影响BLL，因为BLL引用的是DAL的抽象IDAL
            //下面的示例中service局部变量就是抽象IBLL
            //改了BLL中的内容，不直接影响IBLL，IBLL需要手动修改后才会起作用

            #region 普通依赖倒置
            //1.在抽象中不存在元素，在调用时无法调用到 例如下面func PlayPhone的 Iphone参数
            //2.查看定义不方便，如下面的PlayPhone
            //3.每增加一个方法，接口里还是要增加代码，并没有减少工作量
            Console.WriteLine("-----------------普通依赖倒置-----------------");
            {
                IStudentService service = new StudentService();
                Iphone iphone = new Iphone();//加了引用后才引进来Iphone类
                service.PlayPhone(iphone);//F12查看定义直接跳转到接口该函数的定义的地方了
            }
            {
                IStudentService service = new StudentService();
                Horour horour = new Horour();//加了引用后才引进来Iphone类
                service.PlayPhone(horour);//F12查看定义直接跳转到接口该函数的定义的地方了
            }
            #endregion

            #region 依赖倒置+泛型方法+抽象参数
            //
            Console.WriteLine("-----------------依赖倒置+泛型方法+抽象参数-----------------");
            //这种方法可以满足不通类型手机的输出，但还是依赖了非抽象
            {
                IStudentServiceGeneric service = new StudentServiceGeneric();
                HorourAbstract iphone = new HorourAbstract();//这个地方还是依赖于细节了，因为每个类型都要手动写一个然后继承抽象手机类
                service.PlayPhoneGeneric(iphone);//实现了一个方法满足不同手机类的需求
            }
            {
                IStudentServiceGeneric service = new StudentServiceGeneric();
                IphoneAbstract iphone = new IphoneAbstract();//这个地方还是依赖于细节了，因为每个类型都要手动写一个然后继承抽象手机类
                service.PlayPhoneGeneric(iphone);
            }
            #endregion

            #region 依赖倒置：面向抽象编程
            {
                //IStudentServiceGeneric service = new IStudentServiceGeneric();//这个声明是面向抽象编程想实现的效果，但这样声明是不可以的，会报错
                
            }
            {
                //下面的声明是可以的，但运行肯定报错，因为抽象的PlayPhoneGeneric方法没有实际实现
                //IStudentServiceGeneric service = null;
                //HorourAbstract phone = new HorourAbstract();
                //service.PlayPhoneGeneric(phone);
            }
            {
                Console.WriteLine("-----------------利用反射实现面向抽象编程-----------------");
                //自己创建一个生成对象的工厂类SimpleFactory,将对象的创建交给这个工厂
                IStudentServiceGeneric service = SimpleFactory.StudentServiceGenericCreateInstance();
                HorourAbstract phone = new HorourAbstract();
                service.PlayPhoneGeneric(phone);
            }
            {
                //利用配置文件实现创建对象的工厂类
                //这个好处是，做程序集版本更新时只需要将程序dll复制粘贴再改下配置文件即可；不需要重新启动网站就做到了项目的稳定升级
                //实现了程序的可配置、可扩展
                //这就是简易的IOC容器：工厂创建对象=反射+配置文件
                IStudentServiceGeneric service = SimpleFactory.StudentServiceGenericCreateInstanceOne();
                HorourAbstract phone = new HorourAbstract();
                service.PlayPhoneGeneric(phone);
            }
            #endregion


            Console.ReadLine();
        }
    }
}
