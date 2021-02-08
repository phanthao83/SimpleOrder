using System;
using System.Collections.Generic;
using System.Text;

namespace SODtaModel.View
{
    public class ProductDetailView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxAllowedOrderQty { get; set; }
        public double BasePrice { get; set; }

        public List<ProductOptionView> ProductOptions { get; set; }

    }
    public class ProductOptionView
    {
        public int Id { get; set; }
        public string OptionDescription { get; set; }
        public double AdditionalCost { get; set; }
    }
}
