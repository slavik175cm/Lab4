using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace MyWatcher
{
    class FileArchive
    {
        public static void AddToArchive(string path, Entity entity)
        {
            DateTime now = DateTime.Now;
            string subpath = ParseDateToPath(now);
            string newPath = path + "\\" + subpath + entity.GetName();
            Directory.CreateDirectory(path + "\\" + subpath);
            entity.Move(newPath);
        }

        static string ParseDateToPath(DateTime dateTime)
        {
            string[] months = {"January", "February", "March", "April", "May",
                    "June", "July", "August", "September", "October", "November", "December"};
            return (
                "Year" + dateTime.Year.ToString() + "\\" + 
                 months[dateTime.Month - 1] + "\\" + 
                 "Day" + dateTime.Day + "\\" + 
                 dateTime.Hour + (dateTime.Hour < 12 ? "am" : "pm") + "\\" + 
                 "Minute" + dateTime.Minute + "\\" +
                 "Second" + dateTime.Second
            );
        }
    }
}
