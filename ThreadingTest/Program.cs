using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ThreadingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Tuple<string, int, int> tuple1 = new Tuple<string, int, int>("blah",100,123);
            Tuple<string, int, int> tuple2 = new Tuple<string, int, int>("meh", 50, 666);
            Tuple<string, int, int> tuple3 = new Tuple<string, int, int>("trololo", 20, 777);

            List<Tuple<string, int, int>> tuples = new List<Tuple<string, int, int>>();
            tuples.Add(tuple1);
            tuples.Add(tuple2);
            tuples.Add(tuple3);

            foreach (var tuple in tuples)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                worker.RunWorkerAsync(tuple);
            }

            Console.ReadLine();
        }

        private static void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Tuple<string, int, int> tuple = e.Argument as Tuple<string, int, int>;

            DoingStuff(tuple.Item1, tuple.Item2, tuple.Item3);
        }

        static void DoingStuff(string processName, int steps, int processId)
        {
            for (int i=1; i<=steps; i++)
            {
                if (i == 1) { Console.WriteLine("Beginning process with ID: "+ processId);}
                else if (i == steps)
                {
                    Console.WriteLine("Finishing process with ID: " + processId);
                }
                else
                {
                    Console.WriteLine("Process " + processId + ": " + Math.Round((double)i/steps,2)+"%");
                }
                System.Threading.Thread.Sleep(500);
            }
        }
    }
}
