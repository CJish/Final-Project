

namespace BattleShip.Models
{
    public class PlayerModel
    {
        private static string PATTERN = "[a-jA-J]{1}[0-9]{1}"; // why can't I use "sealed"?
        private static sealed Scanner scanner = new Scanner(System.in); // probably using stringreader
        public static string guess;

        public PlayerModel() { }

        public void PlaceShips(ShipBoard shipBoard, FiringBoard firingBoard)
        {
            List<string> thisGeneratedShip;
            try
            {
                for (ShipType ship : ShipType.values())
                {
                    Console.WriteLine($"Placing ship: " + ship.getName() + "; size: " + ship.getSize());
                    thisGeneratedShip = thisGeneratedShip(ship, shipBoard);
                    if (isValidBuild(thisGeneratedShip))
                    {
                        shipBoard.placeShip(thisGeneratedShip);
                    }
                    Console.Clear();
                    shipBoard.printShipBoard(firingBoard);
                }
            } catch (IndexOutOfRangeException e) // TODO StringIndexOutOfBoundsException
            {
                Console.WriteLine("Valid coordinates are [A-J] and [0-9]");
                Console.WriteLine("For example, you could do a7 or j0");
                Console.WriteLine("\tbut not h1 or b10");
            }
        }

        private bool IsShipHorizontal()
        {
            bool result = false;
            string word = "";
            Console.WriteLine("Ship horizontal? (true/false)");

            word = scanner.next().trim.toLowerCase();
            if (word.Equals("t"))
            {
                word = "true";
            }
            result = Boolean.TryParse(word, true); // Boolean.parseBoolean(word)
            return result;
        }

        // Validate coordinates
        public bool IsValidBuild(List<String> ship)
        {
            bool result = false;
            for (string s : ship)
            {
                result = s.matches(PATTERN);
            }
            return result;
        }

        // create a valid ship object
        private List<string> GenerateShip(ShipType ship, ShipBoard shipBoard)
        {
            string shipPlacement;
            bool isHorizontal;
            List<string> shipGenerated;

            do // switch the while and do portions?
            {
                Console.WriteLine("Enter the position you want:" + ship.getName() + " (e.g., C3: ");
                shipPlacement = GetCoordinates();
                isHorizontal = isShipHorizontal();
                shipGenerated = ship.generateShipPlacement(ship, shipPlacement, isHorizontal);
            } while (!shipBoard.isValidPlacement(shipGenerated));
            return shipGenerated;
        }

        private string GetCoordinates()
        {
            string coordinates;
            do
            {
                Console.WriteLine("Valid coordinates are [A-J] and [0-9]");
                Console.WriteLine("For example, you could do a7 or J0");
                Console.WriteLine("\tbut not H1 or b10");
                coordinates = scanner.next().trim().toLowerCase();
                if (!coordinates.matches(PATTERN))
                {
                    Console.WriteLine("Please try again.");
                }
            } while (!coordinates.matches(PATTERN));
            return coordinates;
        }

        public virtual string TakeTurn(FiringBoard firingBoard, ShipBoard shipBoard)
        {
            Console.WriteLine("Enter your firing coordinates:");
            Console.WriteLine("\tValid coordinates are [A-J] and [0-9]");
            guess = GetCoordinates();

            while (!guess.matches(PATTERN))
            {
                Console.WriteLine("Invalid coordinates.");
                Console.WriteLine("\tValid coordinates are [A-J] and [0-9]");
                Console.WriteLine("\t\tFor example, you could do a7 or J0");
                guess = GetCoordinates();
            }
            return guess;
        }

        public static string GetGuess()
        {
            return guess;
        }
    }
}
