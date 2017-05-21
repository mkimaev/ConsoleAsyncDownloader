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
            //Менюшка для сравления времени
            while (true)
            {
                Console.WriteLine("Программа для скачивания книги Приключения Тома Сойера\n");
                string source = "http://www.gutenberg.org/files/74/74-0.txt";
                MyDownloader download = new MyDownloader();
                Stopwatch sw = new Stopwatch(); //счётчики для замера времени.
                Stopwatch sw2 = new Stopwatch();
                Console.WriteLine("нажмите s - для скачивания книги в синхронном режите нажмите");
                Console.WriteLine("нажмите as - для скачивания в асинхронном режите нажмите");
                string command = Console.ReadLine().ToLower(); //ввод
                switch (command)
                {
                    case "s": //синхронный способ
                        Console.ForegroundColor = ConsoleColor.Green;
                        sw.Start();
                        string book = download.UriDownloadText(source); //качаем
                        using (StreamWriter str = new StreamWriter("Tom_Soyer.txt")) //пишем в файл, на случай если вздумается прочитать Тома Сойера
                        {
                            str.Write(book);
                            //str.Close();
                            Console.WriteLine("записано в Tom_Soyer.txt");
                        }
                        sw.Stop();
                        Console.WriteLine("Операция UriDownloadText завершена! Длительность: {0} мс\n", sw.Elapsed);
                        Console.WriteLine("Книга находится в папке проекта в Debug\n");
                        Console.ResetColor();
                        break;
                    case "as": //асинхронный способ
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
                        Console.WriteLine("Книга находится в папке проекта в Debug\n");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}
