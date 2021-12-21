using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    public class InvoiceService: IInvoiceService
    // For this code test, the InvoiceService is just representative. It
    // returns randomly made up Invoices, and doesn't really save anything.
    // You can assume it works as it should and does not need fixing.
    {
        public Task<Invoice[]> GetInvoicesAsync()
        {
            var rng = new Random();

            var endDate = DateTime.Today;

            return Task.FromResult(Enumerable.Range(1, 5).Select(
                index => {
                    var ret = new Invoice
                    {
                        Id = index,
                        Date = endDate.AddMonths(-1 * index),
                        TotalAmount = rng.Next(50000, 100000) / 100.0,
                        AmountPaid = rng.Next(10000, 45000) / 100.0,
                    };
                    ret.Balance = ret.TotalAmount - ret.AmountPaid;
                    return ret;
                }
            ).ToArray());
        }

        public Task SaveInvoicesAsync(Invoice[] invoices) {
            return Task.CompletedTask;
        }
    }
}
