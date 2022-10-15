using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalAccounting.Models
{
    public class Transactions
    {
        public int ID { get; set; }

        [Required]
        [RegularExpression(@"^\d{1,2}\/\d{1,2}\/\d{1,4}(?:,\d{1,2}\/\d{1,2}\/\d{1,4})?$", ErrorMessage = "the required format is DD/MM/YYYY")]
        public string Date { get; set; }
        [Required]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Only numbers are allowed")]
        public double Huf { get; set; }
        [Required]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Only numbers are allowed")]
        public double Gbp { get; set; }
        [Required]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Only numbers are allowed")]
        public double Eur { get; set; }
        [Required]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Only numbers are allowed")]
        public double Usd { get; set; }
        [Required]
        public string Description { get; set; }
        

        
        public Transactions( string Date, double Huf = 0.0, double Gbp = 0.0, double Eur = 0.0, double Usd = 0.0, string Description="")
        {
            this.Date = Date;
            this.Huf = Huf;
            this.Gbp = Gbp;
            this.Eur = Eur;
            this.Usd = Usd;
            this.Description = Description;
        }

        public Transactions()
        {

        }
    }
}
