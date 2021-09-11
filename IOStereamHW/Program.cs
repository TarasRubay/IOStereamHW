using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace IOStereamHW
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = @"Date.txt";
            string pathBin = @"Date.bin";
            ConsoleMenu menu = new ConsoleMenu(path,pathBin);
            menu.Start();
        }
    }
}
