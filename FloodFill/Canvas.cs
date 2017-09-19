namespace FloodFill {
    // 
    // Encapsulates all of the state about drawing into a bitmap
    //
    public sealed class Canvas {
        private readonly Bitmap bitmap;

        public Canvas (Bitmap bitmap) {
            this.bitmap = bitmap;
        }

        public int Height {
            get => bitmap.Height + 2;
        }

        public int Width {
            get => bitmap.Width + 2;
        }

        public char this [Position pos] {
            get {
                if (HitTest (pos)) {
                    return bitmap[new Position (pos.X - 1, pos.Y - 1)];
                }

                return Color.Transparent;
            }
            set {
                if (HitTest (pos)) {
                    bitmap[new Position (pos.X - 1, pos.Y - 1)] = value;
                }
            }
        }

        internal Position Translate (Position pos) {
            return new Position (pos.X + 1, pos.Y + 1);
        }

        private bool HitTest (Position pos) {
            var x = pos.X;
            var y = pos.Y;

            var xAround = x == 0 || x == Width - 1;
            var yAround = y == 0 || y == Height - 1;

            return !(xAround || yAround);
        }
    }
}