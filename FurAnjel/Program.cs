using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurAnjel
{
    /// <summary>
    /// Main entry point static class for the program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point static method for the program.
        /// </summary>
        /// <param name="args">The input arguments from the command line, if any.</param>
        static void Main(string[] args)
        {
            // Create a new game instance.
            GameInternal game = new GameInternal();
            // Run the game!
            game.Run();
        }
    }
}
