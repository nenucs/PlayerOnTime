using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowOnTime.DataModel
{
    public class VideoPath
    {
        public string strPathName { get; set; }

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
