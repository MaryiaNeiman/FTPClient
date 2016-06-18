using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpClient
{
    class WorkFunction
    {
        public static void WriteFiles(List<FileInfo> list)
        {
            foreach (FileInfo fdi in list)
            {
                Console.WriteLine(fdi.Name);
            }
            Console.WriteLine();
        }

        public static List<FileInfo> GetFilesAndDirectories(Client client)
        {
            try
            {
                List<FileInfo> list = new List<FileInfo>();

                foreach (string s in client.ListDirectoryDetails())


                    list.Add(new FileInfo(s, client.URI));

                list.Insert(0, new FileInfo("...", client.URI));

                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + ": \n" + ex.Message);
            }
            return new List<FileInfo>();
        }
    }
}
