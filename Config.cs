namespace T10
{
    public class Config
    {
        public string conStr;
        public Config()
        {
            FileStream f = new (@"config.cfg", FileMode.OpenOrCreate);
            StreamReader s = new(f);
            string dataSource = s.ReadLine() + "";
            string catalog = s.ReadLine() + "";
            string user = s.ReadLine() + "";
            string password = s.ReadLine() + "";
            conStr = dataSource + catalog + user + password;
            s.Close();
        }            
    }
}
