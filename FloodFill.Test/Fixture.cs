using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FloodFill.Test {
    public static class Fixture {
        public static string Resource (string name) {
            var asm = typeof (Fixture).GetTypeInfo ().Assembly;
            var path = $"FloodFill.Test.Resources.{name}";
            using (var stream = asm.GetManifestResourceStream (path)) {
                if (stream == null) {
                    throw new FileNotFoundException (
                        @"Ensure that path is correct. 
                         If 'dotnet test' service is used,
                         be aware that case of project name is sensitive", 
                        path);
                }

                using (var reader = new StreamReader (stream)) {
                    return reader.ReadToEnd ();
                }
            }
        }

        // Line ending agnostic assert
        public static void AreEqual(string expected, string actual) {
            expected = expected?.Replace("\r\n", "\n");
            actual = actual?.Replace("\r\n", "\n");

            Assert.AreEqual(expected, actual);
        }
    }
}