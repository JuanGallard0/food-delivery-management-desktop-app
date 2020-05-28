using System;

namespace SourceCode.Modelo
{
    public class FullOrder
    {
        public int idorder { get; set; }
        public DateTime createdate { get; set; }
        public int idproduct { get; set; }
        public int idaddress { get; set; }
        public int iduser { get; set; }
        public string address { get; set; }
        public int idbusiness { get; set; }
        public string name { get; set; }

        public FullOrder()
        {
        }
    }
}