namespace MtGACardDataParser
{
    public class CardDataJson
    {
        public string grpid;
        public string titleId;
        public string isCollectible;
        public string CollectorNumber;
        public string set;
        public string[] types;
        public string[] subtypes;
    }

    public class CardDataFull
    {
        public string mtgaId;
        public string name;
        public string isCollectible;
        public string CollectorNumber;
        public string set;
        public string types;
        public string[] subtypes;
    }

    public class DataKeySetJson
    {
        public string langKey;
        public DataKeyJson[] keys;
    }

    public class DataKeyJson
    {
        public string id;
        public string text;
    }

    public class TypeEnumJson
    {
        public string name;
        public DataKeyJson[] values;
    }
}