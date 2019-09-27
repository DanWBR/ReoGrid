using DWSIM.CrossPlatform.UI.Controls.ReoGrid.Utility;

namespace DWSIM.CrossPlatform.UI.Controls.ReoGrid
{
    public static class AutoFillSectionEntryFactory
    {
        public static IAutoFillSectionEntry Create(object value)
        {
            if (value == null)
            {
                return new NullAutoFillSectionEntry();
            }

            double number;
            if (CellUtility.TryGetNumberData(value, out number))
            {
                return new NumericalAutoFillSectionEntry(number);
            }

            return new TextAutoFillSectionEntry(value.ToString());
        }
    }
}