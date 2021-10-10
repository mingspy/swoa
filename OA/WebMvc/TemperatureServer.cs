using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace YJ.Utility
{
    public class TemperatureServer
    {
        public TemperatureServer(int port = 1983)
        {
            this.port = port;
        }

        public int port { get; private set; }

        public void start_server()
        {
            //定义接收数据长度变量
            int recv;
            //定义接收数据的缓存
            byte[] data = new byte[1024];
            //定义侦听端口
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, this.port);
            //定义套接字类型
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //连接
            serverSocket.Bind(ipEnd);
            //开始侦听
            serverSocket.Listen(10);
            //控制台输出侦听状态
            Console.Write("Waiting for a client");
            ThreadPool.SetMaxThreads(100, 100);
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();//接受客户端的连接
                IPEndPoint clientipe = (IPEndPoint)clientSocket.RemoteEndPoint;
                Console.WriteLine("[" + clientipe.Address.ToString() + "] Connected");

                ThreadPool.QueueUserWorkItem(new WaitCallback(receiveSocket), clientSocket);

            }
        }

        /// <summary>
        /// 接收来自客户端的消息
        /// </summary>
        static void receiveSocket(object clientSocket)  //////  这里的参数是线程中的参数，参数类型必须是object类型
        {
            Socket myClientSocket = (Socket)clientSocket;  //// 将object类型的参数转换成socket类型  使用参数来启动线程，执行后面的代码
            IPEndPoint clientipe = (IPEndPoint)myClientSocket.RemoteEndPoint;
            DateTime stime = DateTime.Now;
            YJ.Data.MSSQL.DBHelper myDb = new YJ.Data.MSSQL.DBHelper();
            string did = "";
            int first_msg = 0;
            string ip = clientipe.Address.ToString();

            try
            {
                while (true)
                {
                    byte[] data = new byte[1024];
                    int length = myClientSocket.Receive(data);
                    if (length == 0)
                    {
                        //Console.WriteLine("client exist:" + clientipe.Address.ToString());

                        string sql2 = string.Format("MERGE INTO [TemperatureDevice] as T"
                            + " USING(SELECT '{0}' AS did, '{1}' AS stat, '{2}' as ip) AS S"
                            + " ON T.did = S.did "
                            + " WHEN MATCHED THEN"
                            + " UPDATE SET T.stat = S.stat, T.ip = S.ip"
                            + " WHEN NOT MATCHED THEN"
                            + " INSERT([did], [stat], [ip]) VALUES(S.did, S.stat, S.ip); ", did, 0, ip);
                        myDb.Execute(sql2);

                        break;
                    }
                    if (length > 1023)
                    {
                        length = 1023;
                    }
                    string message = Encoding.UTF8.GetString(data, 0, length);
                    
                    //Console.WriteLine(DateTime.Now.ToString() + " 接收到来自客户端的消息:" + message);
                    System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString() + " 接收到来自客户端的消息:" + message);
                    TimeSpan s = DateTime.Now - stime;
                    stime = DateTime.Now;
                    if (!message.StartsWith("upload?"))
                    {
                        continue;
                    }
                    if (s.TotalSeconds < 5)
                    {
                        continue;
                    }


                    string msg = message.Substring(7);
                    string[] its = msg.Split('&');

                    Dictionary<string, string> mp = new Dictionary<string, string>();
                    foreach (var it in its)
                    {
                        string[] ss = it.Split('=');
                        mp[ss[0].Trim()] = ss[1].Trim();
                    }

                    string sql = string.Format("Insert INTO [TemperatureDetail] ([temp], [humb], [did]) values ('{0}','{1}', '{2}')"
                     , mp["temp"], mp["humb"], mp["id"]);
                    myDb.Execute(sql);
                    did = mp["id"];

                    if (first_msg % 1000 == 0)
                    {
                        string sql2 = string.Format("MERGE INTO [TemperatureDevice] as T"
                            + " USING(SELECT '{0}' AS did, '{1}' AS stat, '{2}' as ip) AS S"
                            + " ON T.did = S.did "
                            + " WHEN MATCHED THEN"
                            + " UPDATE SET T.stat = S.stat, T.ip = S.ip"
                            + " WHEN NOT MATCHED THEN"
                            + " INSERT([did], [stat], [ip]) VALUES(S.did, S.stat, S.ip); ", did, 1, ip);
                        myDb.Execute(sql2);
                        first_msg = 0;
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);

            }
        }
    }
}
