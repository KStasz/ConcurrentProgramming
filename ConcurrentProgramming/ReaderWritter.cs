using System;
using System.Threading;

namespace ConcurrentProgramming
{
    class ReaderWritter
    {
        int initial = 0;
        ReaderWriterLockSlim rwl = new ReaderWriterLockSlim();
        public void myRead(object threadName)
        {
            rwl.EnterReadLock();
            Console.WriteLine("Read start: Thread: " + threadName + " " + initial);
            if (threadName.ToString() == "Thread 1")
                Thread.Sleep(10);
            else
                Thread.Sleep(250);
            Console.WriteLine("Read end  : Thread: " + threadName + " " + initial);
            rwl.ExitReadLock();
        }
        public void myWrite()
        {
            rwl.EnterWriteLock();
            Console.WriteLine("\nWriter start: " + initial);
            initial++;
            Console.WriteLine("Writer End: " + initial);
            rwl.ExitWriteLock();
            Console.WriteLine();
        }
        public void Start()
        {
            for (int i = 0; i < 5; i++)
            {
                //Reader Thread
                Thread t1 = new Thread(myRead);
                //Writer Thread
                Thread t2 = new Thread(myWrite);
                //Reader Again
                Thread t3 = new Thread(myRead);
                //Start all threads
                t1.Start("Thread 1");
                t2.Start();
                t3.Start("Thread 3");
                //Wait for them to finish execution
                t1.Join();
                t2.Join();
                t3.Join();
            }
            Console.Read();
        }
    }
}
