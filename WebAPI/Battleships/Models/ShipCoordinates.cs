using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Battleships.Models
{
    public class ShipCoordinates
    {
        public int HorizontalCoordinate { get; set; }
        public int VerticalCoordinate { get; set; }
        public bool HasBeenHit { get; set; }
    }
}