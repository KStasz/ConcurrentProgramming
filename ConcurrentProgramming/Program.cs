using System;
using System.Threading.Tasks;

namespace ConcurrentProgramming
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("R - ReaderWriter");
            Console.WriteLine("F - FivePhilosophers");
            Console.WriteLine("P - Promise");
            switch (Console.ReadLine().ToLower())
            {
                case "r":
                    ReaderWritter r = new ReaderWritter();
                    r.Start();
                    break;
                case "f":
                    FivePhilosophers f = new FivePhilosophers();
                    f.Start();
                    break;
                case "p":
                    Promise promise = new Promise();
                    await promise.Start();
                    break;
                default:
                    Console.WriteLine("Command not recognized");
                    break;
            }
            Console.WriteLine("End? y/n");
            if (Console.ReadLine() == "n")
            {
                await Main(null);
            }
        }
    }
}
