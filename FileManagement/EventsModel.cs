using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    public delegate string MyDel(string str);
    class EventsModel
    {
        event MyDel pickFileEvent;
        public EventsModel()
        {
            this.pickFileEvent += new MyDel(this.getFile);
        }

        public string getFile(string fileName)
        {
            return "Id du ficheir" + fileName;
        }

    }
}
