namespace FloodFill {
    //
    // A position has an x, y coordinates
    //
    public struct Position {
        public readonly uint X;
        public readonly uint Y;

        public Position (uint x, uint y) {
            X = x;
            Y = y;
        }
        
        public override string ToString () {
            return $"[{X}, {Y}]";
        }
    }
}