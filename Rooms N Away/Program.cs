using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (int i in roomsNAway(15, 1)) Console.Write(i + ", ");
            Console.ReadLine();
        }
        public static List<int> roomsNAway(int room, int nAway)
        {
            List<int> rooms = new List<int>();
            rooms.Add(room);
            int[,] rmIndxs = new int[,] 
                { { -6, -5, 1, 6, -1, -7 },
                  { -6, 1, 7, 6, 5, -1 },
                  { -6, -5, 1, 6, 5, -1 } };
            List<int> forAdding = new List<int>();
            for (int n = 0; n < nAway; n++)
            {
                foreach (int rm in rooms)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        int chkevn;
                        if ((rm) % 2 == 1 && (rm) % 6 != 1) chkevn = 0;
                        else if ((rm) % 2 == 0 && (rm) % 6 != 0) chkevn = 1;
                        else chkevn = 2;
                        int check = (rm + rmIndxs[chkevn, i] + 30) % 30;
                        if (!rooms.Contains(check) && !forAdding.Contains(check))
                        {
                            forAdding.Add(check);
                        }
                    }
                }
                rooms.AddRange(forAdding);
                forAdding.Clear();
            }
            rooms.Remove(room);
            if (rooms.Contains(0))
            {
                rooms.Remove(0);
                rooms.Add(30);
            }
            return rooms.OrderBy(r => r).ToList();
        }
    }
}