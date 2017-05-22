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
    class MyDownloader
    {
        bool isAsync = false; //флаг, чтобы покрасить метод UriDownloadTextAsyncV2
        Stopwatch sw = new Stopwatch(); //счётчики для замера времени.
        Stopwatch sw2 = new Stopwatch();
        /// <summary>
        /// синхронный метод скачивания
        /// </summary>
        public void UriDownloadText(object url)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (isAsync)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
            }
            sw.Start();
            string adress = (string)url;
            string data = null;
            WebClient wc = new WebClient();
            Console.WriteLine("Скачивание в потоке ({0})", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("|dowloading...|");
            data = wc.DownloadString(adress); // метод загрузки
            Console.WriteLine("Скачивание было в потоке ({0})", Thread.CurrentThread.ManagedThreadId);
            using (StreamWriter str = new StreamWriter("Tom_Soyer.txt")) //пишем в файл, на случай если вздумается прочитать Тома Сойера
            {
                str.Write(data);
                Console.WriteLine("записано в Tom_Soyer.txt");
            }
            sw.Stop();
            Console.WriteLine("\nОперация UriDownloadText завершена! Длительность: {0} мс\n", sw.Elapsed);
            Console.WriteLine("Книга находится в папке проекта в Debug\n");
            Console.ResetColor();
        }

        /// <summary>
        /// асинхронный метод скачивания с исп. метода WebClient.DownloadStringTaskAsync();
        /// </summary>
        public async void UriDownloadTextAsync(string url)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            sw2.Start();
            WebClient wc2 = new WebClient();
            string data2 = null;
            Console.WriteLine("Скачивание в потоке  ({0})", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("|dowloading...|");
            data2 = await wc2.DownloadStringTaskAsync(url);
            Console.WriteLine("Скачивание было в потоке ({0})", Thread.CurrentThread.ManagedThreadId);
            using (StreamWriter str2 = new StreamWriter("Tom_SoyerAsync.txt"))
            {
                str2.Write(data2);
                Console.WriteLine("записано в Tom_SoyerAsync.txt");
            }
            sw2.Stop();
            Console.WriteLine("\nОперация UriDownloadTextAsync завершена! Длительность: {0} мс\n", sw2.Elapsed);
            Console.WriteLine("Книга находится в папке проекта в Debug\n");
            Console.ResetColor();
        }

        /// <summary>
        /// собственная реализация async/await
        /// </summary>
        public async void UriDownloadTextAsyncV2(string url)
        {
            isAsync = true;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Работа в потоке ({0})", Thread.CurrentThread.ManagedThreadId);
            await Task.Factory.StartNew(UriDownloadText, url);
            Console.WriteLine("Скачивание было в потоке ({0})", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
