using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace LCApp {
    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Form1());
            ////设定服务器IP地址
            //IPAddress ip = IPAddress.Parse("127.0.0.1");
            //Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //try
            //{
            //    clientSocket.Connect(new IPEndPoint(ip, 8081));
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

        //    private static void TestMethod(object obj) {
        //        Socket datastr = obj as Socket;
        //        //通过 clientSocket 发送数据  
        //        while (true)
        //        {
        //            try
        //            {
        //                Thread.Sleep(1000);    //等待1秒钟  
        //                string sendMessage = "Heartbeat Message：" + DateTime.Now;
        //                byte[] byteArray = System.Text.Encoding.Default.GetBytes(sendMessage);
        //                if (datastr != null) datastr.Send(byteArray);
        //            }
        //            catch
        //            {
        //                if (datastr != null)
        //                {
        //                    datastr.Shutdown(SocketShutdown.Both);
        //                    datastr.Close();
        //                }
        //                Application.Restart();
        //                break;
        //            }
        //        }
        //    }
    }
}
