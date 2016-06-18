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

                program.menu();
                key = Console.ReadLine();

                if (key.Equals("change"))
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
                else if (key.Equals("download"))
                {
                    Console.WriteLine("\nEnter file name:");
                    String name = Console.ReadLine();
                    client.DownloadFile(name, directoryForDownload + name);
                }
                else if (key.Equals("exit"))
                {
                    break;
                }
            }
        }

        public void menu()
        {
            Console.WriteLine("Enter 'change' to change directory");
            Console.WriteLine("Enter 'download' to download file");
            Console.WriteLine("Enter 'exit' to exit");
        }

        
    }

   

    
}
