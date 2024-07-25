namespace FactorioConverter
{


    internal class Reader
    {

        public void Read(string path)
        {
            string luaData = File.ReadAllText(path);
            var lines = luaData.Split('\n');

            string[] properties = ["name =", "icon ="];

            // Lua-Daten als String
            foreach (var line in lines)
            {
                Console.ForegroundColor = ConsoleColor.White;
                var checkLine = line.Trim();

                foreach (var property in properties)
                {
                    if (checkLine.Contains(property))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        // Finden der Positionen der doppelten Anführungszeichen
                        int startIndex = line.IndexOf('"') + 1;
                        int endIndex = line.IndexOf('"', startIndex);

                        if (startIndex > 0 && endIndex > startIndex)
                        {
                            string readProperty = line.Substring(startIndex, endIndex - startIndex);
                            Console.WriteLine(property + ": " + readProperty);
                        }
                    }
                }
                Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine(checkLine);
            }
            Console.ReadKey();
        }
    }
}
