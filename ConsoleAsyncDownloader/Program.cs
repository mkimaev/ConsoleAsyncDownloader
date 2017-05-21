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
            while (true)
            {
                Console.WriteLine("Программа для скачивания книги Приключения Тома Сойера\n");
                string source = "http://www.gutenberg.org/files/74/74-0.txt";
                MyDownloader download = new MyDownloader();
                Stopwatch sw = new Stopwatch();
                Stopwatch sw2 = new Stopwatch();
                Console.WriteLine("нажмите s - для скачивания книги в синхронном режите нажмите");
                Console.WriteLine("нажмите as - для скачивания в асинхронном режите нажмите");
                string command = Console.ReadLine().ToLower();
                switch (command)
                {
                    case "s":
                        Console.ForegroundColor = ConsoleColor.Green;
                        sw.Start();
                        string book = download.UriDownloadText(source);
                        using (StreamWriter str = new StreamWriter("Tom_Soyer.txt"))
                        {
                            str.Write(book);
                            //str.Close();
                            Console.WriteLine("записано в Tom_Soyer.txt");
                        }
                        sw.Stop();
                        Console.WriteLine("Операция UriDownloadText завершена! Длительность: {0} мс\n", sw.Elapsed);
                        Console.ResetColor();
                        break;
                    case "as":
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        sw2.Start();
                        Task<string> anytask = download.UriDownloadTextAsync(source);
                        string bookAsync = anytask.Result;
                        using (StreamWriter str2 = new StreamWriter("Tom_SoyerAsync.txt"))
                        {
                            str2.Write(bookAsync);
                            Console.WriteLine("записано в Tom_SoyerAsync.txt");
                        }
                        sw2.Stop();
                        Console.WriteLine("Операция UriDownloadTextAsync завершена! Длительность: {0} мс\n", sw2.Elapsed);
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}
