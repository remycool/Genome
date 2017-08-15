using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FileManagement
{
    public class LazyLoading
    {

        DisplayIhm.DisplayIhm meth;
        private string[] fileTransform = null;

        public LazyLoading()
        {
            meth = new DisplayIhm.DisplayIhm();
            fileTransform = File.ReadLines(@"E:\Projet_Cesi\DNA\DNA-Data\test.txt").ToArray();
            meth.splitFile(fileTransform);
        }

        public List<string> Names

        {
            get
            {
                return meth.splitFile(fileTransform);
            }
        }

    }
}
