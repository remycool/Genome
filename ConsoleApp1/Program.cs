using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Lazy<DisplayIhm.DisplayIhm> h = new Lazy<DisplayIhm.DisplayIhm>();
            Console.WriteLine("IsValueCreated = {0}", h.IsValueCreated);
             DisplayIhm.DisplayIhm test = h.Value;
            // Console.WriteLine("Length = {0}", test.meth);
            test.splitFile();
            test.OnFileReached += new DisplayIhm.DisplayIhm.splitFileEvent(a_FileReached);
            Console.ReadLine();
            Console.ReadKey();
        }
        static void a_FileReached()
        {
            Console.WriteLine("Multiple of five reached!");
        }
    }
    
}
