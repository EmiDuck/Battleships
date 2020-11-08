using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Battleships.Models;

namespace Battleships.Services
{
    public class GameStateService
    {
        private GameState _gameState;
        private IEnumerable<ShipCoordinates> AllShipCoordinates;

        public GameStateService()
        {
            _gameState = GameState.GetInstance();
        }

        public void InitialiseGame()
        {
            _gameState.Ships = new List<Ship>();
            AllShipCoordinates = new List<ShipCoordinates>();

            _gameState.Ships = _gameState.Ships.Append(CreateShip(5, "HMS Queen Elizabeth"));
            _gameState.Ships = _gameState.Ships.Append(CreateShip(4, "HMS Duncan"));
            _gameState.Ships = _gameState.Ships.Append(CreateShip(4, "HMS Defender"));
        }

        private Ship CreateShip(int shipLength, string name)
        {
            bool shipFailedToCreate;
            IEnumerable<ShipCoordinates> coordinates;

            do
            {
                coordinates = CreateShipCoordinates(shipLength);
                shipFailedToCreate = coordinates == null;
            } while (shipFailedToCreate);

            AllShipCoordinates = AllShipCoordinates.Concat(coordinates);

            return new Ship
            {
                Coordinates = coordinates,
                IsSunk = false,
                Name = name
            };
        }

        private IEnumerable<ShipCoordinates> CreateShipCoordinates(int shipLength)
        {
            var rand = new Random();
            int startX, startY, direction;
            bool notValid;

            do
            {
                startX = rand.Next(10);
                startY = rand.Next(10);
                direction = rand.Next(4); // 0 - up, 1 - right, 2 - down, 3 - left

                notValid = 
                    direction == 0 && startY - shipLength < 0 ||
                    direction == 1 && startX + shipLength > 9 ||
                    direction == 2 && startY + shipLength > 9 ||
                    direction == 3 && startX - shipLength < 0;
            } while (notValid);

            var coordinates = new List<ShipCoordinates>();

            for (int i = 0; i < shipLength; i++)
            {
                int x, y;

                switch (direction)
                {
                    case 0:
                        x = startX;
                        y = startY - i;
                        break;
                    case 1:
                        x = startX + i;
                        y = startY;
                        break;
                    case 2:
                        x = startX;
                        y = startY + i;
                        break;
                    case 3:
                        x = startX - i;
                        y = startY;
                        break;
                    default:
                        throw new Exception($"Invalid direction: {direction}");
                }

                if (AllShipCoordinates.Where(o => o.HorizontalCoordinate == x && o.VerticalCoordinate == y).Any())
                {
                    return null;
                }

                coordinates.Add(new ShipCoordinates
                {
                    HorizontalCoordinate = x,
                    VerticalCoordinate = y
                });
            }

            return coordinates;
        }
    }
}