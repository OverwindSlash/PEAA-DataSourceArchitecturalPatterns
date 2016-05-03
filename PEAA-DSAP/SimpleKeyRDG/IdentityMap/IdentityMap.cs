using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleKeyRDG.IdentityMap
{
    public sealed class IdentityMap<TKey, TValue>
    {
        private static readonly IdentityMap<TKey, TValue> instance = new IdentityMap<TKey, TValue>();
        private Dictionary<TKey, TValue> loadMap = null;

        static IdentityMap()
        {

        }

        private IdentityMap()
        {
            loadMap = new Dictionary<TKey, TValue>();
        }

        public static IdentityMap<TKey, TValue> Instance
        {
            get { return instance; }
        }

        public TValue GetEntry(TKey key)
        {
            TValue entry = default(TValue);

            loadMap.TryGetValue(key, out entry);

            return entry;
        }

        public void PutEntry(TKey key, TValue entry)
        {
            if (!loadMap.ContainsKey(key))
            {
                loadMap.Add(key, entry);
            }
        }
    }
}
