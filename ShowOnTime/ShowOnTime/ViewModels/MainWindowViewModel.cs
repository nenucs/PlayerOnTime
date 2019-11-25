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
        private Uri m_currentUri = new Uri(@"C:\Users\temp\Desktop\01.mp4");
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
            lstVedioPath.Add(new VideoPath(@"C:\Users\temp\Desktop\01.mp4"));
            lstVedioPath.Add(new VideoPath(@"C:\Users\temp\Desktop\01.mp4"));
            lstVedioPath.Add(new VideoPath(@"C:\Users\temp\Desktop\01.mp4"));
            lstVedioPath.Add(new VideoPath(@"C:\Users\temp\Desktop\01.mp4"));
            lstVedioPath.Add(new VideoPath(@"C:\Users\temp\Desktop\01.mp4"));
            lstVedioPath.Add(new VideoPath(@"C:\Users\temp\Desktop\01.mp4"));
            lstVedioPath.Add(new VideoPath(@"C:\Users\temp\Desktop\01.mp4"));
            lstVedioPath.Add(new VideoPath(@"C:\Users\temp\Desktop\01.mp4"));
            lstVedioPath.Add(new VideoPath(@"C:\Users\temp\Desktop\01.mp4"));
        }

        public void ClickOnPlayer(object obj)
        {
            var item = obj as FrameworkElement;
        }

    }
}
