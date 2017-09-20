using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FloodFill.Test.Units {
    [TestClass]
    public class Surface {
        [TestMethod]
        public void Should_Fill_Correct_Case_1 () {
            RunCase (1, '@', new Position (8, 12));
        }

        [TestMethod]
        public void Should_Fill_Correct_Case_2 () {
            RunCase (2, '@', new Position (2, 2));
        }

        [TestMethod]
        public void Should_Fill_Correct_Case_3 () {
            RunCase (3, ',', new Position (8, 3));
        }

        [TestMethod]
        public void Should_Fill_Correct_With_Search_Around () {
            RunCase (4, '#', new Position (1, 7));
        }

        private static void RunCase (int no, char color, Position pos) {
            var input = Fixture.Resource ($"Case{no}-Input.txt");
            var output = Fixture.Resource ($"Case{no}-Output.txt");

            var bitmap = new BitmapReader ().Read (input);

            var canvas = new Canvas (bitmap);
            var surface = new FloodFill.Surface (canvas);
            surface.Fill (pos, color);

            var result = new StringBuilder ();
            new BitmapWriter (bitmap).Write (result);

            var newLine = Environment.NewLine;
            var expected = $"{newLine}{output}{newLine}";
            var actual = $"{newLine}{result.ToString()}{newLine}";

            Assert.AreEqual (expected, actual);
        }
    }
}