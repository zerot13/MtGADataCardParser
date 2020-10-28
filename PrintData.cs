using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;

namespace MtGACardDataParser
{
    public static class PrintData
    {
        public static void PrintDataToFile(List<CardDataFull> cardList, string filename)
        {
            List<string> printCardList = new List<string>();
            foreach(CardDataFull card in cardList)
            {
                printCardList.Add(FormatLine(card));
            }
            File.WriteAllLines(filename, printCardList);
        }

        public static string FormatLine(CardDataFull card)
        {
            string line = string.Format("mtgaID: {0}, setNumber: {1}, name: \"{2}\", prettyName: \"{3}\", cardType: \"{4}\", set: \"ELD\", subTypes: \"{5}\", colorIdentity: {6}, colors: {7}, rarity: \"{8}\", cost: {9}, collectible: {10}",
            card.mtgaId, card.CollectorNumber, uglyName(card.name), card.name, card.types, card.subtypes, card.colorIdentity, card.colors, card.rarity, card.castingcost, card.isCollectible);

            return line;
        }

        private static object uglyName(string name)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(name.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)).ToArray());
            return sb.ToString().Replace(' ', '_').ToLower();
        }
    }
}