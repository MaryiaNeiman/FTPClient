using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace FtpClient
{
    public class Client
    {
        private string password;
        private string userName;
        private string uri;
        private int bufferSize = 1024;

        public bool Passive = true;
        public bool Binary = true;
        public bool EnableSsl = false;
        public bool Hash = false;

        public string URI
        {
            get { return uri; }
        }

        public Client(string uri, string userName, string password)
        {
            this.uri = uri;
            this.userName = userName;
            this.password = password;
        }



        public string DownloadFile(string source, string dest)
        {
            var request = createRequest(combine(uri, source), WebRequestMethods.Ftp.DownloadFile);

            byte[] buffer = new byte[bufferSize];
            try
            {
                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var fs = new FileStream(dest, FileMode.OpenOrCreate))
                        {
                            int readCount = stream.Read(buffer, 0, bufferSize);

                            while (readCount > 0)
                            {
                                if (Hash)
                                    Console.Write("#");

                                fs.Write(buffer, 0, readCount);
                                readCount = stream.Read(buffer, 0, bufferSize);
                            }
                        }
                    }

                    return response.StatusDescription;
                }

            }

            catch (System.Net.WebException)
            {
                Console.WriteLine("It's not file!");
                Console.ReadKey();
                return "";
            }

        }



        public string[] ListDirectoryDetails()
        {
            var list = new List<string>();

            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(this.uri);
            request.Credentials = new NetworkCredential(this.userName, this.password);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            using (var response = (FtpWebResponse)request.GetResponse())
            {

                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream, true))
                    {
                        while (!reader.EndOfStream)
                        {
                            list.Add(reader.ReadLine());

                        }
                    }
                }
            }

            return list.ToArray();
        }




        private FtpWebRequest createRequest(string uri, string method)
        {
            var r = (FtpWebRequest)WebRequest.Create(uri);

            r.Credentials = new NetworkCredential(userName, password);
            r.Method = method;
            r.UseBinary = Binary;
            r.EnableSsl = EnableSsl;
            r.UsePassive = Passive;

            return r;
        }



        private string combine(string path1, string path2)
        {
            return Path.Combine(path1, path2).Replace("\\", "/");
        }
    }
}
