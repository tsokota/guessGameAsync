using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;
using Gamers;

namespace GameGuessUI
{
    class Program
    {
        static readonly Random random = new Random((int)DateTime.Now.Ticks);

        static void Main(string[] args)
        {

            int gamersNum = GetInt("Input number of players: ", 2, 8); // get number of players

            Game.Game game = new Game.Game(gamersNum, random.Next(40, 141)); // create new game

            AddNewPlayers(gamersNum, game);  // add players to game

            PrintWinners(game.StartPlay()); // play and print winners

            Console.ReadKey();
        }

        static int GetInt(string str, int leftBorder, int rightBorder)
        {
            int res;
            while (true)
            {
                Console.Write(str);
                if (int.TryParse(Console.ReadLine(), out res) && res >= leftBorder && res <= rightBorder)
                    break;

                Console.WriteLine("Incorrect data! Repeat, please...");
            }

            return res;
        }

        static void PrintWinners(List<Gamer> win)
        {
            foreach (var g in win)
            {
                Console.WriteLine("Winner -> " + g.Name);
            }   
        }

        static void AddNewPlayers(int gamersNum, Game.Game game)
        {
            for (int i = 1; i <= gamersNum; i++)
            {
                game.AddNewGamer((GamerTypesEnum)(GetInt($"Input type {i} player: ", 0, 4)), "Gamer" + i, random.Next(20, 65));
            }
        }
    }
}
