using System.Runtime.CompilerServices;

namespace BattleShip.Models
{

    public abstract class ShipType
    {
        private int _Hits { get; }
        private int _Size { get; }
        private string _Name { get; }

        public ShipType()
        {
            _Name = this._Name;
            _Size = this._Size;
            _Hits = 0;
        }

        public override string ToString() => GetType().Name;

        public int Hits { get; }

        public int Size { get; }

        public string Name { get; }

    }

    public sealed class Submarine : ShipType
    {
        //public Submarine(string name, int size, int hits) : this(name, size, hits) { }

        public Submarine(string name, int size, int hits): base() 
        {
            name = "Submarine";
            size = 3;
        }

        public int Hits { get; }

        public int Size { get; }

        new public string Name { get; }
    }

    public sealed class SmallBoat : ShipType
    {
        public SmallBoat(string name, int size, int hits): base()
        {
            name = "Small Boat";
            size = 2;
        }

        public int Hits { get; }

        public int Size { get; }

        new public string Name { get; }
    }

    public sealed class Destroyer : ShipType
    {
        public Destroyer(string name, int size, int hits): base()
        {
            name = "Destroyer";
            size = 3;
        }

        public int Hits { get; }

        public int Size { get; }

        new public string Name { get; }
    }

    public sealed class AircraftCarrier : ShipType
    {
        public AircraftCarrier(string name, int size, int hits) : base()
        {
            name = "Aircraft Carrier";
            size = 5;
        }

        public int Hits { get; }

        public int Size { get; }

        new public string Name { get; }
    }

    public sealed class Battle : ShipType
    {
        public Battle(string name, int size, int hits) : base()
        {
            name = "Battle Ship";
            size = 4;
        }
    }

    //public int Hits { get; }

    //public int Size { get; }

    //public string Name { get; }
}
