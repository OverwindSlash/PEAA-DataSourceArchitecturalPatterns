using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.CompoundKey
{
    public class Key
    {
        private IList<object> fields = new List<object>();

        #region Generic Constructor
        public Key(IList<object> fields)
        {
            CheckKeyNotNull(fields);
            this.fields = fields;
        }

        private void CheckKeyNotNull(IList<object> fields)
        {
            if (fields == null)
            {
                throw new ArgumentNullException("Cannot have a null key.");
            }

            foreach (object field in fields)
            {
                if (field == null)
                {
                    throw new ArgumentException("Cannot have a null element of key.");
                }
            }
        }
        #endregion

        #region Helper Constructor
        public Key(int intKey)
        {
            this.fields.Add(intKey);
        }

        public Key(string strKey)
        {
            this.fields.Add(strKey);
        }

        public Key(int intKey1, int intKey2)
        {
            this.fields.Add(intKey1);
            this.fields.Add(intKey2);
        }
        #endregion

        public override bool Equals(object obj)
        {
            if (!(obj is Key))
            {
                return false;
            }

            Key otherKey = (Key)obj;

            if (this.fields.Count != otherKey.fields.Count)
            {
                return false;
            }

            for (int i = 0; i < fields.Count; i++)
            {
                if (this.fields[i] != otherKey.fields[i])
                {
                    return false;
                }
            }

            return true;
        }

        public object GetValue(int index)
        {
            return fields[index];
        }

        #region Single Element Key Getter
        public int GetInt32Key()
        {
            CheckSingleKey();
            return (int)fields[0];
        }

        public string GetStringKey()
        {
            CheckSingleKey();
            return (string)fields[0];
        }

        private void CheckSingleKey()
        {
            if (fields.Count > 1)
            {
                throw new ArgumentException("Only single element key can use this method.");
            }
        }
        #endregion
    }
}
