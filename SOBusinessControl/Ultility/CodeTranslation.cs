using SODtaModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOBusinessControl.Ultility
{
    public static class CodeTranslation
    {
        public static string ToOrderStatusDescription(string code) {
            string description = "";
            description = code switch
            {
                OrderStatus.Completed => "Completed",
                OrderStatus.Fullfilled => "Fullfilled",
                OrderStatus.In_Process => "Processing",
                OrderStatus.New => "New",
                OrderStatus.Paid => "Paid",
                OrderStatus.Pending => "Pending",
                _ => code,
            };
            return description; 
        
        }

        public static string ToAddressTypeDescription(string code)
        {
            string des = code switch
            {
                Address_Type.ClosedShop => "Unused address",
                Address_Type.MainOffice => "Main office",
                Address_Type.Shop => "Shop", 
                _=> code
            };
            return des; 
        }
        public static string ToAddressTypeCode(string desc)
        {
            string code = desc switch
            {
                "Unused address" => Address_Type.ClosedShop,
                "Main office" => Address_Type.MainOffice ,
                "Shop" => Address_Type.Shop ,
                _ => Address_Type.Shop
            };
            return code;
        }

        public static string ToOrderLineStatusDescription(string code)
        {
            string des = code switch
            { 
                OrderLineStatus.Active=> "Active",
                OrderLineStatus.Cancelled => "Cancelled", 
                OrderLineStatus.PrePurchased => "In-advance Ordered",
                _=> code
            };
            return des;
        }

        public static string ToOrderLineCode(string desc)
        {
            string code = desc switch
            {
                "Active" => OrderLineStatus.Active,
                 "Cancelled" => OrderLineStatus.Cancelled ,
                 "In-advance Ordered" => OrderLineStatus.PrePurchased,
                _ => OrderLineStatus.Cancelled
            };
            return code;
        }
    }
}
