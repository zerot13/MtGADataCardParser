namespace MtGACardDataParser
{
    public static class CostData
    {
        public static string ConvertCost(string cost)
        {
            if(string.IsNullOrEmpty(cost) || cost == "o0")
            {
                return "[]";
            }
            string[] costSplit = cost.Split('o', System.StringSplitOptions.RemoveEmptyEntries);
            return "['" + string.Join("', '", costSplit) + "']";
        }
    }
}