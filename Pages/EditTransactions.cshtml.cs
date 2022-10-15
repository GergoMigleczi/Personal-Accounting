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
    public class EditTransactionsModel : PageModel
    {
        private readonly TransactionsContext _context;

        public EditTransactionsModel(TransactionsContext context)
        {
            _context = context;
        }

        public List<Transactions> Transactions { get; set; }

        public double HufTotal { get; set; }
        public double GbpTotal { get; set; }
        public double EurTotal { get; set; }
        public double UsdTotal { get; set; }
        public int Id { get; set; }
        [BindProperty]
        public Transactions CurrentT { get; set; }
        public double CurrentTAmount { get; set; }
        public string CurrentTCurrency { get; set; }

        public async Task OnGetAsync( int id)
        {
            Id = id;
            Transactions = await _context.Transactions.ToListAsync();
            foreach (var item in Transactions)
            {
                HufTotal += item.Huf;
                GbpTotal += item.Gbp;
                EurTotal += item.Eur;
                UsdTotal += item.Usd;

            }

            CurrentT = await _context.Transactions.FindAsync(id);
            CurrentT.ID = id;
        }


        [BindProperty]
        [Required]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Only numbers are allowed")]
        public double Amount { get; set; }
        [BindProperty]
        [Required]
        public string Description { get; set; }
        [BindProperty]
        [Required]
        [RegularExpression(@"^\d{1,2}\/\d{1,2}\/\d{1,4}(?:,\d{1,2}\/\d{1,2}\/\d{1,4})?$", ErrorMessage = "the required format is DD/MM/YYYY")]
        public string Date { get; set; }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            CurrentT = await _context.Transactions.FindAsync(id);
            if (CurrentT.Huf != 0)
            {
                CurrentTAmount = CurrentT.Huf;
                CurrentT.Huf = Amount;
            }
            else if (CurrentT.Gbp != 0)
            {
                CurrentTAmount = CurrentT.Gbp;
                CurrentT.Gbp = Amount;

            }
            else if (CurrentT.Eur != 0)
            {
                CurrentTAmount = CurrentT.Eur;
                CurrentT.Eur = Amount;

            }
            else if (CurrentT.Usd != 0)
            {
                CurrentTAmount = CurrentT.Usd;
                CurrentT.Usd = Amount;
            }
            CurrentT.ID = id;
            _context.Attach(CurrentT).State = EntityState.Modified;
            await _context.SaveChangesAsync();

                return RedirectToPage("/Index");

        }

    }
}

