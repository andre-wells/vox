using System;
using Bunit;
using Bunit.TestDoubles;
using Xunit;

using Microsoft.Extensions.DependencyInjection;

using BlazorApp.Pages;
using BlazorApp.Data;


namespace Q3aTests
{
    /// <summary>
    /// These tests are written entirely in C#.
    /// Learn more at https://bunit.egilhansen.com/docs/getting-started/
    /// </summary>
    public class InvoicePageTests : TestContext
    {
        [Fact]
        public void DemoTest()
        {
            // A demo test to show how the bUnit tests work.
            var fakeInvoiceService = new FakeInvoiceService();

            Services.AddSingleton<IInvoiceService>(fakeInvoiceService);

            Invoice[] invoices = {
                new Invoice {
                    Id = 1,
                    Date = new DateTime(2021, 3, 4),
                    TotalAmount = 200,
                    AmountPaid = 50,
                    Balance = 150
                }
            };

            fakeInvoiceService.setInvoicesToReturn(invoices);

            var component = RenderComponent<Invoices>();

            component.Find("#invoice-1-total").MarkupMatches(
                "<td id=\"invoice-1-total\">200</td>"
            );

            component.Find("#amount-received-input").Change("70");
            component.Find("#distribute-amount-button").Click();
        }

        [Fact]
        public void OnInitializedAsync_FakeDataRetrieved_DisplaysInvoices()
        {
            var fakeInvoiceService = new FakeInvoiceService();

            Services.AddSingleton<IInvoiceService>(fakeInvoiceService);

            Invoice[] invoices = {
                new Invoice {
                    Id = 1,
                    Date = new DateTime(2021, 11, 23),
                    TotalAmount = 200,
                    AmountPaid = 50,
                    Balance = 150
                },
                new Invoice {
                    Id = 2,
                    Date = new DateTime(2021, 11, 24),
                    TotalAmount = 500,
                    AmountPaid = 200,
                    Balance = 300
                },
                new Invoice {
                    Id = 3,
                    Date = new DateTime(2021, 11, 25),
                    TotalAmount = 300,
                    AmountPaid = 25,
                    Balance = 275
                }
            };

            fakeInvoiceService.setInvoicesToReturn(invoices);

            var component = RenderComponent<Invoices>();

            foreach (var item in invoices)
            {
                component.Find($"#invoice-{item.Id}-total").MarkupMatches(
                $"<td id=\"invoice-{item.Id}-total\">{item.TotalAmount}</td>");

                component.Find($"#invoice-{item.Id}-paid").MarkupMatches(
                $"<td id=\"invoice-{item.Id}-paid\">{item.AmountPaid}</td>");

                component.Find($"#invoice-{item.Id}-balance").MarkupMatches(
                $"<td id=\"invoice-{item.Id}-balance\">{item.Balance}</td>");

            }            
        }
    }
}
