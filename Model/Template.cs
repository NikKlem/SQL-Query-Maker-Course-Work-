using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace WpfApp2.Model
{
    public class Template: INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }

        public Template(string name, string type, string content)
        {
            Name = name;
            Content = content;
            Type = type;
           
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
