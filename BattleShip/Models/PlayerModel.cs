using BattleShip;
using BattleShip.Models;
using System.Text.RegularExpressions;

namespace BattleShip.Models
{
    public class PlayerModel
    {
        private static string PATTERN = "[a-jA-J]{1}[0-9]{1}";
        internal Regex rgx = new Regex(PATTERN);
        public static string guess;

        public PlayerModel() { }

        public virtual void PlaceShips(ShipBoard shipBoard, FiringBoard firingBoard)
        {
            Ship sub = new Ship("Submarine", 3, 0);
            Ship sb = new Ship("Small Boat", 2, 0);
            Ship destroyer = new Ship("Destroyer", 3, 0);
            Ship carrier = new Ship("Aircraft Carrier", 5, 0);
            Ship battle = new Ship("BattleShip", 4, 0);
            List<Ship> shipArray = new List<Ship> { sub, sb, destroyer, carrier, battle };

            List<string> thisGeneratedShip;
            try
            {
                foreach (Ship ship in shipArray)
                {
                    Console.WriteLine($"Placing ship: " + ship.GetName() + " size: " + ship.GetSize());
                    thisGeneratedShip = GenerateShip(ship, shipBoard);
                    if (IsValidBuild(thisGeneratedShip))
                    {
                        shipBoard.PlaceShip(thisGeneratedShip);
                    }
                    Console.Clear();
                    shipBoard.PrintShipBoard(firingBoard);
                }
            } catch (IndexOutOfRangeException e) // TODO StringIndexOutOfBoundsException
            
            {
                Console.WriteLine(e);
                Console.WriteLine("Invalid Coordinates:");
                Console.WriteLine("Valid coordinates are [A-J] and [0-9]");
                Console.WriteLine("For example, you could do a7 or J10");
                Console.WriteLine("\tbut not H0 or b11");
            }
        }

        private bool IsShipHorizontal()
        {
            bool result = false;
            string word = "";
            Console.WriteLine("Ship horizontal? (true/false)");

            word = Console.ReadLine().ToLower().Trim(); // TODO: need better input validation here
            Boolean.TryParse(word, out result);
            if (word.Equals("t") || word.Equals("T")) { result = true; }; // Boolean.parseBoolean(word)
            return result;
        }

        // Validate coordinates
        public bool IsValidBuild(List<String> shipcoords)
        {
            bool result = false;
            foreach (string s in shipcoords) // TODO: right now sets to true if ANY of the strings are a match
            {
                if (rgx.IsMatch(s)) { result = true; };
            }
            return result;
        }

        // create a valid ship object
        private List<string> GenerateShip(Ship ship, ShipBoard shipBoard)
        {
            string shipPlacement;
            bool isHorizontal;
            List<string> shipGenerated; // This list comes from Ship.GenerateShipPlacement

            do // switch the while and do portions?
            {
                Console.WriteLine("Enter the position you want for your: " + ship.GetName() + " (e.g., C3)");
                shipPlacement = GetCoordinates();
                isHorizontal = IsShipHorizontal();
                shipGenerated = Ship.GenerateShipPlacement(ship, shipPlacement, isHorizontal);
            } while (!shipBoard.IsValidPlacement(shipGenerated)); //false
            return shipGenerated;
        }

        private string GetCoordinates()
        {
            string coordinates;
            do
            {
                Console.WriteLine("Valid coordinates are [A-J] and [0-9]");
                Console.WriteLine("For example, you could do A7 or J9");
                Console.WriteLine("\tbut not K1 or b10");
                coordinates = Console.ReadLine();
                if (!rgx.IsMatch(coordinates))
                {
                    Console.WriteLine("Please try again.");
                }
            } while (!rgx.IsMatch(coordinates));
            return coordinates;
        }

        public virtual string TakeTurn(FiringBoard firingBoard, ShipBoard shipBoard)
        {
            Console.WriteLine("Enter your firing coordinates:");
            Console.WriteLine("\tValid coordinates are [A-J] and [0-9]");
            guess = GetCoordinates();

            while (!rgx.IsMatch(guess))
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
