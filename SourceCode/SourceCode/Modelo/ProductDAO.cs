using System;
using System.Collections.Generic;
using System.Data;

namespace SourceCode.Modelo
{
    public class ProductDAO
    {
        public static List<Product> getList()
        {
            string sql = "select * from product order by idbusiness, idproduct;";

            DataTable dt = Connection.realizarConsulta(sql);

            List<Product> list = new List<Product>();
            foreach (DataRow fila in dt.Rows)
            {
                Product p = new Product();
                p.idproduct = Convert.ToInt32(fila[0].ToString());
                p.idbusiness = Convert.ToInt32(fila[1].ToString());
                p.name = fila[2].ToString();

                list.Add(p);
            }
            return list;
        }
        
        public static void addProduct(Product p)
        {
            string sql = String.Format(
                "insert into \"product\"" + 
                "(\"idbusiness\", \"name\")" +
                "values ('{0}', '{1}');",
                p.idbusiness, p.name);
                
            Connection.realizarAccion(sql);
        }
        
        public static void removeProduct(string name)
        {
            string sql = String.Format(
                "delete from product where name='{0}';",
                name);
            
            Connection.realizarAccion(sql);
        }
    }
}