namespace mtm.Core.Repository.LingToExcel
{
    public class PropertyManager
    {
        public PropertyManager(string propName, object value)
        {
            this.PropertyName = propName;
            this.OrginalValue = value;
            this.HasChanged = false;
        }

        public bool HasChanged
        {
            get;
            set;
        }

        public object OrginalValue
        {
            get;
            set;
        }

        public string PropertyName
        {
            get;
            set;
        }
    }
}