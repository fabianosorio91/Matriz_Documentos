using Microsoft.Extensions.Configuration;

namespace Infraestructura.Datos.Repositorios
{
    
    public class Conexion
    {
        private string connectionString;
        public Conexion(IConfiguration iconfiguration)
        {
            connectionString = iconfiguration.GetConnectionString("CadenaSQL");
        }

        public string GetConnectionString() { return connectionString; }
    }
}
