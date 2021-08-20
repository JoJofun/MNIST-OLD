using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MNISTForms
{
    class conlayer : layer
    {
        public double[,] filter;
        int filtersize;

        List<double[,]> filterlist;
        int psize;

        public conlayer(int Syze, int fsize, Random rand)
        {
            size = Syze;
            type = layer.Activation_Type.convolution;
            filtersize = fsize;
            filterlist = new List<double[,]>();

            //create filters

            for (int k = 0; k < size; k++)
            {
                filterlist.Add(filter = new double[filtersize, filtersize]);
                for (int i = 0; i < filtersize; i++)
                {
                    for (int j = 0; j < filtersize; j++)
                    {
                        filter[i, j] = rand.NextDouble() - 0.5;
                    }

                }
            }
            //
            b = new double[size];
            for (int i = 0; i < size; i++)
            {
                b[i] = rand.NextDouble() - 0.5;
            }
        }
        public override void Activate()
        {
            Console.WriteLine("test");
        }
    }
}
