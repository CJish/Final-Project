


namespace BattleShip.Models
{
    public enum ShipType
    {
        SUBMARINE("Submarine", 3);
        SMALL_BOAT("Small Boat", 2);
        DESTROYER("Destroyer", "3");
        AIRCRAFT_CARRIER("Aircraft Carrier", 5);
        BATTLESHIP("Battleship", 4);


        private sealed string name;
        private sealed int size;

        ShipType(string name, int size)
        {
            this.name = name;
            this.size = size;
        }

        public string GetName() { return name; }

        public interface GetSize() { return size; }
    }
    }
}
