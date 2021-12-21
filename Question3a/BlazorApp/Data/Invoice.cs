using System;
using System.Collections;

namespace BlazorApp.Data
{
    public class Invoice
    {
        public int Id { get; set; }

        // The date on which the invoice was issued by this company.
        public DateTime Date { get; set; }

        // The total amount that the invoice is for. This is the amount of
        // money that the customer is expected to pay to settle the invoice.
        public Double TotalAmount { get; set; }

        // This should record the total amount paid towards this invoice by the
        // customer. It should always be the sum of any individual payments
        // made towards this invoice.
        public Double AmountPaid { get; set; }

        // The Balance of the invoice must always be the difference between the
        // TotalAmount and the AmountPaid for the invoice. A positive balance
        // means that the customer owes the company money for the invoice. A
        // Balance of zero means that the invoice is excatly paid. A negative
        // balance means that the customer overpaid. Paying money towards an
        // invoice decreases the balance.
        public Double Balance { get; set; }

        public class DateComparer: IComparer
        {
            public int Compare(object x, object y) {
                return ((Invoice)x).Date.CompareTo(((Invoice)y).Date);
            }
        }

    }
}