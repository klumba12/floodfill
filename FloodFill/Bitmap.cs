using System;

namespace FloodFill {
    // 
    // A bitmap has an integer width and height, and a pixel accessor (color)
    //
    public sealed class Bitmap {
        private readonly char[, ] pixels;

        private Bitmap (char[, ] pixels) {
            this.pixels = pixels;
        }

        public static Bitmap Create (char[, ] pixels) {
            return new Bitmap (pixels);
        }

        public int Height {
            get => pixels.GetLength (1);
        }

        public int Width {
            get => pixels.GetLength (0);
        }

        // Emulates raw work with pixels
        internal void Lock (Action<char[, ]> visitor) {
            visitor (pixels);
        }

        internal char this [Position pos] {
            get { return pixels[pos.X, pos.Y]; }
            set { pixels[pos.X, pos.Y] = value; }
        }
    }
}