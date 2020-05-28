using System;
using System.Collections.Generic;
using System.Data;

namespace SourceCode.Modelo
{
    public class OrderDAO
    {
        public static List<Order> getList()
        {
            string sql = "select * from apporder;";

            DataTable dt = Connection.realizarConsulta(sql);

            List<Order> list = new List<Order>();
            foreach (DataRow fila in dt.Rows)
            {
                Order o = new Order();
                o.idorder = Convert.ToInt32(fila[0].ToString());
                o.createdate = DateTime.Parse(fila[1].ToString());
                o.idproduct = Convert.ToInt32(fila[2].ToString());
                o.idaddress = Convert.ToInt32(fila[3].ToString());

                list.Add(o);
            }
            return list;
        }
        
        public static void addOrder(Order o)
        {
            string sql = String.Format(
                "insert into \"apporder\"" + 
                "(\"createdate\", \"idproduct\", \"idaddress\")" +
                "values ('{0}', '{1}', '{2}');",
                o.createdate, o.idproduct, o.idaddress);
                
            Connection.realizarAccion(sql);
        }
        
        public static void removeOrder(int idorder)
        {
            string sql = String.Format(
                "delete from apporder where idorder='{0}';",
                idorder);
            
            Connection.realizarAccion(sql);
        }
    }
}