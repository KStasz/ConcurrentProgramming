using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ConcurrentProgramming
{
    class Promise
    {
        public static List<string> fileList { get; set; } = new List<string>() { "Lorem_ipsum.txt", "text2.txt", "text3.txt" };
        public static List<List<string>> readerResult { get; set; }
        public static List<Task<List<string>>> Tasks { get; set; } = new List<Task<List<string>>>();

        public async Task Start()
        {
            readerResult = new List<List<string>>();
            foreach (var item in fileList)
            {
                Tasks.Add(Task.Run(() => ReadFile(item)));
            }

            var result = await Task.WhenAll(Tasks);

            foreach (var item in result)
            {
                readerResult.Add(item);
            }

            int counter = 1;

            foreach (var file in readerResult)
            {
                foreach (var item in file)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("");
                Console.WriteLine($"**End of File: {counter}**");
                Console.WriteLine("");
                counter++;
            }
            Console.ReadKey();
        }

        private List<string> ReadFile(string path)
        {
            List<string> locallist = new List<string>();
            using (StreamReader reader = new StreamReader(path))
            {
                string line = String.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    locallist.Add(line);
                }
            }
            return locallist;
        }
    }
}
