using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalAccounting.Data;
using PersonalAccounting.Models;
using Microsoft.EntityFrameworkCore;

namespace PersonalAccounting.Pages
{
    public class StocksModel : PageModel
    {
        private readonly TransactionsContext _context;

        public StocksModel (TransactionsContext context)
        {
            _context = context;
        }

        public List<Stocks> Stocks { get; set; }

        public async Task OnGetAsync()
        {
            Stocks = await _context.Stocks.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Stocks Stocks= await _context.Stocks.FindAsync(id);
            _context.Stocks.Remove(Stocks);
            await _context.SaveChangesAsync();
            return RedirectToPage();

        }
    }
}
