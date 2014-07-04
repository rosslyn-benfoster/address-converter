using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AddressConverter
{
    public static class XmlExtensions
    {
        public static string ValueOrDefault(this XElement e, string defaultValue = default(string))
        {
            return e != null ? e.Value : defaultValue;
        }
    }
}
