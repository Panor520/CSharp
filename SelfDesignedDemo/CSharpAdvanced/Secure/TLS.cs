using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpAdvanced.Secure
{
    class TLS
    {
        public static X509Certificate cert;
        void Main(string[] args)
        {
            X509Store store = new X509Store(StoreName.Root);
            store.Open(OpenFlags.ReadWrite);
            // 检索证书 
            X509Certificate2Collection certs = store.Certificates.Find(X509FindType.FindBySubjectName, "SSL-SERVER", false); // vaildOnly = true时搜索无结果。
            if (certs.Count == 0) return;
            cert = certs[0];
            store.Close(); // 关闭存储区。

            TcpListener server = new TcpListener(900);
            Thread thread_server = new Thread(new ParameterizedThreadStart(run));
            thread_server.Start(server);
            TcpClient tc = new TcpClient();
            tc.Connect("127.0.0.1", 900);
            SslStream stream = new SslStream(tc.GetStream());
            stream.AuthenticateAsClient("SSL-SERVER");
            while (true)
            {
                string echo = Console.ReadLine();
                byte[] buff = Encoding.UTF8.GetBytes(echo + "<EOF>");
                stream.Write(buff, 0, buff.Length);
                stream.Flush();
                //tc.GetStream().Write(buff, 0, buff.Length);
                //tc.GetStream().Flush();
            }
            tc.Close();
        }
        public static void run(object server)
        {
            TcpListener tcp = (TcpListener)server;
            tcp.Start();
            Console.WriteLine("Listening");
            while (true)
            {
                TcpClient socket = tcp.AcceptTcpClient();
                Console.WriteLine("Client connected");
                SslStream stream = new SslStream(socket.GetStream());
                stream.AuthenticateAsServer(cert);
                while (true)
                {
                    //NetworkStream stream= socket.GetStream();
                    StringBuilder sb = new StringBuilder();
                    MemoryStream ms = new MemoryStream();
                    int len = -1;
                    do
                    {
                        byte[] buff = new byte[1000];
                        len = stream.Read(buff, 0, buff.Length);
                        ms.Write(buff, 0, len);
                        string line = new String(Encoding.UTF8.GetChars(buff, 0, len));
                        if (line.EndsWith("<EOF>"))
                            break;
                    } while (len != 0);
                    //string echo=Encoding.UTF8.GetString(buff).Trim('\0');
                    string echo = Encoding.UTF8.GetString(ms.ToArray());
                    ms.Close();
                    Console.WriteLine(echo);
                    if (echo.Equals("q"))
                    {
                        break;
                    }
                }
                socket.Close();
            }
        }
    }
}
