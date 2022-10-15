using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalAccounting.Data;
using PersonalAccounting.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PersonalAccounting.Pages
{
    public class AddTransactionsModel : PageModel
    {
        private readonly TransactionsContext _context;

        public AddTransactionsModel(TransactionsContext context)
        {
            _context = context;
        }

        public List<Transactions> Transactions { get; set; }

        public double HufTotal { get; set; }
        public double GbpTotal { get; set; }
        public double EurTotal { get; set; }
        public double UsdTotal { get; set; }
        public string Name { get; set; }

        public async Task OnGetAsync(string name)
        {
            Name = name;
            Transactions = await _context.Transactions.ToListAsync();
            foreach (var item in Transactions)
            {
                HufTotal += item.Huf;
                GbpTotal += item.Gbp;
                EurTotal += item.Eur;
                UsdTotal += item.Usd;
            }
        }


        [BindProperty]
        [Required]
        public double Amount { get; set; }
        [BindProperty]
        [Required]
        [RegularExpression(@"(gbp)|(huf)|(usd)|(eur)", ErrorMessage = "gbp, usd, eur, huf")]
        public string Currency { get; set; }
        [BindProperty]
        [Required]
        public string Description { get; set; }
        [BindProperty]
        [Required]
        [RegularExpression(@"^\d{1,2}\/\d{1,2}\/\d{1,4}(?:,\d{1,2}\/\d{1,2}\/\d{1,4})?$", ErrorMessage = "the required format is DD/MM/YYYY")]
        public string Date { get; set; }

        public Transactions t { get; set; }

        [BindProperty]
        [Required]
        [RegularExpression(@"^\d{1,2}\/\d{1,2}\/\d{1,4}(?:,\d{1,2}\/\d{1,2}\/\d{1,4})?$", ErrorMessage = "the required format is DD/MM/YYYY")]
        public string DateStock { get; set; }
        [BindProperty]
        [Required]
        [RegularExpression(@"[A-Z 0-9]+", ErrorMessage = "Only uppercase letters and numbers are allowed")]
        public string NameStock { get; set; }
        [BindProperty]
        [Required]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Only numbers are allowed")]
        public double AmountStock { get; set; }
        [BindProperty]
        [Required]
        [RegularExpression(@"(buy)|(sell)", ErrorMessage = "buy or sell")]
        public string BuySellStock { get; set; }
        [BindProperty]
        [Required]
        public string DescriptionStock { get; set; }
        public Stocks s { get; set; }

        public IActionResult OnPost(string name)
        {
            
            if (name == "transactions")
            {
                switch (Currency.ToLower())
                {
                    case "huf":
                        t = new Transactions(Date: Date, Huf: Amount, Description: Description);
                        break;
                    case "gbp":
                        t = new Transactions(Date: Date, Gbp: Amount, Description: Description);
                        break;
                    case "eur":
                        t = new Transactions(Date: Date, Eur: Amount, Description: Description);
                        break;
                    case "usd":
                        t = new Transactions(Date: Date, Usd: Amount, Description: Description);
                        break;

                }
                _context.Transactions.Add(t);
                _context.SaveChanges();
                return RedirectToPage("/Index");
            }
            else if(name == "stocks")
            {
                switch (BuySellStock.ToLower())
                {
                    case "buy":
                        s = new Stocks(DateStock, NameStock, AmountStock, description: DescriptionStock);
                        break;
                    case "sell":
                        s = new Stocks(DateStock, NameStock, sell: AmountStock, description: DescriptionStock);
                        break;
                }
                _context.Stocks.Add(s);
                _context.SaveChanges();
                return RedirectToPage("/Index");
            }
            return Page();
        }

    }
}

