using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_2
{
    class Program
    {
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
        static float func(float x)
        {
            double c = x * Math.Pow(2, x) - 1;
            return (float)c;
             //return (float)Math.Sin(x);
        }
        static float raznost(float x1,float x2,float x3,float x4)
        {
            return (float)((x2 - x1) / (x4 - x3));
        }
        static float[,] zapol_tabl(float[,] tabl)
        {
            int i = 0, j = 2, n = 5, m = 0, x = 1;
            while (m!=4) {
                i = 0; n--;
                while (i != n+1)
                {
                    if (n != 4)
                    {
                        tabl[i, j] = raznost(tabl[i, j - 1], tabl[i + 1, j - 1], tabl[i, 0], tabl[i + x, 0]);
                    }
                    else
                    {
                        tabl[i, j] = raznost(tabl[i, j - 1], tabl[i + 1, j - 1], tabl[i, 0], tabl[i + 1, 0]);
                    }
                    i++;
                }
                j++;
                m++;
                x++;
            }
            return tabl;
        }
        static int find_in_x(float[] x, float key)
        {
            int i = 0;
            foreach(float j in x)
            {
                if (j == key)
                    return i;
                i++;
            }
            return -1;
        }
        static float Newton(int n,float x, List<float> list, float[] arr_x, float[,] tabl)
        {
            float sum = 0;
            int str = find_in_x(arr_x, list[0]);
            sum += func(list[0]);
            int i = 0;
            int j = 0;
            int str1 = 2;
            while(i!=n)
            {
                float el = 1;
                while (j<=i)
                {
                    el *= (x - list[j]);
                    j++;
                }
                el *= tabl[str, str1];
                sum += el;
                str1++;
                i++;
                j = 0;
            }
            return sum;
        }
        static void Main(string[] args)
        {
            float[,] tabl = new float[6, 7];
            float h = 1f;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if(j==0)
                    {
                            tabl[i, j] = h;
                            h += 0.2f;
                            tabl[i, j + 1] = func(tabl[i, j]);
                    }
        
                    else
                    {
                        if (!(j == 1))
                        {
                            tabl[i, j] = -1;
                        }
                    }
                }
                
            }
            tabl = zapol_tabl(tabl);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (tabl[i, j] != -1)
                    {
                        Console.Write("{0,15}", tabl[i, j]);
                    }
                    else
                    {
                        Console.Write("{0,15}", "");
                    }
                }
                Console.WriteLine();
            }

            float[] x = { 1, 1.2f, 1.4f, 1.6f, 1.8f, 2 };
            //float[] x = { 0, 0.2f, 0.4f, 0.6f, 0.8f, 1 };
            float[] y = new float[6];
            for (int i = 0; i < 6; i++)
            {
                y[i] = func(x[i]);
            }

            Console.Write("Step: ");
            int n;
            n = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter 3 point X: ");
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

            float N1 = Newton(n, x1, yzl_x1, x, tabl);
            float N2 = Newton(n, x2, yzl_x2, x, tabl);
            float N3 = Newton(n, x3, yzl_x3, x, tabl);
            Console.WriteLine("N1 = "+ N1);
            Console.WriteLine("N2 = " + N2);
            Console.WriteLine("N3= " + N3);
            float dX1 = Math.Abs(N1 - func(x1));
            float bX1 = (float)((dX1 / func(x1)) * 100);
            Console.WriteLine("Абс. погр. х1 = " + dX1 + " , Относ. погр. х1 = " + bX1 + "\n");

            float dX2 = Math.Abs(N2 - func(x2));
            float bX2 = (float)((dX2 / func(x2)) * 100);
            Console.WriteLine("Абс. погр. х2 = " + dX2 + " , Относ. погр. х2 = " + bX2 + "\n");

            float dX3 = Math.Abs(N3 - func(x3));
            float bX3 = (float)((dX3 / func(x3)) * 100);
            Console.WriteLine("Абс. погр. х3 = " + dX3 + " , Относ. погр. х3 = " + bX3 + "\n");

        }
    }
}
