using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Battleships.Models
{
    public class Ship
    {
        public string Name { get; set; }
        public IEnumerable<ShipCoordinates> Coordinates { get; set; }
        public bool IsSunk { get; set; }
    }
}