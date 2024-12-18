using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WpfApp2.Model
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Template> Templates { get; set; } = null!;

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=templates.db");
        }
    }
}
