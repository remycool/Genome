using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManagement
{
    public class SendFile<T,V>
    {

        public T _listFile;

        public V _nameFile;
        

        public void Set_listFile(T value)
        {
            _listFile = value;
        }

        public T Get_listFile()
        {
            return _listFile;
        }

        public void SetV_nameFile(V value)
        {
            _nameFile = value;
        }
        public V GetV_nameFile()
        {
            return _nameFile;
        }



        public void listFileFolder_Node(List<T> listFile, V nameFile)
        {
            foreach (var tmp in listFile)
            {
                MessageBox.Show("Bien");

            }
           
          
        }

        public void listFileFolder_Node(IList<string> list, string v)
        {
            throw new NotImplementedException();
        }
    }
}
