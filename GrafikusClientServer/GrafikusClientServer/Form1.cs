using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Globalization;
using System.Collections;
using System.Threading;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace GrafikusClientServer
{
    public partial class Form1 : Form
    {
        Thread t;
        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            t = new Thread(doThisAllTime);
        }
        int counter = 1;
        //X, Y és Z tengely értéke
        double x;
        double y;
        double z;
        double newX;
        double newY;
        double newZ;
        //kezdőpozíció, mind 3 tengely adat kell, ezért tömb
        double[] startPosition = new double[3];
        long time;
        long lastTime;
        //Hány adatot tettünk már ki a diagramra
        int countOfCharts = 0;
        //Beérkező adatok tárolása
        InputDataHandler inputdatahandler = new InputDataHandler(50,"X tengely","Y tengely","Z tengely");
        /// <summary>
        /// Kapcsolódás során az új szál indítja el
        /// Ez végzi a legfontosabb műveleteket
        /// Megkapjuk a telefonról átadott adatokat és itt dolgozzuk fel
        /// </summary>
        public void doThisAllTime()
        {
            int port = 8888;
            label5.Invoke(new Action(() => label5.Text = GetLocalIPAddress()));
            TcpListener myListener = new TcpListener(IPAddress.Parse(GetLocalIPAddress()), port);
            myListener.Start();
            label1.Invoke(new Action(() => label1.Text = "Várakozás kliensekre a " + port + " porton."));
            TcpClient connectedClient = myListener.AcceptTcpClient();
            label1.Invoke(new Action(() => label1.Text = "Cliens csatlakozott"));

            NetworkStream stream = connectedClient.GetStream();
            StreamReader r = new StreamReader(stream);
            StreamWriter sw = new StreamWriter("me're's.txt");
            Invalidate();
            bool stopit = false;
            while (r.ReadLine() != null && stopit != true)
            {
                countOfCharts++;
                //Console.WriteLine("("+counter+") "+r.ReadLine());
                try
                {
                    string s = r.ReadLine();
                    string[] str_array = s.Split(' ');
                    if (clicked != true)
                    {
                        x = double.Parse(str_array[0], CultureInfo.InvariantCulture);
                        y = double.Parse(str_array[1], CultureInfo.InvariantCulture);
                        z = double.Parse(str_array[2], CultureInfo.InvariantCulture);
                        newX = double.Parse(str_array[0], CultureInfo.InvariantCulture);
                        newY = double.Parse(str_array[1], CultureInfo.InvariantCulture);
                        newZ = double.Parse(str_array[2], CultureInfo.InvariantCulture);
                        whichRadioChosen(x, y, z);
                        
                    }
                    else
                    {
                        x = double.Parse(str_array[0], CultureInfo.InvariantCulture)-startPosition[0];
                        y = double.Parse(str_array[1], CultureInfo.InvariantCulture)-startPosition[1];
                        z = double.Parse(str_array[2], CultureInfo.InvariantCulture)-startPosition[2];
                        whichRadioChosen(x, y, z);
                    }
                    time = long.Parse(str_array[3], CultureInfo.InvariantCulture);
                    label2.Invoke(new Action(() => label2.Text = x.ToString()));
                    label3.Invoke(new Action(() => label3.Text = y.ToString()));
                    label4.Invoke(new Action(() => label4.Text = z.ToString()));
                    label9.Invoke(new Action(() => label9.Text = time.ToString()));
                    if (saveData.Checked)
                    {
                        setTime = time;
                        sw.WriteLine(x + "\t" + y + "\t" + z+"\tTime:"+(time-setTime));
                        sw.Flush();
                        lastTime = time;
                    }
                    label11.Invoke(new Action(() => label11.Text = (lastTime - setTime).ToString()));

                    counter++;
                }
                catch(Exception e){/*MessageBox.Show(e.ToString());*/ stopit = true; }
            }
            sw.Close();
            myListener.Stop();
        }
        long? _time=null;
        public long? setTime
        {
            get { return _time; }
            set
            {
                if (_time ==null)
                    _time = value;
            }
        }
        /// <summary>
        /// Diagram kirajzoltatása
        /// </summary>
        /// <param name="x">X tengely értéke</param>
        /// <param name="y">Y tengely értéke</param>
        /// <param name="z">Z tengely értéke</param>
        public void chartDrawing(double x, double y, double z)
        {
            chart1.Invoke(new Action(() => DrawingControl.SuspendDrawing(chart1)));
            inputdatahandler.reciveData("X tengely",x);
            inputdatahandler.reciveData("Y tengely", y);
            inputdatahandler.reciveData("Z tengely", z);
            double[] xdata = inputdatahandler.getAxisData("X tengely");
            double[] ydata = inputdatahandler.getAxisData("Y tengely");
            double[] zdata = inputdatahandler.getAxisData("Z tengely");
            chart1.Invoke(new Action(() => chart1.Series["X tengely"].Points.Clear()));
            chart1.Invoke(new Action(() => chart1.Series["Y tengely"].Points.Clear()));
            chart1.Invoke(new Action(() => chart1.Series["Z tengely"].Points.Clear()));
            for (int i = 0; i < xdata.Length; i++)
            {
                chart1.Invoke(new Action(() => chart1.Series["X tengely"].Points.AddXY("X tengely", xdata[i])));
                chart1.Invoke(new Action(() => chart1.Series["Y tengely"].Points.AddXY("Y tengely", ydata[i])));
                chart1.Invoke(new Action(() => chart1.Series["Z tengely"].Points.AddXY("Z tengely", zdata[i])));
            }
            chart1.Invoke(new Action(() => chart1.ChartAreas[0].AxisY.Minimum = -20));
            chart1.Invoke(new Action(() => chart1.ChartAreas[0].AxisY.Maximum = 20));
            chart1.Invoke(new Action(() => DrawingControl.ResumeDrawing(chart1)));
            chart1.Invoke(new Action(() => chart1.Invalidate()));
            chart1.Invoke(new Action(() => chart1.Update()));

            ifChecked(xVisible, "X tengely");
            ifChecked(yVisible, "Y tengely");
            ifChecked(zVisible, "Z tengely");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            switch(button1.Text)
            {
                case "Kapcsolódás": t.Start();button1.Text = "Szétkapcsolás";
                    break;
                case "Szétkapcsolás":t.Abort(); button1.Text = "Újrakapcsolódás";
                    break;
                case "Újrakapcsolódás":t = new Thread(doThisAllTime);t.Start(); button1.Text = "Szétkapcsolás";
                    break;
            }
        }
        /// <summary>
        /// Minden hálózatváltásnál változhat az IP, ezért ezt le kell kérnünk
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
        bool clicked = false;
        /// <summary>
        /// Elmentjük az aktuális pozíciót (gravitáció kiküszöbölése)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAct_Click(object sender, EventArgs e)
        {
            if (saveAct.Text == "Save actual position")
            {
                startPosition[0] = x;
                startPosition[1] = y;
                startPosition[2] = z;
                label10.Text = "X: " + startPosition[0] + "\nY: " + startPosition[1] + "\nZ: " + startPosition[2];
                saveAct.Text = "Reset actual position";
                clicked = true;
            }
            else if(saveAct.Text == "Reset actual position")
            {
                label10.Text = "";
                saveAct.Text = "Save actual position";
                clicked = false;
            }

        }
        /// <summary>
        /// A megtett utat számítjuk ki (még nincs kész)
        /// </summary>
        /// <param name="time"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public double distanceTravelled(long time, double speed)
        {
            double distance= 0;


            return distance;
        }
        /// <summary>
        /// Be van e kapcsolva az adott tengely láthatósága
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="serie"></param>
        public void ifChecked(CheckBox ch,string serie)
        {
            if (!ch.Checked)
                chart1.Invoke(new Action(() => chart1.Series[serie].Enabled=false));
            else
                chart1.Invoke(new Action(() => chart1.Series[serie].Enabled = true));
        }
        /// <summary>
        /// Eldönti, melyik radioButton van kiválasztva, és aszerint "dönt"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void whichRadioChosen(double x, double y, double z) //dynaamic is lehetne a tipusa
        {
            //else if, hogy elkerüljük azt, hogy minden ágba belemenjen
            if (radioButton1.Checked)
            {
                chart1.Invoke(new Action(() => chart1.Visible = true));
                chartDrawing(x, y, z);
                return;
            }
            else if (radioButton2.Checked)
            {
                chart1.Invoke(new Action(() => chart1.Visible = false));
                drawEllipse(x, y);
                return;
            }
            else if (radioButton3.Checked)
            {
                chart1.Invoke(new Action(() => chart1.Visible = false));
                drawSpring(y);
                return;
            }
        }
        /// <summary>
        /// Ellipszis rajzoló az ingához
        /// </summary>
        /// <param name="Nx"></param>
        /// <param name="Ny"></param>
        private void drawEllipse(double Nx, double Ny)
        {
            Invoke(new Action(() => Invalidate()));
            Invoke(new Action(() => Update()));
            Graphics g = CreateGraphics();
            Pen p = new Pen(Color.Red);
            Pen pendPen = new Pen(Color.Black);
            //Lassú mozgásnál a gravitáció elég a tengelyekre, de goyrs mozgásnál csak 1 tengelyre megy rá
            //Nx = Ny - 9.6F;
            if(time == -1)
            {
                Nx = Nx * 10;
                Ny = Ny * 10;
            }
            else
            {
                Nx = Nx * 10;
                Ny = Nx / 10 + 80;
            }
            label10.Invoke(new Action(() => label10.Text = Nx.ToString()));
            float xPoint = Convert.ToSingle(Nx) + 500;
            float yPoint = Convert.ToSingle(Ny) + 50;
            g.FillRectangle(new SolidBrush(Color.Black), 450, 20,130,20);
            //"Fonál"
            g.DrawLine(pendPen, 515, 40, xPoint+15, yPoint);
            //Kezdő függőleges vonal
            g.DrawLine(new Pen(Color.Blue,3), 515, 40, 515, 200);
            //Thetajelző vonal
            g.DrawLine(new Pen(Color.Red),515,150,(float)Nx+515,(float)Ny+65);
            //A telefont helyettesítő test
            g.FillEllipse(new SolidBrush(Color.Red), ((float)Nx+500), ((float)Ny + 50) , 30, 30);
        }
        /// <summary>
        /// Harmonikus rezgőmozgás
        /// 
        /// Ebben az esetben csak a rugós mozgást nézzük, a többi irányba való kitérést nem vizsgáljuk
        /// </summary>
        /// <param name="Ny">Csak az Y tengelyen mért adatra van szükségünk</param>
        private void drawSpring(double Ny)
        {
            Invoke(new Action(() => Invalidate()));
            Invoke(new Action(() => Update()));
            Ny = Ny * 5;
            Graphics g = CreateGraphics();
            Pen arrow = new Pen(Color.Green,5);
            arrow.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            g.FillRectangle(new SolidBrush(Color.Black), 450, 20, 130, 20);
            g.DrawLine(new Pen(Color.Blue, 2),515,40,515,Convert.ToSingle(Ny)+100);
            g.FillEllipse(new SolidBrush(Color.Red), 500, Convert.ToSingle(Ny) + 100, 30, 30);
            //Rajzoljunk ki egy 2oldalú nyilat a szemléltetéshez
            //g.DrawLine(arrow, 600, 80, 600, Convert.ToSingle(Ny) +100);
            Pen arrow2 = new Pen(Color.Orange, 7);
            arrow2.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            g.DrawLine(arrow2, 600, Convert.ToSingle(Ny)+38, 600,75 );
            //Gravitácót levonva kiírjuk a gyorsulást
            g.DrawString("Gyorsulása: ~ " + (Ny/3-9.6).ToString() + " m/s^2", new Font("Arial", 12), new SolidBrush(Color.Black), 610, 75);
        }
        private void xVisible_CheckedChanged(object sender, EventArgs e)
        {
            ifChecked(xVisible, "X tengely");
        }

        private void yVisible_CheckedChanged(object sender, EventArgs e)
        {
            ifChecked(yVisible, "Y tengely");
        }

        private void zVisible_CheckedChanged(object sender, EventArgs e)
        {
            ifChecked(zVisible, "Z tengely");
        }
    }
}
