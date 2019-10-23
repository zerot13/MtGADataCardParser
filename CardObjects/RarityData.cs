namespace MtGACardDataParser
{
    public static class RarityData
    {
        public static string ConvertRarity(string rarityId)
        {
            switch(rarityId)
            {
                case "1": return "Basic";
                case "2": return "Common";
                case "3": return "Uncommon";
                case "4": return "Rare";
                case "5": return "Mythic Rare";
            }
            return "Unknown";
        }
    }
}