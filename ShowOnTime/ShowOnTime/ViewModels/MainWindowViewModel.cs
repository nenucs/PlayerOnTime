using Caliburn.Micro;
using ShowOnTime.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
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

        private Uri m_currentUri = new Uri(@"C:\Users\YaZhou\Desktop\01.mp4");
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
            lstVedioPath.Add(new VideoPath(@"C:\Users\YaZhou\Desktop\01.mp4"));
            lstVedioPath.Add(new VideoPath(@"C:\Users\YaZhou\Desktop\01.mp4"));
            lstVedioPath.Add(new VideoPath(@"C:\Users\YaZhou\Desktop\01.mp4"));
            lstVedioPath.Add(new VideoPath(@"C:\Users\YaZhou\Desktop\01.mp4"));
            lstVedioPath.Add(new VideoPath(@"C:\Users\YaZhou\Desktop\01.mp4"));
            lstVedioPath.Add(new VideoPath(@"C:\Users\YaZhou\Desktop\01.mp4"));
            lstVedioPath.Add(new VideoPath(@"C:\Users\YaZhou\Desktop\01.mp4"));
            lstVedioPath.Add(new VideoPath(@"C:\Users\YaZhou\Desktop\01.mp4"));
            lstVedioPath.Add(new VideoPath(@"C:\Users\YaZhou\Desktop\01.mp4"));
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

        public void VideoPlayEnd()
        {
            lstVedioPath.ElementAt(1).isPlaying = Visibility.Visible;
            m_currentUri = lstVedioPath.ElementAt(1).GetUri();
            this.Refresh();
        }
    }
}
