

using System.Runtime.ConstrainedExecution;

namespace BattleShip.Models
{
    internal class CPUPlayer : PlayerModel
    {
        private Random random;
        private string previousHit;
        private sealed string PATTERN = "[a-jA-J]{1}[0-9]{1}";

        public CPUPlayer() { random = new Random(); }

        // have the CPU place its ships
        public override void PlaceShips(ShipBoard shipBoard, FiringBoard firingBoard)
        {
            List<string> thisGeneratedShip;
            for (ShipType ship : ShipType.values())
            {
                Console.WriteLine($"CPU is placing {ship.getName()}");
                thisGeneratedShip = thisGeneratedShip(ship, shipBoard);
                Console.WriteLine(thisGeneratedShip);
                if (isValidBuild(thisGeneratedShip))
                {
                    shipBoard.placeShip(thisGeneratedShip);
                    Console.WriteLine($"CPU placed {ship.getName()}");
                }
            }
        }

        // CPU gets valid coords
        private string GetCoordinates()
        {
            string coordinates;
            do
            {
                char letter = (char)(random.nextInt(9) + ('a'));
                char number = (char)(random.nextInt(10) - 1);
                string s1 = String.valueOf(letter);
                string s2 = String.valueOf(number + 1);
                coordinates = s1 + s2;
            }
            while (!coordinates.matches(PATTERN));
            return coordinates;
        }

        // CPU creates valid ships
        private List<string> GenerateShip(ShipType ship, ShipBoard shipBoard)
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
                while (!guess.matches(PATTERN));
            }

            else
            {
                if (firingBoard.GetFireRecord() != null)
                {
                    do
                    {
                        guess = GetCoordinates();
                    }
                    while (firingBoard.GetFireRecord().contains(guess));
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
            char row = previousHit.charAt(0);
            char col = previousHit.charAt(1);

            // shoot adjacent to the previous hit
            do
            {
                int direction = random.nextInt(4);
                switch (direction)
                {
                    case 0: // north
                        guess = string.valueOf(row) + (col += 1);
                        break;
                    case 1: // south
                        guess = string.valueOf(row) + (col -= 1);
                        break;
                    case 2: //west
                        guess = string.valueOf(row -= 1) + col;
                        break;
                    case 3: // east
                        guess = string.valueOf(row += 1) + col;
                        break;
                    default:
                        return previousHit;
                }
            }
            while (firingBoard.GetFireRecord().contains(guess));
            return guess;
        }

    }
}