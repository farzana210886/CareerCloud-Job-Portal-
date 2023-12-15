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
    public class SystemLanguageCodeRepository : IDataRepository<SystemLanguageCodePoco>
    {
        private readonly string connectionString;
        public SystemLanguageCodeRepository()
        {
            connectionString = ConnectionStringProvider.GetConnectionString();
        }
        public void Add(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                foreach (SystemLanguageCodePoco p in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into System_Language_Codes(LanguageID, Name, Native_Name) " +
                                      "Values (@LanguageID, @Name, @NativeName)";

                    cmd.Parameters.AddWithValue("@LanguageID", p.LanguageID);
                    cmd.Parameters.AddWithValue("@NativeName", p.NativeName);
                    cmd.Parameters.AddWithValue("@Name", p.Name);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = new SqlCommand("Select * from System_Language_Codes", conn).ExecuteReader();

                var systemLanguageCodes = new List<SystemLanguageCodePoco>();

                while (r.Read())
                {
                    systemLanguageCodes.Add(new SystemLanguageCodePoco()
                    {
                        LanguageID = (string)r["LanguageID"],
                        NativeName = (string)r["Native_Name"],
                        Name = (string)r["Name"]
                    }
                    );
                }
                return systemLanguageCodes;
            }
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (var poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[System_Language_Codes]
                                                     WHERE LanguageID = @LanguageID";

                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (SystemLanguageCodePoco p in items)
                {
                    cmd.CommandText = @"update System_Language_Codes
                                       set Name = @Name
                                          ,Native_Name = @NativeName
                                       where LanguageID = @LanguageID";

                    cmd.Parameters.AddWithValue("@LanguageID", p.LanguageID);
                    cmd.Parameters.AddWithValue("@Name", p.Name);
                    cmd.Parameters.AddWithValue("@NativeName", p.NativeName);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
