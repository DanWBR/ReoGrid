using Eto.Forms;
using Eto.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWSIM.CrossPlatform.UI.Controls.ReoGrid
{
    public class ReoGridFullControl: PixelLayout
    {

        public ReoGridFullControl(): base()
        {

            var container = new DynamicLayout();

            container.BeginVertical();

            this.SizeChanged += (sender, e) => {
                Console.WriteLine("PixelLayout Size Changed");
                container.Size = new Size(this.Width, this.Height);
            };

            DWSIM.CrossPlatform.UI.Controls.ReoGrid.ReoGridControl rgcontrol;

            rgcontrol = new DWSIM.CrossPlatform.UI.Controls.ReoGrid.ReoGridControl(this);
            container.Add(rgcontrol, true, true);

            rgcontrol.NewWorksheet();
            rgcontrol.NewWorksheet();
            rgcontrol.NewWorksheet();
            rgcontrol.NewWorksheet();
            rgcontrol.NewWorksheet();

            this.Add(container, 0, 0);
            this.Add(rgcontrol.editTextbox, 0, 0);

            container.Add(rgcontrol.bottomPanel);

            container.EndVertical();

            rgcontrol.Width = 5000;
            rgcontrol.Height = 2000;

        }

    }
}
