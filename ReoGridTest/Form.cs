using Eto.Forms;
using Eto.Drawing;

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

            var scrollable = new Scrollable();

            var pixlayout = new PixelLayout();

            var container = new TableLayout();

            var rgcontrol = new unvell.ReoGrid.ReoGridControl(scrollable, pixlayout);

            pixlayout.Add(container, 0, 0);
            pixlayout.Add(rgcontrol.editTextbox, 0, 0);

            pixlayout.SizeChanged += (sender, e) => {
                container.Size = new Size(pixlayout.Width, pixlayout.Height);
            };

            rgcontrol.NewWorksheet();
            rgcontrol.NewWorksheet();
            rgcontrol.NewWorksheet();
            rgcontrol.NewWorksheet();
            rgcontrol.NewWorksheet();

            container.Rows.Add(new TableRow { ScaleHeight = true, Cells = { scrollable } });

            container.Rows.Add(new TableRow { ScaleHeight = false, Cells = { rgcontrol.bottomPanel } });

            rgcontrol.Width = 5000;
            rgcontrol.Height = 3000;

            scrollable.Content = rgcontrol;

            Title = "ReoGrid Eto Demo";

            ClientSize = new Size(800, 480);

            pixlayout.Size = ClientSize;

            Content = pixlayout;

        }

    }
}

