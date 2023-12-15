using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Company_Descriptions")]
    public class CompanyDescriptionPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Company")]
        public Guid Company { get; set; }

        [Column("Company_Name")]
        public string CompanyName { get; set; }

        [Column("Company_Description")]
        public string CompanyDescription { get; set; }

        [Column("LanguageId")]
        public string LanguageId { get; set; }

        [Column("Time_Stamp")]
        [NotMapped]
        public byte[] TimeStamp { get; set; }

        public virtual CompanyProfilePoco CompanyProfile { get; set; }
        public virtual SystemLanguageCodePoco SystemLanguageCode { get; set; }

    }
}
