using System;
using PersonalAccounting.Models;
using System.Linq;
using System.Collections.Generic;

namespace PersonalAccounting.Data
{
    //------------------SAMPLE DATA---------------------
    public static class DbInitializer
    {
        public static void Initialize(TransactionsContext context)
        {
            context.Database.EnsureCreated();
            //DbInitializer.Initialize(context);

            // Look for any Transactions.
            if (context.Transactions.Any())
            {
                return;   // DB has been seeded
            }

            Transactions[] T = new Transactions[]
            {
                new Transactions ("11/09/2022", Huf: 100, Description: "Sample data" ),
                new Transactions ("11/09/2022", Gbp: 100, Description: "Sample data" ),
                new Transactions ("11/09/2022", Eur: 100, Description: "Sample data"),
                new Transactions ("11/09/2022", Usd: 100, Description: "Sample data" )
            };
            foreach(Transactions item in T)
            {
                context.Transactions.Add(item);
            }
            context.SaveChanges();

            Stocks[] S = new Stocks[]
            {
                new Stocks ("15/09/2022", "APPL", sell: 110, description: "Sold, Profit=10" ),
                new Stocks ("14/09/2022", "APPL",buy: 100, description: "Capital")
            };
            foreach (Stocks item in S)
            {
                context.Stocks.Add(item);
            }
            context.SaveChanges();
        }
    }
    //------------------SAMPLE DATA---------------------
}
