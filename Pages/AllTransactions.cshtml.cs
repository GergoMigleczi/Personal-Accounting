using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalAccounting.Models;
using PersonalAccounting.Data;
using Microsoft.EntityFrameworkCore;

namespace PersonalAccounting.Pages
{
    public class AllTransactionsModel : PageModel
    {
        private readonly TransactionsContext _context;

        public AllTransactionsModel(TransactionsContext context)
        {
            _context = context;
        }

        public List<Transactions> Transactions { get; set; }

        public double HufTotal { get; set; }
        public double GbpTotal { get; set; }
        public double EurTotal { get; set; }
        public double UsdTotal { get; set; }

        public async Task OnGetAsync()
        {

            Transactions = await _context.Transactions.ToListAsync();
            foreach (var item in Transactions)
            {
                HufTotal += item.Huf;
                GbpTotal += item.Gbp;
                EurTotal += item.Eur;
                UsdTotal += item.Usd;
            }
        }
    }
}
