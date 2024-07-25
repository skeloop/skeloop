using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC
{
    partial class MainWindow
    {
        public void Init()
        {
            Console.WriteLine("Init");
            Console.WriteLine("Drücke Enter...");
            project_name_text_box.Text = "dev_test";
        }
    }
}
