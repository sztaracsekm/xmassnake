using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snakev2
{


    public enum Directions
    {
        Left,
        Right,
        Up,
        Down
    };


    class Settings
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Speed { get; set; }
        public static int Score { get; set; }
        public static int Points { get; set; }
        public static bool GamerOver { get; set; }
        public static Directions direction {get;set;}

        public Settings()
        {
            Width = 16;
            Height = 16;
            Speed = 20;
            Score = 0;
            Points = 100;
            GamerOver = false;
            direction = Directions.Down;
        }
    }
}
