using System;
using System.Collections.Generic;
using System.Linq;

namespace mtm.Core.Repository.LingToExcel
{
    public class ChangeSet
    {
        private readonly List<ObjectState> trackedList;

        public ChangeSet()
        {
            this.trackedList = new List<ObjectState>();
        }

        public List<ObjectState> ChangedObjects
        {
            get { return (from c in this.trackedList where c.ChangeState != ChangeState.Retrieved select c).ToList(); }
        }

        public void AddObject(ObjectState objectState)
        {
            this.trackedList.Add(objectState);
        }

        public void DeleteObject(Object item)
        {
            ObjectState objectState = (from o in this.trackedList where ReferenceEquals(o.Entity, item) select o).FirstOrDefault();

            if (objectState != null)
            {
                if (objectState.ChangeState == ChangeState.Inserted)
                {
                    this.trackedList.Remove(objectState);
                }
                else
                {
                    objectState.ChangeState = ChangeState.Deleted;
                }
            }
        }

        public void InsertObject(Object item)
        {
            foreach (ObjectState trackedObjectState in this.trackedList)
            {
                if (ReferenceEquals(trackedObjectState.Entity, item))
                {
                    throw new InvalidOperationException("Object already in list");
                }
            }

            ObjectState objectState = new ObjectState(item, new List<PropertyManager>());
            objectState.ChangeState = ChangeState.Inserted;

            this.trackedList.Add(objectState);
        }
    }
}