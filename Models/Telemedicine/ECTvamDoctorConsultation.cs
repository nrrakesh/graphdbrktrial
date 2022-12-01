using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Telemedicine
{
    public class ECTvamDoctorConsultation
    {
        public string TvamRefNo { get; set; }
        [Required, Key]
        public string TvamCustID { get; set; }
        public string Customer_id { get; set; }
        public string ConsultationId { get; set; }
        public string DoctorId { get; set; }
        public string Type { get; set; }
        public string UserDob { get; set; }
        public string UserGender { get; set; }
        public string Status { get; set; }
        public string PolicyNum { get; set; }
        public string IsPrimaryCustomer { get; set; }
        public string Createddate { get; set; }
        public string Updatetdate { get; set; }
        public string ConsultationDate { get; set; }
        public string DoctorName { get; set; }
        public string FamilyMemberId { get; set; }
        public string PaymentStatus { get; set; }
        public string StatusMessage { get; set; }
        public string TvamConsultationId { get; set; }
        public double ConsultationFee { get; set; }
        public string Remarks { get; set; }
        public double TvamAdvance { get; set; }
        public double TvamAdvanceUsed { get; set; }
        public string PaymentRefNo { get; set; }
        public string DoctorSpeciality { get; set; }
    }
}
