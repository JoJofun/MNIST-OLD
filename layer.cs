using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNISTForms
{
class layer
{
    public int size;
    public double[] a;//activations
    public double[] b;//biases
    public double[] s;//sum before act
    public double[] b0;
    public Activation_Type type;

    public enum Activation_Type
    {
        tanh,
        sigmoid,
        softmax,
        relu,
        convolution
    }



    public int getSize()
    {
        return size;
    }

    public virtual void Activate() {

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
