using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamers
{
    public class UsualGamer : Gamer
    {
        public UsualGamer(string name, int age) : base(name, age) { }

        public override int GetNumber()
        {
            int num = random.Next(40, 141);
            localMem.Add(num);
            return num;
        }
    }
}
