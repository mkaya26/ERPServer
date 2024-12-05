using ERPServer.Domain.Abstractions;
using ERPServer.Domain.Enums;

namespace ERPServer.Domain.Entities
{
    public sealed class Invoice : Entity
    {
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }
        //public long? InvoiceNumber { get; set; }
        //public short? InvoiceNumberYear { get; set; }
        public string InvoiceNumberFull { get; set; } = default!;
        public DateOnly InvoiceDate { get; set; }
        public InvoiceTypeEnum InvoiceType { get; set; } = InvoiceTypeEnum.Purchase;
        public List<InvoiceDetail>? InvoiceDetails { get; set; }

        //public string SetNumber()
        //{
        //    string prefix = "SF";
        //    string initialString = prefix + InvoiceNumberYear.ToString() + InvoiceNumber.ToString();
        //    int targetLength = 16;
        //    int missingLength = targetLength - initialString.Length;
        //    string finalString = prefix + InvoiceNumberYear.ToString() + new string('0', missingLength) + InvoiceNumber.ToString();
        //    return finalString;
        //}
    }
}
