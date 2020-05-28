using System;
using System.Collections.Generic;
using System.Data;

namespace SourceCode.Modelo
{
    public class AddressDAO
    {
        public static List<Address> getList()
        {
            string sql = "select * from address;";

            DataTable dt = Connection.realizarConsulta(sql);

            List<Address> list = new List<Address>();
            foreach (DataRow fila in dt.Rows)
            {
                Address a = new Address();
                a.idaddress = Convert.ToInt32(fila[0].ToString());
                a.iduser = Convert.ToInt32(fila[1].ToString());
                a.address = fila[2].ToString();

                list.Add(a);
            }
            return list;
        }
        
        public static List<Address> getUserList(int iduser)
        {
            string sql = String.Format("SELECT * FROM address INNER JOIN appuser ON appuser.iduser = address.iduser where appuser.iduser = '{0}';",
                iduser);

            DataTable dt = Connection.realizarConsulta(sql);

            List<Address> list = new List<Address>();
            foreach (DataRow fila in dt.Rows)
            {
                Address a = new Address();
                a.idaddress = Convert.ToInt32(fila[0].ToString());
                a.iduser = Convert.ToInt32(fila[1].ToString());
                a.address = fila[2].ToString();

                list.Add(a);
            }
            return list;
        }
        public static void addAddress(Address a)
        {
            string sql = String.Format(
                "insert into \"address\"" + 
                "(\"iduser\", \"address\")" +
                "values ('{0}', '{1}');",
                a.iduser, a.address);
                
            Connection.realizarAccion(sql);
        }
        
        public static void removeAddress(int idaddress)
        {
            string sql = String.Format(
                "delete from address where idaddress='{0}';",
                idaddress);
            
            Connection.realizarAccion(sql);
        }
    }
}