using System.Collections.Generic;
using Battleships.Models;

namespace Battleships.Services
{
    public class GameState
    {
        private static GameState _instance;

        private GameState() { }

        public IEnumerable<Ship> Ships { get; set; }

        public static GameState GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameState();
            }

            return _instance;
        }
    }
}