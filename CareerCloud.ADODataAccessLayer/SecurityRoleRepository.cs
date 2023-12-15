using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityRoleRepository : IDataRepository<SecurityRolePoco>
    {
        private readonly string connectionString;
        public SecurityRoleRepository()
        {
            connectionString = ConnectionStringProvider.GetConnectionString();
        }
        public void Add(params SecurityRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
               
                foreach (SecurityRolePoco p in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Security_Roles (Id, Is_Inactive, Role) " +
                                      "Values (@Id, @IsInactive, @Role)";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@IsInactive", p.IsInactive);
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

        public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = new SqlCommand("Select * from Security_Roles", conn).ExecuteReader();
                var securityRoles = new List<SecurityRolePoco>();

                while (r.Read())
                {
                    securityRoles.Add(new SecurityRolePoco()
                    {
                        Id = (Guid)r["Id"],
                        IsInactive = (bool)r["Is_Inactive"],
                        Role = (string)r["Role"]
                    }
                    );
                }
                return securityRoles;
            }
        }

        public IList<SecurityRolePoco> GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityRolePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (var poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Security_Roles]
                                                     WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SecurityRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (SecurityRolePoco p in items)
                {
                    cmd.CommandText = @"update Security_Roles
                                       set Id = @Id
                                          ,Role = @Role
                                          ,Is_Inactive = @IsInactive
                                       where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Role", p.Role);
                    cmd.Parameters.AddWithValue("@IsInactive", p.IsInactive);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
