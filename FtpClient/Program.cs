using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace FtpClient
{
    class Program
    {
        static string directoryForDownload = "D://"; // место куда скачивать
        static string ftpAddress = "ftp://ftp.man.lodz.pl/"; //свой ftp-адресс 
        static string currentAddress = "", prevAddress = "";
        
        static void Main(string[] args)
        {
            Program program = new Program();
            String key;
            prevAddress = currentAddress = ftpAddress;
            Client client;


            while (true)
            {
                Console.Clear();

                client = new Client(currentAddress, "", "");

                List<FileInfo> list = WorkFunction.GetFilesAndDirectories(client);
                Console.WriteLine(client.URI);
                WorkFunction.WriteFiles(list);

                program.printmenu();
                key = Console.ReadLine();

                if (key.Equals("change"))
                {
                    OnChange(client);
                }
                else if (key.Equals("download"))
                {
                    OnDownload(client);
                }
                else if (key.Equals("exit"))
                {
                    break;
                }
            }
        }

        private static void OnDownload(Client client)
        {
            Console.WriteLine("\nEnter file name:");
            String name = Console.ReadLine();
            client.DownloadFile(name, directoryForDownload + name);
        }

        private static void OnChange(Client client)
        {
            Console.WriteLine("\nEnter directory name:");
            String name = Console.ReadLine();
            if (name.Equals("..."))
            {
                currentAddress = prevAddress;
            }
            else
            {
                prevAddress = client.URI;
                currentAddress = client.URI + name + "/";
            }
        }

        public void printmenu()
        {
            Console.WriteLine("Enter 'change' to change directory");
            Console.WriteLine("Enter 'download' to download file");
            Console.WriteLine("Enter 'exit' to exit");
        }

        
    }

   

    
}
