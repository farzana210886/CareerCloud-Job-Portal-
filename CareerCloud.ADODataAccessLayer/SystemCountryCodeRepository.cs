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
    public class SystemCountryCodeRepository : IDataRepository<SystemCountryCodePoco>
    {
        private readonly string connectionString;
        public SystemCountryCodeRepository()
        {
            connectionString = ConnectionStringProvider.GetConnectionString();
        }
        public void Add(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SystemCountryCodePoco p in items)
                {
                    cmd.CommandText = "Insert into System_Country_Codes (Code, Name) " +
                                     "Values (@Code, @Name)";

                    cmd.Parameters.AddWithValue("@Code", p.Code);
                    cmd.Parameters.AddWithValue("@Name", p.Name);

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

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = new SqlCommand("Select * from System_Country_Codes", conn).ExecuteReader();

                var systemCountryCodes = new List<SystemCountryCodePoco>();

                while (r.Read())
                {
                    systemCountryCodes.Add(new SystemCountryCodePoco()
                    {
                        Code = (string)r["Code"],
                        Name = (string)r["Name"]
                    }
                    );
                }
                return systemCountryCodes;
            }
        }

        public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemCountryCodePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (var poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[System_Country_Codes]
                                                     WHERE Code = @Code";

                    cmd.Parameters.AddWithValue("@Code", poco.Code);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (SystemCountryCodePoco p in items)
                {
                    cmd.CommandText = @"update System_Country_Codes
                                        set Name = @Name
                                       where Code = @Code";

                    cmd.Parameters.AddWithValue("@Code", p.Code);
                    cmd.Parameters.AddWithValue("@Name", p.Name);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
