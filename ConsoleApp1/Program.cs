using FileManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    public delegate void dgPointer();
    class Program
    {
        static void Main(string[] args)
        {
            Lazy<DisplayIhm.DisplayIhm> h = new Lazy<DisplayIhm.DisplayIhm>();
            Console.WriteLine("IsValueCreated = {0}", h.IsValueCreated);
            DisplayIhm.DisplayIhm msgLazyLoading = h.Value;

            // Console.WriteLine("Length = {0}", test.meth);
            // DisplayIhm.DisplayIhm.splitFileEvent fileEvent = new DisplayIhm.DisplayIhm.splitFileEvent(test.splitFile);
            //test.OnFileReached += new DisplayIhm.DisplayIhm.splitFileEvent(a_FileReached);
            // test.splitFile();

            DisplayIhm.DisplayIhm ihm = new DisplayIhm.DisplayIhm();

            ihm.OnFileSplit += a_FileReached;
           // dgPointer pAdder = new dgPointer(ihm.splitFile);
           // pAdder();
            Console.ReadLine();
            Console.ReadKey();
        }
        static void a_FileReached(object sender, EventsModel e)
        {
            Console.WriteLine("File created!");
        }
    }
    
}
