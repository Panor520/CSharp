using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpAdvanced.Tasks
{
    class TaskInstance
    {
        public void One()
        {
            Action<object> action = (object obj) =>
            {
                Console.WriteLine($"Task={Task.CurrentId}, obj={obj}, Thread={Thread.CurrentThread.ManagedThreadId}");
            };

            // Create a task but do not start it.
            Task t1 = new Task(action, "alpha");

            //Construct a started task
             Task t2 = Task.Factory.StartNew(action, "beta");
            // Block the main thread to demonstrate that t2 is executing
            t2.Wait();

            // Launch t1
            t1.Start();

            Console.WriteLine($"t1 has been launched. (Main Thread={Thread.CurrentThread.ManagedThreadId})");
            // Wait for the task to finish.
            t1.Wait();

            // Construct a started task using Task.Run.
            String taskData = "delta";
            Task t3 = Task.Run(() =>
            {
                Console.WriteLine($"Task={Task.CurrentId}, obj={taskData}, Thread={Thread.CurrentThread.ManagedThreadId}");
            });
            // Wait for the task to finish.
            t3.Wait();

            // Construct an unstarted task
            Task t4 = new Task(action, "gamma");
            // Run it synchronously
            t4.RunSynchronously();
            // Although the task was run synchronously, it is a good practice
            // to wait for it in the event exceptions were thrown by the task.
            t4.Wait();

            //任务以 t4 同步方式执行，因此它在主应用程序线程上执行。 其余任务通常在一个或多个线程池线程上异步执行。
        }

        public void Two()
        {
            Task taskA = Task.Run(() =>
            {
                Thread.Sleep(5000);
                Console.WriteLine("Sleep 5 second ");
            });
            Console.WriteLine($"taskA status:{taskA.Status}");

            try
            {
                taskA.Wait(1000);//TASK completed
                bool completeFlag = taskA.IsCompleted;
                Console.WriteLine("taskA Status: {0}", taskA.Status);
                if (!completeFlag)
                    Console.WriteLine("Timed out before task A completed.");
            }
            catch (AggregateException)
            {
                Console.WriteLine("Exception in taskA.");
            }
        }

        public void Three()
        {
            var tasks = new Task[3];
            //var rnd = new Random();
            //for (int ctr = 0; ctr <= 2; ctr++)
            //    tasks[ctr] = Task.Run(() => Thread.Sleep(rnd.Next(500, 3000)));
            tasks[0] = Task.Run(() => Thread.Sleep(3000));// task ID 1
            tasks[1] = Task.Run(() => Thread.Sleep(50));// task ID 2
            tasks[2] = Task.Run(() => Thread.Sleep(3000));//task ID 3

            try
            {
                int index = Task.WaitAny(tasks);//返回任一已完成的task对应的ID，启动的task对应的ID从1开始累计
                Console.WriteLine("Task #{0} completed first.\n", tasks[index].Id);
                Console.WriteLine("Status of all tasks:");
                foreach (var t in tasks)
                    Console.WriteLine("   Task #{0}: {1}", t.Id, t.Status);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task  Four()
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            var files = new List<Tuple<string, string, long, DateTime>>();

            var t = new Task(()=>
            {
                string dir = "C:\\Windows\\System32\\";
                object obj = new Object();
                if (Directory.Exists(dir))
                {
                    Parallel.ForEach(Directory.GetFiles(dir),f=> {
                        if (token.IsCancellationRequested)
                            token.ThrowIfCancellationRequested();
                        var fi = new FileInfo(f);
                        lock(obj)
                        {
                            files.Add(Tuple.Create(fi.Name, fi.DirectoryName, fi.Length, fi.LastWriteTimeUtc));
                        }
                    });
                }
            },token);

            t.Start();
            tokenSource.Cancel();
            try
            {
                await t;
                Console.WriteLine("Retrieved information for {0} files.", files.Count);
            }
            catch (AggregateException e)
            {
                Console.WriteLine("Exception messages:");
                foreach (var ie in e.InnerExceptions)
                    Console.WriteLine("   {0}: {1}", ie.GetType().Name, ie.Message);
                Console.WriteLine("\nTask status: {0}", t.Status);
            }
            finally
            {
                tokenSource.Dispose();
            }
        }

        public static  void Five()
        {
            //define the cancellation token
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            var files = new List<Tuple<string, string, long, DateTime>>();

            var t = new Task(() =>
            {
                string dir = "C:\\Windows\\System32\\";
                object obj = new Object();
                if (Directory.Exists(dir))
                {
                    Parallel.ForEach(Directory.GetFiles(dir), f => {
                        if (token.IsCancellationRequested)
                            token.ThrowIfCancellationRequested();
                        var fi = new FileInfo(f);
                        lock (obj)
                        {
                            files.Add(Tuple.Create(fi.Name, fi.DirectoryName, fi.Length, fi.LastWriteTimeUtc));
                        }
                    });
                }
            }, token);

            t.Start();
            t.Wait();
            tokenSource.Cancel();
            try
            {
                Console.WriteLine("Retrieved information for {0} files.", files.Count);
            }
            catch (AggregateException e)
            {
                Console.WriteLine("Exception messages:");
                foreach (var ie in e.InnerExceptions)
                    Console.WriteLine("   {0}: {1}", ie.GetType().Name, ie.Message);
                Console.WriteLine("\nTask status: {0}", t.Status);
            }
            finally
            {
                tokenSource.Dispose();
            }

        }

        public void Six()
        {
            //Define the cancellation token
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            Random rnd = new Random();
            Object lockObj = new Object();

            List<Task<int[]>> tasks = new List<Task<int[]>>();
            TaskFactory factory = new TaskFactory(token);
            for (int taskCtr = 0; taskCtr < 10; taskCtr++)
            {
                int iteration = taskCtr + 1;
                tasks.Add(factory.StartNew(()=> {
                    int value;
                    int[] values = new int[10];
                    for (int ctr = 1; ctr  <= 10; ctr ++)
                    {
                        lock(lockObj)
                        {
                            value= rnd.Next(0, 101);
                        }
                        if (value==0)
                        {
                            tokenSource.Cancel();
                            Console.WriteLine("Cancelling at task {0}", iteration);
                            break;
                        }
                        values[ctr - 1] = value;
                    }
                    return values;
                },token));
            }
            try
            {
                Task<double> fTask = factory.ContinueWhenAll(tasks.ToArray(),(result)=> {
                    Console.WriteLine("Calculating overall mean...");
                    long sum = 0;
                    int n = 0;
                    foreach (var t in result)
                    {
                        foreach (var r in t.Result)
                        {
                            sum += r;
                            n++;
                        }
                    }
                    return sum / (double)n;
                },token);
                Console.WriteLine("The mean is {0}.", fTask.Result);
            }
            catch (AggregateException ae)
            {
                foreach (Exception e in ae.InnerExceptions)
                {
                    if (e is TaskCanceledException)
                    {
                        Console.WriteLine("Unable to compute mean: {0}",
                                 ((TaskCanceledException)e).Message);
                    }
                    else
                        Console.WriteLine("Exception: " + e.GetType().Name);
                }
            }
            finally
            {
                 tokenSource.Dispose();
            }

        }

        public void TestLock()
        {
            int a = 0;
            Object oLock = new object();
            List<Task> tasks = new List<Task>();
            var t=(new Task(()=> {
                lock (oLock)
                {
                    //Thread.Sleep(3000);
                    
                    a += 1;
                    Console.WriteLine(a);
                    // Thread.Sleep(5000);
                }
            }));
            t.Start();
            lock (oLock)
            {
                //Thread.Sleep(3000);

                a += 1;
                Console.WriteLine(a);
                 Thread.Sleep(5000);
            }
            t.Wait();
        }

        public  void LockMain()
        {
            TaskInstance taskInstance = new TaskInstance();
            taskInstance.TestLock();
        }
    }
}
