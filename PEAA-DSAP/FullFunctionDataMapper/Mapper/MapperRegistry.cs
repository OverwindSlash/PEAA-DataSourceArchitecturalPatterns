using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FullFunctionDataMapper.Domain;
using FullFunctionDataMapper.UoW;

namespace FullFunctionDataMapper.Mapper
{
    public sealed class MapperRegistry
    {
        private static readonly MapperRegistry instance = new MapperRegistry();

        private Dictionary<Type, AbstractMapper> mapperDictionary = new Dictionary<Type, AbstractMapper>(); 

        static MapperRegistry() { }

        private MapperRegistry() { }


        public static MapperRegistry Instance
        {
            get { return instance; }
        }

        public void Add(Type type, AbstractMapper mapper)
        {
            if (!mapperDictionary.ContainsKey(type))
            {
                mapperDictionary.Add(type, mapper);
            }
        }

        public AbstractMapper Get(Type type)
        {
            AbstractMapper mapper = null;
            mapperDictionary.TryGetValue(type, out mapper);
            return mapper;
        }
    }
}
