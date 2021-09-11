using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOStereamHW
{
    class ConsoleMenu
    {
        public ConsoleMenu(string Path, string PathBin)
        {
            menu = 100;
            max_menu = 10;
            switch_on = menu;
            path = Path;
            pathBIN = PathBin;
            collection = null;
            collectionEvent = null;
        }
        int menu  { get;}
        int max_menu { get; }
        int switch_on { get; set; }
        SortedDictionary <DateTime, string> collectionEvent { get; set; }
        List<DateTime> collection { get; set; } 
        string path { get; }
        string pathBIN { get; }
        public void Start()
        {
            collectionEvent = new SortedDictionary<DateTime, string>();
            do
            {
                switch (switch_on)
                {
                    case 100:
                        do
                        {
                        try
                        {
                            print();
                           switch_on = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception msg)
                        {
                            Console.WriteLine(msg.Message);
                                switch_on = menu;    
                        }
                            Console.Clear();
                        } while (switch_on < 0 || switch_on > max_menu);

                        break;
                    case 1:
                        DataManager data = new DataManager(path);
                        collection = data.LoadDataTXT();
                        if(collection == null)Console.WriteLine("file not read");
                        else Console.WriteLine("read file ok");
                        switch_on = menu;
                        break;
                    case 2:
                        Console.WriteLine("all list");
                        if (collection != null)
                        {
                        foreach (var item in collection)
                        {
                            Console.WriteLine(item);
                        }
                        }
                        else Console.WriteLine("Collection is Empty");
                        switch_on = menu;
                        break;
                    case 3:
                        Console.WriteLine("Min YEar");
                        if(collection != null)
                        {
                          Console.WriteLine(collection.Min(it => it.Year));
                        }
                        else Console.WriteLine("Collection is Empty");
                        switch_on = menu;
                        break;
                    case 4:
                        Console.WriteLine("Spring Dates: ");
                        if (collection != null)
                        {
                            var seq = collection.FindAll(it => it.Month >= 3 && it.Month <= 5);
                            foreach (var item in seq)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        else Console.WriteLine("Collection is Empty");
                        switch_on = menu;
                        break;
                    case 5:
                        Console.WriteLine("Date near today");
                        if(collection != null)Console.WriteLine(findNearDate());
                        else Console.WriteLine("Collection is Empty");
                        switch_on = menu;     
                        break;
                    case 6:
                        Console.WriteLine("read history event from .bin file");
                        DataManager dataBin = new DataManager(pathBIN);
                        collectionEvent = dataBin.LoadDataBIN();
                        if (collectionEvent == null || collectionEvent.Count == 0) Console.WriteLine("Error Read");
                        else Console.WriteLine("Read Completed......");
                        switch_on = menu;
                        break;
                    case 7:
                        Console.WriteLine("write history event to .bin file");
                        dataBin = new DataManager(pathBIN);
                        if (collectionEvent != null)
                        {
                            if (dataBin.SaveDataBIN(collectionEvent)) Console.WriteLine("Write Completed......");
                            else Console.WriteLine("Error Write");
                        }
                        else Console.WriteLine("Collection is Empty");
                        switch_on = menu;
                        break;
                    case 8:
                        Console.WriteLine("Add history event");
                        AddHistoryEvent();
                        switch_on = menu;
                        break;
                    case 9:
                        Console.WriteLine("sort history event");
                        if(collectionEvent != null)
                        foreach (var item in collectionEvent)
                        {
                            Console.WriteLine(item);
                        }
                        else Console.WriteLine("Collection is Empty");
                        switch_on = menu;
                        break;
                    case 10:
                        Console.WriteLine("Count Average Date Span betweean event");
                        if(collectionEvent != null)
                        {
                          
                            Console.WriteLine($"Average Date Span betweean event: {(collectionEvent.Keys.Last() - collectionEvent.Keys.First()).Days / collectionEvent.Count} days ");
                        }
                        else Console.WriteLine("Collection is Empty");
                        switch_on = menu;
                        break;
                    default:
                        break;
                }

            } while (switch_on != 0);

        }
        void print()
        {
            Console.WriteLine();
            Console.WriteLine($"1 - Read DataTime from file: {path}");
            Console.WriteLine("2 - print all Date list");
            Console.WriteLine("3 - Find min Year");
            Console.WriteLine("4 - Find Spring Dates");
            Console.WriteLine("5 - Find Date near today");
            Console.WriteLine();
            Console.WriteLine($"6 - Read History Event from file: {pathBIN}");
            Console.WriteLine($"7 - Write History Event to file: {pathBIN}");
            Console.WriteLine("8 - Add history event");
            Console.WriteLine("9 - sort history event");
            Console.WriteLine("10 - Count Average Date Span betweean event");
            Console.WriteLine("0 - exit");
        }
        DateTime findNearDate()
        {
            // ще не розібрався до кінця з LINQ тому зробив так
            List<DateTime> tmp = new List<DateTime>();
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    tmp.Add(item);
                }
                tmp.Add(DateTime.Today);
                tmp.Sort();
                for (int i = 0; i < tmp.Count; i++)
                {
                    if (tmp[i] == DateTime.Today && i != 1 && i != tmp.Count - 1)
                    {
                        if (tmp[i] - tmp[i - 1] < tmp[i + 1] - tmp[i]) return tmp[i - 1];
                        else return tmp[i + 1];
                    }
                    else if (tmp[i] == DateTime.Today && i == 1)
                    {
                         return tmp[i + 1];
                    }
                    else if (tmp[i] == DateTime.Today && i == tmp.Count - 1)
                    {
                        return tmp[i - 1];
                    }
                }
            }
            else Console.WriteLine("Collection is Empty");
            return DateTime.Today;
        }
        public void AddHistoryEvent()
        {
            int year = -1, mount = -1, day = -1;
            bool chek = false;
                Console.WriteLine("Enter date history evnt");
            do
            {
                try
                {
                    Console.Write("Enter year: ");
                    year = Convert.ToInt32(Console.ReadLine());
                    if (year < 1 || year > 9999) chek = true;
                    else chek = false;
                }
                catch (Exception e)
                {
                    chek = true;
                    Console.WriteLine(e.Message);
                }
            } while (chek != false);
           
            do
            {
                try
                {
                    Console.Write("Enter mount: ");
                    mount = Convert.ToInt32(Console.ReadLine());
                    if (mount < 1 || mount > 12) chek = true;
                    else chek = false;
                }
                catch (Exception e)
                {
                    chek = true;
                    Console.WriteLine(e.Message);
                }
            } while (chek != false);
           
            do
            {
                try
                {
                    Console.Write($"Max in {DateTime.DaysInMonth(year, mount)} Enter day: ");
                    day = Convert.ToInt32(Console.ReadLine());
                    if (day < 1 || day > DateTime.DaysInMonth(year, mount)) chek = true;
                    else chek = false;
                }
                catch (Exception e)
                {
                    chek = true;
                    Console.WriteLine(e.Message);
                }
            } while (chek != false);
            if (year == -1 || mount == -1 || day == -1)
                Console.WriteLine("Wrong Data");
                    else{
                Console.WriteLine("enter history event:");
                string HisEvent = Console.ReadLine();
                collectionEvent.Add(new DateTime(year, mount, day), HisEvent);
                     }
        }
    }
}