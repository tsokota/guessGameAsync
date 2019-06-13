using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamers
{
    public class UberCheater : Gamer
    {
        public UberCheater(string name, int age) : base(name, age) { }

        private int _counter { get; set; } = 40;

        public override int GetNumber()
        {
            while (true)
            {
                lock(used)
                {
                    if (used.IndexOf(_counter) == -1)
                        break;
                }
                
                _counter++;
            }

            localMem.Add(_counter);
            return _counter++;
        }
    }
}
