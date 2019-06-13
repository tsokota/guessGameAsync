using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamers
{
    public abstract class Gamer
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public abstract int GetNumber();

        internal readonly Random random = new Random((int)DateTime.Now.Ticks);

        public static List<int> used { get; set; } = new List<int>();

        public List<int> localMem { get; set; }

        public Gamer(string name, int age)
        {
            Name = name;
            Age = age;
            localMem = new List<int>();
        }

        public int MakeStep()
        {
            int num = GetNumber();

            lock(used)
            {
                used.Add(num);
            }

            return num;
        }
    }
}
