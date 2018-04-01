/* Copyright (C) 2018. Hitomi Parser Developers */

using System.Drawing;
using System.Windows.Forms;

namespace Hitomi_Copy
{
    public partial class ScrollFixLayoutPanel : FlowLayoutPanel
    {
        protected override Point ScrollToControl(Control activeControl)
        {
            return DisplayRectangle.Location;
        }
    }
}
