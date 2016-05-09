using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FullFunctionDataMapper.Domain;
using FullFunctionDataMapper.Mapper;

namespace FullFunctionDataMapper.UoW
{
    public sealed class UnitOfWork
    {
        private static readonly UnitOfWork instance = new UnitOfWork();

        private IList<DomainObject> newObjects = new List<DomainObject>();
        private IList<DomainObject> dirtyObjects = new List<DomainObject>();
        private IList<DomainObject> removeObjects = new List<DomainObject>();

        static UnitOfWork() { }

        private UnitOfWork() { }

        public static UnitOfWork Instance
        {
            get { return instance; }
        }

        public void RegisterNew(DomainObject domainObject)
        {
            //if (domainObject.UniqueKey == null) { return; }
            if (dirtyObjects.Contains(domainObject)) { return; }
            if (removeObjects.Contains(domainObject)) { return; }
            if (newObjects.Contains(domainObject)) { return; }
            newObjects.Add(domainObject);
        }

        public void RegisterDirty(DomainObject domainObject)
        {
            if (domainObject.UniqueKey == null) { return; }
            if (dirtyObjects.Contains(domainObject)) { return; }
            if (newObjects.Contains(domainObject)) { return; }
            if (removeObjects.Contains(domainObject)) { return; }
            dirtyObjects.Add(domainObject);
        }

        public void RegisterRemoved(DomainObject domainObject)
        {
            if (domainObject.UniqueKey == null) { return; }
            newObjects.Remove(domainObject);
            dirtyObjects.Remove(domainObject);
            if (removeObjects.Contains(domainObject)) { return; }
            removeObjects.Add(domainObject);
        }

        public void RegisterClean(DomainObject domainObject)
        {
            if (domainObject.UniqueKey == null) { return; }
            if (dirtyObjects.Contains(domainObject))
            {
                dirtyObjects.Remove(domainObject);
            }
            if (newObjects.Contains(domainObject))
            {
                newObjects.Remove(domainObject);
            }
            if (removeObjects.Contains(domainObject))
            {
                removeObjects.Remove(domainObject);
            }
        }

        public void CommitChanges()
        {
            InsertNew();
            UpdateDirty();
            DeleteRemoved();
        }

        private void DeleteRemoved()
        {
            //throw new NotImplementedException();
        }

        private void UpdateDirty()
        {
            //throw new NotImplementedException();
        }

        private void InsertNew()
        {
            foreach (DomainObject domainObject in newObjects)
            {
                MapperRegistry.Instance.Get(domainObject.GetType()).Insert(domainObject);
            }
        }
    }
}
