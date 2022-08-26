namespace GarageRelation.API.Configuration
{
    public class MySqlConfiguration
    {
        public string Host { get; set; } = default!;
        public string Port { get; set; } = default!;
        public string User { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Database { get; set; } = default!;

        public string ConnectionString
        {
            get
            {
                return $"server={Host},{Port};user={User};password={Password};database={Database}";
            }
        }
    }
}