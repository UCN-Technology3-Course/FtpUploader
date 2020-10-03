using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FtpUploader
{

    // you need to install a local ftp server like Filezilla, and configure the ftpguest account, to make this example work.

    class Program
    {
        static void Main(string[] args)
        {
            FtpClient client = new FtpClient("127.0.0.1");
            client.Credentials = new NetworkCredential("ftpguest", "qwerty");
            client.Connect();

            foreach (var file in client.GetListing())
            {
                Console.WriteLine(file.Name);
            }                       

            while (true)
            {
                Console.Write(">");

                switch (Console.ReadLine())
                {
                    case "x":
                    case "exit": // disconnects the ftp server and exits the program
                        client.Disconnect();
                        return;
                    case "u": // upload file to ftp server
                        Console.Write("file: ");
                        var uploadfile = Console.ReadLine();
                        client.UploadFile(uploadfile, uploadfile.Substring(uploadfile.LastIndexOf("\\")+1));
                        break;
                    case "d": // download file from ftp server
                        Console.Write("file: ");
                        var downloadfile = Console.ReadLine();
                        client.DownloadFile($"C:\\test\\{downloadfile}", downloadfile);
                        break;
                    default:
                        Console.WriteLine("Unknown command...");
                        break;
                }
            }
        }
    }
}
