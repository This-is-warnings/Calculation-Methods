using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_CSharp
{
    class Program
    {
        static float func(float x)
        {
            double c = x * Math.Pow(2, x) - 1;
            return (float)c;
           // return (float)Math.Sin(x);
        }

        static bool find(List<float> v, float a)
        {
            int i = 0;
            while (i != v.Count)
            {
                if (v[i] == a) return true;
                i++;
            }
            return false;
        }
        static List<float> yzli_setki(float[] mas, float k, float n)
        {
            List<float> c = new List<float>();
            int schetchik = 0;

            float raznica = 999999, chislo = -1;
            while (schetchik != n + 1)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (Math.Abs(mas[i] - k) < raznica && !find(c, mas[i]))
                    {
                        raznica = Math.Abs(mas[i] - k);
                        chislo = mas[i];
                    }
                }
                c.Add(chislo);
                raznica = 999999; chislo = -1;
                schetchik++;
            }
            return c;
        }
        static float Lagrange(float x, int n, List<float> xA,float[] yA)
        {
            float sum = 0;
            for(int i = 0; i < n+1;i++)
            {
                float element = 1;
                for(int j = 0; j < n+1;j++)
                {
                    if(j!=i)
                    {
                        element *= (x - xA[j]) / (xA[i] - xA[j]);
                    }
                }
                sum += element * func(xA[i]);
            }
            return sum;
        }
        static float[] list_to_mas(List<float> f)
        {
            float[] mas = new float[f.Count];
            float min = 99999;
            for (int j = 0; j < f.Count; j++)
            {
                for (int i = 0; i < f.Count - j; i++)
                {
                    if (f[i] < min) min = f[i];
                }
                mas[j] = min;
                f.Remove(min);
            }
            return mas;
        }
       /* static float third_degree_derivative(float x)
        {
            // 2^(x)*x*(ln(2))^2 + 2*2^(x)*ln(2)
            // return (float)(Math.Pow(2, x) * x * Math.Pow(Math.Log10(2), 2) + 2 * Math.Pow(2, x) * Math.Log10(2));
            return (float)Math.Cos(x);
        }

        static float find_eps(float[] mas, float x)
        {
            float raznica = 999999, chislo = -1;
            for (int i = 0; i < 6; i++)
               {
                    if (Math.Abs(mas[i] - x) < raznica)
                    {
                        raznica = Math.Abs(mas[i] - x);
                        chislo = mas[i];
                    }
               }
            return chislo;
        }
        static int fact(int n)
        {
            int k = 1;
            for(int i = 1; i <= n;i++)
            {
                k *= i;
            }
            return k;
        }
        static float ret_r(float x, float eps, int n, List<float> yzli)
        {
            float a = (float)(third_degree_derivative(eps) / fact(n + 1));
            float b = 1;
            for(int i = 0; i < yzli.Count;i++)
            {
                b *= x - yzli[i];
            }
            return a * Math.Abs(b);
        }
        */
        
        static void Main(string[] args)
        {
            int x0 = 1, xn = 2;
            float h = 0.2f;
            float[] x = { 1, 1.2f, 1.4f, 1.6f, 1.8f, 2 };
            //float[] x = { 0, 0.2f, 0.4f, 0.6f, 0.8f, 1 };
            float[] y = new float[6];
            for (int i = 0; i < 6; i++)
            {
                y[i] = func(x[i]);
            }
            Console.Write("x : ");
            for (int i = 0; i < 6; i++)
            {
                Console.Write("{0,15}", x[i]);

            }
            Console.WriteLine();
            Console.Write("y : ");
            for (int i = 0; i < 6; i++)
            {
                Console.Write("{0,15}", y[i]);

            }
            Console.WriteLine();

            Console.Write("Step: ");
            int n;
            n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine( "Enter 3 point X: ");
            float x1, x2, x3;
            x1 = (float)Convert.ToDouble(Console.ReadLine());
            x2 = (float)Convert.ToDouble(Console.ReadLine());
            x3 = (float)Convert.ToDouble(Console.ReadLine());

            List<float> yzl_x1 = yzli_setki(x, x1, n);            
            List<float> yzl_x2 = yzli_setki(x, x2, n);
            List<float> yzl_x3 = yzli_setki(x, x3, n);

            yzl_x1.Sort();
            yzl_x2.Sort();
            yzl_x3.Sort();

            Console.WriteLine("Узлы сетки для x1 = " + x1);
            foreach (float i in yzl_x1)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Узлы сетки для x2 = " + x2);
            foreach (float i in yzl_x2)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Узлы сетки для x3 = " + x3);
            foreach (float i in yzl_x3)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();

            float L1 = Lagrange(x1, n, yzl_x1, y);
            float L2 = Lagrange(x2, n, yzl_x2, y);
            float L3 = Lagrange(x3, n, yzl_x3, y);

            Console.WriteLine("L1(" + x1 + ") для n = " + n + " : " + L1);
            Console.WriteLine("L2(" + x2 + ") для n = " + n + " : " + L2);
            Console.WriteLine("L3(" + x3 + ") для n = " + n + " : " + L3);

            //float eps = find_eps(x,x1);
            //Console.WriteLine("r = " + ret_r(x1, eps, n, yzl_x1));

            // 1 - абсол, 2 - относ.

            float dX1 = Math.Abs(L1 - func(x1));
            float bX1 = (float)((dX1 / func(x1)) * 100);
            Console.WriteLine("Абс. погр. х1 = " + dX1 + " , Относ. погр. х1 = " + bX1 + "\n");

            float dX2 = Math.Abs(L2 - func(x2));
            float bX2 = (float)((dX2 / func(x2)) * 100);
            Console.WriteLine("Абс. погр. х2 = " + dX2 + " , Относ. погр. х2 = " + bX2 + "\n");

            float dX3 = Math.Abs(L3 - func(x3));
            float bX3 = (float)((dX3 / func(x3)) * 100);
            Console.WriteLine("Абс. погр. х3 = " + dX3 + " , Относ. погр. х3 = " + bX3 + "\n");

            Console.Read();
        }
    }
}
