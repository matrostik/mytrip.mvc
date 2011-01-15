using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using mtm.Core.Repository.LingToExcel;

namespace mtm.Store.Repository.DataEntities
{
    [ExcelSheet(Name = "product")]
    public class ExcelProduct : INotifyPropertyChanged
    {

        private string _nambercatalog;
        private string _title;
        private string _body;
        private string _details;
        private double _price;
        private string _moneyid;
        private string _packing;
        private double _totalcount;
        private string _viewprice;
        private string _viewcount;
        private string _viewvotes;
        private string _allculture;
        private string _producerid;
        private string _departmentid;
        private double _productid;
        private string _saleid;
        private string _culture;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        [ExcelColumn(Name = "NamberCatalog", Storage = "_nambercatalog")]
        public string NamberCatalog
        {
            get { return _nambercatalog; }
            set
            {
                _nambercatalog = value;
                SendPropertyChanged("NamberCatalog");
            }
        }

        [ExcelColumn(Name = "Title", Storage = "_title")]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                SendPropertyChanged("Title");
            }
        }

        [ExcelColumn(Name = "Body", Storage = "_body")]
        public string Body
        {
            get { return _body; }
            set
            {
                _body = value;
                SendPropertyChanged("Body");
            }
        }

        [ExcelColumn(Name = "Details", Storage = "_details")]
        public string Details
        {
            get { return _details; }
            set
            {
                _details = value;
                SendPropertyChanged("Details");
            }
        }

        [ExcelColumn(Name = "Price", Storage = "_price")]
        public double Price
        {
            get { return _price; }
            set
            {
                _price = value;
                SendPropertyChanged("Price");
            }
        }

        [ExcelColumn(Name = "MoneyId", Storage = "_moneyid")]
        public string MoneyId
        {
            get { return _moneyid; }
            set
            {
                _moneyid = value;
                SendPropertyChanged("MoneyId");
            }
        }

        [ExcelColumn(Name = "Packing", Storage = "_packing")]
        public string Packing
        {
            get { return _packing; }
            set
            {
                _packing = value;
                SendPropertyChanged("Packing");
            }
        }

        [ExcelColumn(Name = "TotalCount", Storage = "_totalcount")]
        public double TotalCount
        {
            get { return _totalcount; }
            set
            {
                _totalcount = value;
                SendPropertyChanged("TotalCount");
            }
        }

        [ExcelColumn(Name = "ViewPrice", Storage = "_viewprice")]
        public string ViewPrice
        {
            get { return _viewprice; }
            set
            {
                _viewprice = value;
                SendPropertyChanged("ViewPrice");
            }
        }

        [ExcelColumn(Name = "ViewCount", Storage = "_viewcount")]
        public string ViewCount
        {
            get { return _viewcount; }
            set
            {
                _viewcount = value;
                SendPropertyChanged("ViewCount");
            }
        }

        [ExcelColumn(Name = "ViewVotes", Storage = "_viewvotes")]
        public string ViewVotes
        {
            get { return _viewvotes; }
            set
            {
                _viewvotes = value;
                SendPropertyChanged("ViewVotes");
            }
        }

        [ExcelColumn(Name = "AllCulture", Storage = "_allculture")]
        public string AllCulture
        {
            get { return _allculture; }
            set
            {
                _allculture = value;
                SendPropertyChanged("AllCulture");
            }
        }

        [ExcelColumn(Name = "ProducerId", Storage = "_producerid")]
        public string ProducerId
        {
            get { return _producerid; }
            set
            {
                _producerid = value;
                SendPropertyChanged("ProducerId");
            }
        }

        [ExcelColumn(Name = "DepartmentId", Storage = "_departmentid")]
        public string DepartmentId
        {
            get { return _departmentid; }
            set
            {
                _departmentid = value;
                SendPropertyChanged("DepartmentId");
            }
        }

        [ExcelColumn(Name = "ProductId", Storage = "_productid")]
        public double ProductId
        {
            get { return _productid; }
            set
            {
                _productid = value;
                SendPropertyChanged("ProductId");
            }
        }
        [ExcelColumn(Name = "SaleId", Storage = "_saleid")]
        public string SaleId
        {
            get { return _saleid; }
            set
            {
                _saleid = value;
                SendPropertyChanged("SaleId");
            }
        }
        [ExcelColumn(Name = "Culture", Storage = "_culture")]
        public string Culture
        {
            get { return _culture; }
            set
            {
                _culture = value;
                SendPropertyChanged("Culture");
            }
        }
    }
}

