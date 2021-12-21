using System;
using System.Threading.Tasks;

using BlazorApp.Data;


namespace Q3aTests
{
    public class FakeInvoiceService: IInvoiceService
    // A fake invoice service that can be used to test the invoice page easily.
    {
        private Invoice[] invoicesToReturn = new Invoice[0];
        private Invoice[] savedInvoices = new Invoice[0];

        public Task<Invoice[]> GetInvoicesAsync()
        {
            return Task.FromResult(invoicesToReturn);
        }

        public Task SaveInvoicesAsync(Invoice[] invoices) {
            this.savedInvoices = invoices;
            return Task.CompletedTask;
        }

        public void setInvoicesToReturn(Invoice[] invoices) {
            this.invoicesToReturn = invoices;
        }

        public Invoice[] getSavedInvoices() {
            return savedInvoices;
        }

        public void reset() {
            invoicesToReturn = new Invoice[0];
            savedInvoices = new Invoice[0];
        }
    }
}