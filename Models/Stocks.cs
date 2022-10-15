using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalAccounting.Models
{
    public class Stocks
    {
        public int ID { get; set; }
        [Required]
        [RegularExpression(@"^\d{1,2}\/\d{1,2}\/\d{1,4}(?:,\d{1,2}\/\d{1,2}\/\d{1,4})?$", ErrorMessage = "the required format is DD/MM/YYYY")]
        public string Date { get; set; }
        [Required]
        [RegularExpression(@"[A-Z 0-9]+", ErrorMessage ="Only uppercase letters and numbers are allowed")]
        public string StockName { get; set; }
        [Required]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Only numbers are allowed")]
        public double Buy { get; set; }
        [Required]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Onlynumbers are allowed")]
        public double Sell { get; set; }
        [Required]
        public string Description { get; set; }

        
        public Stocks(string date, string stockName, double buy=0.0, double sell=0.0, string description="")
        {
            this.Date = date;
            this.StockName = stockName;
            this.Buy = buy;
            this.Sell = sell;
            this.Description = description;
        }
        public Stocks()
        {

        }
    }
}
