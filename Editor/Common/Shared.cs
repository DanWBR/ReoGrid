using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unvell.ReoGrid.Editor.Common
{
    public class Shared
    {

        public static double DpiScale = 1.0;

        public static bool IsPro = false;

        public static Action<ReoGridControl> SaveInPro;

        public static Action<ReoGridControl> OpenInPro;

    }
}
