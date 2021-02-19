using System;
using System.Threading;

namespace ConcurrentProgramming
{
    class FivePhilosophers
    {
        public FivePhilosophers(bool first)
        {

        }

        private Semaphore[] forks;
        private Semaphore waiter;  
        private Thread[] philosophers; 

        public void Start()
        {
            FivePhilosophers pf = new FivePhilosophers();
            pf.Begin();
            Thread.Sleep(10000);
            pf.Finish();
        }

        public FivePhilosophers()
        {
            forks = new Semaphore[5];
            for (int i = 0; i < 5; i++)
            {
                forks[i] = new Semaphore(1, 1);
            }
            waiter = new Semaphore(4, 4);

            philosophers = new Thread[5];
            for (int i = 0; i < 5; i++)
            {
                philosophers[i] = new Thread(new ThreadStart(Philosopher));
                philosophers[i].Name = "Filozof " + i;
            }

        }

        public void Begin()
        {
            foreach (Thread t in philosophers)
            {
                t.Start();
            }
        }

        public void Finish()
        {
            foreach (Thread t in philosophers)
            {
                t.Interrupt();
            }
        }

        public void Philosopher()
        {
            try
            {
                int numer = Int32.Parse(Thread.CurrentThread.Name.Split(' ')[1]);
                Random rand = new Random();
                while (true)
                {
                    Console.WriteLine("{0} myśli", Thread.CurrentThread.Name);
                    Thread.Sleep(rand.Next(100, 500));

                    waiter.WaitOne();
                    forks[numer].WaitOne();
                    forks[(numer + 1) % 5].WaitOne();
                    Console.WriteLine("{0} je", Thread.CurrentThread.Name);
                    Thread.Sleep(rand.Next(100, 300));
                    forks[numer].Release();
                    forks[(numer + 1) % 5].Release();
                    waiter.Release();
                }
            }
            catch (ThreadInterruptedException)
            { }
        }
    }
}
