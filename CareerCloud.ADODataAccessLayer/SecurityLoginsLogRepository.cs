using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginsLogRepository : IDataRepository<SecurityLoginsLogPoco>
    {
        private readonly string connectionString;
        public SecurityLoginsLogRepository()
        {
            connectionString = ConnectionStringProvider.GetConnectionString();
        }
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {

                foreach (SecurityLoginsLogPoco p in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Security_Logins_Log (Id, Login, Source_IP, Logon_Date, Is_Succesful) " +
                                      "Values (@Id, @Login, @SourceIP, @LogonDate, @IsSuccesful)";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Login", p.Login);
                    cmd.Parameters.AddWithValue("@SourceIP", p.SourceIP);
                    cmd.Parameters.AddWithValue("@LogonDate", p.LogonDate);
                    cmd.Parameters.AddWithValue("@IsSuccesful", p.IsSuccesful);
                   
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = new SqlCommand("Select * from Security_Logins_Log", conn).ExecuteReader();

                var securityLoginsLogs = new List<SecurityLoginsLogPoco>();

                while (r.Read())
                {
                    securityLoginsLogs.Add(new SecurityLoginsLogPoco()
                    {
                        Id = (Guid)r["Id"],
                        Login = (Guid)r["Login"],
                        SourceIP = (string)r["Source_IP"],
                        LogonDate = (DateTime)r["Logon_Date"],
                        IsSuccesful = (bool)r["Is_Succesful"],
                    }
                    );
                }
                return securityLoginsLogs;
            }
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (SecurityLoginsLogPoco poco in items)
                {
                    cmd.CommandText = @"delete from [dbo].[Security_Logins_Log]
                                                     where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (SecurityLoginsLogPoco p in items)
                {
                    cmd.CommandText = @"update Security_Logins_Log
                                       set Login = @Login
                                          ,Source_IP = @SourceIP
                                          ,Logon_Date = @LogonDate
                                          ,Is_Succesful = @IsSuccesful
                                       WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Login", p.Login);
                    cmd.Parameters.AddWithValue("@SourceIP", p.SourceIP);
                    cmd.Parameters.AddWithValue("@LogonDate", p.LogonDate);
                    cmd.Parameters.AddWithValue("@IsSuccesful", p.IsSuccesful);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
