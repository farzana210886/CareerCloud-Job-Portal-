using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Educations")]
    public class ApplicantEducationPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Applicant")]
        public Guid Applicant { get; set; }

        [Column("Certificate_Diploma")]
        public String CertificateDiploma { get; set; }

        [Column("Start_Date")]
        public DateTime? StartDate { get; set; }

        [Column("Completion_Date")]
        public DateTime? CompletionDate { get; set; }

        [Column("Completion_Percent")]
        public byte? CompletionPercent { get; set; }

        [Column("Time_Stamp")]
        [NotMapped]
        public byte[] TimeStamp { get; set; }

        [Column("Major")]
        public String Major { get; set; }

        public virtual ApplicantProfilePoco ApplicantProfile { get; set; }
    }
}