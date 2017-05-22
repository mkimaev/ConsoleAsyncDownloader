using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace ConsoleAsyncDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            //Менюшка для сравнения времени
            while (true)
            {
                
                Console.WriteLine("\nПрограмма для скачивания книги Приключения Тома Сойера\n");
                //string source = "http://www.gutenberg.org/files/74/74-0.txt"; (ссылка стала длительной, довавил новую)
                string source = "https://drive.google.com/open?id=0B0szhqOvjWyvYmhOVTBHUHpFWDA";
                MyDownloader download = new MyDownloader();

                Console.WriteLine("нажмите s - для скачивания книги в синхронном режите");
                Console.WriteLine("нажмите as - для скачивания в асинхронном режите");
                Console.WriteLine("нажмите as-v2 - для скачивания в асинхронном режите (другая версия)");
                string command = Console.ReadLine().ToLower(); //ввод
                switch (command)
                {
                    case "s": //синхронный способ
                        download.UriDownloadText(source);
                        break;
                    case "as": //асинхронный способ c исп.существующего асинхронного метода из класса Webclient
                        download.UriDownloadTextAsync(source);
                        break;
                    case "as-v2": //асинхронный способ v2
                        download.UriDownloadTextAsyncV2(source);
                        break;
                }
                Console.WriteLine("основной поток №({0}) освободился ", Thread.CurrentThread.ManagedThreadId);
            }

        }
    }
}
