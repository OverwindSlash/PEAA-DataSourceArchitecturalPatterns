using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullFunctionDataMapper.Domain
{
    public class Product : DomainObject
    {
        private int _productId;
        private string _productName;
        private int? _supplierId;
        private int? _categoryId;
        private string _quantityPerUnit;
        private decimal? _unitPrice;
        private short? _unitsInStock;
        private short? _unitsOnOrder;
        private short? _reorderLevel;
        private bool _discontinued;

        public int ProductID
        {
            get { return _productId; }
            set
            {
                _productId = value;
                MarkDirty();
            }
        }

        public string ProductName
        {
            get { return _productName; }
            set
            {
                _productName = value;
                MarkDirty();
            }
        }

        public int? SupplierID
        {
            get { return _supplierId; }
            set
            {
                _supplierId = value;
                MarkDirty();
            }
        }

        public int? CategoryID
        {
            get { return _categoryId; }
            set
            {
                _categoryId = value;
                MarkDirty();
            }
        }

        public string QuantityPerUnit
        {
            get { return _quantityPerUnit; }
            set
            {
                _quantityPerUnit = value;
                MarkDirty();
            }
        }

        public decimal? UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                _unitPrice = value;
                MarkDirty();
            }
        }

        public short? UnitsInStock
        {
            get { return _unitsInStock; }
            set
            {
                _unitsInStock = value;
                MarkDirty();
            }
        }

        public short? UnitsOnOrder
        {
            get { return _unitsOnOrder; }
            set
            {
                _unitsOnOrder = value;
                MarkDirty();
            }
        }

        public short? ReorderLevel
        {
            get { return _reorderLevel; }
            set
            {
                _reorderLevel = value;
                MarkDirty();
            }
        }

        public bool Discontinued
        {
            get { return _discontinued; }
            set
            {
                _discontinued = value;
                MarkDirty();
            }
        }
    }
}
