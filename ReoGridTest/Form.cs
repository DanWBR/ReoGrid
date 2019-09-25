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

            var rgcontrol = new unvell.ReoGrid.ReoGridControl(scrollable);

            rgcontrol.Width = 5000;
            rgcontrol.Height = 3000;

            scrollable.Content = rgcontrol;

            Title = "ReoGrid Eto Demo";

            ClientSize = new Size(1024, 768);

            Content = scrollable;

        }

    }
}

