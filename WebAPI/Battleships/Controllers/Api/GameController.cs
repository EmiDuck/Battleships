using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using Battleships.Models;
using Battleships.Services;

namespace Battleships.Controllers.Api
{
    public class GameController : ApiController
    {
        private GameStateService _gameStateService;
        private CoordinateGuessService _coordinateGuessService;

        public GameController()
        {
            _gameStateService = new GameStateService();
            _coordinateGuessService = new CoordinateGuessService();
        }

        [HttpPost]
        [Route("api/initialise")]
        public IHttpActionResult StartGame()
        {
            _gameStateService.InitialiseGame();
            return Ok();
        }

        [HttpPost]
        [Route("api/fire")]
        public IHttpActionResult Fire([FromBody] string coordinates)
        {
            var isValid = Regex.IsMatch(coordinates, @"^[a-jA-J]([0-9]|10)$");
            if (!isValid)
            {
                return BadRequest("The coordinates are not valid.");
            }

            var coordinateGuess = new CoordinateGuess
            {
                HorizontalCoordinate = ToCoordinate(coordinates[0]),
                VerticalCoordinate = GetVerticalCoordinate(coordinates)
            };

            var result = _coordinateGuessService.Guess(coordinateGuess);

            return Ok(result);
        }

        private int GetVerticalCoordinate(string coordinates)
        {
            if (coordinates.Length == 3)
            {
                return 10;
            }
            
            return ToCoordinate((int)char.GetNumericValue(coordinates[1]));
        }

        private int ToCoordinate(char letter)
        {
            switch (letter.ToString().ToUpper())
            {
                case "A": return 0;
                case "B": return 1;
                case "C": return 2;
                case "D": return 3;
                case "E": return 4;
                case "F": return 5;
                case "G": return 6;
                case "H": return 7;
                case "I": return 8;
                case "J": return 9;
                default: throw new Exception("Letter is not valid");
            }
        }

        private int ToCoordinate(int number)
        {
            return number - 1;
        }
    }
}
