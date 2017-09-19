using System;
using System.Collections.Generic;

namespace FloodFill {
    // 
    // The surface represents the backend/results of drawing to a canvas      
    //
    public sealed class Surface {
        private readonly Canvas canvas;

        public Surface (Canvas canvas) {
            this.canvas = canvas;
        }

        public void Fill (Position start, char color) {
            start = canvas.Translate (start);
            
            var width = canvas.Width;
            var height = canvas.Height;            
            if (start.X > width || start.Y > height) {
                throw new ArgumentOutOfRangeException ("position");
            }

            var search = canvas[start];            
            var canWalk = CanWalkFactory (search);
            var successor = SuccessorFactory (canWalk);
            var open = new Queue<Position> ();
            var closed = new HashSet<Position> ();

            open.Enqueue (start);
            while (open.Count > 0) {
                var p = open.Dequeue ();
                if (closed.Contains (p)) {
                    continue;
                }

                closed.Add (p);
                canvas[p] = color;

                foreach (var y in successor (p)) {
                    open.Enqueue (y);
                }
            }
        }

        private Predicate<Position> CanWalkFactory (char search) {
            var width = canvas.Width;
            var height = canvas.Height;

            return pos => {
                if (pos.X < width && pos.Y < height) {
                    var color = canvas[pos];
                    return color == search || color == Color.Transparent;
                }

                return false;
            };
        }

        private Func<Position, IEnumerable<Position>> SuccessorFactory (Predicate<Position> canWalk) {
            return p => {
                var result = new List<Position> ();

                var w = new Position (p.X - 1, p.Y);
                var e = new Position (p.X + 1, p.Y);
                var n = new Position (p.X, p.Y - 1);
                var s = new Position (p.X, p.Y + 1);

                if (canWalk (w)) {
                    result.Add (w);
                }

                if (canWalk (e)) {
                    result.Add (e);
                }

                if (canWalk (n)) {
                    result.Add (n);
                }

                if (canWalk (s)) {
                    result.Add (s);
                }

                return result;
            };
        }
    }
}