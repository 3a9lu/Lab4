using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Lab4
{
    public partial class Form1 : Form
    {
        static double[] array;
        static double[] array2 = new double[] { 2, 6, 7, 10, 1, 4, 8, 14, 3, 1, 2, 0 };
        static double[] array3 = new double[] { 2, 6, 7, 10, 1, 4, 8, 14, 3, 1, 2, 0 };
        static double[] array4 = new double[] { 2, 6, 7, 10, 1, 4, 8, 14, 3, 1, 2, 0 };
        static double[] array5 = new double[] { 2, 6, 7, 10, 1, 4, 8, 14, 3, 1, 2, 0 };

        //static double maxNumArray = array.Max();
        static double maxNumArray2 = array2.Max();
        static double maxNumArray3 = array3.Max();
        static double maxNumArray4 = array4.Max();
        static double maxNumArray5 = array5.Max();

        private BufferedGraphics buffered;
        private BufferedGraphics buffered2;
        private BufferedGraphics buffered3;
        private BufferedGraphics buffered4;
        private BufferedGraphics buffered5;

        public static List<double> Excel = new List<double>(); // Список для точек
        public static List<Point> Ru = new List<Point>(); // Список для точек

        public class Point // Сохраниние чисел
        {
            public double x;
            public Point(double X)
            {
                this.x = X;
            }
        }

        public string[,] list;

        List<Thread> threads = new List<Thread>();

        public bool pause;

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;  // Вывод формы по центру экрана
            groupBox1.Text = "";
        }

        // Считывание с Excel
        #region
        private void считатьСExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            string path = textBox2.Text;
            Excel.Application ObjExcel = new Excel.Application();

            Workbook ObjWorkBook = ObjExcel.Workbooks.Open(path); // Открываем книгу
            Worksheet ObjWorkSheet = (Worksheet)ObjWorkBook.Sheets[1]; // Выбираем лист

            Range xRange = ObjWorkSheet.UsedRange.Columns[1]; // Первый столбец
            Array xCells = (Array)xRange.Cells.Value2;

            string[] xColumn = xCells.OfType<object>().Select(o => o.ToString()).ToArray();

            for (int i = 0; i < xColumn.Length; ++i)
            {
                Excel.Add(Convert.ToDouble(xColumn[i]));
                dataGridView1.Rows.Add(Excel[i]);
            }
            array = Excel.ToArray();


            ObjWorkBook.Close(); // Закрытие книги
            ObjExcel.Quit(); // Выход из Excel
        }
        #endregion

        // Считывание с Google Sheets
        #region
        private void считатьСGoogleSheetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            string link = textBox3.Text;
            string path = @"D:\Проекты\Lab4\Lab4\Sheets.xlsx";
            System.IO.File.Delete(path);

            string qq = link.Replace("edit?usp=sharing", "export?format=xlsx");

            using (var client = new WebClient()) // Скачивание файла
            {
                client.DownloadFile(new Uri(qq), path);
            }

            Excel.Application ObjExcel = new Excel.Application();

            Workbook ObjWorkBook = ObjExcel.Workbooks.Open(path); // Открываем книгу
            Worksheet ObjWorkSheet = (Worksheet)ObjWorkBook.Sheets[1]; // Выбираем лист

            Range xRange = ObjWorkSheet.UsedRange.Columns[1]; // Первый столбец
            Array xCells = (Array)xRange.Cells.Value2;

            string[] xColumn = xCells.OfType<object>().Select(o => o.ToString()).ToArray();

            for (int i = 0; i < xColumn.Length; ++i)
            {
                //Point point = new Point(double.Parse(xColumn[i]));
                //Excel.Add(point);
                //dataGridView1.Rows.Add(Excel[i].x);
            }
            ObjWorkBook.Close(); // Закрытие книги
            ObjExcel.Quit(); // Выход из Excel
        }
        #endregion

        private void стартToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
            pictureBox5.Image = null;

            pause = true;
            if (checkBox1.Checked == true)
            {

                Thread bubble = new Thread(new ParameterizedThreadStart(BubbleSort));
                threads.Add(bubble);
                bubble.Start(array);
                /*await Task.Run(() =>
                {
                    BubbleSort(array);
                });*/
            }

            if (checkBox2.Checked == true)
            {
                Thread bubble = new Thread(new ParameterizedThreadStart(InsertionSort));
                threads.Add(bubble);
                bubble.Start(array2);
            }

            if (checkBox3.Checked == true)
            {
                Thread bubble = new Thread(new ParameterizedThreadStart(ShakerSort));
                threads.Add(bubble);
                bubble.Start(array3);
            }

            if (checkBox4.Checked == true)
            {

            }

            if (checkBox5.Checked == true)
            {
                Thread bubble = new Thread(new ParameterizedThreadStart(BogoSort));
                threads.Add(bubble);
                bubble.Start(array5);
            }

            if (checkBox6.Checked == true) // По убыванию сортировка
            {
                if (checkBox1.Checked == true)
                {

                }

                if (checkBox2.Checked == true)
                {

                }

                if (checkBox3.Checked == true)
                {

                }

                if (checkBox4.Checked == true)
                {

                }

                if (checkBox5.Checked == true)
                {

                }

            }
        }

        // Сорировка методом пузырька 1
        #region
        async private void BubbleSort(object arr1)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            double dop;
            for (int i = 0; i < array.Length; ++i)
            {
                for (int j = 0; j < array.Length - 1; ++j)
                {
                    if (array[j] > array[j + 1])
                    {
                        dop = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = dop;
                    }
                }
                await Task.Run(() =>
                {
                    drawMarking();
                    drawSort(array);
                    buffered.Render();
                    Thread.Sleep(1000);
                });
            }
            Thread.Sleep(1000);
            stopwatch.Stop();
            var elapsedTime = stopwatch.Elapsed;

            System.Action action1 = () => label5.Text = Convert.ToString(elapsedTime);
            Invoke(action1);
        }
        #endregion

        // Сортировка вставками 2
        #region
        async private void InsertionSort(object arr1)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            double x;
            int j;
            for (int i = 1; i < array2.Length; ++i)
            {
                x = array2[i]; // сам 2 элемент
                j = i;
                while (j > 0 && array2[j - 1] > x)
                {
                    double dop = array2[j];
                    array2[j] = array2[j - 1];
                    array2[j - 1] = dop;
                    j -= 1;
                }
                array2[j] = x;


                await Task.Run(() =>
                {
                    drawMarking2();
                    drawSort2(array2);
                    buffered2.Render();
                    Thread.Sleep(1000);
                });
            }
            Thread.Sleep(1000);
            stopwatch.Stop();
            var elapsedTime = stopwatch.Elapsed;

            System.Action action2 = () => label6.Text = Convert.ToString(elapsedTime);
            Invoke(action2);
        }
        #endregion

        // Шейкерная сортировка 3
        #region
        static void Swap1(ref double e1, ref double e2)
        {
            var temp = e1;
            e1 = e2;
            e2 = temp;
        }

        async private void ShakerSort(object arr1)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < array3.Length / 2; ++i)
            {
                var swapFlag = false;
                //проход слева направо
                for (var j = i; j < array3.Length - i - 1; ++j)
                {
                    if (array3[j] > array3[j + 1])
                    {
                        Swap1(ref array3[j], ref array3[j + 1]);
                        swapFlag = true;
                    }
                }

                //проход справа налево
                for (var j = array3.Length - 2 - i; j > i; --j)
                {
                    if (array3[j - 1] > array3[j])
                    {
                        Swap1(ref array3[j - 1], ref array3[j]);
                        swapFlag = true;
                    }

                }
                await Task.Run(() =>
                {
                    drawMarking3();
                    drawSort3(array3);
                    buffered3.Render();
                    Thread.Sleep(1000);
                });

                //если обменов не было выходим
                if (!swapFlag)
                {
                    break;
                }
            }
            Thread.Sleep(1000);
            stopwatch.Stop();
            var elapsedTime = stopwatch.Elapsed;

            System.Action action3 = () => label7.Text = Convert.ToString(elapsedTime);
            Invoke(action3);
        }
        #endregion

        // Быстрая сортировка 4
        #region


        #endregion

        // Сортировка BOGO 5
        #region
        static bool IsSorted(double[] array5) // Метод для проверки упорядоченности массива
        {
            for (int i = 0; i < array5.Length - 1; ++i)
            {
                if (array5[i] > array5[i + 1])
                    return false;
            }
            return true;
        }
        static double[] Random(double[] array5) // Метод для перемешивания элементов массива
        {
            Random random = new Random();
            for (int i = array5.Length - 1; i >= 0; --i)
            {
                int j = random.Next(i); // Возвращение случайного числа
                double dop = array5[i];
                array5[i] = array5[j];
                array5[j] = dop;
            }
            return array5;
        }
        async void BogoSort(object arr5) // Сама сортировка
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (!IsSorted(array5)) // Пока массив не упорядочен
            {
                array5 = Random(array5); // Меняем местами дальше
                await Task.Run(() =>
                {
                    drawMarking5();
                    drawSort5(array5);
                    buffered5.Render();
                    Thread.Sleep(1000);
                });
            }
            Thread.Sleep(1000);
            stopwatch.Stop();
            var elapsedTime = stopwatch.Elapsed;

            System.Action action5 = () => label9.Text = Convert.ToString(elapsedTime);
            Invoke(action5);
        }
        #endregion


        private void drawSort(double[] array)
        {
            bool flag = true;
            Pen pen = new Pen(Color.DarkOrange);

            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (flag)
                        pen = new Pen(Color.Yellow);
                    else
                        pen = new Pen(Color.Blue);
                    flag = !flag;

                    if (array[j] >= i)
                        buffered.Graphics.FillRectangle(new SolidBrush(pen.Color), 10 * j, pictureBox1.Height - 10 * i, 10, 10);
                }
            }
        }
        private void drawMarking()
        {
            buffered = BufferedGraphicsManager.Current.Allocate(pictureBox1.CreateGraphics(), pictureBox1.DisplayRectangle);
            Pen pen = new Pen(Color.DarkGreen);

            for (int i = 0; i < pictureBox1.Height; i += 10)
                buffered.Graphics.DrawLine(pen, 0, pictureBox1.Height - i, pictureBox1.Width, pictureBox1.Height - i);
            for (int i = 0; i < pictureBox1.Width; i += 10)
                buffered.Graphics.DrawLine(pen, i, 0, i, pictureBox1.Width);
        }
        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            drawMarking();
            buffered.Render();
        }


        private void drawSort2(double[] array2)
        {
            bool flag = true;
            Pen pen = new Pen(Color.DarkOrange);

            for (int i = 0; i <= maxNumArray2; i++)
            {
                for (int j = 0; j < array2.Length; j++)
                {
                    if (flag)
                        pen = new Pen(Color.Yellow);
                    else
                        pen = new Pen(Color.Blue);
                    flag = !flag;

                    if (array2[j] >= i)
                        buffered2.Graphics.FillRectangle(new SolidBrush(pen.Color), 10 * j, pictureBox2.Height - 10 * i, 10, 10);
                }
            }
        }
        private void drawMarking2()
        {
            buffered2 = BufferedGraphicsManager.Current.Allocate(pictureBox2.CreateGraphics(), pictureBox2.DisplayRectangle);
            Pen pen = new Pen(Color.DarkGreen);

            for (int i = 0; i < pictureBox2.Height; i += 10)
                buffered2.Graphics.DrawLine(pen, 0, pictureBox2.Height - i, pictureBox2.Width, pictureBox2.Height - i);
            for (int i = 0; i < pictureBox2.Width; i += 10)
                buffered2.Graphics.DrawLine(pen, i, 0, i, pictureBox2.Width);
        }
        private void pictureBox2_Resize(object sender, EventArgs e)
        {
            drawMarking2();
            buffered2.Render();
        }

        private void drawSort3(double[] array3)
        {
            bool flag = true;
            Pen pen = new Pen(Color.DarkOrange);

            for (int i = 0; i <= maxNumArray3; i++)
            {
                for (int j = 0; j < array3.Length; j++)
                {
                    if (flag)
                        pen = new Pen(Color.Yellow);
                    else
                        pen = new Pen(Color.Blue);
                    flag = !flag;

                    if (array3[j] >= i)
                        buffered3.Graphics.FillRectangle(new SolidBrush(pen.Color), 10 * j, pictureBox3.Height - 10 * i, 10, 10);
                }
            }
        }
        private void drawMarking3()
        {
            buffered3 = BufferedGraphicsManager.Current.Allocate(pictureBox3.CreateGraphics(), pictureBox3.DisplayRectangle);
            Pen pen = new Pen(Color.DarkGreen);

            for (int i = 0; i < pictureBox3.Height; i += 10)
                buffered3.Graphics.DrawLine(pen, 0, pictureBox3.Height - i, pictureBox3.Width, pictureBox3.Height - i);
            for (int i = 0; i < pictureBox3.Width; i += 10)
                buffered3.Graphics.DrawLine(pen, i, 0, i, pictureBox3.Width);
        }
        private void pictureBox3_Resize(object sender, EventArgs e)
        {
            drawMarking3();
            buffered3.Render();
        }


        private void drawSort4(double[] array4)
        {
            bool flag = true;
            Pen pen = new Pen(Color.DarkOrange);

            for (int i = 0; i <= maxNumArray4; i++)
            {
                for (int j = 0; j < array4.Length; j++)
                {
                    if (flag)
                        pen = new Pen(Color.Yellow);
                    else
                        pen = new Pen(Color.Blue);
                    flag = !flag;

                    if (array4[j] >= i)
                        buffered4.Graphics.FillRectangle(new SolidBrush(pen.Color), 10 * j, pictureBox4.Height - 10 * i, 10, 10);
                }
            }
        }
        private void drawMarking4()
        {
            buffered4 = BufferedGraphicsManager.Current.Allocate(pictureBox4.CreateGraphics(), pictureBox4.DisplayRectangle);
            Pen pen = new Pen(Color.DarkGreen);

            for (int i = 0; i < pictureBox4.Height; i += 10)
                buffered4.Graphics.DrawLine(pen, 0, pictureBox4.Height - i, pictureBox4.Width, pictureBox4.Height - i);
            for (int i = 0; i < pictureBox4.Width; i += 10)
                buffered4.Graphics.DrawLine(pen, i, 0, i, pictureBox4.Width);
        }
        private void pictureBox4_Resize(object sender, EventArgs e)
        {
            drawMarking4();
            buffered4.Render();
        }

        private void drawSort5(double[] array5)
        {
            bool flag = true;
            Pen pen = new Pen(Color.DarkOrange);

            for (int i = 0; i <= maxNumArray5; i++)
            {
                for (int j = 0; j < array5.Length; j++)
                {
                    if (flag)
                        pen = new Pen(Color.Yellow);
                    else
                        pen = new Pen(Color.Blue);
                    flag = !flag;

                    if (array5[j] >= i)
                        buffered5.Graphics.FillRectangle(new SolidBrush(pen.Color), 10 * j, pictureBox5.Height - 10 * i, 10, 10);
                }
            }
        }
        private void drawMarking5()
        {
            buffered5 = BufferedGraphicsManager.Current.Allocate(pictureBox5.CreateGraphics(), pictureBox5.DisplayRectangle);
            Pen pen = new Pen(Color.DarkGreen);

            for (int i = 0; i < pictureBox5.Height; i += 10)
                buffered5.Graphics.DrawLine(pen, 0, pictureBox5.Height - i, pictureBox5.Width, pictureBox5.Height - i);
            for (int i = 0; i < pictureBox5.Width; i += 10)
                buffered5.Graphics.DrawLine(pen, i, 0, i, pictureBox5.Width);
        }
        private void pictureBox5_Resize(object sender, EventArgs e)
        {
            drawMarking5();
            buffered5.Render();
        }

        private void Добавить_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Заполните поле!", "Ошибка!");
                }
                else
                {
                    Point xy = new Point(Convert.ToDouble(textBox1.Text));
                    Ru.Add(xy);
                    dataGridView1.Rows.Add(xy.x);
                    textBox1.Text = "";
                }
            }
            catch
            {
                MessageBox.Show("Введите число!", "Ошибка!");
            }
        }
    }
}
