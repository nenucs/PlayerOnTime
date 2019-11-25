using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShowOnTime.DataModel
{
    public class VideoPath
    {
        public string strPathName { get; set; }

        public Visibility isPlaying { get; set; }

        public Visibility isPlaed { get; set; }

        public string strShortPathName { get; set; }

        public VideoPath(string strPath)
        {
            strPathName = strPath;
            strShortPathName = strPath.Split('\\').LastOrDefault();
        }

        public Uri GetUri()
        {
            return new Uri(this.strPathName);
        }
    }
}
