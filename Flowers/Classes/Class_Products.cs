using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowers.Classes
{
    //элемент списка
    public class Class_Products
    {
        public string ProductArticleNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int IDProductCategory { get; set; }
        public byte[] ProductPhoto { get; set; }
        public string ProductManufacturer { get; set; }
        public int ProductCost { get; set; }
        public Nullable<byte> ProductDiscountAmount { get; set; }
        public int ProductQuantityInStock { get; set; }
    }
}
