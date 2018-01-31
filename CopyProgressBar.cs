using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace LCApp {
    class CopyProgressBar
    {
        public  string Sourcefile;
        public string Targetfile;
        public ProgressBar progressBar = new ProgressBar();

        private Thread thdCopyFileThread;//创建一个线程
        private string str = "";//用来记录源文件的名字
        FileStream FormerOpenFileStream;//实例化源文件FileStream类
        FileStream ToFileOpenFileStream;//实例化目标文件FileStream类

        #region //复制文件函数

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="FormerFile">源文件路径</param>
        /// <param name="ToFile">目的文件路径</param>
        /// <param name="TranSize">传输大小</param>
        /// <param name="progressBar">ProgressBar控件</param>
        public void CopyFile(string FormerFile, string ToFile, int TranSize, ProgressBar progressBar) {

            progressBar.Value = 0;//设置进度条的当前位置为0
            progressBar.Minimum = 0;//设置进度条的最小值为0

            try
            {
                FormerOpenFileStream = new FileStream(FormerFile, FileMode.Open, FileAccess.Read);//以只读方式打开源文件
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return;
            }

            try
            {
                FileStream fileToCreate = new FileStream(ToFile, FileMode.Create);//创建目的文件，如果已存在将被覆盖
                fileToCreate.Close();//关闭所有fileToCreate的资源
                fileToCreate.Dispose();//释放所有fileToCreate的资源
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return;
            }

            ToFileOpenFileStream = new FileStream(ToFile, FileMode.Append, FileAccess.Write);//以写方式打开目的文件

            int max = Convert.ToInt32(Math.Ceiling((double)FormerOpenFileStream.Length / (double)TranSize));//根据一次传输的大小，计算最大传输个数. Math.Ceiling 方法 (Double),返回大于或等于指定的双精度浮点数的最小整数值

            progressBar.Maximum = max;//设置进度条的最大值
            int FileSize;//每次要拷贝的文件的大小

            if (TranSize < FormerOpenFileStream.Length)//如果分段拷贝，即每次拷贝内容小于文件总长度
            {
                byte[] buffer = new byte[TranSize];//根据传输的大小，定义一个字节数组，用来存储传输的字节
                int copied = 0;//记录传输的大小
                int tem_n = 1;//设置进度栏中进度的增加个数

                while (copied <= ((int)FormerOpenFileStream.Length - TranSize))
                {
                    FileSize = FormerOpenFileStream.Read(buffer, 0, TranSize); //从0开始读到buffer字节数组中，每次最大读TranSize
                    ToFileOpenFileStream.Write(buffer, 0, TranSize); //向目的文件写入字节
                    ToFileOpenFileStream.Flush(); //清空缓存,写入到文件中
                    ToFileOpenFileStream.Position = FormerOpenFileStream.Position; //是源文件的目的文件流的位置相同
                    copied += FileSize; //记录已经拷贝的大小
                    progressBar.Value = progressBar.Value + tem_n; //增加进度栏的进度块
                }

                int leftSize = (int)FormerOpenFileStream.Length - copied;//获取剩余文件的大小
                FileSize = FormerOpenFileStream.Read(buffer, 0, leftSize);//读取剩余的字节
                FormerOpenFileStream.Flush();//清空缓存,写入到文件中
                ToFileOpenFileStream.Write(buffer, 0, leftSize);//写入剩余的部分
                ToFileOpenFileStream.Flush();//清空缓存,写入到文件中
            }
            else//如果整体拷贝，即每次拷贝内容大于文件总长度
            {
                byte[] buffer = new byte[FormerOpenFileStream.Length];
                FormerOpenFileStream.Read(buffer, 0, (int)FormerOpenFileStream.Length);
                FormerOpenFileStream.Flush();
                ToFileOpenFileStream.Write(buffer, 0, (int)FormerOpenFileStream.Length);
                ToFileOpenFileStream.Flush();
            }

            FormerOpenFileStream.Close();
            ToFileOpenFileStream.Close();

            progressBar.Value = 0;
            str = "";
        }
        #endregion
        
        public delegate void CopyFile_Delegate();//定义委托/托管线程
        /// <summary>
        /// 在线程上执行委托（设置托管线程函数）
        /// </summary>
        public void SetCopyFile() {
            //this.Invoke(new CopyFile_Delegate(RunCopyFile));//对指定的线程进行托管
            //下面两行代码等同上面一行代码
            //CopyFile_Delegate copyFile_Delegate = new CopyFile_Delegate(RunCopyFile);//创建delegate对象
            //this.Invoke(copyFile_Delegate); //调用delegate
        }

        public void RunCopy()
        {
            thdCopyFileThread = new Thread(start: new ThreadStart(RunCopyFile));
            thdCopyFileThread.Start();
        }

        /// <summary>
        /// 设置线程，运行copy文件，它与代理CopyFile_Delegate应具有相同的参数和返回类型
        /// </summary>
        public void RunCopyFile() {
            CopyFile(Sourcefile, Targetfile, 1024, progressBar);//复制文件
            Thread.Sleep(0);//避免假死 
            thdCopyFileThread.Abort();//关闭线程
        }
    }
}
