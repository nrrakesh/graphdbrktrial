using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDBIntegration.Models.Customer
{
    public class QuestionResponse
    {
        [Required, Key]
        public string CustomerId { get; set; }

        public string Education { get; set; }

        public string Profession { get; set; }

        public string BusinessCategory { get; set; }

        public string MaritalStatus { get; set; }

        public long Children { get; set; }

        public long SchoolGoingKids { get; set; }

        public long Dependent { get; set; }

        public long NoOfSeniorCitizen { get; set; }

        public string Anniversary { get; set; }

        public string Vehicle { get; set; }

        public string Income { get; set; }

        public string SocialMedia { get; set; }

        public string CreatedDate { get; set; }

        public string UpdatedDateTime { get; set; }
    }
}
