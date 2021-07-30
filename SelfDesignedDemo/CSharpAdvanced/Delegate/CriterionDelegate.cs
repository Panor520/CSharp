using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvanced.Delegate
{
    class CriterionDelegate
    {

    }

    //事件的定义、调用、触发
    class DelegatePublisher
    {
        public event EventHandler<EventArgs> CustomEvent;

        //受保护的包装调用，允许派生类调用
        protected virtual void OnCustomEvent(EventArgs eventArgs)//参数是一个类
        {
            CustomEvent?.Invoke(this, eventArgs);//这个地方写两个参数的原因是，CustomEvent来自EventHandler，而EventHandler委托就有两个参数
        }

        public void Dosomething()
        {
            //调用之前可以在这里做点其他事情
            OnCustomEvent(new EventArgs());
        }
    }
    //事件订阅者：事件方法编写和订阅功能
    class Subscriber
    {
        private readonly string Str;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str">SubscriberName</param>
        /// <param name="delegatePublisher">Publisher</param>
        public Subscriber(string str,DelegatePublisher delegatePublisher)
        {
            //订阅事件
            delegatePublisher.CustomEvent += HandlerCustomEvent;
            this.Str = str;
        }
        //这里有两个参数是因为此方法是给事件的方法，而事件依靠的委托有两个参数
        private void HandlerCustomEvent(object  o, EventArgs  eventArgs)
        {
            Console.WriteLine($"发布者：{o.GetType()},订阅者：{Str},参数：{Str}");
        }
    }
}
