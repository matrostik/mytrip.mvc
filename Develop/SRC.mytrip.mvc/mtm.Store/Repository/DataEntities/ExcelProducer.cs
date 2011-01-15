using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using mtm.Core.Repository.LingToExcel;

namespace mtm.Store.Repository.DataEntities
{
    [ExcelSheet(Name = "producer")]
    public class ExcelProducer : INotifyPropertyChanged
    {
        private string _title;
        private string _body;
        private string _allculture;
        private double _producerid;
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
        public double ProducerId
        {
            get { return _producerid; }
            set
            {
                _producerid = value;
                SendPropertyChanged("ProducerId");
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