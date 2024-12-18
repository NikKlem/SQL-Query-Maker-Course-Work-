using Microsoft.EntityFrameworkCore;
using WpfApp2.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Data.Sqlite;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data;
using System.Text.Json.Serialization;
namespace WpfApp2.ViewModel
{
    class ApplicationViewModel:INotifyPropertyChanged
    {
        ApplicationContext db = new ApplicationContext();
        RelayCommand? clearCommand;
        RelayCommand? fetchCommand;
        RelayCommand? copyCommand;
        public string output { get; set; }
        public string Output
        {
            get { return output; }
            set
            {
                output = value;
                OnPropertyChanged("Output");
            }
        }
        public ObservableCollection<Template> Templates { get; set; }

        public ApplicationViewModel()
        {
            LoadBDContext();
            db.Templates.Load();
            var Templates = db.Templates.Local.ToObservableCollection();
        }
        public RelayCommand ClearCommand
        {
            get
            {
                return clearCommand ?? (clearCommand = new RelayCommand((o) =>
                {
                    Output = null;
                }));
            }
        }
        public RelayCommand FetchCommand
        {
            get
            {
                return fetchCommand ?? (fetchCommand = new RelayCommand((o) =>
                {
                    int str = Convert.ToInt32(o);
                    Template select = db.Templates.FirstOrDefault(q => q.Id == str);
                    Output += select.Content;
                    
                }));
            }
        }

        public RelayCommand CopyCommand
        {
            get
            {
                return copyCommand ?? (copyCommand = new RelayCommand((o) =>
                {

                    string str = Output.ToString();
                    System.Windows.Clipboard.SetText(str);
                    //save(str);
                }));
            }
        }
        [STAThread]
        public void save(string str)
        {
            Clipboard.SetData(DataFormats.StringFormat, str);
            var t = Clipboard.GetData(DataFormats.Text);
            var q = Clipboard.GetText();
        }
        private void LoadBDContext()
        {
            using (ApplicationContext app = new ApplicationContext())
            {
                app.Database.EnsureCreated();
                app.Templates.Add(new Template("createT", "create", "CREATE TABLE <TABLE NAME> \n"));
                app.Templates.Add(new Template("addint", "addrow", "<name> int NULL \n"));
                app.Templates.Add(new Template("addnvch", "addrow", "<name> nvarchar<size> NULL \n"));
                app.Templates.Add(new Template("addfloat", "addrow", "<name> float NULL \n"));
                app.Templates.Add(new Template("addate", "addrow", "<name> date NULL \n"));
                app.Templates.Add(new Template("addbin", "addrow", "<name> binary NULL \n"));
                app.Templates.Add(new Template("dropcol", "alter", "DROP COLUMN <NAME> \n"));
                app.Templates.Add(new Template("sortby", "sorting", "SORT BY <NAME> \n"));
                app.Templates.Add(new Template("groupby", "sortby", "GROUP BY <NAME> \n"));
                app.Templates.Add(new Template("orderby", "sortby", "ORDER BY <NAME> \n"));
                app.Templates.Add(new Template("dropt", "deletet", "DROP TABLE <NAME> \n"));
                app.SaveChanges();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
