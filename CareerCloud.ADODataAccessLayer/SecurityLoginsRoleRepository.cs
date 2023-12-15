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
    public class SecurityLoginsRoleRepository : IDataRepository<SecurityLoginsRolePoco>
    {
        private readonly string connectionString;
        public SecurityLoginsRoleRepository()
        {
            connectionString = ConnectionStringProvider.GetConnectionString();
        }
        public void Add(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                
                foreach (SecurityLoginsRolePoco p in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Security_Logins_Roles(Id, Login, Role) " +
                                      "Values (@Id, @Login, @Role)";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Login", p.Login);
                    cmd.Parameters.AddWithValue("@Role", p.Role);
                                    
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsRolePoco> GetAll(params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = new SqlCommand("Select * from Security_Logins_Roles", conn).ExecuteReader();

                var securityLoginsRoles = new List<SecurityLoginsRolePoco>();

                while (r.Read())
                {
                    securityLoginsRoles.Add(new SecurityLoginsRolePoco()
                    {
                        Id = (Guid)r["Id"],
                        Login = (Guid)r["Login"],
                        Role = (Guid)r["Role"],
                        TimeStamp = (byte[])r["Time_Stamp"]
                    }
                    );
                }
                return securityLoginsRoles;
            }
        }

        public IList<SecurityLoginsRolePoco> GetList(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsRolePoco GetSingle(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsRolePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (var poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Security_Logins_Roles]
                                                     WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (SecurityLoginsRolePoco p in items)
                {
                    cmd.CommandText = @"update Security_Logins_Roles
                                       set Login = @Login,
                                          Role = @Role
                                       WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Login", p.Login);
                    cmd.Parameters.AddWithValue("@Role", p.Role);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
