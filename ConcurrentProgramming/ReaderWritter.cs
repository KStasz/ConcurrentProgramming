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
            //Accquire Reader Lock.
            rwl.EnterReadLock();
            Console.WriteLine("Read start: Thread: " + threadName + " " + initial);
            if (threadName.ToString() == "Thread 1")
                //Irregular sleeps makes more chances of
                //Multiple threads trying to access it
                //at same time
                Thread.Sleep(10);
            else
                Thread.Sleep(250);
            Console.WriteLine("Read end  : Thread: " + threadName + " " + initial);
            rwl.ExitReadLock();
            //Release Lock
        }
        public void myWrite()
        {
            rwl.EnterWriteLock();
            Console.WriteLine("\nWriter start: " + initial);
            initial++; //Writing
            Console.WriteLine("Writer End: " + initial);
            rwl.ExitWriteLock();
            Console.WriteLine();
        }
        public void Start()
        {
            ReaderWritter p = new ReaderWritter();
            for (int i = 0; i < 5; i++)
            {
                //Reader Thread
                Thread t1 = new Thread(p.myRead);
                //Writer Thread
                Thread t2 = new Thread(p.myWrite);
                //Reader Again
                Thread t3 = new Thread(p.myRead);
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
