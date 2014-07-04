using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace AddressConverter
{
    class Program
    {
        /*
         * 1. Allow the user to provide any import path and any export path.
         * 2. Support import *from* CSV and export *to* XML 
         * 3. Support import/export from/to JSON
         * 4. Make it possible to import/export from other sources to accomodate other file systems e.g. cloud storage.
         * 5. Prove the application works, without opening the export file.
         */
        
        static void Main(string[] args)
        {
            var importPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "addresses.xml");
            var exportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "addresses.csv");

            Console.WriteLine("Importing XML from " + importPath);
            var xml = XDocument.Load(importPath);

            var addresses = from address in xml.Root.Descendants("address")
                            select new Address
                            {
                                Name = address.Element("name").ValueOrDefault(),
                                StreetNumber = address.Element("streetNumber").ValueOrDefault(),
                                Street = address.Element("street").ValueOrDefault(),
                                Locality = address.Element("locality").ValueOrDefault(),
                                PostTown = address.Element("postTown").ValueOrDefault(),
                                PostCode = address.Element("postCode").ValueOrDefault()
                            };


            Console.WriteLine("Exporting CSV to " + exportPath);

            using (var writer = new StreamWriter(exportPath))
            {
                // Write CSV header
                var properties = typeof(Address).GetProperties();

                var header = string.Join(",", properties.Select(p => p.Name));
                writer.WriteLine(header);
                
                foreach (var address in addresses)
                {
                    var addressLine = string.Join(",", properties.Select(prop => prop.GetValue(address)));
                    writer.WriteLine(addressLine);
                }
            }

            Console.WriteLine("All done!");
            Console.ReadKey();
        }

        public class Address
        {
            public string Name { get; set; }
            public string StreetNumber { get; set; }
            public string Street { get; set; }
            public string Locality { get; set; }
            public string PostTown { get; set; }
            public string PostCode { get; set; }
        }
    }
}
