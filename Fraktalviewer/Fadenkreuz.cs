using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Fraktalviewer
{
    class Fadenkreuz: Form
    {
        public void c_Wähler(int cursorx, int cursory, int schleifex, int schleifey)
        {
            double teilschrittx, teilschritty, x1, x2, y1, y2, pictureboxwidth = 500, pictureboxheight = 400;
       


            x1 = cursorx - pictureboxwidth / 2;
            x2 = cursorx + pictureboxwidth / 2;

            y1 = cursory - pictureboxheight / 2;
            y2 = cursory + pictureboxheight / 2;

            teilschrittx = (x2 - x1) / pictureboxwidth;
            teilschritty = (y2 - y1) / pictureboxheight;
            

            

        }
    }
}
