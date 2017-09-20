using System.IO;
using System.Reflection;

namespace FloodFill.Test {
    public static class Fixture {
        public static string Resource (string name) {
            var asm = typeof (Fixture).GetTypeInfo ().Assembly;
            using (var stream = asm.GetManifestResourceStream ($"FloodFill.Test.Resources.{name}")) {
                using (var reader = new StreamReader (stream)) {
                    return reader.ReadToEnd ();
                }
            }
        }
    }
}