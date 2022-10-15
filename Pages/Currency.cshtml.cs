using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PersonalAccounting.Data;
using PersonalAccounting.Models;

namespace PersonalAccounting.Pages
{
    public class CurrencyModel : PageModel
    {
        private readonly TransactionsContext _context;

        public CurrencyModel(TransactionsContext context)
        {
            _context = context;
        }

        public List<Transactions> Transactions { get; set; }

        public string Currency { get; set; }
        public double Total { get; private set; }

        public async Task OnGetAsync(string name)
        {
            ListTheMonths();
            Currency = name;
            
            Transactions = await _context.Transactions.ToListAsync();
            switch (name)
            {
                case "eur":
                    foreach (var item in Transactions)
                    {
                        if (item.Eur != 0.0)
                        {
                            Total += (double)item.Eur;
                        }
                    }
                    break;
                case "huf":
                    foreach (var item in Transactions)
                    {
                        if (item.Huf != 0.0)
                        {
                            Total += (double)item.Huf;
                        }
                    }
                    break;
                case "usd":
                    foreach (var item in Transactions)
                    {
                        if (item.Usd != 0.0)
                        {
                            Total += (double)item.Usd;
                        }
                    }
                    break;
                case "gbp":
                    foreach (var item in Transactions)
                    {
                        if (item.Gbp != 0.0)
                        {
                            Total += (double)item.Gbp;
                        }
                    }
                    break;
            }
        
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
           
            Transactions Transaction = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(Transaction);

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public List<string> Months { get; set; }
        public void ListTheMonths()
        {
            Months = new List<string>();

            List<Transactions> t = _context.Transactions.ToList();

            foreach(Transactions item in t)
            {
                int start = item.Date.IndexOf("/");
                int lenght = item.Date.LastIndexOf("/")-start; // 09/22/2022
                string month = item.Date.Substring(start+1, lenght).PadLeft(2,'0');
                if (!Months.Contains(month))
                    Months.Add(month);
            }
        }
    }
}
