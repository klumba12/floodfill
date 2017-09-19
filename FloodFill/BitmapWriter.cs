using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FloodFill {
    // 
    // Write bitmap to text stream
    //
    public sealed class BitmapWriter {
        private readonly string delimiter = Environment.NewLine;
        private readonly Bitmap bitmap;

        public BitmapWriter (Bitmap bitmap) {
            this.bitmap = bitmap;
        }

        public void Write (StringBuilder builder) {
            using (var writer = new StringWriter (builder)) {
                Write (writer);
            }
        }

        public void Write (TextWriter writer) {
            bitmap.Lock (pixels => {
                var read = ReadFactory (pixels);
                var line = read ();
                if (line == null) {
                    return;
                }

                writer.Write (line);
                while (true) {
                    line = read ();

                    if (line == null) {
                        break;
                    }

                    writer.WriteLine ();
                    writer.Write (line);
                }
            });
        }

        private Func<string> ReadFactory (char[, ] pixels) {
            var width = bitmap.Width;
            var height = bitmap.Height;
            var cursor = 0;
            return () => {
                if (cursor < height) {
                    var buffer = new char[width];
                    for (int i = 0; i < width; i++) {
                        buffer[i] = pixels[i, cursor];
                    }

                    cursor++;
                    return new string (buffer);
                }

                return null;
            };
        }
    }
}