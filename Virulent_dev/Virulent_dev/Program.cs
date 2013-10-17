using System;

namespace Virulent_dev
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (VirulentGame game = new VirulentGame())
            {
                game.Run();
            }
        }
    }
#endif
}

