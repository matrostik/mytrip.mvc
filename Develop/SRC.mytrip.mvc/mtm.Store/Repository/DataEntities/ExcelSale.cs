using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using mtm.Core.Repository.LingToExcel;

namespace mtm.Store.Repository.DataEntities
{
    [ExcelSheet(Name = "sale")]
    public class ExcelSale : INotifyPropertyChanged
    {
        private string _title;
        private double _sale;
        private string _startdate;
        private string _closedate;
        private double _saleid;

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

        [ExcelColumn(Name = "Sale", Storage = "_sale")]
        public double Sale
        {
            get { return _sale; }
            set
            {
                _sale = value;
                SendPropertyChanged("Sale");
            }
        }

        [ExcelColumn(Name = "StartDate", Storage = "_startdate")]
        public string StartDate
        {
            get { return _startdate; }
            set
            {
                _startdate = value;
                SendPropertyChanged("StartDate");
            }
        }

        [ExcelColumn(Name = "CloseDate", Storage = "_closedate")]
        public string CloseDate
        {
            get { return _closedate; }
            set
            {
                _closedate = value;
                SendPropertyChanged("CloseDate");
            }
        }
        [ExcelColumn(Name = "SaleId", Storage = "_saleid")]
        public double SaleId
        {
            get { return _saleid; }
            set
            {
                _saleid = value;
                SendPropertyChanged("SaleId");
            }
        }
    }
}