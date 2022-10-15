using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalAccounting.Models;
using PersonalAccounting.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PersonalAccounting.Pages
{
    public class EditStocksModel : PageModel
    {
        private readonly TransactionsContext _context;

        public EditStocksModel(TransactionsContext context)
        {
            _context = context;
        }

        public List<Stocks> Stocks { get; set; }

        public double HufTotal { get; set; }
        public double GbpTotal { get; set; }
        public double EurTotal { get; set; }
        public double UsdTotal { get; set; }
        [BindProperty]
        public Stocks CurrentS { get; set; }
        public double CurrentSAmount { get; set; }
        public string BuySell { get; set; }

        public async Task OnGetAsync( int id)
        {
            CurrentS = await _context.Stocks.FindAsync(id);
            if(CurrentS.Buy != 0)
            {
                CurrentSAmount = CurrentS.Buy;
                BuySell = "buy";
            }else if ( CurrentS.Sell != 0)
            {
                CurrentSAmount = CurrentS.Sell;
                BuySell = "sell";
            }
        }


        
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

        public IActionResult OnPost(int id)
        {
                switch (BuySellStock)
                {
                    case "buy":
                    CurrentS.Buy = AmountStock;
                        break;
                    case "sell":
                    CurrentS.Sell = AmountStock;
                        break;
                }
            CurrentS.ID = id;
                _context.Attach(CurrentS).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToPage();
        }

    }
}
 
