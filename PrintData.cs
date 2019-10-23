using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MtGACardDataParser
{
    public static class PrintData
    {
        public static void PrintDataToFile(List<CardDataFull> cardList)
        {
            List<string> printCardList = new List<string>();
            foreach(CardDataFull card in cardList)
            {
                printCardList.Add(FormatLine(card));
            }
            File.WriteAllLines("finalCardList.txt", printCardList);
        }

        public static string FormatLine(CardDataFull card)
        {
            string line = "throne_of_eldraine.addCard(new Card({ " + string.Format("mtgaID: {0}, setNumber: {1}, name: \"{2}\", prettyName: \"{3}\", cardType: \"{4}\", set: \"ELD\", subTypes: \"{5}\", colorIdentity: {6}, colors: {7}, rarity: \"{8}\", cost: {9}, collectible: {10}",
            card.mtgaId, card.CollectorNumber, uglyName(card.name), card.name, card.types, card.subtypes, card.colorIdentity, card.colors, card.rarity, card.castingcost, card.isCollectible) + " }))";

            return line;
        }

        private static object uglyName(string name)
        {
            return name.Replace(' ', '_').ToLower();
        }
    }
}