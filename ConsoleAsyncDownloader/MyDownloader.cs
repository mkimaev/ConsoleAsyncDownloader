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
        /// <summary>
        /// синхронный метод скачивания
        /// </summary>
        public string UriDownloadText(string url)
        {
            string data = null;
            WebClient wc = new WebClient();
            for (int i = 0; i < 50; i++) // рисуем точки, чтобы увидеть разницу - будет задержка при скачке
            {
                Console.Write(".");
                Thread.Sleep(50);
                if (i == 25)
                {
                    Console.Write("|dowloading...|");
                    data = wc.DownloadString(url); // метод загрузки
                    continue;
                }
            }
            return data;

        }

        /// <summary>
        /// асинхронный метод скачивания
        /// </summary>
        public async Task<string> UriDownloadTextAsync(string url)
        {
            WebClient wc2 = new WebClient();
            Task<string> taskAsync = null;
            for (int i = 0; i < 50; i++)
            {
                Console.Write("."); // рисуем точки, чтобы увидеть разницу - задержки не будет
                Thread.Sleep(50);
                if (i == 25)
                {
                    Console.Write("|dowloading...|(без задержки)");
                    taskAsync = wc2.DownloadStringTaskAsync(url); // асинхронный метод загрузки
                    continue;
                }
            }
            return await taskAsync; ;
        }
    }
}
