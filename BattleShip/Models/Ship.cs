

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
            //string firstLetter = string.valueOf(firstChar); // redundant, why cast the char to int?
            //string secondLetter = string.valueOf(secondChar); // redundant, why cast the char to int?
            if (isHorizontal)
            {
                for (int i = 0; i < size; i++)
                {
                    //string result = firstLetter; // redundant, why not just use firstLetter?
                    //string value = string.valueOf(secondChar); // redundant, why not use secondLetter?
                    secondChar++;
                    shipCoordinates.Add(firstChar.ToString());
                    shipCoordinates.Add(secondChar.ToString());
                }
            } else
            {
                for (int i = 0; i < size; i++)
                {
                    //string value = string.valueOf(firstChar); // redundant, why not use firstChar
                    firstChar++;
                    shipCoordinates.Add(firstChar.ToString());
                    shipCoordinates.Add(secondChar.ToString());
                }
            }
            return shipCoordinates;
        }

        public int GetSize() { return this.size; }

        public string GetName() { return this.name; }

        public int GetHits() { return this.hits; }
    }
}
