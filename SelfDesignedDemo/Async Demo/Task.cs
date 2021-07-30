using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskDemoOne
    {
        static readonly Random random = new Random();

        static async Task Main1() => Console.WriteLine($"You rolled {await GetDiceRollAsync()}");

        static async Task<int> GetDiceRollAsync()
        {
            Console.WriteLine("Shaking dice...");

            int rol1 = await RollAsync();
            int rol2 = await RollAsync();

            return rol1 + rol2;
        }

        static async Task<int> RollAsync()
        {
            await Task.Delay(500);

            int diceRoll = random.Next(1, 7);
            return diceRoll;
        }
    }


    /// <summary>
    ///  https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/async/cancel-async-tasks-after-a-period-of-time
    /// </summary>
    class TaskDemoTwo
    {
        static readonly CancellationTokenSource s_cts = new CancellationTokenSource();

        static readonly HttpClient s_client = new HttpClient
        {
            MaxResponseContentBufferSize = 1_000_000
        };

        static readonly IEnumerable<string> s_urlList = new string[]
        {
            "https://docs.microsoft.com",
            "https://docs.microsoft.com/aspnet/core",
            "https://docs.microsoft.com/azure",
            "https://docs.microsoft.com/azure/devops",
            "https://docs.microsoft.com/dotnet",
            "https://docs.microsoft.com/dynamics365",
            "https://docs.microsoft.com/education",
            "https://docs.microsoft.com/enterprise-mobility-security",
            "https://docs.microsoft.com/gaming",
            "https://docs.microsoft.com/graph",
            "https://docs.microsoft.com/microsoft-365",
            "https://docs.microsoft.com/office",
            "https://docs.microsoft.com/powershell",
            "https://docs.microsoft.com/sql",
            "https://docs.microsoft.com/surface",
            "https://docs.microsoft.com/system-center",
            "https://docs.microsoft.com/visualstudio",
            "https://docs.microsoft.com/windows",
            "https://docs.microsoft.com/xamarin"
        };

        /// <summary>
        /// Get Key then cancel
        /// </summary>
        /// <returns></returns>
        static async Task Main21()
        {
            Console.WriteLine("Application started.");
            Console.WriteLine("Press the ENTER key to cancel...\n");

            Task cancelTask = Task.Run(() =>
            {
                while (Console.ReadKey().Key != ConsoleKey.Enter)
                {
                    Console.WriteLine("Press the ENTER key to cancel...");
                }

                Console.WriteLine("\nENTER key pressed: cancelling downloads.\n");
                s_cts.Cancel();
            });

            Task sumPageSizesTask = SumPageSizesAsync();

            await Task.WhenAny(new[] { cancelTask, sumPageSizesTask });

            Console.WriteLine("Application ending.");
        }

        static async Task Main22()
        {
            Console.WriteLine("Application started.");

            try
            {
                //after 3.5s,SumPageSizesAsync will cancel 
                s_cts.CancelAfter(3500);
                await SumPageSizesAsync();
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("\nTasks cancelled: timed out.\n");
            }
            finally
            {
                //Releases All resource used by the current instance of the System.Threading.CancellationTokenSource class
                s_cts.Dispose();
            }
            Console.WriteLine("Application ending.");
        }

        static async Task SumPageSizesAsync()
        {
            var stopwatch = Stopwatch.StartNew();

            int total = 0;
            foreach (string url in s_urlList)
            {
                int contentLength = await ProcessUrlAsync(url, s_client, s_cts.Token);
                total += contentLength;
            }

            stopwatch.Stop();

            Console.WriteLine($"\nTotal bytes returned:  {total:#,#}");
            Console.WriteLine($"Elapsed time:          {stopwatch.Elapsed}\n");
        }

        static async Task<int> ProcessUrlAsync(string url, HttpClient client, CancellationToken token)
        {
            HttpResponseMessage response = await client.GetAsync(url, token);
            byte[] content = await response.Content.ReadAsByteArrayAsync(token);
            Console.WriteLine($"{url,-60} {content.Length,10:#,#}");

            return content.Length;
        }
    }

    /// <summary>
    /// https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/async/start-multiple-async-tasks-and-process-them-as-they-complete
    /// </summary>
    class TaskDemoThree
    {
        static readonly HttpClient s_client = new HttpClient { MaxResponseContentBufferSize = 1_000_000 };

        static readonly IEnumerable<string> s_urlList = new string[]
        {
            "https://docs.microsoft.com",
            "https://docs.microsoft.com/aspnet/core",
            "https://docs.microsoft.com/azure",
            "https://docs.microsoft.com/azure/devops",
            //"https://docs.microsoft.com/dotnet",
            //"https://docs.microsoft.com/dynamics365",
            //"https://docs.microsoft.com/education",
            //"https://docs.microsoft.com/enterprise-mobility-security",
            //"https://docs.microsoft.com/gaming",
            //"https://docs.microsoft.com/graph",
            //"https://docs.microsoft.com/microsoft-365",
            //"https://docs.microsoft.com/office",
            //"https://docs.microsoft.com/powershell",
            //"https://docs.microsoft.com/sql",
            //"https://docs.microsoft.com/surface",
            //"https://docs.microsoft.com/system-center",
            //"https://docs.microsoft.com/visualstudio",
            //"https://docs.microsoft.com/windows",
            //"https://docs.microsoft.com/xamarin"
        };

        
        static async Task<int> ProcessUrlAsync(string url,HttpClient client)
        {
            //GetByteArrayAsync:Return the response body as a byte array in an Asynchronous operation.
            byte[] content = await client.GetByteArrayAsync(url);
            Console.WriteLine($"{url,-60} {content.Length,10:#,#}");

            return content.Length;
        }

        static async Task SumPageSizesAsync()
        {
            //Stopwatch:Measure elapsed time
            var stopWatch = Stopwatch.StartNew();

            IEnumerable<Task<int>> downloadTaskQuery =
                s_urlList.Select((url) => ProcessUrlAsync(url, s_client));

            //from url in s_urlList
            //select ProcessUrlAsync(url,s_client);
            List<Task<int>> downloadTasks = downloadTaskQuery.ToList();
            int total = 0;

            //Any(): downloadTasks is not empty
            while (downloadTasks.Any())
            {
                Task<int> finishedTask = await Task.WhenAny(downloadTasks);
                downloadTasks.Remove(finishedTask);

                total += await finishedTask;//requestconent total
            }

            stopWatch.Stop();

            Console.WriteLine($"\nTotal bytes returned:  {total:#,#}");
            Console.WriteLine($"Elapsed time:          {stopWatch.Elapsed}\n");
        }

        static Task Main31() => SumPageSizesAsync();
    }

    /// <summary>
    /// 异步文件访问 https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/async/using-async-for-file-access
    /// </summary>
    class TaskDemoFour
    {
        #region ProcessWriteAsync
        static async Task WriteTextAsync(string filePath,string text)
        {
            byte[] encodedText = Encoding.Unicode.GetBytes(text);

            //Read Write difference
            using var sourceStream = 
                new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None,
                                bufferSize:4096,useAsync:true);

            await sourceStream.WriteAsync(encodedText,0,encodedText.Length);
            //上面一步是下面两步的缩写
            //Task theTask = sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            //await theTask;
        }

        //ProcessWriteAsync
        public static  async Task Main41()
        {
            string filePath = "temp.txt";
            string text = $"Hello World{Environment.NewLine}";

            await WriteTextAsync(filePath, text);
        }

        #endregion

        #region ProcessReadAsync
        static async Task<string> ReadTextAsync(string filePath)
        {
            //Read Write difference
            using var sourceStream =
                new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read,
                                bufferSize: 4096, useAsync: true);

            var sb = new StringBuilder();
            byte[] buffer = new byte[0x1000];
            int numRead;
            while ((numRead= await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                string text = Encoding.Unicode.GetString(buffer,0, numRead);
                sb.Append(text);
            }

            return sb.ToString();
        }

        //ProcessReadAsync
        public static async Task Main42()
        {
            try
            {
                string filePath = "temp.txt";
                if (File.Exists(filePath) != false)
                {
                    string text = await ReadTextAsync(filePath);
                    Console.WriteLine(text);
                }
                else
                {
                    Console.WriteLine($"file not found: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region Parallel Demo
        /// <summary>
        /// SimpleParallelWriteAsync
        /// </summary>
        /// <returns></returns>
        static async Task Main43()
        {
            string folder = Directory.CreateDirectory("tempfolder").Name;
            IList<Task> writeTaskList = new List<Task>();

            for (int index = 11; index <= 20; ++index)
            {
                string fileName = $"file-{index:00}.txt";
                string filePath = $"{folder}/{fileName}";
                string text = $"In file {index}{Environment.NewLine}";

                writeTaskList.Add(File.WriteAllTextAsync(filePath, text));
            }

            await Task.WhenAll(writeTaskList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static async Task Main()
        {
            //IList不包含List中的功能，更便捷。List中有很多方法例如lambda查询，但这里只是存储数据流，不做查询类操作。使用IList最好
            IList<FileStream> sourceStreams = new List<FileStream>();

            try
            {
                string folder = Directory.CreateDirectory("tempfolder").Name;
                IList<Task> writeTaskList = new List<Task>();

                for (int index = 1; index < 10; index++)
                {
                    string fileName = $"file-{index:00}.txt";
                    string filePath = $"{folder}/{fileName}";

                    //Environment.NewLine 获取此环境的换行符
                    string text = $"In file {index}{Environment.NewLine}";
                    byte[] encodedText = Encoding.Unicode.GetBytes(text);

                    var sourceStream =
                        new FileStream(
                            filePath,
                            FileMode.Create, FileAccess.Write, FileShare.None,
                            bufferSize: 4096, useAsync: true);
                    //已存在是不会覆盖的
                    Task writeTask = sourceStream.WriteAsync(encodedText, 0, encodedText.Length);

                    sourceStreams.Add(sourceStream);
                    writeTaskList.Add(writeTask);
                }

                await Task.WhenAll(writeTaskList);

            }
            catch (Exception ex)
            {
            }
            finally
            {
                foreach (FileStream sourceStream in sourceStreams)
                {
                    sourceStream.Close();
                }
            }
        }
        #endregion
    }
}
