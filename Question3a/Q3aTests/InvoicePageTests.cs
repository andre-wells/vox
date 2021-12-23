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

        [Fact]
        public void DistributePaymentAndUpdateInvoices_WithOneInvoice_BalanceIsDifferenceBetweenTotalAmountAndAmountPaid()
        {
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
            component.Find("#amount-received-input").Change("50");
            component.Find("#distribute-amount-button").Click();

            component.Find("#invoice-1-paid").MarkupMatches(
                "<td id=\"invoice-1-paid\">100</td>"
            );
            component.Find("#invoice-1-balance").MarkupMatches(
                "<td id=\"invoice-1-balance\">100</td>"
            );

        }

        [Fact]
        public void DistributePaymentAndUpdateInvoices_PaymentLessThanFirstInvoiceBalance_FirstInvoiceBalanceIsDifferenceBetweenTotalAmountAndAmountPaid()
        {
            var fakeInvoiceService = new FakeInvoiceService();

            Services.AddSingleton<IInvoiceService>(fakeInvoiceService);

            Invoice[] invoices = {
                new Invoice {
                    Id = 1,
                    Date = new DateTime(2021, 3, 4),
                    TotalAmount = 200,
                    AmountPaid = 50,
                    Balance = 150
                },
                new Invoice {
                    Id = 2,
                    Date = new DateTime(2021, 3, 4),
                    TotalAmount = 100,
                    AmountPaid = 10,
                    Balance = 90
                }
            };

            fakeInvoiceService.setInvoicesToReturn(invoices);

            var component = RenderComponent<Invoices>();
            component.Find("#amount-received-input").Change("50");
            component.Find("#distribute-amount-button").Click();

            component.Find("#invoice-1-paid").MarkupMatches(
                "<td id=\"invoice-1-paid\">100</td>"
            );
            component.Find("#invoice-1-balance").MarkupMatches(
                "<td id=\"invoice-1-balance\">100</td>"
            );

            component.Find("#invoice-2-paid").MarkupMatches(
                "<td id=\"invoice-2-paid\">10</td>"
            );
            component.Find("#invoice-2-balance").MarkupMatches(
                "<td id=\"invoice-2-balance\">90</td>"
            );

        }

        [Fact]
        public void DistributePaymentAndUpdateInvoices_PaymentGreaterThanFirstInvoiceBalance_FirstInvoicePaidAndRemainderAppliedToSecondInvoice()
        {
            var fakeInvoiceService = new FakeInvoiceService();

            Services.AddSingleton<IInvoiceService>(fakeInvoiceService);

            Invoice[] invoices = {
                new Invoice {
                    Id = 1,
                    Date = new DateTime(2021, 3, 4),
                    TotalAmount = 200,
                    AmountPaid = 50,
                    Balance = 150
                },
                new Invoice {
                    Id = 2,
                    Date = new DateTime(2021, 3, 5),
                    TotalAmount = 100,
                    AmountPaid = 10,
                    Balance = 90
                }
            };

            fakeInvoiceService.setInvoicesToReturn(invoices);

            var component = RenderComponent<Invoices>();
            component.Find("#amount-received-input").Change("200");
            component.Find("#distribute-amount-button").Click();

            component.Find("#invoice-1-paid").MarkupMatches(
                "<td id=\"invoice-1-paid\">200</td>"
            );
            component.Find("#invoice-1-balance").MarkupMatches(
                "<td id=\"invoice-1-balance\">0</td>"
            );

            component.Find("#invoice-2-paid").MarkupMatches(
                "<td id=\"invoice-2-paid\">60</td>"
            );
            component.Find("#invoice-2-balance").MarkupMatches(
                "<td id=\"invoice-1-balance\">40</td>"
            );

        }

        [Fact]
        public void DistributePaymentAndUpdateInvoices_PaymentGreaterThanTwoInvoiceBalance_RemainderAppliedToThirdInvoice()
        {
            var fakeInvoiceService = new FakeInvoiceService();

            Services.AddSingleton<IInvoiceService>(fakeInvoiceService);

            Invoice[] invoices = {
                new Invoice {
                    Id = 1,
                    Date = new DateTime(2021, 3, 4),
                    TotalAmount = 200,
                    AmountPaid = 50,
                    Balance = 150
                },
                new Invoice {
                    Id = 2,
                    Date = new DateTime(2021, 3, 5),
                    TotalAmount = 100,
                    AmountPaid = 10,
                    Balance = 90
                },
                new Invoice {
                    Id = 3,
                    Date = new DateTime(2021, 3, 6),
                    TotalAmount = 700,
                    AmountPaid = 0,
                    Balance = 700
                }
            };

            fakeInvoiceService.setInvoicesToReturn(invoices);

            var component = RenderComponent<Invoices>();
            component.Find("#amount-received-input").Change("500");
            component.Find("#distribute-amount-button").Click();

            component.Find("#invoice-1-paid").MarkupMatches(
                "<td id=\"invoice-1-paid\">200</td>"
            );
            component.Find("#invoice-1-balance").MarkupMatches(
                "<td id=\"invoice-1-balance\">0</td>"
            );

            component.Find("#invoice-2-paid").MarkupMatches(
                "<td id=\"invoice-2-paid\">100</td>"
            );
            component.Find("#invoice-2-balance").MarkupMatches(
                "<td id=\"invoice-2-balance\">0</td>"
            );

            component.Find("#invoice-3-paid").MarkupMatches(
                "<td id=\"invoice-3-paid\">260</td>"
            );
            component.Find("#invoice-3-balance").MarkupMatches(
                "<td id=\"invoice-3-balance\">440</td>"
            );

        }

        [Fact]
        public void DistributePaymentAndUpdateInvoices_PaymentGreaterThanAllOutstandingBalance_LatestInvoiceBalanceBecomesNegative()
        {
            var fakeInvoiceService = new FakeInvoiceService();

            Services.AddSingleton<IInvoiceService>(fakeInvoiceService);

            Invoice[] invoices = {
                new Invoice {
                    Id = 1,
                    Date = new DateTime(2021, 3, 4),
                    TotalAmount = 200,
                    AmountPaid = 50,
                    Balance = 150
                },
                new Invoice {
                    Id = 2,
                    Date = new DateTime(2021, 3, 5),
                    TotalAmount = 100,
                    AmountPaid = 10,
                    Balance = 90
                },
                new Invoice {
                    Id = 3,
                    Date = new DateTime(2021, 3, 6),
                    TotalAmount = 700,
                    AmountPaid = 0,
                    Balance = 700
                }
            };

            fakeInvoiceService.setInvoicesToReturn(invoices);

            var component = RenderComponent<Invoices>();
            component.Find("#amount-received-input").Change("1000");
            component.Find("#distribute-amount-button").Click();

            component.Find("#invoice-1-paid").MarkupMatches(
                "<td id=\"invoice-1-paid\">200</td>"
            );
            component.Find("#invoice-1-balance").MarkupMatches(
                "<td id=\"invoice-1-balance\">0</td>"
            );

            component.Find("#invoice-2-paid").MarkupMatches(
                "<td id=\"invoice-2-paid\">100</td>"
            );
            component.Find("#invoice-2-balance").MarkupMatches(
                "<td id=\"invoice-2-balance\">0</td>"
            );

            component.Find("#invoice-3-paid").MarkupMatches(
                "<td id=\"invoice-3-paid\">700</td>"
            );
            component.Find("#invoice-3-balance").MarkupMatches(
                "<td id=\"invoice-3-balance\">-60</td>"
            );

        }
    }
}
