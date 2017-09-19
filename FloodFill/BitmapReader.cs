using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FloodFill {
    // 
    // Read bitmap from text stream
    //
    public sealed class BitmapReader {
        private static readonly Bitmap Empty = Bitmap.Create (new char[0, 0]);

        public BitmapReader () { }

        public Bitmap Read (string text) {
            using (var reader = new StringReader (text)) {
                return Read (reader);
            }
        }

        public Bitmap Read (TextReader reader) {
            var line = reader.ReadLine ();
            if (line == null) {
                return Empty;
            }

            var lines = new List<string> { line };
            var width = line.Length;
            while (true) {
                line = reader.ReadLine ();

                if (line == null) {
                    break;
                }

                if (line.Length != width) {
                    throw new FormatException ($"Invalid format of {line}, expected width {width} - got {line.Length}");
                }

                lines.Add (line);
            }

            var height = lines.Count;
            var pixels = new char[width, height];
            for (var i = 0; i < height; i++) {
                line = lines[i];
                for (var j = 0; j < width; j++) {
                    pixels[j, i] = line[j];
                }
            }

            return Bitmap.Create (pixels);
        }
    }
}