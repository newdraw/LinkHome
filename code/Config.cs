using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkHome
{
    [Serializable]
    public class Config
    {
        public int TTL = 600;
        public string AccessKeyID;
        public string AccessKeySecret;

        public int Interval = 10;
        public bool HideForm = false;
    }
}
