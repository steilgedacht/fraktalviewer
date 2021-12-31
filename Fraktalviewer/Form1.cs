using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Numerics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Fraktalviewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        //
        // Zommvaribalen Initialisieren
        //
        double komplexplanecoordx = -2.5, komplexplanecoordy = 2, komplexplanecoord2x = -2.5, komplexplanecoord2y = 2, zoomfaktor = 2, juliazoomfaktor = 2, Basis = 10, counter = 1;
        bool dynamische_Ansicht = false, Iterationsverlauf = false, speichern = false, mandelbrotselektiert = true, juliamengeselektiert = false, Animieren = false;
        string selectedp;
        //
        // Juliamenge berechnen lassen wenn man auf die Mandelbrotmenge klickt
        //
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            panel1.Location = new Point(e.X + pictureBox1.Location.X - panel1.Width / 2, e.Y + pictureBox1.Location.Y - panel1.Height / 2);
            label5.Text = "c = " + Convert.ToString(komplexplanecoordx + e.X / Math.Pow(Basis, zoomfaktor)) + "  + j" + Convert.ToString(komplexplanecoordy - panel1.Location.Y / Math.Pow(Basis, zoomfaktor));
            label7.Text = "k = " + komplexplanecoord2x + "  + j" + komplexplanecoord2y;
            komplexplanecoord2x = -2.5;
            komplexplanecoord2y = 2;
            juliazoomfaktor = 2;
            Berechner(false);
        }
        //
        //Zoomen wenn man auf den Zoombutton klickt
        //
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (mandelbrotselektiert == true)
            {
                komplexplanecoordx += (Convert.ToDouble(panel1.Location.X) / (Math.Pow(Basis, zoomfaktor)) - 25 / (Math.Pow(Basis, zoomfaktor))) * trackBar1.Value / 10;
                komplexplanecoordy -= (Convert.ToDouble(panel1.Location.Y) / (Math.Pow(Basis, zoomfaktor)) - 20 / (Math.Pow(Basis, zoomfaktor))) * trackBar1.Value / 10;
                zoomfaktor += trackBar1.Value/10;
                Berechner(true);
            }
            if (juliamengeselektiert == true)
            {
                komplexplanecoord2x += Convert.ToDouble(panel2.Location.X - 500) / (100 * Math.Pow(Basis, juliazoomfaktor - 2)) - 25 / (Math.Pow(Basis, juliazoomfaktor));
                komplexplanecoord2y -= Convert.ToDouble(panel2.Location.Y) / (100 * Math.Pow(Basis, juliazoomfaktor - 2)) - 20 / (Math.Pow(Basis, juliazoomfaktor));
                juliazoomfaktor += trackBar1.Value/10;
                Berechner(false);
            }
        }
        //
        //Berechner für die Mandelbrotmenge, Juliamenge, Iterationsverlauf, Speichervorgang
        //
        private void Berechner(bool Mandelmenge)
        {
            {
                int width, height, Iterationen = 0;
                int Grundgleichung = toolStripComboBox1.SelectedIndex;
                int Farbschema = Farbe.SelectedIndex;
                width = pictureBox2.Size.Width;
                height = pictureBox2.Size.Height;

                Bitmap bmp = new Bitmap(width, height);

                toolStripProgressBar1.Maximum = height;
                toolStripProgressBar1.Value = 0;

                bool Abbruch = false, draw = false, Iabbruch = false; 
                int Zähler = 0;
                Complex z = Complex.Zero, c=Complex.Zero;

                if (Iabbruch == true)
                {
                    Iterationsverlaufmethode();
                }
                
                for (double y = 0; y < height; y++)
                {

                    toolStripProgressBar1.Value += 1;


                    for (double x = 0; x < width; x++)
                    {
                        Abbruch = false;
                        draw = false;
                        Zähler = 0;

                        if (Mandelmenge == true)
                        {
                            z = Complex.Zero;
                            c = (komplexplanecoordx + x / Math.Pow(Basis, zoomfaktor) + Complex.ImaginaryOne * (komplexplanecoordy - y / Math.Pow(Basis, zoomfaktor)));
                        }
                        else
                        {
                            c = komplexplanecoordx + Convert.ToDouble(panel1.Location.X) * (1 / (Math.Pow(Basis, zoomfaktor))) + Complex.ImaginaryOne * (komplexplanecoordy - Convert.ToDouble(panel1.Location.Y) * (1 / (Math.Pow(Basis, zoomfaktor))));
                            z = komplexplanecoord2x + x * (1 / (Math.Pow(10, juliazoomfaktor))) + Complex.ImaginaryOne * (komplexplanecoord2y - y * (1 / (Math.Pow(10, juliazoomfaktor))));
                        }

                        while (Abbruch == false)
                        {
                            Zähler++;

                            switch (Grundgleichung)
                            {
                                case 0:
                                    z = Complex.Add(Complex.Pow(z, 2), c);
                                    break;
                                case 1:
                                    z = Complex.Add(Complex.Pow(z, 3), c);
                                    break;
                                case 2:
                                    z = Complex.Add(Complex.Pow(z, 4), c);
                                    break;
                                case 3:
                                    z = Complex.Add(Complex.Pow(z, 8), c);
                                    break;
                                case 4:
                                    z = Complex.Add(Complex.Pow(z, 10), c);
                                    break;
                                case 5:
                                    z = 0.9005/2 * z * Complex.Log((1+z)/(1-z)) + c;
                                    break;
                                case 6:
                                    z = Complex.Add(Complex.Sin(z), c);
                                    break;
                                case 7:
                                    z = Complex.Add(Complex.Cos(z), c);
                                    break;
                                case 8:
                                    z = Complex.Add(Complex.Pow(Complex.Sin(z),2), c);
                                    break;
                                case 9:
                                    z = Complex.Add(Complex.Pow(z,7)+Complex.Pow(z,3)+0.01/z, c); //z7+z3+0,01/z
                                    break;
                                case 10:
                                    z = Complex.Add(Complex.Pow(z, counter), c);
                                    break;
                            }

                            Iterationen ++;

                            if (z.Magnitude>=10)
                            {
                                switch (Farbschema)
                                {
                                    case 0:
                                        bmp.SetPixel(Convert.ToInt16(x), Convert.ToInt16(y), Color.FromArgb(255, Convert.ToInt16(255 - (127 * Math.Sin(10 * Math.Log10(Zähler)) + 128)), Convert.ToInt16(255 - (127 * Math.Sin(12 * Math.Log10(Zähler)) + 128)), Convert.ToInt16(255 - (127 * Math.Sin(12 * Math.Log10(Zähler)) + 128))));
                                        break;
                                    case 1:
                                        bmp.SetPixel(Convert.ToInt16(x), Convert.ToInt16(y), Color.FromArgb(255, Convert.ToInt16((127 * Math.Sin(4 * Math.Log10(Zähler)) + 128)), Convert.ToInt16((127 * Math.Sin(6 * Math.Log10(Zähler)) + 128)), Convert.ToInt16((127 * Math.Sin(4 * Math.Log10(Zähler)) + 128))));
                                        break;
                                    case 2:
                                        bmp.SetPixel(Convert.ToInt16(x), Convert.ToInt16(y), Color.FromArgb(255, Convert.ToInt16((127 * Math.Sin(2 * Math.Log(Zähler)) + 128)), Convert.ToInt16((127 * Math.Sin(2 * Math.Log(Zähler)) + 128)), Convert.ToInt16((127 * Math.Sin(2 * Math.Log(Zähler)) + 128))));
                                        break;
                                    case 3:
                                        bmp.SetPixel(Convert.ToInt16(x), Convert.ToInt16(y), Color.FromArgb(255, Convert.ToInt16(255 - (127 * Math.Sin(3 * Math.Log10(Zähler)) + 128)), Convert.ToInt16(255 - (127 * Math.Sin(12 * Math.Log10(Zähler)) + 128)), Convert.ToInt16(255 - (127 * Math.Sin(16 * Math.Log10(Zähler)) + 128))));
                                        break;
                                    case 4:
                                        bmp.SetPixel(Convert.ToInt16(x), Convert.ToInt16(y), Color.FromArgb(255, Convert.ToInt16(255 - (127 * Math.Sin(10 * Math.Log10(Zähler)) + 128)), Convert.ToInt16(255 - (127 * Math.Sin(10 * Math.Log10(Zähler)) + 128)), Convert.ToInt16(255 - (127 * Math.Sin(1 * Math.Log10(Zähler)) + 128))));
                                        break;
                                    case 5:
                                        bmp.SetPixel(Convert.ToInt16(x), Convert.ToInt16(y), Color.FromArgb(255, Convert.ToInt16((127 * Math.Sin(Math.Log10(Zähler)) + 128)), Convert.ToInt16((127 * Math.Sin(2 * Math.Log10(Zähler)) + 128)), Convert.ToInt16((127 * Math.Sin(8 * Math.Log10(Zähler)) + 128))));
                                        break;
                                    case 6:
                                        bmp.SetPixel(Convert.ToInt16(x), Convert.ToInt16(y), Color.FromArgb(255, Convert.ToInt16((127 * Math.Sin(Math.Log10(Zähler)) + 128)), Convert.ToInt16((127 * Math.Sin(2 * Math.Log10(Zähler)) + 128)), Convert.ToInt16((127 * Math.Sin(2 * Math.Log10(Zähler)) + 128))));

                                        break;
                                }

                                draw = false;
                                Abbruch = true;
                            }

                            if (Zähler >= (100 * Math.Pow(zoomfaktor, 1.4)))
                            {
                                draw = true;
                                Abbruch = true;
                            }
                        }

                        if (draw == true)
                        {
                            bmp.SetPixel(Convert.ToInt16(x), Convert.ToInt16(y), colorDialog1.Color);
                        }
                    }
                }
                if (Mandelmenge == true)
                {
                    label4.Text = "I = " + Iterationen;
                    label3.Text = "k = " + komplexplanecoordx + " + j " + komplexplanecoordy;
                    label2.Text = "c = " + c.Real + " + j " + c.Imaginary;
                }
                else
                {
                    label8.Text = "I = " + Iterationen;
                    label7.Text = "k = " + komplexplanecoord2x + " + j " + komplexplanecoord2y;
                    label5.Text = "c = " + c.Real + " + j " + c.Imaginary;
                }
                if (Mandelmenge == true)
                {
                    pictureBox1.Image = bmp;
                }
                else
                {
                    if (dynamische_Ansicht == true)
                    {
                        pictureBox3.Image = bmp;
                    }
                    else
                    {
                        pictureBox2.Image = bmp;
                    }
                }

                if (speichern == true && Animieren == false)
                {
                    if (folderBrowserDialog1.ShowDialog()==System.Windows.Forms.DialogResult.OK)
                    {
                        selectedp = folderBrowserDialog1.SelectedPath;
                        selectedp.Replace(@"\", @"\\");
                        if (mandelbrotselektiert == true)
                        {
                            bmp.Save(Convert.ToString(folderBrowserDialog1.SelectedPath) + "\\" + Convert.ToString(textBox1.Text) + ".png");
                        }
                        if (juliamengeselektiert == true)
                        {
                            bmp.Save(Convert.ToString(folderBrowserDialog1.SelectedPath) + "\\" + Convert.ToString(textBox1.Text) + ".png");
                        }
                        speichern = false;
                    }
                }
                if (Animieren == true)
                {
                    if (mandelbrotselektiert == true)
                    {
                        bmp.Save(Convert.ToString(folderBrowserDialog1.SelectedPath) + "\\" + Convert.ToString(textBox1.Text) + ".png");
                    }
                    if (juliamengeselektiert == true)
                    {
                        bmp.Save(Convert.ToString(folderBrowserDialog1.SelectedPath) + "\\" + Convert.ToString(textBox1.Text) + ".png");
                    }
                }

            }
        }
        //
        // gewählter C-Wert im eingabebereich des Toolstrips
        //
        double gewähltesc1, gewähltesc2;
        //
        //Berechner für die dynamische Ansicht, Echtzeitberechnung
        //
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dynamische_Ansicht == true)
            {
                {
                    {
                        int width, height, Iterationen = 0;
                        width = pictureBox3.Size.Width;
                        height = pictureBox3.Size.Height;
                        int Grundgleichung = toolStripComboBox1.SelectedIndex;
                        int Farbschema = Farbe.SelectedIndex;

                        Bitmap bmp3 = new Bitmap(width, height);

                        bool Abbruch = false, draw = false;
                        int Zähler = 0;

                        for (double y = 0; y < height; y++)
                        { 
                            for (double x = 0; x < width; x++)
                            {
                                Abbruch = false;
                                draw = false;
                                Zähler = 0;

                                Complex c = (komplexplanecoordx + Convert.ToDouble(e.X) * (Math.Pow(Basis, -zoomfaktor))) + Complex.ImaginaryOne * (komplexplanecoordy - Convert.ToDouble(e.Y) * (Math.Pow(Basis, -zoomfaktor)));

                                Complex z = ((x - width / 2) / 100) + Complex.ImaginaryOne * ((height / 2 - y) / 100);

                                while (Abbruch == false)
                                {
                                    Zähler++;
                                    Iterationen++;

                                    switch (Grundgleichung)
                                    {
                                        case 0:
                                            z = Complex.Add(Complex.Pow(z, 2), c);
                                            break;
                                        case 1:
                                            z = Complex.Add(Complex.Pow(z, 3), c);
                                            break;
                                        case 2:
                                            z = Complex.Add(Complex.Pow(z, 4), c);
                                            break;
                                        case 3:
                                            z = Complex.Add(Complex.Pow(z, 5), c);
                                            break;
                                        case 4:
                                            z = Complex.Add(Complex.Pow(z, 10), c);
                                            break;
                                        case 5:
                                            z = 0.9005 / 2 * z * Complex.Log((1 + z) / (1 - z)) + c;
                                            break;
                                        case 6:
                                            z = Complex.Add(Complex.Sin(z), c);
                                            break;
                                        case 7:
                                            z = Complex.Add(Complex.Cos(z), c);
                                            break;
                                        case 8:
                                            z = Complex.Add(Complex.Pow(Complex.Sin(z), 2), c);
                                            break;
                                        case 9:
                                            z = Complex.Add(Complex.Sinh(z), c);
                                            break;
                                        case 10:
                                            z = Complex.Add(Complex.Cosh(z), c);
                                            break;
                                    }                                   if (z.Magnitude >= 10)
                                    {
                                        switch (Farbschema)
                                        {
                                            case 0:
                                                bmp3.SetPixel(Convert.ToInt16(x), Convert.ToInt16(y), Color.FromArgb(255, Convert.ToInt16(255 - (127 * Math.Sin(10 * Math.Log10(Zähler)) + 128)), Convert.ToInt16(255 - (127 * Math.Sin(12 * Math.Log10(Zähler)) + 128)), Convert.ToInt16(255 - (127 * Math.Sin(12 * Math.Log10(Zähler)) + 128))));
                                                break;
                                            case 1:
                                                bmp3.SetPixel(Convert.ToInt16(x), Convert.ToInt16(y), Color.FromArgb(255, Convert.ToInt16((127 * Math.Sin(4 * Math.Log10(Zähler)) + 128)), Convert.ToInt16((127 * Math.Sin(6 * Math.Log10(Zähler)) + 128)), Convert.ToInt16((127 * Math.Sin(4 * Math.Log10(Zähler)) + 128))));
                                                break;
                                            case 2:
                                                bmp3.SetPixel(Convert.ToInt16(x), Convert.ToInt16(y), Color.FromArgb(255, Convert.ToInt16((127 * Math.Sin(2 * Math.Log(Zähler)) + 128)), Convert.ToInt16((127 * Math.Sin(2 * Math.Log(Zähler)) + 128)), Convert.ToInt16((127 * Math.Sin(2 * Math.Log(Zähler)) + 128))));
                                                break;
                                            case 3:
                                                bmp3.SetPixel(Convert.ToInt16(x), Convert.ToInt16(y), Color.FromArgb(255, Convert.ToInt16(255 - (127 * Math.Sin(3 * Math.Log10(Zähler)) + 128)), Convert.ToInt16(255 - (127 * Math.Sin(12 * Math.Log10(Zähler)) + 128)), Convert.ToInt16(255 - (127 * Math.Sin(16 * Math.Log10(Zähler)) + 128))));
                                                break;
                                            case 4:
                                                bmp3.SetPixel(Convert.ToInt16(x), Convert.ToInt16(y), Color.FromArgb(255, Convert.ToInt16(255 - (127 * Math.Sin(10 * Math.Log10(Zähler)) + 128)), Convert.ToInt16(255 - (127 * Math.Sin(10 * Math.Log10(Zähler)) + 128)), Convert.ToInt16(255 - (127 * Math.Sin(1 * Math.Log10(Zähler)) + 128))));
                                                break;
                                            case 5:
                                                bmp3.SetPixel(Convert.ToInt16(x), Convert.ToInt16(y), Color.FromArgb(255, Convert.ToInt16((127 * Math.Sin(Math.Log10(Zähler)) + 128)), Convert.ToInt16((127 * Math.Sin(2 * Math.Log10(Zähler)) + 128)), Convert.ToInt16((127 * Math.Sin(8 * Math.Log10(Zähler)) + 128))));
                                                break;
                                            case 6:
                                                break;
                                        }
                                        draw = false;
                                        Abbruch = true;
                                    }

                                    if (Zähler >= (10 * Math.Pow(zoomfaktor, 1.4)))
                                    {
                                        draw = true;
                                        Abbruch = true;
                                    }
                                }

                                if (draw == true)
                                {
                                    bmp3.SetPixel(Convert.ToInt16(x), Convert.ToInt16(y), colorDialog1.Color);
                                }
                            }
                        }
                        pictureBox3.Image = bmp3;
                    }
                }
            }
        }
        //
        // Herauszoomen beim drücken des Herauszoombuttons
        //
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (mandelbrotselektiert == true)
            {
                zoomfaktor -= 1;
                komplexplanecoordx -= 250 / (Math.Pow(Basis, zoomfaktor)) - 25 / (Math.Pow(Basis, zoomfaktor));
                komplexplanecoordy += 200 / (Math.Pow(Basis, zoomfaktor)) - 20 / (Math.Pow(Basis, zoomfaktor));
                Berechner(true);
            }
            if (juliamengeselektiert == true)
            {
                juliazoomfaktor -= 1;
                komplexplanecoord2x -= 250 / (Math.Pow(Basis, juliazoomfaktor)) - 25 / (Math.Pow(Basis, juliazoomfaktor));
                komplexplanecoord2y += 200 / (Math.Pow(Basis, juliazoomfaktor)) - 20 / (Math.Pow(Basis, juliazoomfaktor));
                Berechner(false);
            }
        }
        //
        //Schließen Button
        //
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //
        // Nach Unten
        //
        private void button2_Click(object sender, EventArgs e)
        {
            if (mandelbrotselektiert == true)
            {
                komplexplanecoordy += -100 / (Math.Pow(Basis, zoomfaktor));
                Berechner(true);
            }
            if (juliamengeselektiert == true)
            {
                komplexplanecoord2y += -100 / (Math.Pow(Basis, juliazoomfaktor));
                Berechner(false);
            }
        }
        //
        // Nach Oben
        //
        private void Oben_Click(object sender, EventArgs e)
        {
            if (mandelbrotselektiert == true)
            {
                komplexplanecoordy += 100 / (Math.Pow(Basis, zoomfaktor));
                Berechner(true);
            }
            if (juliamengeselektiert == true)
            {
                komplexplanecoord2y += 100 / (Math.Pow(Basis, juliazoomfaktor));
                Berechner(false);
            }
        }

        //
        // Nach rechts
        //
        private void button3_Click(object sender, EventArgs e)
        {
            if (mandelbrotselektiert == true)
            {
                komplexplanecoordx += -100 / (Math.Pow(Basis, zoomfaktor));
                Berechner(true);
            }
            if (juliamengeselektiert == true)
            {
                komplexplanecoord2x += -100 / (Math.Pow(Basis, juliazoomfaktor));
                Berechner(false);
            }
        }
        //
        //Doppelklickzoom bei der Juliamenge
        //
        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            komplexplanecoord2x += Convert.ToDouble(panel2.Location.X-500) / (100 * Math.Pow(Basis, juliazoomfaktor - 2)) - 25 / (Math.Pow(Basis, juliazoomfaktor));
            komplexplanecoord2y -= Convert.ToDouble(panel2.Location.Y) / (100 * Math.Pow(Basis, juliazoomfaktor - 2)) - 20 / (Math.Pow(Basis, juliazoomfaktor));
            juliazoomfaktor += 1;
            Berechner(false);
        }
        //
        // aktiviert oder deaktiviert Iterationsverlauf
        //
        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            if (Iterationsverlauf == false)
            {
                Iterationsverlauf = true;
            }
            else
            {
                Iterationsverlauf = false;
            }
        }
        //
        //Formgröße größer machen
        //
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (this.Width < 1100)
            {
                this.Size = new System.Drawing.Size(1353, 465);
            }
            else
            {
                this.Size = new System.Drawing.Size(1016, 465);
            }
        }
        //
        // Color des unendlichen Bestimmen
        //
        private void button6_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            panel8.BackColor = colorDialog1.Color;
        } 

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.M)
            {
                toolStripLabel1_Click(sender,e);
            }
            if (e.KeyCode == Keys.J)
            {
                toolStripButton11_Click_1(sender, e);
            }
            if (e.KeyCode == Keys.Up)
            {
                Oben_Click(sender, e);
            }
            if (e.KeyCode == Keys.Down)
            {
                button2_Click(sender, e);
            }
            if (e.KeyCode == Keys.Right)
            {
                button4_Click(sender, e);
            }
            if (e.KeyCode == Keys.Left)
            {
                button3_Click(sender, e);
            }
            if (e.KeyCode == Keys.R)
            {
                toolStripButton3_Click(sender, e);
            }
        }

        //
        //Wenn die Formel selektiert wird, wird die Menge neu Berechnet
        //
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Berechner(true);
        }
        //
        //Wenn die Farbe selektiert wird, wird die Menge neu Berechnet
        //
        private void Farbe_SelectedIndexChanged(object sender, EventArgs e)
        {
            Berechner(true);
            Berechner(false);
        }
        //
        //Animieren
        //
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
  
            Complex Animationskoordinate_1 = Complex.Zero, Animationskoordinate_2 = Complex.Zero;
            double Range = 1;
            try
            {
                Animationskoordinate_1 = 0.31 + Complex.ImaginaryOne * (0.48);
                Animationskoordinate_2 = 2.15 + Complex.ImaginaryOne * (0.54);

                Range = Convert.ToInt16(toolStripTextBox5.Text);


                if (juliamengeselektiert == true)
                {
                    Complex Step = (Animationskoordinate_2.Real - Animationskoordinate_1.Real) / Range + Complex.ImaginaryOne * (Animationskoordinate_2.Imaginary - Animationskoordinate_1.Imaginary) / Range;
                    for (int i = 0; i <= Range; i++)
                    {
                        panel1.Location = new Point(Convert.ToInt16(Math.Round(((Animationskoordinate_1.Real + Step.Real * i - komplexplanecoordx) * 100), 0)),
                                                    Convert.ToInt16(Math.Round((komplexplanecoordy - (Animationskoordinate_1.Imaginary + Step.Imaginary * i)) * 100, 0)));

                        textBox1.Text = Convert.ToString(i);
                        speichern = true;

                        Berechner(false);
                        Animieren = true;
                    }
                }
                if (mandelbrotselektiert == true && toolStripComboBox1.SelectedIndex == 10)
                {
                    counter = Animationskoordinate_1.Real;
                    for (int i = 0; i <= Range; i++)
                    {
                        counter += ((Animationskoordinate_1.Imaginary - Animationskoordinate_1.Real)/Range);

                        textBox1.Text = Convert.ToString(i);
                        speichern = true;

                        Berechner(true);
                        Animieren = true;
                    }
                }
            }
            catch
            {

            }
            Animieren = false;
            speichern = false;

        }

        //
        // aktiviert oder deaktiviert dynamische Ansicht
        //
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (dynamische_Ansicht == false)
            {
                dynamische_Ansicht = true;
            }
            else
            {
                dynamische_Ansicht = false;
            }
        }

        //
        // Mandelbrotmenge selektieren
        //
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            mandelbrotselektiert = true;
            juliamengeselektiert = false;
            panel3.Location = new Point(0, 0);
            panel4.Location = new Point(497, 0);
            panel5.Location = new Point(0,397);
            panel6.Location = new Point(0,0);
            label1.Text = "Mandelb";
        }
        //
        // Juliamenge selektieren
        //
        private void toolStripButton11_Click_1(object sender, EventArgs e)
        {
            juliamengeselektiert = true;
            mandelbrotselektiert = false;
            panel3.Location = new Point(500, 0);
            panel4.Location = new Point(997, 0);
            panel5.Location = new Point(500,397);
            panel6.Location = new Point(500,0);
            label1.Text = "Julia";
        }
        //
        // Nach links
        //
        private void button4_Click(object sender, EventArgs e)
        {
            if (mandelbrotselektiert == true)
            {
                komplexplanecoordx += 100 / (Math.Pow(Basis, zoomfaktor));
                Berechner(true);
            }
            if (juliamengeselektiert == true)
            {
                komplexplanecoord2x += 100 / (Math.Pow(Basis, juliazoomfaktor));
                Berechner(false);
            }
        }
        //
        // Speichern aktivieren
        //
        private void button1_Click(object sender, EventArgs e)
        {
            
            speichern = true;
            if (mandelbrotselektiert == true)
            {
                Berechner(true);
            }
            if (juliamengeselektiert == true)
            {
                Berechner(false);
            }
        }
        //
        //Doppelclick zoomen wird hier aktiviert (aufs panel ist der zweite Klick)
        //
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            komplexplanecoordx += Convert.ToDouble(panel1.Location.X) / (100 * Math.Pow(Basis, zoomfaktor - 2)) - 25 / (Math.Pow(Basis, zoomfaktor));
            komplexplanecoordy -= Convert.ToDouble(panel1.Location.Y) / (100 * Math.Pow(Basis, zoomfaktor - 2)) - 20 / (Math.Pow(Basis, zoomfaktor));
            zoomfaktor += 1;
            Berechner(true);
        }

        //
        // Panel2 folgt dem Mausklick
        //
        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            panel2.Location = new Point(e.X + pictureBox2.Location.X - panel2.Width / 2, e.Y + pictureBox2.Location.Y - panel2.Height / 2);
        }
        //
        // Eingabe der gewählten C Wertes
        //
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                gewähltesc1 = Convert.ToDouble(textBox2.Text);
                gewähltesc2 = Convert.ToDouble(textBox3.Text);
                panel1.Location = new Point(Convert.ToInt16(gewähltesc1 * 100 + 250), Convert.ToInt16(-gewähltesc2 * 100 + 200));
                Berechner(false);
            }
            catch
            {
                MessageBox.Show("Nur Zahlen eingeben (Komma = Beistrich)");
            }
            Berechner(false);
        }
        //
        //Reset Button für Mandelbrotmenge und juliamenge
        //
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (mandelbrotselektiert == true)
            {
                zoomfaktor = 2;
                komplexplanecoordx = -2.5;
                komplexplanecoordy = 2;
                Berechner(true);
            }
            if (juliamengeselektiert == true)
            {
                komplexplanecoord2x = -2.5;
                komplexplanecoord2y = 2;
                juliazoomfaktor = 2;
                Berechner(false);
            }
        }
        //
        //Beim Starten der Anwendung wird das ausgeführt
        //
        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.SelectedIndex = 0;
            Farbe.SelectedIndex = 0;
            Berechner(false);
            Berechner(true);
            panel1.Location = new Point(250, 200);
            panel2.Location = new Point(750, 200);
            Berechner(false);
            this.Size = new System.Drawing.Size(1016,465);
            panel8.BackColor = colorDialog1.Color;
        }

        private void Iterationsverlaufmethode()
        {
            Complex Iz, Ic;
            bool Iabbruch = false;
            int Zähler=0, Grundgleichung = toolStripComboBox1.SelectedIndex;
            Bitmap bmp2 = new Bitmap(200, 200);
            for (int yclear = 0; yclear < 200; yclear++)
            {
                for (int xclear = 0; xclear < 200; xclear++)
                {
                    bmp2.SetPixel(xclear, yclear, Color.FromArgb(255, 64, 64, 64));
                }
            }

            int pwidth = pictureBox4.Width, pheight = pictureBox4.Height, cp1 = 0, cp2 = 0;

            for (int xline = 1; xline < 199; xline++)
            {
                bmp2.SetPixel(xline, 100, Color.Black);
                bmp2.SetPixel(100, xline, Color.Black);
            }
            for (int i = 0; i < 360; i++)
            {
                cp1 = 100 + Convert.ToInt16(Math.Round(80 * Math.Cos(i), 0));
                cp2 = 100 + Convert.ToInt16(Math.Round(80 * Math.Sin(i), 0));
                bmp2.SetPixel(cp1, cp2, Color.DarkGray);
            }
            pictureBox4.Image = bmp2;
            Iz = Complex.Zero;
            Ic = komplexplanecoordx + panel1.Location.X / Math.Pow(Basis, zoomfaktor) + Complex.ImaginaryOne * komplexplanecoordy - panel1.Location.Y / Math.Pow(Basis, zoomfaktor);

            while (Iabbruch == false)
            {
                Zähler += 1;
                switch (Grundgleichung)
                {
                    case 0:
                        Iz = Complex.Add(Complex.Pow(Iz, 2), Ic);
                        break;
                    case 1:
                        Iz = Complex.Add(Complex.Pow(Iz, 3), Ic);
                        break;
                    case 2:
                        Iz = Complex.Add(Complex.Pow(Iz, 4), Ic);
                        break;
                    case 3:
                        Iz = Complex.Add(Complex.Pow(Iz, 5), Ic);
                        break;
                    case 4:
                        Iz = Complex.Add(Complex.Pow(Iz, 10), Ic);
                        break;
                    case 5:
                        Iz = 0.9005 / 2 * Iz * Complex.Log((1 + Iz) / (1 - Iz)) + Ic;
                        break;
                    case 6:
                        Iz = Complex.Add(Complex.Sin(Iz), Ic);
                        break;
                    case 7:
                        Iz = Complex.Add(Complex.Cos(Iz), Ic);
                        break;
                    case 8:
                        Iz = Complex.Add(Complex.Pow(Complex.Sin(Iz), 2), Ic);
                        break;
                    case 9:
                        Iz = Complex.Add(Complex.Sinh(Iz), Ic);
                        break;
                    case 10:
                        Iz = Complex.Add(Complex.Cosh(Iz), Ic);
                        break;
                }

                if (Iz.Magnitude >= 2)
                {
                    Iabbruch = true;
                }
                int FarbeIterationsverlauf = Convert.ToInt16(Math.Round(255 * (1 - Math.Pow(Math.E, -Zähler * 0.1)), 0));
                try
                {
                    bmp2.SetPixel(Convert.ToInt16(Math.Round(Iz.Real * 40 + 100, 0)), Convert.ToInt16(Math.Round(-40 * Iz.Imaginary + 100, 0)), Color.FromArgb(255, FarbeIterationsverlauf, FarbeIterationsverlauf, FarbeIterationsverlauf));
                }
                catch
                {
                    Iabbruch = true;
                }
            }
            pictureBox4.Image = bmp2;

        }
    }
}