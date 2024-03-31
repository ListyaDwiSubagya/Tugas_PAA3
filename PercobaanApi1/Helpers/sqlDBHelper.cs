using Npgsql;
using PercobaanApi1.Models;
using System.Data;

namespace PercobaanApi1.Helpers
{
    public class SqlDBHelper
    {
        private NpgsqlConnection connection;
        private string constr;

        public SqlDBHelper(IConfiguration configuration)
        {
            constr = configuration.GetConnectionString("WebApiDatabase");
            connection = new NpgsqlConnection(constr);
        }

        public SqlDBHelper(string constr)
        {
            this.constr = constr;
            connection = new NpgsqlConnection(constr);
        }

        public NpgsqlCommand GetNpgsqlCommand(string query)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            return cmd;
        }

        public void closeConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public async Task<Admin> GetAdminByUsernameAsync(string username)
        {
            string query = "SELECT * FROM Admin WHERE username = @Username";
            using (NpgsqlCommand cmd = GetNpgsqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        return new Admin
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Username = reader["username"].ToString(),
                            Password = reader["password"].ToString()
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}