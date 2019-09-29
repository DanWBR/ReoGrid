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

            Title = "ReoGrid Eto Demo";

            ClientSize = new Size(800, 480);

            Content = new DWSIM.CrossPlatform.UI.Controls.ReoGrid.ReoGridFullControl();

        }

    }
}

