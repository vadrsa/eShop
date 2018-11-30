using BusinessEntities.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace eShopApi.Helpers
{
    public static class EnumSerializationHelper
    {
        public static List<EnumItemSerialized> Serialize(Type type)
        {
            if (!type.IsEnum) throw new ArgumentException("type must be enum.");
            Array values = type.GetEnumValues();
            List<EnumItemSerialized> ret = new List<EnumItemSerialized>();
            foreach(object enumVal in values)
            {
                string value = enumVal.ToString();
                MemberInfo member = type.GetMember(value).First();
                DescriptionAttribute descAttr = member.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault();
                string desc = (descAttr != null) ? descAttr.Description : value;
                ret.Add(new EnumItemSerialized { Name = value, DisplayName = desc });
            }
            return ret;
        }
    }
}
