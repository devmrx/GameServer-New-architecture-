using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServerData.Model;

namespace GameServerData {
    public class UserDAL : IRepository<User> {

        private SqlConnection sqlCn = null;
        private string _connectionstring = @"Data Source=DESKTOP-G4BL3RC;Initial Catalog=GameServer;Integrated Security=True";


        public void OpenConnection() {  // param string connectionstring
            sqlCn = new SqlConnection();
            sqlCn.ConnectionString = _connectionstring;
            sqlCn.Open();
        }

        public void OpenConnection(string connectionstring) {  // param string connectionstring
            sqlCn = new SqlConnection();
            sqlCn.ConnectionString = connectionstring;
            sqlCn.Open();
        }

        //public void InsertAuto(int id, string color, string make, string petName) {
        //    
        //    string sql = string.Format("Insert Into Inventory" +
        //                               "(CarlD, Make, Color, PetName) Values" +
        //                               "('{0}', '{1}', '{2}', '{3}')", id, make, color, petName);
        //    // Выполнить SQL-оператор с применением нашего подключения.
        //    using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn)) {
        //        cmd.ExecuteNonQuery();
        //    }
        //}

        //public void InsertAuto(NewCar car) {
        //    // Сформировать SQL-оператор.
        //    string sql = string.Format("Insert Into Inventory" +
        //                               "(CarID, Make, Color, PetName) Values" +
        //                               "(’{0}', '{1}', '{2}', '{3}')", car.CarID, car.Make, car.Color, car.PetName);
        //    // Выполнить SQL-оператор с применением нашего подключения. using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
        //    {
        //        cmd.ExecuteNonQuery();
        //    }
        //}

        public void Delete(int id) {
            
            string sql = $"Delete from Users where Id = '{id}'";

            using (SqlCommand cmd = new SqlCommand(sql, sqlCn)) {
                try {
                    cmd.ExecuteNonQuery();
                } catch (SqlException ex) {
                    Exception error = new Exception("Sorry! That user is not found in database!", ex);
                    throw error;
                }
            }
        }

        public void UpdateBanned(int id, bool banned) {

            string sql = $"Update Users Set Banned = '{banned}' Where Id = '{id}'";
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn)) {
                cmd.ExecuteNonQuery();
            }
        }

        public List<User> GetAllList() {
            // Здесь будут храниться записи.
            List<User> users = new List<User>();
            // Подготовить объект команды.
            string sql = "Select * From Users";
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn)) {
                SqlDataReader dr = cmd.ExecuteReader(); while (dr.Read()) {
                    users.Add(new User {
                        Id = (int)dr["Id"],
                        Login = (string)dr["Login"],
                        Email = (string)dr["Email"],
                        PasswordHash = (string)dr["PasswordHash"],
                        IsBanned = (bool)dr["Banned"],
                        //LastGameSession = (DateTime?)dr["LastGameSession"]
                    });
                }
                dr.Close();
            }
            return users;
        }

        // Автономный уровень
        public DataTable GetAllDataTable() {
            // Здесь будут храниться записи.
            DataTable users = new DataTable();
            // Подготовить объект команды.
            string sql = "Select * From Users";
            using (SqlCommand cmd = new SqlCommand(sql, sqlCn)) {
                SqlDataReader dr = cmd.ExecuteReader();
                // Заполнить DataTable данными из объекта чтения и выполнить очистку.
                users.Load(dr);
                dr.Close();
            }
            return users;
        }

        public void CloseConnection() {
            sqlCn.Close();
        }
    }
}
