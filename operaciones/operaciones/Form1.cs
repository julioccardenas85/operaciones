using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace operaciones
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "Archivos de texto (*.txt)|*.txt";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string str_RutaArchivo = openFileDialog1.FileName;
                    textBox1.Text = str_RutaArchivo;
                    richTextBox1.Clear();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread t0 = new Thread(moda);
            Thread t1 = new Thread(mediana);
            Thread t2 = new Thread(suma);
            Thread t3 = new Thread(reciprocoMayor);

            t0.Start();
            t1.Start();
            t2.Start();
            t3.Start();
        }

        private void moda()
        {
            char[] delimit = new char[] { '\t' };
            StreamReader objReader = new StreamReader(textBox1.Text, System.Text.Encoding.UTF8);
            string linea = "";
            Dictionary<string, int> moda = new Dictionary<string, int>();
            while (linea != null)
            {
                linea = objReader.ReadLine();
                if (linea != null)
                {
                    foreach (string substr in linea.Split(delimit))
                    {
                        if (substr != "" && Convert.ToDouble(substr) >= 0 && Convert.ToDouble(substr) <= 50)
                        {
                            if (moda.ContainsKey(substr))
                            {
                                moda[substr] += 1;
                            }
                            else
                            {
                                moda.Add(substr, 1);
                            }
                        }
                    }
                }
            }
            var resultado = moda.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            escribeDato("La moda de los valores entre 0 y 50 es: " + resultado + "\n");
        }

        private void mediana()
        {
            char[] delimit = new char[] { '\t' };
            StreamReader objReader = new StreamReader(textBox1.Text, System.Text.Encoding.UTF8);
            string linea = "";
            ArrayList list = new ArrayList();
            while (linea != null)
            {
                linea = objReader.ReadLine();
                if (linea != null)
                {
                    foreach (string substr in linea.Split(delimit))
                    {
                        if (substr != "" && Convert.ToDouble(substr) >= 100 && Convert.ToDouble(substr) <= 600)
                        {
                            list.Add(substr);
                        }
                    }
                }
            }
            list.Sort();
            int index = 0;
            double mediana = 0;
            if ((list.Count % 2) != 0)
            {
                index = (list.Count + 1) / 2;
                escribeDato("La mediana de los valores entre 100 y 600 es: " + list[index] + "\n");
            }
            if (list.Count > 0 && (list.Count % 2) == 0)
            {
                mediana = (Convert.ToDouble(list[(list.Count / 2) - 1]) + Convert.ToDouble(list[list.Count / 2])) / 2;
                escribeDato("La mediana de los valores entre 100 y 600 es: " + mediana + "\n");
            }
            if (list.Count == 0)
            {
                escribeDato("No hay valores entre 100 y 600\n");
            }
        }

        private void suma ()
        {
            char[] delimit = new char[] { '\t' };
            StreamReader objReader = new StreamReader(textBox1.Text, System.Text.Encoding.UTF8);
            string linea = "";
            double suma = 0;
            while (linea != null)
            {
                linea = objReader.ReadLine();
                if (linea != null)
                {
                    foreach (string substr in linea.Split(delimit))
                    {
                        if (substr != "" && Convert.ToDouble(substr) >= 5000 && Convert.ToDouble(substr) <= 10000)
                        {
                            suma += Convert.ToDouble(substr);
                        }
                    }
                }
            }
            escribeDato("La suma total de los valores entre 5,000 y 10,000 es: " + Convert.ToString(suma) + "\n");
        }

        private void reciprocoMayor()
        {
            char[] delimit = new char[] { '\t' };
            StreamReader objReader = new StreamReader(textBox1.Text, System.Text.Encoding.UTF8);
            string linea = "";
            double max = 49999.999999999;
            while (linea != null)
            {
                linea = objReader.ReadLine();
                if (linea != null)
                {
                    foreach (string substr in linea.Split(delimit))
                    {
                        if (substr != "" && Convert.ToDouble(substr) >= 50000 && Convert.ToDouble(substr) <= 100000)
                        {

                            if (Convert.ToDouble(substr) > max)
                            {
                                max = Convert.ToDouble(substr);
                            }
                        }
                    }
                }
            }
            double reciproco = 1 / max;
            if (max >= 50000)
            {
                escribeDato("El mayor valor entre 50,000 y 100,000 es: " + max + " y su recíproco es: " + reciproco + "\n");
            } 
            else
            {
                escribeDato("No hay valores entre 50,000 y 100,000\n");
            }
            
        }

        private void escribeDato(string dato)
        {
            richTextBox1.Invoke(new Action(() => richTextBox1.AppendText(dato)));
        }
    }
}
