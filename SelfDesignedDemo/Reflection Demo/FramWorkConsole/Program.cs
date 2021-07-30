using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace FramWorkConsole
{
    class Program
    {
        //定义Timer类
        private static System.Timers.Timer timer;
        static void Main(string[] args)
        {
            Server server = new Server();
            server.CurrentTaskUID = 1;

            Thread taskThread = new Thread(server.ExecuteTask);
            DateTime dtBegin = DateTime.Now;
            //Console.WriteLine($"Task {server.CurrentTaskUID} execute Start!");
            
            timer = new System.Timers.Timer(1000);
            //设置执行一次（false）还是一直执行(true)
            timer.AutoReset = true;
            //设置是否执行System.Timers.Timer.Elapsed事件
            timer.Enabled = true;
            //绑定Elapsed事件
            timer.Elapsed += (Object source, ElapsedEventArgs e) => Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);

            taskThread.Start();
            taskThread.Join();
            DateTime dtEnd = DateTime.Now;
            if (taskThread.ThreadState==ThreadState.Stopped)
            {
                timer.Stop();
                timer.Dispose();
            }
            Console.WriteLine($"Task {server.CurrentTaskUID} execute End,共耗时{dtEnd- dtBegin}");

            Console.ReadKey();
        }

        static void Timer()
        {
            Console.WriteLine($"{DateTime.Now.ToString()}");
        }
    }
}
