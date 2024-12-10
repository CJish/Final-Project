

namespace BattleShip.Models
{
    internal class Ship
    {
        private sealed ShipType type;
        private int hits;

        public Ship(ShipType type)
        {
            this.type = type;
            this.hits = 0;
        }

        public static List<string> GenerateShipPlacement(ShipType shipType, string location, bool isHorizontal)
        {
            List<string> shipCoordinates = new List<string>();
            int size = shipType.GetSize();
        }
    }
}
