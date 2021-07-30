using CSharpAdvanced.Attribute;
using cc;
using CSharpAdvanced.ExtendMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CSharpAdvanced.Delegate;
using static CSharpAdvanced.Delegate.DelegateOne;
using CSharpAdvanced.Lambda;
using CSharpAdvanced.Linq;
using CSharpAdvanced.Async;
using CSharpAdvanced.Tasks;

namespace CSharpAdvanced
{
    class Start
    {
        static void Main(string[] args)
        {
            //扩展方法
            //SealClass s = new SealClass();
            //s.ExtendShow();
            //Interface @interface = new Interface();
            //Console.WriteLine(@interface.Sub(1, 2));
            //Console.WriteLine(@interface.ADD(1, 2));
            //Console.WriteLine(@interface.Multipy(1, 2));
            //Console.WriteLine(@interface.Divide(1, 2));
            //-------------------------------------------
            ////Attribute
            //UseAttribute use = new UseAttribute();
            //use.Management();

            //MyClass myClass = new MyClass();
            //myClass.OtherMethod();

            //UseAttributeAbstract useAttributeAbstract = new UseAttributeAbstract()
            //{PassWord=123456,
            //Name="XXXX"};

            //useAttributeAbstract.GetAttribute1();

            //BubbleSort2 bubbleSort2 = new BubbleSort2(new int[] { 23, 44, 66, 76, 98, 11, 3, 9, 7 });

            //MulticastDelegateTest multicastDelegateTest = new MulticastDelegateTest();
            //multicastDelegateTest.Show();

            //EventMyClass.Invoke();

            //DelegatePublisher delegatePublisher = new DelegatePublisher();
            //Subscriber subscriber = new Subscriber("subscriber", delegatePublisher);
            //delegatePublisher.Dosomething();

            //Lambda1 lambda = new Lambda1();
            //lambda.Show();

            //DelegateTwo delegateTwo = new DelegateTwo();
            //delegateTwo.Show();

            //LinqOne linq = new LinqOne();
            //linq.Linq1();
            //linq.Linq2();

            //XSD.InvokeCenter invokeCenter = new XSD.InvokeCenter();
            //invokeCenter.InvokeData();

            //LambdaOne lambdaOne = new LambdaOne();
            //lambdaOne.Show();

            //LinqToDataset linqToDataset = new LinqToDataset();
            //linqToDataset.testConn();

            //TaskInstance taskInstance = new TaskInstance();
            ////taskInstance.Six();
            //taskInstance.LockMain();

            AsyncOne asyncOne = new AsyncOne();
            asyncOne.All();
            AsyncTwo asyncTwo = new AsyncTwo();
            asyncTwo.All();

            Console.ReadKey();
        }
    }
}
