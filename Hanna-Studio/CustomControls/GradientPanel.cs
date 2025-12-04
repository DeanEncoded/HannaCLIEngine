using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Hanna_Studio.UI;

namespace Hanna_Studio
{
    public class GradientPanel : Panel
    {
        public Color ColorTop { get; set; } = UIStyles.BackgroundLight;
        public Color ColorBottom { get; set; } = UIStyles.BackgroundLight;
        public bool UseGradient { get; set; } = false;
        public bool ShowBottomBorder { get; set; } = true;

        public GradientPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (UseGradient && ColorTop != ColorBottom)
            {
                using (var lgb = new LinearGradientBrush(this.ClientRectangle, this.ColorTop, this.ColorBottom, 90F))
                {
                    g.FillRectangle(lgb, this.ClientRectangle);
                }
            }
            else
            {
                using (var brush = new SolidBrush(ColorTop))
                {
                    g.FillRectangle(brush, this.ClientRectangle);
                }
            }

            // Draw subtle bottom border
            if (ShowBottomBorder)
            {
                using (var pen = new Pen(UIStyles.BorderDark))
                {
                    g.DrawLine(pen, 0, Height - 1, Width, Height - 1);
                }
            }

            base.OnPaint(e);
        }
    }
}
