using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace ValidVacations.Controllers
{
    public class DatabaseController : Controller
    {
        public NpgsqlDataSource? DataSource { get; private set; }
        public NpgsqlConnection Connection { get; private set; }

        private DatabaseController()
        {
        }

        public void Initialize()
        {
            GetDataSource();
            GetConnection();
        }

        private void GetConnection()
        {
            var connectionString = "Host=localhost;Username=postgres;Password=qwerty;Database=db_0322";
            Connection = new NpgsqlConnection(connectionString);
            Connection.Open();
        }

        private void GetDataSource()
        {
            var connectionString = "Host=localhost;Username=postgres;Password=qwerty;Database=db_0322";
            DataSource = NpgsqlDataSource.Create(connectionString);
        }

        public void ExecuteNonQuery(string name, string email)
        {
            string sql = string.Format("INSERT INTO public.customer (name, email) VALUES ('{0}', '{1}')", name, email);
            var command = DataSource?.CreateCommand(sql);
            command?.ExecuteNonQuery();
        }

        public string ExecuteReader()
        {
            string result = string.Empty;
            string sql = string.Format("SELECT * FROM public.customer");
            var command = DataSource?.CreateCommand(sql);
            var reader = command?.ExecuteReader();

            while (reader.Read())
            {
                //result += string.Format("Name: {0} Email: {1}\n", reader[NameColumn], reader[EmailColumn]);
            }

            reader?.Dispose();
            return result;
        }
    }
}
