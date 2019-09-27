using Eto.Forms;
using Eto.Drawing;
using System;

namespace ReoGridTest
{
    class TestForm : Form
    {

        public TestForm() : base()
        {
            Init();
        }

        void Init()
        {

            var pixlayout = new PixelLayout();

            var container = new DynamicLayout();

            container.BeginVertical();
            
            pixlayout.SizeChanged += (sender, e) => {
                Console.WriteLine("PixelLayout Size Changed");
                container.Size = new Size(pixlayout.Width, pixlayout.Height);
            };

            unvell.ReoGrid.ReoGridControl rgcontrol;

            rgcontrol = new unvell.ReoGrid.ReoGridControl(pixlayout);
            container.Add(rgcontrol, true, true);

            rgcontrol.NewWorksheet();
            rgcontrol.NewWorksheet();
            rgcontrol.NewWorksheet();
            rgcontrol.NewWorksheet();
            rgcontrol.NewWorksheet();

            pixlayout.Add(container, 0, 0);
            pixlayout.Add(rgcontrol.editTextbox, 0, 0);

            container.Add(rgcontrol.bottomPanel);

            container.EndVertical();

            rgcontrol.Width = 5000;
            rgcontrol.Height = 2000;

            Title = "ReoGrid Eto Demo";

            ClientSize = new Size(800, 480);

            pixlayout.Size = ClientSize;

            Content = pixlayout;

        }

    }
}

