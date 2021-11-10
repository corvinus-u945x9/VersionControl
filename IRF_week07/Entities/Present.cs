using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRF_week07
{
    class Present:Toy
    {
        public SolidBrush AjanándékSzin { get; set; }
        public SolidBrush szalagszin { get; set; }
        public Present(Color alao, Color szalag)
        {
            AjanándékSzin = new SolidBrush(alao);
            szalagszin = new SolidBrush(szalag);
        }

        protected override void DrawImage(Graphics g)
        {
            g.FillRectangle(AjanándékSzin, 0, 0, 70, 70);
            g.FillRectangle(szalagszin, 20, 0, 10, 70);
            g.FillRectangle(szalagszin, 0, 20, 70, 10);
        }
    }
}
