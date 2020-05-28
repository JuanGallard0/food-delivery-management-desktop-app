namespace SourceCode.Modelo
{
    public class Business
    {
        public int idbusiness { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public Business(string name)
        {
            this.name = name;
        }

        public Business()
        {
        }
    }
}