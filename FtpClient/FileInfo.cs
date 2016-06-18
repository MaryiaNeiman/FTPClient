using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpClient
{
    class FileInfo
    {
        string name;
        public string adress;


        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public FileInfo() { }

        public FileInfo(string name, string adress)
        {
            Name = name;
            this.adress = adress;
        }
    }
}
