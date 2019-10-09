using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Assignment2
{
    public class Game<T> where T : IPlayer
    {
        private List<T> _players;

        public Game(List<T> players)
        {
            _players = players;
        }

        public T[] GetTop10Players()
        {
            // ... write code that returns 10 players with highest scores

            List<Player> tmplist = _players.ToList() as List<Player>;
            //return _players.Take(10).ToArray();

            var list = tmplist
            .OrderByDescending(p => p.Score)
            .Take(10);

            T[] _players2 = list.ToArray() as T[];

            return _players2;
        }
    }
}
