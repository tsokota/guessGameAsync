using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamers
{
    public class UberGamer : Gamer
    {
        public UberGamer(string name, int age) : base(name, age) { }

        private int _counter { get; set; } = 40;

        public override int GetNumber()
        {
            localMem.Add(_counter);
            return _counter++;
        }
    }
}
