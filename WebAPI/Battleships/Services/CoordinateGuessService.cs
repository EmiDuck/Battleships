using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Battleships.Models;
using Battleships.Models.Enums;

namespace Battleships.Services
{
    public class CoordinateGuessService
    {
        private GameState _gameState;

        public CoordinateGuessService()
        {
            _gameState = GameState.GetInstance();
        }

        public GuessResult Guess(CoordinateGuess coordinates)
        {
            foreach (var ship in _gameState.Ships)
            {
                var coordinatesHit = ship.Coordinates.FirstOrDefault(
                    o => o.HorizontalCoordinate == coordinates.HorizontalCoordinate &&
                    o.VerticalCoordinate == coordinates.VerticalCoordinate);

                if (coordinatesHit != null)
                {
                    coordinatesHit.HasBeenHit = true;
                    if (ship.Coordinates.Any(o => !o.HasBeenHit))
                    {
                        return GuessResult.Hit;
                    }

                    return GuessResult.Sink;
                }
            }

            return GuessResult.Miss;
        }
    }
}