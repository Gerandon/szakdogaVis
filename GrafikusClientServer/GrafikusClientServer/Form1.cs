using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Globalization;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Diagnostics;

namespace GrafikusClientServer
{
    public partial class Form1 : Form
    {
        Thread t;
        long duration;
        Stopwatch watch = new Stopwatch();
        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            withGraph.Enabled = false;
            t = new Thread(doThisAllTime);
        }
        DateTime period=DateTime.Now;
        double deltaTime;

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
                    x = double.Parse(str_array[0], CultureInfo.InvariantCulture);
                    y = double.Parse(str_array[1], CultureInfo.InvariantCulture);
                    z = double.Parse(str_array[2], CultureInfo.InvariantCulture);
                    newX = double.Parse(str_array[0], CultureInfo.InvariantCulture);
                    newY = double.Parse(str_array[1], CultureInfo.InvariantCulture);
                    newZ = double.Parse(str_array[2], CultureInfo.InvariantCulture);
                    whichRadioChosen(x, y, z);
                        
                    time = long.Parse(str_array[3], CultureInfo.InvariantCulture);
                    label2.Invoke(new Action(() => label2.Text = x.ToString()));
                    label3.Invoke(new Action(() => label3.Text = y.ToString()));
                    label4.Invoke(new Action(() => label4.Text = z.ToString()));
                    //label9.Invoke(new Action(() => label9.Text = time.ToString()));
                    
                    if (saveData.Checked)
                    {
                        setTime = time;
                        sw.WriteLine(x + "\t" + y + "\t" + z+"\tTime:"+(time-setTime));
                        sw.Flush();
                        lastTime = time;
                    }
                    //label11.Invoke(new Action(() => label11.Text = (lastTime - setTime).ToString()));
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
        public void chartDrawing(double x, double y, double z,bool isHarmonical)
        {
            if (!isHarmonical)
            {
                chart1.Invoke(new Action(() => DrawingControl.SuspendDrawing(chart1)));
                inputdatahandler.reciveData("X tengely", x);
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
            else
            {
                y = y - 9.8;
                chart1.Invoke(new Action(() => DrawingControl.SuspendDrawing(chart1)));
                inputdatahandler.reciveData("X tengely", x);
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
            if (saveAct.Text == "Aktuális pozíció mentése")
            {
                //startPosition[0] = x;
                startPosition[1] = y;
                //startPosition[2] = z;
                label10.Text = "X: " + startPosition[0] + "\nY: " + startPosition[1] + "\nZ: " + startPosition[2];
                saveAct.Text = "Aktuális pozíció törlése";
                clicked = true;
            }
            else if(saveAct.Text == "Aktuális pozíció törlése")
            {
                label10.Text = "";
                saveAct.Text = "Aktuális pozíció mentése";
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
                label9.Invoke(new Action(() => label9.Visible = false));
                chart1.Invoke(new Action(() => chart1.Visible = true));
                chart1.Invoke(new Action(() => chart1.Size = new Size(950,287)));
                chart1.Invoke(new Action(() => chart1.Location = new Point(239, 18)));
                chartDrawing(x, y, z,false);
                return;
            }
            else if (radioButton2.Checked)
            {

                label9.Invoke(new Action(() => label9.Visible = false));
                chart1.Invoke(new Action(() => chart1.Visible = false));
                drawEllipse(x, y);
                return;
            }
            else if (radioButton3.Checked)
            {
                

                label9.Invoke(new Action(() => label9.Visible = true));
                withGraph.Invoke(new Action(() => withGraph.Enabled = true));
                if (withGraph.Checked)
                {
                    zVisible.Invoke(new Action(() => zVisible.Checked=false));
                    xVisible.Invoke(new Action(() => xVisible.Checked=false));
                    chart1.Invoke(new Action(() => chart1.Location= new Point(400,50)));
                    chart1.Invoke(new Action(() => chart1.Size = new Size(700,287)));
                    
                    chartDrawing(x, y, z,true);
                    deltaTime = (DateTime.Now-period).TotalMilliseconds;
                    period = DateTime.Now;
                }
                else
                {
                    chart1.Invoke(new Action(() => chart1.Visible = false));
                }
                
                drawSpring(y);
                return;
            }
            else if(egyenesVonalu.Checked)
            { 
                label10.Invoke(new Action(() => label10.Text = watch.Elapsed.ToString()));
                if (y < -1)
                {
                    watch.Stop();
                }
            }
        }
        private void stopper_button_Click(object sender, EventArgs e)
        {
            watch.Restart();
            
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
            if (time == -1)
            {
                Nx = Nx * 10;
                Ny = Ny * 10;
            }
            else
            {
                Nx = Nx * 10;
                Ny = Nx / 10 + 80;
            }
            if (clicked && Ny / 10 > startPosition[1])
            {
                Ny = startPosition[1] * 10;
            }
            //label10.Invoke(new Action(() => label10.Text = Nx.ToString()));
            float xPoint = Convert.ToSingle(Nx) + 500;
            float yPoint = Convert.ToSingle(Ny) + 50;
            g.FillRectangle(new SolidBrush(Color.Black), 450, 20, 130, 20);
            //"Fonál"
            g.DrawLine(pendPen, 515, 40, xPoint + 15, yPoint);
            //Kezdő függőleges vonal
            g.DrawLine(new Pen(Color.Blue, 3), 515, 40, 515, 200);
            //Thetajelző vonal
            g.DrawLine(new Pen(Color.Red), 515, 150, (float)Nx + 515, (float)Ny + 65);
            //A telefont helyettesítő test
            g.FillEllipse(new SolidBrush(Color.Red), ((float)Nx + 500), ((float)Ny + 50), 30, 30);

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
            g.FillRectangle(new SolidBrush(Color.Black), 260, 20, 100, 20);
            //Spring

            //g.DrawLine(new Pen(Color.Blue, 2),309,40,309,Convert.ToSingle(Ny)+80);
            //pictureBox1.Invoke(new Action(() => pictureBox1.Location = new Point(260, 40)));
            //pictureBox1.Invoke(new Action(() => pictureBox1.Size = pictureBox1.Size = new Size(100, (int)Ny+40)));
            DrawRugo(309, 40, Convert.ToInt32(Ny)+40, 0,30, 10);
            //Lefelé nyíl
            Pen lenyil = new Pen(Color.Green, 5);
            lenyil.EndCap = LineCap.ArrowAnchor;
            g.DrawLine(lenyil, 309, Convert.ToSingle(Ny) +95, 309, Convert.ToSingle(Ny) + 145);
            g.DrawString("Fₙ", new Font("Arial", 16), new SolidBrush(Color.Black), 255, Convert.ToSingle(Ny) + 110);

            //g.FillEllipse(new SolidBrush(Color.Red), 294, Convert.ToSingle(Ny) + 80, 30, 30);
            g.FillRectangle(new SolidBrush(Color.RosyBrown), 290 , Convert.ToSingle(Ny) + 80, 40, 30);
            //Rajzoljunk ki egy 2oldalú nyilat a szemléltetéshez
            //g.DrawLine(arrow, 600, 80, 600, Convert.ToSingle(Ny) +100);
            Pen arrow2 = new Pen(Color.Red, 4);
            arrow2.StartCap = LineCap.ArrowAnchor;
            
            //Gravitácót levonva kiírjuk a gyorsulást
            label9.Invoke(new Action(() => label9.Text = "Gyorsulás: "+Math.Round(Convert.ToDecimal((Ny / 5-9.81).ToString()), 1) + "m/s\xB2"));
            //rugó line
            Pen rugoArrow = new Pen(Color.Blue, 5);
            rugoArrow.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            g.DrawLine(rugoArrow, 309, Convert.ToSingle(Ny) + 95, 309, 100);
            double felNyilLength = (Convert.ToSingle(Ny) + 95) - 100;
            double leNyilLength = (Convert.ToSingle(Ny)+95)-(Convert.ToSingle(Ny)+145);
            double eredoEro = felNyilLength + leNyilLength;
            //Gyorsulás vektor
            g.DrawLine(arrow2, 350, 100, 350, (float)eredoEro+100);
            g.DrawString("Fₙ", new Font("Arial", 16), new SolidBrush(Color.Red), 355, 110);
            g.DrawString("Fᵣ", new Font("Arial", 16), new SolidBrush(Color.Blue), 255, Convert.ToSingle(Ny) + 45);
            if (circle.Checked)
            {
                g.DrawEllipse(new Pen(Color.Green), 390, 90, 80, 80);
                if (Ny > lastY)
                {
                    b = 455;
                    g.FillEllipse(new SolidBrush(Color.Blue), a--, Convert.ToSingle(Ny)+80, 10, 10);
                }
                else if(Ny<lastY)
                {
                    a = 390;
                    g.FillEllipse(new SolidBrush(Color.Blue), b++, Convert.ToSingle(Ny)+80, 10, 10);

                }
                lastY = Ny;
            }
        }
        int a = 390;
        int b = 455;
        List<int> circleXCoord = new List<int>();
        double lastY = 0;
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
        private void DrawRugo(int x, int y, int length, int angle, int partLength, int partCount)
        {
            double xM, yM;
            int dir;
            double dummy, dummyAngle, dist;
            List<PointF> points = new List<PointF>();

            int bodyLength = length - 20;
            float newX = x;
            float newY = y;
            // y kitérés a rugó testhossza / részelem darabszám
            yM = (double)bodyLength / (double)partCount;
            xM = Math.Sqrt(Math.Pow(partLength, 2) - Math.Pow(yM, 2));
            dir = -1;
            points.Add(new PointF(newX, newY));
            newY += 7;
            points.Add(new PointF(newX, newY));
            newX -= (float)(xM / 2d);
            newY += 3;
            points.Add(new PointF(newX, newY));
            for (int i = 0; i < partCount; i++)
            {
                dir *= -1;
                newX += (float)(xM * dir);
                newY += (float)yM;
                points.Add(new PointF(newX, newY));
            }
            points.Add(new PointF(x, newY + 3f));
            points.Add(new PointF(x, y + length));
            PointF newPF = new PointF();

            for (int i = 1; i < points.Count(); i++)
            {
                dist = Math.Sqrt(Math.Pow(points[i].X - points[0].X, 2) + Math.Pow(points[i].Y - points[0].Y, 2));
                dummy = points[i].X - points[0].X;
                dummyAngle = Math.Asin(dummy / dist);
                dummyAngle *= 180d / Math.PI;
                dummyAngle = 90d - dummyAngle - (double)angle;
                dummyAngle /= 180d / Math.PI;
                newPF.X = points[0].X + (float)(Math.Cos(dummyAngle) * dist);
                newPF.Y = points[0].Y + (float)(Math.Sin(dummyAngle) * dist);
                points[i] = newPF;
            }

            using (var gr = this.CreateGraphics())
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.CompositingQuality = CompositingQuality.HighQuality;
                gr.CompositingMode = CompositingMode.SourceOver;
                Pen grayPen = new Pen(Color.Gray, 3);
                Pen blackPen = new Pen(Color.Black, 3);
                Pen pen = new Pen(Color.Black, 1);
                gr.DrawLine(pen, points[0].X, points[0].Y, points[1].X, points[1].Y);
                gr.DrawLine(pen, points[points.Count() - 2].X, points[points.Count() - 2].Y, points[points.Count() - 1].X, points[points.Count() - 1].Y);
                for (int i = 1; i < points.Count() - 2; i++)
                {
                    if (i % 2 == 0)
                        pen = grayPen;
                    else
                        pen = blackPen;

                    gr.DrawLine(pen, points[i].X, points[i].Y, points[i + 1].X, points[i + 1].Y);
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


    }
}
