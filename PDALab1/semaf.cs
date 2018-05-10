using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PDALab1
{
    class Semaf
    {
        public Mutex mtx = new Mutex();
        public int maxNr;
        public int iterator;

        public Semaf(int nrT = 1) //mutex by default
        {
            maxNr = nrT;
            iterator = maxNr;
        }

        public void Incr()
        {
            mtx.WaitOne();
            if(iterator < maxNr)
            {
                iterator++;
            }
        }
    }
}
