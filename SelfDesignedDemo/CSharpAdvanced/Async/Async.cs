using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CSharpAdvanced.Async
{
    class  AsyncOne
    {
        public void All()
        {
            DateTime starttime = DateTime.Now;
            AsyncOne.ApplyCoffee();                    //2s
            AsyncOne.jiareguo();                       //5
            AsyncOne.jianjidan();                      //4
            AsyncOne.jianpeigen();                     //4
            AsyncOne.kaomianbao();                     //3
            AsyncOne.miaobaojiahuangyouheguojiang();   //2
            AsyncOne.daoguozhi();                      //1
            Console.WriteLine("共耗时:"+(DateTime.Now-starttime)); //
        }
        public static void ApplyCoffee()
        {
            Console.WriteLine("ApplyCoffee! 2s");
            Thread.Sleep(2000);
        }

        public static void jiareguo()
        {
            Console.WriteLine("加热锅！ 5s");
            Thread.Sleep(5000);
            //Task.Delay(5000).Wait();
        }

        public static void jianjidan()
        {
            Console.WriteLine("煎鸡蛋！ 4s");
            Thread.Sleep(4000);
        }

        public static void jianpeigen()
        {
            Console.WriteLine("煎培根！ 4s");
            Thread.Sleep(4000);
        }

        public static void kaomianbao()
        {
            Console.WriteLine("烤面包！ 3s");
            Thread.Sleep(3000);
        }

        public static void miaobaojiahuangyouheguojiang()
        {
            Console.WriteLine("面包上加黄油和果酱！ 2s");
            Thread.Sleep(2000);
        }
        public static  void daoguozhi()
        {
            Console.WriteLine("倒果汁！ 5s");
            Thread.Sleep(5000);
        }
    }

    class AsyncTwo
    {
        public async Task All()
        {
            Task ApplyCoffeeTask = Task.Run(ApplyCoffee);
            //Task jiareguoTask = Task.Run(jiareguo);
            //Task jianjidanTask= Task.Run(jianjidan);
            //Task jianpeigenTask = Task.Run(jianpeigen);
            //Task kaomianbaoTask = Task.Run(kaomianbao);
            //Task miaobaojiahuangyouheguojiangTask = Task.Run(miaobaojiahuangyouheguojiang);

            DateTime starttime = DateTime.Now;
            ApplyCoffeeTask.Wait();
            if(ApplyCoffeeTask.Status== Task.CompletedTask.Status)
            {
                 jian();
            }
            if (ApplyCoffeeTask.Status == Task.CompletedTask.Status)
            {
                 kao();
            }

            Task daoguozhiTask = Task.Run(daoguozhi);
            await daoguozhiTask;

            Console.WriteLine("共耗时："+(DateTime.Now-starttime));
        }

        public async Task jian()
        {
            Task jiareguoTask = Task.Run(jiareguo);
            jiareguoTask.Wait();
            Task jianjidanTask = Task.Run(jianjidan);
            jianjidanTask.Wait();
            Task jianpeigenTask = Task.Run(jianpeigen);
            jianpeigenTask.Wait();
        }

        public async Task kao()
        {
            Task kaomianbaoTask = Task.Run(kaomianbao);
            kaomianbaoTask.Wait();
            Task miaobaojiahuangyouheguojiangTask = Task.Run(miaobaojiahuangyouheguojiang);
            miaobaojiahuangyouheguojiangTask.Wait();
        }
        public static  void  ApplyCoffee()
        {
            Console.WriteLine("ApplyCoffee! 2s");
            Thread.Sleep(2000);
           //return "ApplyCoffee! 2s";
        }

        public static void jiareguo()
        {
            Console.WriteLine("加热锅！ 5s");
            Task.Delay(5000).Wait();
            //return "加热锅！ 5s";
        }

        public static void jianjidan()
        {
            Console.WriteLine("煎鸡蛋！ 4s");
            Thread.Sleep(4000);
            //return "煎鸡蛋！ 4s";
        }

        public static void jianpeigen()
        {
            Console.WriteLine("煎培根！ 4s");
            Thread.Sleep(4000);
            //return "煎培根！ 4s";
        }

        public static void kaomianbao()
        {
            Console.WriteLine("烤面包！ 3s");
            Thread.Sleep(3000);
            //return "烤面包！ 3s";
        }

        public static void miaobaojiahuangyouheguojiang()
        {
            Console.WriteLine("面包上加黄油和果酱！ 2s");
            Thread.Sleep(2000);
            //return "面包上加黄油和果酱！ 2s";
        }
        public static void daoguozhi()
        {
            Console.WriteLine("倒果汁！ 5s");
            Thread.Sleep(5000);
            //return "倒果汁！ 1s";
        }
    }
}
