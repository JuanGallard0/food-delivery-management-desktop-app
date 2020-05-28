namespace SourceCode.Modelo
{
    public class Demand
    {
        public string name { get; set; }
        public int quantity { get; set; }

        public Demand()
        {
            name = "";
            quantity = 0;
        }
    }
}