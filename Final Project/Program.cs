

namespace Final_Project
{
    // This class will only be responsible for getting things started
    // All of the logic and actual management of the game will happen elsewhere
    internal class Program
    {
        static void Main(string[] args)
        {
            GameApp app = new GameApp();
            GameApp.Start();
        }
    }
}
