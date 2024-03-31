using Npgsql;
using PercobaanApi1.Helpers;
using PercobaanApi1.Models;

namespace PercobaanApi1.Models
{
    public class MuridContext
    {
        private string __constr;
        private string __ErrorMsg;

        public MuridContext(string pConstr)
        {
            __constr = pConstr;
        }

        public void AddDataMurid(Murid murid)
        {
            string query = string.Format(@"INSERT INTO murid (nama, alamat, email) VALUES ('{0}', '{1}', '{2}');",
                murid.nama, murid.alamat, murid.email);
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }
        }
        public List<Murid> ListDataMurid()
        {
            List<Murid> list1 = new List<Murid>();
            string query = string.Format(@"SELECT id_murid, nama, alamat, email FROM murid;");
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list1.Add(new Murid()
                    {
                        id_murid = int.Parse(reader["id_murid"].ToString()),
                        nama = reader["nama"].ToString(),
                        alamat = reader["alamat"].ToString(),
                        email = reader["email"].ToString()
                    });
                }
                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }
            return list1;

        }


        public void UpdateDataMurid(Murid murid)
        {
            string query = string.Format(@"UPDATE murid SET nama = '{0}', alamat = '{1}', email = '{2}' WHERE id_murid = {3};",
                murid.nama, murid.alamat, murid.email, murid.id_murid);
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
                throw;
            }
        }

        public void DeleteDataMurid(int id)
        {
            string query = string.Format(@"DELETE FROM murid WHERE id_murid = {0};", id);
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
                throw;
            }
        }




    }


}
