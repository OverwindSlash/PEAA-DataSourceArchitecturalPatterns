using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FullFunctionDataMapper.CompoundKey;
using FullFunctionDataMapper.UoW;

namespace FullFunctionDataMapper.Domain
{
    public class DomainObject
    {
        public Key UniqueKey { get; set; }

        protected void MarkNew()
        {
            UnitOfWork.Instance.RegisterNew(this);
        }

        protected void MarkClean()
        {
            UnitOfWork.Instance.RegisterClean(this);
        }

        protected void MarkDirty()
        {
            UnitOfWork.Instance.RegisterDirty(this);
        }

        protected void MarkRemoved()
        {
            UnitOfWork.Instance.RegisterRemoved(this);
        }
    }
}
