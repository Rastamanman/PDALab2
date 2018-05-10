using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace PDALab1
{
    class Program
    {
        public static int qSize = 20;
        public static int PTnr = 10;
        public static int CTnr = 10;
        public static List<Produs> lista = new List<Produs>();
        public static Mutex mtx = new Mutex();
        public static Semaphore sem = new Semaphore(1, 10);
        public static Semaphore semf = new Semaphore(1, 10);

        public static void Produce()
        {
            Produs prod = new Produs("obiect");
            sem.WaitOne();
            mtx.WaitOne();

            lista.Add(prod);
            Console.WriteLine("Produs creat, nr total de produse: " + lista.Count);

            mtx.ReleaseMutex();
            sem.Release();
        }

        public static void Consuma()
        {
            Produs prod = new Produs();
            sem.WaitOne();
            mtx.WaitOne();

            prod = lista[0];
            lista.RemoveAt(0);

            mtx.ReleaseMutex();
            sem.Release();
            if(prod.nume != "nimic")
            {
                Console.WriteLine("Produs consumat, nr total de produse ramase: " + lista.Count);
            }
        }

        static void Main(string[] args)
        {

            Thread[] producatori = new Thread[PTnr];
            Thread[] consumatori = new Thread[CTnr];

            for(int i = 0; i < PTnr; i++)
            {
                producatori[i] = new Thread(Produce);
                producatori[i].Start();
            }
            for (int i = 0; i < CTnr; i++)
            {
                consumatori[i] = new Thread(Consuma);
                consumatori[i].Start();
            }

            for(int i = 0; i < PTnr; i++)
            {
                producatori[i].Join();
            }
            for(int i = 0; i < CTnr; i++)
            {
                consumatori[i].Join();
            }

            Console.ReadLine();
        }
    }
}
