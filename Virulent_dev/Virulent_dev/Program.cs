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
            using (Virulent game = new Virulent())
            {
                game.Run();
            }
        }
    }
#endif
}

