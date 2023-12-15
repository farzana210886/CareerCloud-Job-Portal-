using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    //[Table("Company_Locations")]
    //public class CompanyLocationPoco : IPoco
    //{
    //    //[Key]
    //    //public Guid Id { get; set; }

    //    //[Column("Company")]
    //    //public Guid Company { get; set; }

    //    //[Column("Country_Code")]
    //    //public string CountryCode { get; set; }

    //    //[Column("State_Province_Code")]
    //    //public string? Province { get; set; }

    //    //[Column("Street_Address")]
    //    //public string? Street { get; set; }

    //    //[Column("City_Town")]
    //    //public string? City { get; set; }

    //    //[Column("Zip_Postal_Code")]
    //    //public string? PostalCode { get; set; }

    //    //[Column("Time_Stamp")]
    //    //[NotMapped]
    //    //public byte[] TimeStamp { get; set; }

    //    //public virtual CompanyProfilePoco CompanyProfile { get; set; }
    //    //public virtual SystemCountryCodePoco SystemCountryCode { get; set; }

    //}

    [Table("Company_Locations")]
    public class CompanyLocationPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }

        [Key]
        public Guid Company { get; set; }

        [Column("Country_Code")]
        public string CountryCode { get; set; }

        [Column("State_Province_Code")]
        public string Province { get; set; }

        [Column("Street_Address")]
        public string Street { get; set; }

        [Column("City_Town")]
        public string City { get; set; }

        [Column("Zip_Postal_Code")]
        public string PostalCode { get; set; }

        [Column("Time_Stamp")]
        [NotMapped]
        public byte[] TimeStamp { get; set; }

        public virtual CompanyProfilePoco CompanyProfile { get; set; }

        public virtual SystemCountryCodePoco SystemCountryCode { get; set; }

    }
}
