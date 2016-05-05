using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMapper.CompoundKey;

namespace DataMapper.Domain
{
    public class DomainObject
    {
        public Key UniqueKey { get; set; }
    }
}
