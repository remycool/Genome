using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Genome
{
    public class LazyLoad
    {
  
        public string[] fileEntries = null;
        private string targetDirectory = @"E:\Projet_Cesi\DNA\DNA-Data\SplitFile";

        public LazyLoad()
        {
            Console.WriteLine("List files : ");
            fileEntries = Directory.GetFiles(this.targetDirectory);
        }

        public string[] Names

        {
            get
            {
                return fileEntries;
            }
        }




    }
}
