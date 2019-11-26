using Caliburn.Micro;
using FFMpegSharp;
using ShowOnTime.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ShowOnTime.ViewModels
{
    [Export(typeof(MainWindowViewModel))]
    public class MainWindowViewModel:Screen
    {
        private MediaState m_LoadedBehavior = MediaState.Manual;

        public MediaState LoadedBehavior
        {
            get { return m_LoadedBehavior; }
            set
            {
                m_LoadedBehavior = value;
                this.NotifyOfPropertyChange(()=> LoadedBehavior);
            }
        }

        private static string testUri = @"E:\vs_proj_2019\PlayerOnTime\ShowOnTime\ShowOnTime\TestFile\01.mp4";

        private Uri m_currentUri = new Uri(testUri);
        public Uri currentUri
        {
            get { return m_currentUri; }
            set
            {
                value = m_currentUri;
                this.NotifyOfPropertyChange(()=>currentUri);
            }
        }

        public ObservableCollection<VideoPath> lstVedioPath { get; set; }

        [ImportingConstructor]
        public MainWindowViewModel()
        {
            lstVedioPath = new ObservableCollection<VideoPath>();
            lstVedioPath.Add(new VideoPath(testUri));
            lstVedioPath.Add(new VideoPath(testUri));
            lstVedioPath.Add(new VideoPath(testUri));
            lstVedioPath.Add(new VideoPath(testUri));
            lstVedioPath.Add(new VideoPath(testUri));
            lstVedioPath.Add(new VideoPath(testUri));
            lstVedioPath.Add(new VideoPath(testUri));
            lstVedioPath.Add(new VideoPath(testUri));
            lstVedioPath.Add(new VideoPath(testUri));
        }

        public void Start()
        {
            LoadedBehavior = MediaState.Play;
        }

        public void PlayerStateChange()
        {
            if (LoadedBehavior == MediaState.Play || LoadedBehavior == MediaState.Manual)
            {
                LoadedBehavior = MediaState.Pause;
                return;
            }
            if (LoadedBehavior == MediaState.Pause || LoadedBehavior == MediaState.Manual)
            {
                LoadedBehavior = MediaState.Play;
                return;
            }
        }

        public void ClickOnPlayer()
        {
            
        }

        public void NewVideoLoad()
        {
            //var videoInfo = new VideoInfo(@"E:\vs_proj_2019\PlayerOnTime\ShowOnTime\ShowOnTime\TestFile\01.mp4");
            Process p = new Process();
            p.StartInfo.FileName = @"E:\vs_proj_2019\PlayerOnTime\ShowOnTime\ShowOnTime\FFMPEG\bin\x64\ffmpeg.exe";
            //string strArg = "-stats -i " + currentUri.LocalPath + " ";
            string strArg = "-i " + currentUri.LocalPath + " -codec copy -bsf: h264_mp4toannexb -f h264 20130312_133314.264";

            p.StartInfo.Arguments = strArg;

            p.StartInfo.UseShellExecute = false;//不使用操作系统外壳程序启动线程(一定为FALSE,详细的请看MSDN)
            p.StartInfo.RedirectStandardError = true;//把外部程序错误输出写到StandardError流中(这个一定要注意,FFMPEG的所有输出信息,都为错误输出流,用StandardOutput是捕获不到任何消息的...这是我耗费了2个多月得出来的经验...mencoder就是用standardOutput来捕获的)
            p.StartInfo.CreateNoWindow = false;//不创建进程窗口
            p.ErrorDataReceived += new DataReceivedEventHandler(Output);//外部程序(这里是FFMPEG)输出流时候产生的事件,这里是把流的处理过程转移到下面的方法中,详细请查阅MSDN
            p.Start();//启动线程
            p.BeginErrorReadLine();//开始异步读取
            p.WaitForExit();//阻塞等待进程结束
            p.Close();//关闭进程
            p.Dispose();//释放资源
        }

        private void Output(object sendProcess, DataReceivedEventArgs output)
        {
            if (!String.IsNullOrEmpty(output.Data))
            {
                //处理方法...
                Console.WriteLine(output.Data);

                FileStream fs = new FileStream(@"E:\vs_proj_2019\PlayerOnTime\ShowOnTime\ShowOnTime\TestFile\filedata.txt", FileMode.Open);

                StreamReader sr = new StreamReader(fs);
                
                StreamWriter sw = new StreamWriter(fs);             //开始写入
                sw.WriteLine(output.Data);//写入
                             //清空缓冲区         
                sw.Flush();
                //关闭流             
                sw.Close();

                sr.Close();
                fs.Close();

            }
        }

                public void VideoPlayEnd()
        {
            lstVedioPath.ElementAt(1).isPlaying = Visibility.Visible;
            m_currentUri = lstVedioPath.ElementAt(1).GetUri();
            this.Refresh();
        }
    }
}
