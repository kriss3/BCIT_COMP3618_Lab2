using System;
using System.IO;
using System.Xml.Serialization;
using static System.Console;

namespace Serialize_People
{
    /// <summary>
    /// Krzysztof Szczurowski;
    /// BCIT COMP3608 Lab 2;
    /// Repo: https://github.com/kriss3/BCIT_COMP3618_Lab2.git
    /// Simple program that accepts a name, year, month date,
    /// creates a Person object from that information, 
    /// and then displays that person's age on the console.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                // If they provide no arguments, display the last person
                Person p = Deserialize();
                WriteLine(p.ToString());
            }
            else
            {
                try
                {
                    if (args.Length != 4)
                    {
                        throw new ArgumentException("You must provide four arguments.");
                    }

                    DateTime dob = new DateTime(Int32.Parse(args[1]), Int32.Parse(args[2]), Int32.Parse(args[3]));
                    Person p = new Person(args[0], dob);
                    WriteLine(p.ToString());

                    Serialize(p);
                }
                catch (Exception ex)
                {
                    DisplayUsageInformation(ex.Message);
                }
            }
        }

        private static void DisplayUsageInformation(string message)
        {
            WriteLine("\nERROR: Invalid parameters. " + message);
            WriteLine("\nSerialize_People \"Name\" Year Month Date");
            WriteLine("\nFor example:\nSerialize_People \"Tony\" 1922 11 22");
            WriteLine("\nOr, run the command with no arguments to display that previous person.");
        }

        private static void Serialize(Person sp)
        {
            using (FileStream fs = new FileStream("Person.xml", FileMode.Create))
            {
                // Create an XmlSerializer object to perform the serialization
                XmlSerializer xs = new XmlSerializer(typeof(Person));
                // Use the XmlSerializer object to serialize the data to the file
                xs.Serialize(fs, sp);
            }
        }

        private static Person Deserialize()
        {
            Person dsp = new Person();

            using (FileStream fs = new FileStream("Person.xml", FileMode.Open))
            {
                // Create an XmlSerializer object to perform the deserialization
                XmlSerializer xs = new XmlSerializer(typeof(Person));
                // Use the XmlSerializer object to deserialize the data to the file
                dsp = (Person)xs.Deserialize(fs);
            }
            return dsp;
        }
    }
}
