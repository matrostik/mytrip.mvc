using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace mtm.Core.Repository.LingToExcel
{
    public class ObjectState
    {
        private readonly object entity;

        private readonly List<PropertyManager> properties;

        private ChangeState state;

        public ObjectState(object entity, List<PropertyManager> props)
        {
            this.entity = entity;
            this.properties = props;
            this.state = ChangeState.Retrieved;

            if (entity is INotifyPropertyChanged)
            {
                INotifyPropertyChanged notifyPropertyChanged = entity as INotifyPropertyChanged;

                notifyPropertyChanged.PropertyChanged += this.PropertyChanged;
            }
        }

        public List<PropertyManager> ChangedProperties
        {
            get { return (from p in this.properties where p.HasChanged select p).ToList(); }
        }

        public ChangeState ChangeState
        {
            get { return this.state; }
            set { this.state = value; }
        }

        public Object Entity
        {
            get { return this.entity; }
        }

        public List<PropertyManager> Properties
        {
            get { return this.properties; }
        }

        public PropertyManager GetProperty(string propertyName)
        {
            return (from p in this.properties where p.PropertyName == propertyName select p).FirstOrDefault();
        }

        public void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyManager propertyManager =
                    (from p in this.properties where p.HasChanged == false && p.PropertyName == e.PropertyName select p).
                            FirstOrDefault();
            if (propertyManager != null)
            {
                propertyManager.HasChanged = true;

                if (this.state == ChangeState.Retrieved)
                {
                    this.state = ChangeState.Updated;
                }
            }
        }
    }
}