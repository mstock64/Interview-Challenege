using System;
using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        [Key]
        public String Employee { get; set; }
        public Decimal Salary { get; set; }
        public String EffectiveDate { get; set; }
    }
}
