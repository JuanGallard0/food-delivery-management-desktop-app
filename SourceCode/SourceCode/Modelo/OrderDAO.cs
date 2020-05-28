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

        public static List<FullOrder> consultOrders()
        {
            string sql = "SELECT * FROM apporder INNER JOIN address ON apporder.idaddress = address.idaddress INNER JOIN product ON apporder.idproduct = product.idproduct;";
            
            DataTable dt = Connection.realizarConsulta(sql);

            List<FullOrder> list = new List<FullOrder>();
            foreach (DataRow fila in dt.Rows)
            {
                FullOrder f = new FullOrder();
                f.idorder = Convert.ToInt32(fila[0].ToString());
                f.createdate = DateTime.Parse(fila[1].ToString());
                f.idproduct = Convert.ToInt32(fila[2].ToString());
                f.idaddress = Convert.ToInt32(fila[3].ToString());
                f.iduser = Convert.ToInt32(fila[5].ToString());
                f.address = fila[6].ToString();
                f.idbusiness = Convert.ToInt32(fila[8].ToString());
                f.name = fila[9].ToString();
                

                list.Add(f);
            }
            return list;
        }
        
        public static List<Demand> getEstadisticas()
        {
            string sql = "SELECT business.name, count(*) as c FROM apporder INNER JOIN product ON product.idproduct = apporder.idproduct INNER JOIN business ON business.idbusiness = product.idbusiness group by business.name;";

            DataTable dt = Connection.realizarConsulta(sql);

            List<Demand> lista = new List<Demand>();
            foreach (DataRow fila in dt.Rows)
            {
                Demand d = new Demand();
                d.name = fila[0].ToString();
                d.quantity = Convert.ToInt32(fila[1].ToString());

                lista.Add(d);
            }
            return lista;
        }
    }
}