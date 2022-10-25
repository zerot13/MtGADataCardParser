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
            List<CardDataFull> fullEldList = ParseCardData.ParseCards();
            PrintData.PrintDataToFile(fullEldList, "finalEldCardList.txt");

            // List<CardDataFull> fullZnrList = ParseCardData.ParseZnrCards();
            // PrintData.PrintDataToFile(fullZnrList, "finalZnrCardList.txt");
        }
    }
}
