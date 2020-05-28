using System;
using System.Collections.Generic;
using System.Data;

namespace SourceCode.Modelo
{
    public class BusinessDAO 
    {
        public static List<Business> getList()
        {
            string sql = "select * from business";

            DataTable dt = Connection.realizarConsulta(sql);

            List<Business> list = new List<Business>();
            foreach (DataRow fila in dt.Rows)
            {
                Business b = new Business();
                b.idbusiness = Convert.ToInt32(fila[0].ToString());
                b.name = fila[1].ToString();
                b.description = fila[2].ToString();

                list.Add(b);
            }
            return list;
        }
        
        public static void addBusiness(Business b)
        {
            string sql = String.Format(
                "insert into \"business\"" + 
                "(\"name\", \"description\")" +
                "values ('{0}', '{1}');",
                b.name, b.description);
                
            Connection.realizarAccion(sql);
        }
        
        public static void removeBusiness(string name)
        {
            string sql = String.Format(
                "delete from business where name='{0}';",
                name);
            
            Connection.realizarAccion(sql);
        }
    }
}