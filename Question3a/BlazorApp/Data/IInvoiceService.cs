using System.Threading.Tasks;


namespace BlazorApp.Data
{
    public interface IInvoiceService
    {
        // Retrieve Invoices to display.
        public Task<Invoice[]> GetInvoicesAsync();

        // Save updated Invoices.
        public Task SaveInvoicesAsync(Invoice[] invoices);
    }
}