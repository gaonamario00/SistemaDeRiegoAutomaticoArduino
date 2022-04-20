namespace SistemaDeRiegoAutomaticoArduino
{
    public partial class Form1 : Form
    {

        private System.IO.Ports.SerialPort port;
        bool isClosed = false;

        public Form1()
        {
            InitializeComponent();

            textBox.Visible = false;

            /////////////////
            ///
            port = new System.IO.Ports.SerialPort();
            port.PortName = "COM3";
            port.BaudRate = 9600;
            port.ReadTimeout = 1500;



            try
            {
                port.Open();
            }
            catch (Exception e)
            {
            }

        }

        public void escucharSerial()
        {
            while (!isClosed)
            {
                try
                {
                    string cadena = port.ReadLine();
                    //invoke para leer datos
                    textBox.Invoke(new MethodInvoker(
                    delegate
                    {
                        textBox.Text = cadena;
                        llenarCampos(textBox.Text);
                    }));
                }
                catch (Exception e)
                {
                }
            }
        }

        public void llenarCampos(string cadena)
        {
            string[] result = cadena.Split("&", StringSplitOptions.RemoveEmptyEntries);

            try
            {
                labelHumedad.Text = result[0];
                labelTemp.Text = result[1];
                labelCalor.Text = result[2];
            }
            catch { }


            //List<string> list = new List<string>();

            //foreach (string a in result)
            //{
            //    list.Add(a);
            //}

            //labelHumedad.Text = list[0];
            //labelTemp.Text = list[1];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(escucharSerial);
            thread.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            isClosed = true;
            if (port.IsOpen)
            {
                port.Close();
            }
        }

        private void labelTemp_Click(object sender, EventArgs e)
        {

        }
    }
}