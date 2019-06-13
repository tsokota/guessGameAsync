using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gamers;

namespace Game
{
    public class Game
    {
        public List<Gamer> Gamers { get; set; }

        public readonly int Weight;

        public delegate Gamer GetSomePlayer(string name, int age);

        private int _totalCount = 0;

        private Gamer _winner = null;

        private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private CancellationToken token = cancellationTokenSource.Token;


        public static Dictionary<GamerTypesEnum, GetSomePlayer> DictiorOfTypes = new Dictionary<GamerTypesEnum, GetSomePlayer>
        {
            { GamerTypesEnum.Usual, new GetSomePlayer(GetUsual) },
            { GamerTypesEnum.Notebook, new GetSomePlayer(GetNotebook) },
            { GamerTypesEnum.Uber, new GetSomePlayer(GetUber) },
            { GamerTypesEnum.Cheater, new GetSomePlayer(GetCheater) },
            { GamerTypesEnum.UberCheater, new GetSomePlayer(GetUberCheater) }
        };

        public Game(int numGamers, int weight)
        {
            Gamers = new List<Gamer>(numGamers);

            Weight = weight;
        }

        public void AddNewGamer(GamerTypesEnum type, string name, int age)
        {
            Gamers.Add(DictiorOfTypes[type].Invoke(name, age));
        }

        public static Gamer GetUsual(string name, int age) => new UsualGamer(name, age);

        public static Gamer GetNotebook(string name, int age) => new NotebookGamer(name, age);

        public static Gamer GetUber(string name, int age) => new UberGamer(name, age);

        public static Gamer GetCheater(string name, int age) => new Cheater(name, age);

        public static Gamer GetUberCheater(string name, int age) => new UberCheater(name, age);

        public List<Gamer> StartPlay()
        {
            List<Gamer> winners = new List<Gamer>();

            ToPlay();

            if (_winner != null)
            {
                winners.Add( _winner);
            }
            else
            {
                winners = FindTheBest();
            }

            return winners;
        }


        public List<Gamer> FindTheBest()
        {
            int leftPointer = Weight - 1, rightPointer = Weight + 1;
            List<Gamer> gamers = new List<Gamer>();

            for (; ; )
            {
                if (leftPointer < 0)
                    leftPointer = 0;
                if (rightPointer > 140)
                    rightPointer = 140;
                if (leftPointer == 0 && rightPointer == 140)
                    break;

                gamers.AddRange(Gamers.Where(x => x.localMem.IndexOf(leftPointer) != -1));
                gamers.AddRange(Gamers.Where(x => x.localMem.IndexOf(rightPointer) != -1));

                if (gamers.Count > 0)
                    break;

                ++rightPointer;
                --leftPointer;
            }

            return gamers;
        }


        public void ToPlay()
        {
         

            Task.WaitAll(Gamers.Select(i => ToPlayGamerAsync(i)).ToArray());

            //foreach (var g in Gamers)
            //{
            //    tasks.Add(Task.Run(()=>ToPlayGamerAsync());
                    
                    
            //        ToPlayGamerAsync();
            //}
        }

        public async Task ToPlayGamerAsync(Gamer gamer)
        {
            for (;;)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Операция прервана токеном");
                    return;
                }

                Console.WriteLine(gamer.Name);

                if ((await Task.Run(() => gamer.MakeStep(token))) == Weight)
                {
                    _winner = gamer;
                    cancellationTokenSource.Cancel();
                }

                if(++_totalCount > 100)
                {
                    cancellationTokenSource.Cancel();
                }
            }
        }
    }
}
