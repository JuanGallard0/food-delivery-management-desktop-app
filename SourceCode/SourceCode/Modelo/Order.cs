using System;

namespace SourceCode.Modelo
{
    public class Order
    {
        public int idorder { get; set; }
        public DateTime createdate { get; set; }
        public int idproduct { get; set; }
        public int idaddress { get; set; }

        public Order()
        {
        }
    }
}