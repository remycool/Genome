using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Cluster
{
    public class LazyLoad
    {
  
        private string[] _fileEntries = null;
        private static string _pathUser;
        private string _targetDirectory;

        public LazyLoad()
        {
            _pathUser = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _targetDirectory = (_pathUser + "\\SplitFile\\");
            Console.WriteLine("List files : ");
            _fileEntries = Directory.GetFiles(this._targetDirectory);
        }

        public string[] Names
        {
            get
            {
                return _fileEntries;
            }
        }

    





    }
}
