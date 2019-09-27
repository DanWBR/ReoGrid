using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Forms;
using Eto;

namespace ReoGridTest
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            var platform = new Eto.WinForms.Platform();

            new Application(platform).Run(new TestForm());

        }
    }
}
