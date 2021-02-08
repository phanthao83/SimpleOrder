using System;
using System.Collections.Generic;
using System.Text;

namespace SODtaModel.View
{
    public class OrderDetailView
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public List<OrderLineView> OrderLines { get; set; }
    }

    public class OrderLineView
    {
        public int Id { get; set; }
        public int ProductiId { get; set; }
        public string ProductName { get; set; }
        public int Quanity { get; set; }
        public double UPrice { get; set; }
        public double DiscountTotal { get; set; }
        public double SubTotal { get; set; }
        public string PromotionCode { get; set; }
        public string Status { get; set; }
        
        public List<OrderLineOptionView> PurchasedOptions { get; set; }
    }

    public class OrderLineOptionView
    {
        public int Id { get; set; }
        public int OrderDetailId { get; set; }
        public int OptionId { get; set; }
        public string OptionDescription { get; set; }
        public double AdditionalCost { get; set; }
    }


   
}
