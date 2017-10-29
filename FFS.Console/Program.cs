using FFS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFS.Console
{
    class Program
    {
        static void Main()
        {
            string path = @"C:\Temp\lol.xlsx";
            FFSDao.ReadFile(path);
        }
    }
}
