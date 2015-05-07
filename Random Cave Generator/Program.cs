using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace ConsoleApplication7
{
    class Program
    {
        static void Main(string[] args)
        {
            createRndCave(); 
        }

        public static void createRndCave()
        {
            bool works = false;
            int[,] rmIndxs = new int[,] 
                { { -6, -5, 1, 6, -1, -7 },
                  { -6, 1, 7, 6, 5, -1 },
                  { -6, -5, 1, 6, 5, -1 } };
            List<int> rms = new List<int> { 0, 1, 2, 3, 4, 5 };
            int counter = 0;
            while (!works)
            {
                Random rnd = new Random();
                int[,] doors = new int[30, 6];
                for (int a = 0; a < 30; a++)
                {
                    int drschanges = rnd.Next(3);
                    for (int b = 0; b < drschanges + 1; b++)
                    {
                        int drchanged = rnd.Next(6);
                        for (int i = 0; i < 6; i++) { counter += doors[a, i]; }
                        if (counter < 3) doors[a, drchanged] = 1;
                        counter = 0;
                    }
                    List<int> selectedDoors = rms.Where(r => doors[a, r] == 1).ToList();
                    int odevn;
                    if ((a + 1) % 2 == 1 && (a + 1) % 6 != 1) odevn = 0;
                    else if ((a + 1) % 2 == 0 && (a + 1) % 6 != 0) odevn = 1;
                    else odevn = 2;
                    foreach (int r in selectedDoors)
                    {
                        for (int i = 0; i < 6; i++) { counter += doors[(a + rmIndxs[odevn, r] + 30) % 30, i]; }
                        if (counter < 3) doors[((a + rmIndxs[odevn, r] + 30) % 30), (r + 3) % 6] = doors[a, r];
                        else if (doors[((a + rmIndxs[odevn, r] + 30) % 30), (r + 3) % 6] == 1) doors[a, r] = 1;
                        else
                        {
                            doors[a, r] = 0;
                            doors[((a + rmIndxs[odevn, r] + 30) % 30), (r + 3) % 6] = 0;
                        }
                        counter = 0;
                    }
                }

                int[] beentouched = new int[30];
                beentouched[0] = 1;
                int previous = 0;
                bool checking = true;
                while (checking)
                {
                    for (int a = 0; a < 30; a++)
                    {
                        
                        if (beentouched[a] == 1)
                        {
                            List<int> selectedDoors = rms.Where(r => doors[a, r] == 1).ToList();
                            int odevn;
                            if ((a + 1) % 2 == 1 && (a + 1) % 6 != 1) odevn = 0;
                            else if ((a + 1) % 2 == 0 && (a + 1) % 6 != 0) odevn = 1;
                            else odevn = 2;
                            foreach (int r in selectedDoors)
                            {
                                beentouched[(a + rmIndxs[odevn, r] + 30) % 30] = 1;
                            }
                        }
                    }
                    if (previous == beentouched.Sum()) checking = false;
                    previous = beentouched.Sum();
                    if (beentouched.Sum() == 30)
                    {
                        StreamWriter riter = new StreamWriter(File.Open("TextFile1.txt", FileMode.OpenOrCreate));
                        for (int i = 0; i < 30; i++)
                        {
                            for (int ii = 0; ii < 6; ii++)
                            {
                                riter.Write(doors[i, ii]);
                                Console.Write(doors[i, ii]);
                            }
                            riter.WriteLine();
                            Console.WriteLine();
                        }
                        riter.Close();
                        checking = false;
                        works = true;
                    }
                }
            }
            Process.Start("notepad.exe", "TextFile1.txt");
            Console.ReadLine();
        }
    }
}