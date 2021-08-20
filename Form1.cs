using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace MNISTForms
{
public partial class Form1 : Form
{
    string fileinput = "";
    neuralnet MyNN;
    public Form1()
    {
        InitializeComponent();
        MyNN = new neuralnet();
        textBox2.Text = MyNN.getLayerList();
        comboBox1.SelectedIndex = 1;
        comboBox2.SelectedIndex = 0;
        comboBox3.SelectedIndex = 1;
        numericUpDown3.Value = (decimal)MyNN.lr;
    }

    private void button1_Click(object sender, EventArgs e)
    {

        //open menu to select file
        openFileDialog1.ShowDialog();
        fileinput = openFileDialog1.FileName;
        if (pictureBox1.Image != null)
        {
            pictureBox1.Image.Dispose();
            pictureBox1.Image = null;
        }

        if (File.Exists(@fileinput))//if file exists
        {
            //print image
            pictureBox1.Image = Image.FromFile(@fileinput);

            //read the image then propogate
            MyNN.read_image(fileinput);
            MyNN.AutoFProp();

            textBox1.Text = MyNN.get_guess();
            Console.WriteLine(MyNN.get_guess());
            textBox1.Update();

            textBox1.Refresh();
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        //MyNN.xortest();
        //MyNN.txtoutput();

        layer.Activation_Type tipe;
        if (comboBox3.SelectedIndex == 0)
        {
            tipe = layer.Activation_Type.tanh;
        }
        else if (comboBox3.SelectedIndex == 1)
        {
            tipe = layer.Activation_Type.sigmoid;
        }
        else
        {
            tipe = layer.Activation_Type.relu;
        }
        /*else
        {
            tipe = layer.Activation_Type.softmax;
        }*/
        MyNN.last.type = tipe;
        MyNN.trained = false;
        textBox2.Text = MyNN.getLayerList();
        textBox2.Update();

        textBox2.Refresh();

        warning();

    }

    private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
    {

    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {

    }

    private void button3_Click(object sender, EventArgs e)
    {
        textBox1.Text = "";
        for (int j = 0; j < numericUpDown1.Value; j++)
        {
            textBox1.Text += "Running Epoch " + (j+1).ToString();
            textBox1.Update();

            textBox1.Refresh();

            int epochL;

            string filenaym;

            if (comboBox2.SelectedIndex == 0)
            {
                epochL = 60000;
                filenaym = "\\mnist_train.csv";
            }
            else
            {
                    epochL = 20000;
                filenaym = "\\mnist_test.csv";

            }
            progressBar1.Maximum = epochL;


            System.IO.StreamReader file = new System.IO.StreamReader(@Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + filenaym);
            progressBar1.Value = 0;
            MyNN.correct = 0;
            for (int i = 0; i < epochL; i++)
            {
                //inputlayer x = new inputlayer(784);
                //Console.WriteLine("Test");
                MyNN.readcsv(file);
                //MyNN.testpritn();
                //Console.WriteLine("iteration: " + i);

                /*
                Thread t = new Thread(MyNN.autoBF);
                t.Start();
                t.Join();
                */
                MyNN.autoBF();
                progressBar1.Increment(1);
                progressBar1.Update();
            }

                if (filenaym == "\\mnist_train.csv")
                {
                    chart1.Series["MNIST_TRAIN"].Points.Add(MyNN.correct / (double)epochL);
                }
                else
                {
                    chart1.Series["MNIST_TEST"].Points.Add(MyNN.correct / (double)epochL);
                }
            textBox1.Text = "Epoch " + (j+1) +' ';
            textBox1.Text += MyNN.correct.ToString() + '/' + epochL.ToString() + " Correct! " + (MyNN.correct/ ((double)epochL) * 100.0).ToString() + '%' + System.Environment.NewLine;
            chart1.Update();
        }

        MyNN.trained = true;
        textBox1.Text += "Done training!";

        textBox1.Update();

        textBox1.Refresh();
    }

    private void progressBar1_Click(object sender, EventArgs e)
    {
    }

    private void button4_Click(object sender, EventArgs e)
    {
        MyNN.txtoutput(textBox3.Text);
        textBox1.Text = "weights and biases exported to" + System.Environment.NewLine + @Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + "\\" + textBox3.Text;
    }

    private void button5_Click(object sender, EventArgs e)
    {

        MyNN.trained = false;
        layer.Activation_Type tipe;
        if (comboBox1.SelectedIndex == 0)
        {
            //Console.WriteLine("tanh");
            tipe = layer.Activation_Type.tanh;
        }
        else if (comboBox1.SelectedIndex == 1)
        {
            //Console.WriteLine("sig");
            tipe = layer.Activation_Type.sigmoid;
        }
        else
        {
            tipe = layer.Activation_Type.relu;
        }
        /*else
        {
            tipe = layer.Activation_Type.softmax;
        }*/
        MyNN.addLayer(Convert.ToInt32(numericUpDown2.Value), tipe);
        textBox2.Text = MyNN.getLayerList();
        textBox2.Update();

        textBox2.Refresh();

        warning();
    }

    private void label1_Click(object sender, EventArgs e)
    {

    }
    private void warning()
    {
        string toreturn = "";
        if (!MyNN.trained)
        {
            toreturn += "Neural Network is untrained, please train or load a pre-trained set" + System.Environment.NewLine;
        }
        textBox1.Text = toreturn;
        chart1.Series["MNIST_TRAIN"].Points.Clear();
        chart1.Series["MNIST_TEST"].Points.Clear();
        textBox1.Update();

        textBox1.Refresh();
    }

    private void button6_Click(object sender, EventArgs e)
    {

        MyNN.trained = false;
        MyNN.removeLayer();
        textBox2.Text = MyNN.getLayerList();
        textBox2.Update();

        textBox2.Refresh();
        warning();
    }

    private void button7_Click(object sender, EventArgs e)
    {
        //open menu to select file
        openFileDialog2.ShowDialog();
        fileinput = openFileDialog2.FileName;
        if (File.Exists(@fileinput))//if file exists
        {
                MyNN.txtinput(fileinput);
            MyNN.trained = true;

                //write success message here
                textBox2.Text = MyNN.getLayerList();
                textBox2.Update();

                textBox2.Refresh();
            }
        if (!MyNN.trained)
        {
            warning();
        }

    }

    private void numericUpDown3_ValueChanged(object sender, EventArgs e)
    {
        MyNN.lr = (double)numericUpDown3.Value;
    }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
