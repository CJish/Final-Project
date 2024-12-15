

namespace BattleShip.Models
{
    internal class Ship
    {
        private int hits;
        private int size;
        private string name;

        public Ship(string name, int size, int hits)
        {
            this.name = name;
            this.size = size;
            this.hits = hits;
        }

        public static List<string> GenerateShipPlacement(Ship shipType, string location, bool isHorizontal)
        {
            List<string> shipCoordinates = new List<string>();
            int size = shipType.GetSize();
            char firstChar = location[0];
            char secondChar = location[1];
            if (isHorizontal)
            {
                for (int i = 0; i < size; i++)
                {
                    string coords  = (firstChar.ToString() + secondChar.ToString());
                    shipCoordinates.Add(coords);
                    secondChar++;
                }
            } else
            {
                for (int i = 0; i < size; i++)
                {
                    string coords = (firstChar.ToString() + secondChar.ToString());
                    shipCoordinates.Add(coords);
                    firstChar++;
                }
            }
            return shipCoordinates;
        }

        public int GetSize() { return this.size; }

        public string GetName() { return this.name; }

        public int GetHits() { return this.hits; }
    }
}
