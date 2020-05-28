using System;
using System.Collections.Generic;
using System.Data;
using SourceCode.Controlador;

namespace SourceCode.Modelo
{
    public class AppUserDAO 
    {
        public static List<AppUser> getList()
        {
            string sql = "select * from appuser";

            DataTable dt = Connection.realizarConsulta(sql);

            List<AppUser> list = new List<AppUser>();
            foreach (DataRow fila in dt.Rows)
            {
                AppUser u = new AppUser();
                u.idUser = Convert.ToInt32(fila[0].ToString());
                u.fullname = fila[1].ToString();
                u.username = fila[2].ToString();
                u.password = fila[3].ToString();
                u.userType = Convert.ToBoolean(fila[4].ToString());

                list.Add(u);
            }
            return list;
        }
        
        public static void addAppUser(AppUser u)
        {
            u.password = Encryptor.CrearMD5(u.username);
            string sql = String.Format(
                "insert into \"appuser\"" + 
                "(\"fullname\", \"username\", \"password\", \"usertype\")" +
                "values ('{0}', '{1}', '{2}', '{3}');",
                u.fullname, u.username, u.password, u.userType);
                
            Connection.realizarAccion(sql);
        }
        
        public static void passwordChange(string username, string newPwd)
        {
            string sql = String.Format(
                "update appuser set password='{0}' where username='{1}';",
                newPwd, username);
            
            Connection.realizarAccion(sql);
        }
        
        public static void removeAppUser(string username)
        {
            string sql = String.Format(
                "delete from appuser where username='{0}';",
                username);
            
            Connection.realizarAccion(sql);
        }
    }
}