using System;
using System.Threading;

namespace ConcurrentProgramming
{
    class FivePhilosophers
    {
        public FivePhilosophers(bool first)
        {

        }

        private Semaphore[] widelce;  //tablica binarnych semaforów widelce
        private Semaphore lokaj;  //semafor lokaj, który dopuszcza
                                  //co najwyżej 4 filozofów do stołu
        private Thread[] filozofowie; //tablica wątków filozofów

        public void Start()
        {
            FivePhilosophers pf = new FivePhilosophers();
            pf.Startuj();
            Thread.Sleep(10000); //czekamy losowy czas
            pf.Zakoncz(); //przerywamy wątki
        }

        /// Konstruktor klasy PieciuFilozofow odpowiedzialny między innymi
        /// za inicjalizację i nadanie nazw wątkom.
        /// 
        public FivePhilosophers()
        {
            widelce = new Semaphore[5];
            for (int i = 0; i < 5; i++)
            {
                widelce[i] = new Semaphore(1, 1);  //semafory binarne
            }
            lokaj = new Semaphore(4, 4);

            filozofowie = new Thread[5];
            for (int i = 0; i < 5; i++)
            {
                filozofowie[i] = new Thread(new ThreadStart(Filozof)); //tworzymy wątki
                filozofowie[i].Name = "Filozof " + i; //i nadajemy im nazwy
            }

        }

        /// Funkcja uruchamiająca wszystkie wątki.
        /// 
        public void Startuj()
        {
            foreach (Thread t in filozofowie)
            {
                t.Start();
            }
        }

        /// Funkcja przerywająca wszystkie wątki
        /// 
        public void Zakoncz()
        {
            foreach (Thread t in filozofowie)
            {
                t.Interrupt();
            }
        }

        /// Wątek filozofa, który w nieskończonej pętli myśli przez losowy czas,
        /// a następnie próbuje zdobyć dwa widelce i przez losowy czas je.
        /// 
        public void Filozof()
        {
            try
            {
                int numer = Int32.Parse(Thread.CurrentThread.Name.Split(' ')[1]);
                Random rand = new Random();
                while (true)
                {
                    Console.WriteLine("{0} myśli", Thread.CurrentThread.Name);
                    Thread.Sleep(rand.Next(100, 500)); //losowy czas myślenia

                    lokaj.WaitOne(); //sprawdzamy, czy możemy usiąść do stołu
                    widelce[numer].WaitOne(); //bierzemy lewy widelec
                    widelce[(numer + 1) % 5].WaitOne(); //bierzemy prawy widelec
                    Console.WriteLine("{0} je", Thread.CurrentThread.Name);
                    Thread.Sleep(rand.Next(100, 300)); //losowy czas jedzenia
                    widelce[numer].Release(); //odkładamy lewy widelec
                    widelce[(numer + 1) % 5].Release(); //odkładamy prawy widelec
                    lokaj.Release(); //dopuszczamy kolejnego filozofa do stołu                    
                }
            }
            catch (ThreadInterruptedException)
            { }
        }
    }
}
