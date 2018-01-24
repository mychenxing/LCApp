using System;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace LCApp {
    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string[] IP_Port;
            StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"\IP.txt", false);
            IP_Port = sr.ReadLine().Split(',');
            Console.WriteLine(IP_Port[0]);
            Console.WriteLine(IP_Port[1]);
            sr.Close();
            //设定服务器IP地址
            IPAddress ip = IPAddress.Parse(IP_Port[0]);
            int port = Convert.ToInt16(IP_Port[1]);

            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(ip, port));
                clientSocket.Close();
                Application.Run(new Form1());
            }
            catch
            {
                Application.Run(new Warning1());
            }

            //string[] IP_Port;
            //StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"\IP.txt", false);
            //IP_Port = sr.ReadLine().ToString().Split(',');
            //Console.WriteLine(IP_Port[0].ToString());
            //Console.WriteLine(IP_Port[1].ToString());
            //sr.Close();
            ////设定服务器IP地址
            //IPAddress ip = IPAddress.Parse(IP_Port[0]);
            //int Port = Convert.ToInt16(IP_Port[1]);
            //Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //try
            //{
            //    clientSocket.Connect(new IPEndPoint(ip, Port));
            //    //clientSocket.Close();
            //    Thread t1 = new Thread(TestMethod)
            //    {
            //        IsBackground = true
            //    };
            //    t1.Start(clientSocket);
            //    Application.Run(new Form1());
            //}
            //catch
            //{
            //    Application.Run(new Warning1());
            //}

        }

        //private static void TestMethod(object obj) {
        //    Socket datastr = obj as Socket;
        //    //通过 clientSocket 发送数据  
        //    while (true)
        //    {
        //        try
        //        {
        //            Thread.Sleep(1000);    //等待1秒钟  
        //            string sendMessage = "Heartbeat Message：" + DateTime.Now;
        //            byte[] byteArray = System.Text.Encoding.Default.GetBytes(sendMessage);
        //            if (datastr != null) datastr.Send(byteArray);
        //        }
        //        catch
        //        {
        //            if (datastr != null)
        //            {
        //                datastr.Shutdown(SocketShutdown.Both);
        //                datastr.Close();
        //            }
        //            Application.Restart();
        //            break;
        //        }
        //    }
        //}

    }
}
