namespace MtGACardDataParser
{
    public static class ColorData
    {
        public static string ShortenColorName(string color)
        {
            switch(color)
            {
                case "White": return "W";
                case "Blue": return "U";
                case "Black": return "B";
                case "Red": return "R";
                case "Green": return "G";
            }
            return "Unknown";
        }
    }
}