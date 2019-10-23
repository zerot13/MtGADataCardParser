using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MtGACardDataParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string cardDataString = File.ReadAllText("data_cards.json");
            string cardDataKeyFile = File.ReadAllText("data_loc.json");
            string cardEnumsFile = File.ReadAllText("data_enums.json");
            JsonSerializer jsonSerializer = new JsonSerializer();
            CardDataJson[] cdArray = JsonConvert.DeserializeObject<CardDataJson[]>(cardDataString);
            List<CardDataJson> eldList = cdArray.Where(c => c.set == "ELD").ToList();

            DataKeySetJson dataKeySet = JsonConvert.DeserializeObject<DataKeySetJson>(cardDataKeyFile);
            TypeEnumJson[] typeEnumSet = JsonConvert.DeserializeObject<TypeEnumJson[]>(cardEnumsFile);

            List<CardDataFull> fullEldList = eldList.ConvertAll(j => new CardDataFull { 
                mtgaId = j.grpid,
                isCollectible = j.isCollectible,
                CollectorNumber = j.CollectorNumber,
                set = j.set,
                types = convertTypes(j.types, typeEnumSet.First(t => t.name == "CardType").values, dataKeySet.keys),
                subtypes = convertTypes(j.subtypes, typeEnumSet.First(t => t.name == "SubType").values, dataKeySet.keys),
                name = convertName(j.titleId, dataKeySet.keys),
                colors = convertColors(j.colors, typeEnumSet.First(t => t.name == "Color").values, dataKeySet.keys),
                colorIdentity = convertColorIdentity(j.colorIdentity, typeEnumSet.First(t => t.name == "Color").values, dataKeySet.keys),
                castingcost = CostData.ConvertCost(j.castingcost),
                rarity = RarityData.ConvertRarity(j.rarity)
             });
        }

        private static string convertColors(string[] colors, DataKeyJson[] values, DataKeyJson[] dataKey)
        {
            if(colors.Length == 0)
            {
                return "['Colorless']";
            }
            List<string> enumTypeList = colors.ToList().ConvertAll(t => values.First(v => v.id == t).text);
            string convertedColors = "['" + string.Join("', '", enumTypeList.ConvertAll(e => convertName(e, dataKey))) + "']";

            return convertedColors;
        }

        private static string convertColorIdentity(string[] colorId, DataKeyJson[] values, DataKeyJson[] dataKey)
        {
            if(colorId.Length == 0)
            {
                return "[]";
            }
            List<string> enumTypeList = colorId.ToList().ConvertAll(t => values.First(v => v.id == t).text);
            string convertedColors = "['" + string.Join("', '", enumTypeList.ConvertAll(e => convertAndShortenColor(e, dataKey))) + "']";

            return convertedColors;
        }

        private static string convertAndShortenColor(string colorId, DataKeyJson[] dataKey)
        {
            string fullColor = dataKey.First(k => k.id == colorId).text;
            return ColorData.ShortenColorName(fullColor);
        }

        private static string convertTypes(string[] types, DataKeyJson[] values, DataKeyJson[] dataKey)
        {
            List<string> enumTypeList = types.ToList().ConvertAll(t => values.First(v => v.id == t).text);
            string convertedType = string.Join(" ", enumTypeList.ConvertAll(e => convertName(e, dataKey)));

            return convertedType;
        }

        private static string convertName(string titleId, DataKeyJson[] dataKey)
        {
            return dataKey.First(k => k.id == titleId).text;
        }
    }
}
