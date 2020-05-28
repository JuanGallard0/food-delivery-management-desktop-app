namespace SourceCode.Modelo
{
    public class AppUser
    {
        public int idUser { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool userType { get; set; }

        public AppUser(string fullname, string username, string password, bool userType)
        {
            this.fullname = fullname;
            this.username = username;
            this.password = password;
            this.userType = userType;
        }

        public AppUser()
        {
        }
    }
}