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
    public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>
    {
        private readonly string connectionString;
        public SecurityLoginRepository()
        {
            connectionString = ConnectionStringProvider.GetConnectionString();
        }
        public void Add(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
               
                foreach (SecurityLoginPoco p in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Security_Logins(Id, Created_Date, Password_Update_Date, Agreement_Accepted_Date, Is_Locked, Is_Inactive, Email_Address, Phone_Number, Full_Name, Force_Change_Password, Prefferred_Language, Login, Password) " +
                                      "Values (@Id, @CreatedDate, @PasswordUpdateDate, @AgreementAcceptedDate, @IsLocked, @IsInactive, @EmailAddress, @PhoneNumber, @FullName, @ForceChangePassword, @PrefferredLanguage, @Login, @Password)";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@CreatedDate", p.Created);
                    cmd.Parameters.AddWithValue("@PasswordUpdateDate", p.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@AgreementAcceptedDate", p.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@IsLocked", p.IsLocked);
                    cmd.Parameters.AddWithValue("@IsInactive", p.IsInactive);
                    cmd.Parameters.AddWithValue("@EmailAddress", p.EmailAddress);
                    cmd.Parameters.AddWithValue("@PhoneNumber", p.PhoneNumber);
                    cmd.Parameters.AddWithValue("@FullName", p.FullName);
                    cmd.Parameters.AddWithValue("@ForceChangePassword", p.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@PrefferredLanguage", p.PrefferredLanguage);
                    cmd.Parameters.AddWithValue("@Login", p.Login);
                    cmd.Parameters.AddWithValue("@Password", p.Password);


                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = new SqlCommand("Select * from Security_Logins", conn).ExecuteReader();

                var securityLoginInfo = new List<SecurityLoginPoco>();

                while (r.Read())
                {
                    securityLoginInfo.Add(new SecurityLoginPoco()
                    {
                        Id = (Guid)r["Id"],
                        Created = (DateTime)r["Created_Date"],                       
                        PasswordUpdate = r.IsDBNull(r.GetOrdinal("Password_Update_Date")) ? (DateTime?)null : (DateTime)r["Password_Update_Date"],                       
                        AgreementAccepted = r.IsDBNull(r.GetOrdinal("Agreement_Accepted_Date")) ? (DateTime?)null : (DateTime)r["Agreement_Accepted_Date"],
                        IsLocked = (bool)r["Is_Locked"],
                        IsInactive = (bool)r["Is_Inactive"],
                        EmailAddress = (string)r["Email_Address"],                      
                        PhoneNumber = r.IsDBNull(r.GetOrdinal("Phone_Number")) ? (string?)null : (string)r["Phone_Number"],                      
                        FullName = r.IsDBNull(r.GetOrdinal("Full_Name")) ? (string?)null : (string)r["Full_Name"],
                        ForceChangePassword = (bool)r["Force_Change_Password"],                      
                        PrefferredLanguage = r.IsDBNull(r.GetOrdinal("Prefferred_Language")) ? (string?)null : (string)r["Prefferred_Language"],
                        Login = (string)r["Login"],
                        Password = (string)r["Password"],
                        TimeStamp = (byte[])r["Time_Stamp"]
                    }
                    );
                }
                return securityLoginInfo;
            }
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();

            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (var poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Security_Logins]
                                                     WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (SecurityLoginPoco p in items)
                {
                    cmd.CommandText = @"update Security_Logins
                                       set Login = @Login
                                          ,Password = @Password
                                          ,Created_Date = @CreatedDate
                                          ,Password_Update_Date = @PasswordUpdateDate
                                          ,Agreement_Accepted_Date = @AgreementAcceptedDate
                                          ,Is_Locked = @IsLocked
                                          ,Is_Inactive = @IsInactive
                                          ,Email_Address = @EmailAddress
                                          ,Phone_Number = @PhoneNumber
                                          ,Full_Name = @FullName
                                          ,Force_Change_Password = @ForceChangePassword
                                          ,Prefferred_Language = @PrefferredLanguage
                                       WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", p.Id);
                    cmd.Parameters.AddWithValue("@Login", p.Login);
                    cmd.Parameters.AddWithValue("@Password", p.Password);
                    cmd.Parameters.AddWithValue("@CreatedDate", p.Created);
                    cmd.Parameters.AddWithValue("@PasswordUpdateDate", p.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@AgreementAcceptedDate", p.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@IsLocked", p.IsLocked);
                    cmd.Parameters.AddWithValue("@IsInactive", p.IsInactive);
                    cmd.Parameters.AddWithValue("@EmailAddress", p.EmailAddress);
                    cmd.Parameters.AddWithValue("@PhoneNumber", p.PhoneNumber);
                    cmd.Parameters.AddWithValue("@FullName", p.FullName);
                    cmd.Parameters.AddWithValue("@ForceChangePassword", p.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@PrefferredLanguage", p.PrefferredLanguage);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
