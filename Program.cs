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
            Console.WriteLine("Hello World!");
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
                subtypes = j.subtypes,
                name = convertName(j.titleId, dataKeySet.keys)
             });
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
