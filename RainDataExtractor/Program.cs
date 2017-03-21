using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;

namespace RainDataExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("It is assumed that you have already extracted the mak-gnd raw data file using:");
            Console.WriteLine("cat var/www/sensors.dat | grep P0_LST60 >> data/mak-gnd.dat");
            Console.WriteLine("USAGE: RainDataExtrator.exe inputfile outputfile");
            if (args.Length == 0) return;

            System.IO.StreamReader reader = new System.IO.StreamReader(args[0]);
            System.IO.StreamWriter writer = new System.IO.StreamWriter(args[1]);

            Console.WriteLine("Enter start date (format is dd-MM-yyyy) :\t");
            DateTime startdate = DateTime.Parse(Console.ReadLine());

            List<DateTime> rainfall = new List<DateTime>();     //this list contains the total tips in a day
            int index = 0;
            do
            {
                rainfall.Add(startdate.AddDays(index));
                index++;
            } while (index <= DateTime.Now.Subtract(startdate).Days);

            DateTime currdate = startdate;
            int totalrain = 0;
            string line = "";

            foreach (DateTime item in rainfall)
            {
                currdate = item;
                while ((line = reader.ReadLine()) != null)
                {
                    try
                    {
                        if (Convert.ToDateTime(line.Substring(0, 19)).Date == currdate.Date)  //first 19 bytes are date-time information
                        {
                            index = line.IndexOf("P0_LST60");
                            string tag = line.Substring(index, 11); // caters for upto P0_LST=99
                            totalrain += Int32.Parse(tag.Split('=')[1]);
                        }
                        else
                        {
                            Console.WriteLine(item.ToShortDateString() + " , " + totalrain * 0.2); //one tip = 0.2mm
                            writer.WriteLine(item.ToShortDateString() + "," + totalrain * 0.2); //one tip = 0.2mm
                            totalrain = 0;
                            break;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            writer.Close(); //flush the data to physical disk
        }
    }
}
