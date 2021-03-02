using System;

namespace ExampleLib
{
    public class Counter
    {
        private int n;
        public void Inc()
        {
            if(n == 2) { n++; }
            n++;
        }

        public int Get() => n;

        public void Dec()
        {
            n--;
        }
    }
}