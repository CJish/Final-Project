

using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

namespace BattleShip.Models
{
    internal class CPUPlayer : PlayerModel
    {
        private Random random;
        private string previousHit;
        private static string PATTERN = "[a-jA-J]{1}[0-9]{1}";
        internal Regex rgx = new Regex(PATTERN);
        Random rnd = new Random();

        public CPUPlayer() { random = new Random(); }

        // have the CPU place its ships
        public override void PlaceShips(ShipBoard shipBoard, FiringBoard firingBoard)
        {
            Ship sub = new Ship("Submarine", 3, 0);
            Ship sb = new Ship("Small Boat", 2, 0);
            Ship destroyer = new Ship("Destroyer", 3, 0);
            Ship carrier = new Ship("Aircraft Carrier", 5, 0);
            Ship battle = new Ship("BattleShip", 4, 0);
            List<Ship> shipArray = new List<Ship> { sub, sb, destroyer, carrier, battle };

            List<string> thisGeneratedShip;
            foreach (Ship ship in shipArray)
            {
                Console.WriteLine($"CPU is placing {ship.GetName()}");
                thisGeneratedShip = GenerateShip(ship, shipBoard);
                Console.WriteLine(thisGeneratedShip);
                if (IsValidBuild(thisGeneratedShip))
                {
                    shipBoard.PlaceShip(thisGeneratedShip);
                    Console.WriteLine($"CPU placed {ship.GetName()}");
                }
            }
        }

        // CPU gets valid coords
        private string GetCoordinates()
        {
            string coordinates;
            do
            { 
                char[] coords = new char[2];
                coords[0] = (char)(rnd.Next() % 10);
                coords[1] = (char)(rnd.Next() % 10);
                coordinates = coords.ToString();
            }
            while (!rgx.IsMatch(coordinates));
            return coordinates;
        }

        // CPU creates valid ships
        private List<string> GenerateShip(Ship ship, ShipBoard shipBoard)
        {
            string shipPlacement;
            bool isHorizontal;
            List<string> shipGenerated;

            do // TODO aren't these the same?
            {
                shipPlacement = GetCoordinates();
                isHorizontal = IsShipHorizontal();
                shipGenerated = Ship.GenerateShipPlacement(ship, shipPlacement, isHorizontal);
            }
            while (!shipBoard.IsValidPlacement(shipGenerated));

            do
            {
                shipPlacement = GetCoordinates();
                isHorizontal = IsShipHorizontal();
                shipGenerated = Ship.GenerateShipPlacement(ship, shipPlacement, isHorizontal);
            }
            while (!IsValidBuild(shipGenerated));
            return shipGenerated;
        }

        private bool IsShipHorizontal() {
            bool isHorizontal = false;
            int horizontalInt = random.Next();
            if (horizontalInt % 2 == 1)
            {
                isHorizontal = true;
            }


            return isHorizontal; 
        }

        // CPU shooting strategy
        public override string TakeTurn(FiringBoard firingBoard, ShipBoard shipBoard)
        {
            string guess;

            if (firingBoard.GetCpuHit() != null)
            {
                previousHit = firingBoard.GetCpuHit();
                do
                {
                    guess = GenerateNearbyTarget(previousHit, firingBoard);
                }
                while (!rgx.IsMatch(guess));
            }

            else
            {
                if (firingBoard.GetFireRecord() != null)
                {
                    do
                    {
                        guess = GetCoordinates();
                    }
                    while (firingBoard.GetFireRecord().Contains(guess));
                }
                else
                {
                    guess = GetCoordinates();
                }
            }
            return guess;
        }

        // if CPU hits a ship, it will pick a random grid right next to it
        private string GenerateNearbyTarget(String previousHit, FiringBoard firingBoard)
        {
            string guess = null;
            char row = previousHit[0];
            char col = previousHit[1];

            // shoot adjacent to the previous hit
            do
            {
                int direction = random.Next(4);
                switch (direction)
                {
                    case 0: // north
                        guess = row.ToString() + (col + 1).ToString();
                        break;
                    case 1: // south
                        guess = row.ToString() + (col - 1).ToString();
                        break;
                    case 2: //west
                        guess = (row - 1).ToString() + col.ToString();
                        break;
                    case 3: // east
                        guess = (row + 1).ToString() + col.ToString();
                        break;
                    default:
                        return previousHit;
                }
            }
            while (firingBoard.GetFireRecord().Contains(guess));
            return guess;
        }

    }
}