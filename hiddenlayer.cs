using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNISTForms
{
class hiddenlayer : layer
{
    public hiddenlayer(int Size, layer.Activation_Type Type, Random rand)
    {
        type = Type;
        size = Size;
        a = new double[size];
        b = new double[size];
        s = new double[size];
        b0 = new double[size];

        rand.Next();
        for (int i = 0; i < size; i++)
        {
            b[i] = rand.NextDouble() -0.5;
        }
    }

    public hiddenlayer(layer tocopy)
    {
        size = tocopy.size;
        a = new double[size];
        b = new double[size];
        s = new double[size];
        b0 = new double[size];
        for (int i = 0; i < size; i++)
        {
            b[i] = tocopy.b[i];
        }
        type = tocopy.type;
    }
        public override void Activate()
        {

            switch (type)
            {
                case Activation_Type.tanh:
                    {
                        for (int i = 0; i < size; i++)
                        {
                            a[i] = Math.Tanh(s[i]);
                        }
                    }
                    break;
                case Activation_Type.sigmoid:
                    {
                        for (int i = 0; i < size; i++)
                        {
                            a[i] = 1 / (1 + Math.Exp(-s[i]));
                        }
                    }
                    break;
                case Activation_Type.softmax:
                    {
                        double softsum = 0.0;
                        for (int i = 0; i < size; i++)
                        {
                            softsum += Math.Exp(s[i]);
                        }
                        for (int i = 0; i < size; i++)
                        {
                            a[i] = Math.Exp(s[i]) / softsum;
                        }
                    }
                    break;
                case Activation_Type.relu:
                    {
                        for (int i = 0; i < size; i++)
                        {
                            if (s[i] < 0)
                            {
                                a[i] = 0;
                            }
                            else
                            {
                                a[1] = s[i];
                            }
                        }
                    }

                    break;
                default:

                    break;


            }
            for (int i = 0; i < size; i++)
            {

            }

        }
    }
}
