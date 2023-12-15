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
    public class CompanyJobRepository : IDataRepository<CompanyJobPoco>
    {
        private readonly string connectionString;
        public CompanyJobRepository()
        {
            connectionString = ConnectionStringProvider.GetConnectionString();
        }
        public void Add(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
               
                foreach (CompanyJobPoco p in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Company_Jobs(Id, Company, Profile_Created, Is_Inactive, Is_Company_Hidden) " +
                                      "Values (@Id, @Company, @ProfileCreated, @IsInactive, @IsCompanyHidden)";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Company", p.Company);
                    cmd.Parameters.AddWithValue("@ProfileCreated", p.ProfileCreated);
                    cmd.Parameters.AddWithValue("@IsInactive", p.IsInactive);
                    cmd.Parameters.AddWithValue("@IsCompanyHidden", p.IsCompanyHidden);
                 
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = new SqlCommand("Select * from Company_Jobs", conn).ExecuteReader();

                var companyJobs = new List<CompanyJobPoco>();

                while (r.Read())
                {
                    companyJobs.Add(new CompanyJobPoco()
                    {
                        Id = (Guid)r["Id"],
                        Company = (Guid)r["Company"],
                        ProfileCreated = (DateTime)r["Profile_Created"],
                        IsInactive = (bool)r["Is_Inactive"],
                        IsCompanyHidden = (bool)r["Is_Company_Hidden"],
                        TimeStamp = (byte[])r["Time_Stamp"]
                        //TimeStamp = Convert.IsDBNull(r["TimeStamp"]) ? null : (byte[])r["TimeStamp"]
                    }
                    );
                }
                return companyJobs;
            }
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (var poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Jobs]
                                                     WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (CompanyJobPoco p in items)
                {
                    cmd.CommandText = @"update Company_Jobs
                                       set Company = @Company
                                          ,Profile_Created = @ProfileCreated
                                          ,Is_Inactive = @IsInactive
                                          ,Is_Company_Hidden = @IsCompanyHidden
                                       WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Company", p.Company);
                    cmd.Parameters.AddWithValue("@ProfileCreated", p.ProfileCreated);
                    cmd.Parameters.AddWithValue("@IsInactive", p.IsInactive);
                    cmd.Parameters.AddWithValue("@IsCompanyHidden", p.IsCompanyHidden);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
