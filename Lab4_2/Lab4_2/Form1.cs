using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Action = System.Action;
using Excel = Microsoft.Office.Interop.Excel;

namespace Lab4_2
{
    public partial class Form1 : Form
    {
        public double[] array1;
        public double[] array2;
        public double[] array3;
        public double[] array4;
        public double[] array5;
        public string[,] list;


        List<Thread> threads = new List<Thread>();
        public bool stop = false;
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;  // Вывод формы по центру экрана
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
        }

        public void Clear() // Метод очистки
        {
            foreach (var qq in threads)
            {
                if (qq.ThreadState == System.Threading.ThreadState.Suspended)
                {
                    qq.Abort();
                }

                else
                {
                    qq.Abort();
                }
            }
            threads.Clear();
            dataGridView1.Rows.Clear();

            array1 = null;
            array2 = null;
            array3 = null;
            array4 = null;
            array5 = null;
            list = null;

            stop = false;

            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
            chart3.Series[0].Points.Clear();
            chart4.Series[0].Points.Clear();
            chart5.Series[0].Points.Clear();

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;

            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
        }
        private void очисткаToolStripMenuItem_Click(object sender, EventArgs e) // Очистка
        {
            Clear();
        }
        private void начатьToolStripMenuItem_Click(object sender, EventArgs e) // По возрастанию
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Заполните задержку", "Ошибка");
                return;
            }
            if (stop == false)
            {
                if (checkBox1.Checked == true)
                {
                    chart1.Series[0].Points.DataBindY(array1);
                    Thread Bubble = new Thread(new ParameterizedThreadStart(BubbleSort));
                    threads.Add(Bubble);
                    Bubble.Start(array1);
                }
               if (checkBox2.Checked == true)
                {
                    Thread Insert = new Thread(new ParameterizedThreadStart(InsertSort));
                    threads.Add(Insert);
                    Insert.Start(array2);
                }
                if (checkBox3.Checked == true)
                {
                    Thread Shaker = new Thread(new ParameterizedThreadStart(ShakerSort));
                    threads.Add(Shaker);
                    Shaker.Start(array3);
                }
                if (checkBox4.Checked == true)
                {
                    Thread Quick = new Thread(new ParameterizedThreadStart(QuickSort));
                    threads.Add(Quick);
                    Quick.Start(array4);
                }
                if (checkBox5.Checked == true)
                {
                    Thread Bogo = new Thread(new ParameterizedThreadStart(BogoSort));
                    threads.Add(Bogo);
                    Bogo.Start(array5);
                }
                stop = true;
            }
        }
        private void поУбываниюToolStripMenuItem_Click(object sender, EventArgs e) // По убыванию
        {
            if (stop == false)
            {
                if (checkBox1.Checked == true)
                {
                    chart1.Series[0].Points.DataBindY(array1);
                    Thread bubble = new Thread(new ParameterizedThreadStart(BubbleSort1));
                    threads.Add(bubble);
                    bubble.Start(array1);
                }
                if (checkBox2.Checked == true)
                {
                    Thread Insert = new Thread(new ParameterizedThreadStart(InsertSort1));
                    threads.Add(Insert);
                    Insert.Start(array2);
                }
                if (checkBox3.Checked == true)
                {
                    Thread Shaker = new Thread(new ParameterizedThreadStart(ShakerSort1));
                    threads.Add(Shaker);
                    Shaker.Start(array3);
                }
                if (checkBox4.Checked == true)
                {
                    Thread Quick = new Thread(new ParameterizedThreadStart(QuickSort1));
                    threads.Add(Quick);
                    Quick.Start(array4);
                }
                if (checkBox5.Checked == true)
                {
                    Thread Bogo = new Thread(new ParameterizedThreadStart(BogoSort1));
                    threads.Add(Bogo);
                    Bogo.Start(array5);
                }
                stop = true;
            }
        }
        private void считатьСExcelToolStripMenuItem_Click(object sender, EventArgs e) // Считывание с Excel
        {
            Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "*.xls;*.xlsx";
            ofd.Filter = "файл Excel (Spisok.xlsx)|*.xlsx";
            ofd.Title = "Выберите файл базы данных";

            if (!(ofd.ShowDialog() == DialogResult.OK))
            {
                MessageBox.Show("Выберите файл!", "Ошибка");
            }

            Excel.Application ObjWorkExcel = new Excel.Application();
            Workbook ObjWorkBook = ObjWorkExcel.Workbooks.Open(ofd.FileName);
            Worksheet ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1];

            Range lastCell = ObjWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);
            int lastColumn = (int)lastCell.Column;
            int lastRow = (int)lastCell.Row;

            if (lastRow <= 2)
            {
                MessageBox.Show("Мало точек!", "Ошибка!");
            }

            else
            {
                list = new string[lastRow, lastColumn];

                for (int i = 0; i < 1; ++i)
                {
                    for (int j = 0; j < lastRow; ++j)
                    {
                        list[j, i] = ObjWorkSheet.Cells[j + 1, i + 1].Text.ToString();
                    }
                }
            }

            array1 = new double[list.Length];
            array2 = new double[list.Length];
            array3 = new double[list.Length];
            array4 = new double[list.Length];
            array5 = new double[list.Length];

            for (int i = 0; i < list.GetLength(0); ++i)
            {
                array1[i] = Convert.ToDouble(list[i, 0]);
                array2[i] = Convert.ToDouble(list[i, 0]);
                array3[i] = Convert.ToDouble(list[i, 0]);
                array4[i] = Convert.ToDouble(list[i, 0]);
                array5[i] = Convert.ToDouble(list[i, 0]);
                dataGridView1.Rows.Add(array1[i]);
            }

            ObjWorkBook.Close(false, Type.Missing, Type.Missing);
            ObjWorkExcel.Quit();
            ObjWorkExcel.Quit();
            GC.Collect();
        }
        private void считатьСGoogleSheetsToolStripMenuItem_Click(object sender, EventArgs e) // Считывание с Google Sheets
        {
            Clear();
            string link = textBox1.Text;
            string path = @"D:\Проекты\Lab4_2\Lab4_2\Sheets.xlsx";

            File.Delete(path);

            string qq = link.Replace("edit?usp=sharing", "export?format=xlsx");

            using (var client = new WebClient()) // Скачивание файла
            {
                client.DownloadFile(new Uri(qq), path);
            }

            Excel.Application ObjExcel = new Excel.Application();

            Workbook ObjWorkBook = ObjExcel.Workbooks.Open(path); // Открываем книгу
            Worksheet ObjWorkSheet = (Worksheet)ObjWorkBook.Sheets[1]; // Выбираем лист

            Range lastCell = ObjWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);
            int lastColumn = (int)lastCell.Column;
            int lastRow = (int)lastCell.Row;

            if (lastRow <= 2)
            {
                MessageBox.Show("Мало точек!", "Ошибка!");
            }

            else
            {
                list = new string[lastRow, lastColumn];

                for (int i = 0; i < 1; ++i)
                {
                    for (int j = 0; j < lastRow; ++j)
                    {
                        list[j, i] = ObjWorkSheet.Cells[j + 1, i + 1].Text.ToString();
                    }
                }
            }

            array1 = new double[list.Length];
            array2 = new double[list.Length];
            array3 = new double[list.Length];
            array4 = new double[list.Length];
            array5 = new double[list.Length];

            for (int i = 0; i < list.GetLength(0); ++i)
            {
                array1[i] = Convert.ToDouble(list[i, 0]);
                array2[i] = Convert.ToDouble(list[i, 0]);
                array3[i] = Convert.ToDouble(list[i, 0]);
                array4[i] = Convert.ToDouble(list[i, 0]);
                array5[i] = Convert.ToDouble(list[i, 0]);
                dataGridView1.Rows.Add(array1[i]);
            }

            ObjWorkBook.Close(); // Закрытие книги
            ObjExcel.Quit(); // Выход из Excel

        }
        private void рандомноеToolStripMenuItem_Click(object sender, EventArgs e) // Рандомное заполнение
        {
            Clear();
            Random rand = new Random();

            array1 = new double[25];
            array2 = new double[25];
            array3 = new double[25];
            array4 = new double[25];
            array5 = new double[25];

            for (int i = 0; i < 25; ++i)
            {
                double a = rand.Next(-10, 20);
                array1[i] = a;
                array2[i] = a;
                array3[i] = a;
                array4[i] = a;
                array5[i] = a;
                dataGridView1.Rows.Add(a);
            }
        }
        private void button3_Click(object sender, EventArgs e) // Ручной ввод
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Заполните поле!", "Ошибка!");
                }
                else
                {
                    dataGridView1.Rows.Add(textBox2.Text);
                    textBox2.Text = "";
                }

                array1 = new double[dataGridView1.RowCount - 1];
                for (int i = 0; i < dataGridView1.RowCount - 1; ++i)
                    array1[i] = double.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());

                array2 = new double[dataGridView1.RowCount - 1];
                for (int i = 0; i < dataGridView1.RowCount - 1; ++i)
                    array2[i] = double.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());


                array3 = new double[dataGridView1.RowCount - 1];
                for (int i = 0; i < dataGridView1.RowCount - 1; ++i)
                    array3[i] = double.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());


                array4 = new double[dataGridView1.RowCount - 1];
                for (int i = 0; i < dataGridView1.RowCount - 1; ++i)
                    array4[i] = double.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());

                array5 = new double[dataGridView1.RowCount - 1];
                for (int i = 0; i < dataGridView1.RowCount - 1; ++i)
                    array5[i] = double.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
            }
            catch
            {
                MessageBox.Show("Введите число!", "Ошибка!");
            }
        }
        private void button1_Click(object sender, EventArgs e) // Приостановка
        {
            foreach (var qq in threads)
            {
                if (qq.ThreadState != System.Threading.ThreadState.Stopped)
                    qq.Suspend();
            }
        }
        private void button2_Click(object sender, EventArgs e) // Продолжение
        {
            foreach (var qq in threads)
            {
                if (qq.ThreadState != System.Threading.ThreadState.Stopped)
                    qq.Resume();
            }
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e) // Кнопка выход
        {
            Close();
        }

        // Сортировки по возрастанию
        #region
        public void BubbleSort(object arr1) // Сортировка методом Пузырька
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            double dop;
            for (int i = 0; i < array1.Length; ++i)
            {
                for (int j = 0; j < array1.Length - 1; ++j)
                {
                    if (array1[j] > array1[j + 1])
                    {
                        Thread.Sleep(Convert.ToInt32(textBox3.Text));
                        dop = array1[j];
                        array1[j] = array1[j + 1];
                        array1[j + 1] = dop;

                        Action action = () => chart1.Series[0].Points.DataBindY(array1);
                        Invoke(action);
                    }
                }
            }
            stopwatch.Stop();
            Action actionBubble = () => label2.Text = stopwatch.ElapsedMilliseconds / 1000 + " секунд";
            Invoke(actionBubble);
        }

        public void InsertSort(object arr2)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            double x;
            int j;
            for (int i = 1; i < array2.Length; ++i)
            {
                Thread.Sleep(Convert.ToInt32(textBox3.Text));
                x = array2[i]; // Сам 2 элемент
                j = i;
                while (j > 0 && array2[j - 1] > x)
                {
                    double dop = array2[j];
                    array2[j] = array2[j - 1];
                    array2[j - 1] = dop;
                    j -= 1;
                }
                array2[j] = x;
                Action action = () => chart2.Series[0].Points.DataBindY(array2);
                Invoke(action);
                
            }
            stopwatch.Stop();
            Action actionInsert = () => label3.Text = stopwatch.ElapsedMilliseconds / 1000 + " секунд";
            Invoke(actionInsert);
        }

        static void Swap1(ref double e1, ref double e2)
        {
            var temp = e1;
            e1 = e2;
            e2 = temp;
        }

        public void ShakerSort(object arr3) // Шейкерная сортировка
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < array3.Length / 2; ++i)
            {
                Action action = () => chart3.Series[0].Points.DataBindY(array3);
                Invoke(action);

                var swapFlag = false;
                Thread.Sleep(Convert.ToInt32(textBox3.Text));
                for (var j = i; j < array3.Length - i - 1; ++j) // Проход слева направо
                {
                    if (array3[j] > array3[j + 1])
                    {
                        Swap1(ref array3[j], ref array3[j + 1]);
                        swapFlag = true;
                    }
                    Invoke(action);
                }
                Thread.Sleep(Convert.ToInt32(textBox3.Text));
                for (var j = array3.Length - 2 - i; j > i; --j) // Проход справа налево
                {
                    if (array3[j - 1] > array3[j])
                    {
                        Swap1(ref array3[j - 1], ref array3[j]);
                        swapFlag = true;
                    }
                    Invoke(action);
                }
                
                // Если обменов не было - выходим
                if (!swapFlag)
                {
                    break;
                }
                Invoke(action);
                Thread.Sleep(Convert.ToInt32(textBox3.Text));
            }
            stopwatch.Stop();

            Action actionShake = () => label4.Text = stopwatch.ElapsedMilliseconds / 1000 + " секунд";
            Invoke(actionShake);
        }


        int Partition(double[] array, int minIndex, int maxIndex) // Метод, возвращающий индекс опорного элемента
        {
            var pivot = minIndex - 1;
            for (int i = minIndex; i < maxIndex; ++i)
            {
                if (array[i] < array[maxIndex])
                {
                    ++pivot;
                    Swap1(ref array[pivot], ref array[i]);
                }
            }

            ++pivot;
            Swap1(ref array[pivot], ref array[maxIndex]);
            Thread.Sleep(Convert.ToInt32(textBox3.Text));
            return pivot;
        }
        public double[] QuickSort(double[] array4, int minIndex, int maxIndex) // Быстрая сортировка
        {
            if (minIndex >= maxIndex)
            {
                return array4;
            }

            var pivotIndex = Partition(array4, minIndex, maxIndex);

            Action action = () => chart4.Series[0].Points.DataBindY(array4);
            Invoke(action);

            QuickSort(array4, minIndex, pivotIndex - 1); // Левая сторона

            QuickSort(array4, pivotIndex + 1, maxIndex); // Правая сторона
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            return array4;
        }
        public void QuickSort(object arr4)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            double[] array4 = (double[])arr4;

            QuickSort(array4, 0, array4.Length - 1);

            stopwatch.Stop();
            Action actionQuick = () => label5.Text = stopwatch.ElapsedMilliseconds / 1000 + " секунд";
            Invoke(actionQuick);
        }

        static bool IsSorted(double[] array5) // Метод для проверки упорядоченности массива
        {
            for (int i = 0; i < array5.Length - 1; ++i)
            {
                if (array5[i] > array5[i + 1])
                    return false;
            }
            return true;
        }
        public void Random() // Метод для перемешивания элементов массива
        {
            Random random = new Random();

            var lenght = array5.Length;
            while (lenght > 1)
            {
                --lenght;
                var i = random.Next(lenght + 1);
                var temp = array5[i];
                array5[i] = array5[lenght];
                array5[lenght] = temp;
            }
        }
        void BogoSort(object arr5)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (!IsSorted(array5)) // Пока массив не упорядочен
            {
                Random();
                Thread.Sleep(Convert.ToInt32(textBox3.Text));
                Action action = () => chart5.Series[0].Points.DataBindY(array5);
                Invoke(action);
            }
            
            stopwatch.Stop();
            var elapsedTime = stopwatch.Elapsed;

            Action actionBogo = () => label6.Text = stopwatch.ElapsedMilliseconds / 1000 + " секунд";
            Invoke(actionBogo);
        }
        #endregion

        // Сортировки по убыванию
        #region
        public void BubbleSort1(object arr1) // Сортировка методом Пузырька
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            double dop;
            for (int i = 0; i < array1.Length; ++i)
            {
                for (int j = 0; j < array1.Length - 1; ++j)
                {
                    if (array1[j] < array1[j + 1])
                    {
                        Thread.Sleep(Convert.ToInt32(textBox3.Text));
                        dop = array1[j];
                        array1[j] = array1[j + 1];
                        array1[j + 1] = dop;

                        Action action = () => chart1.Series[0].Points.DataBindY(array1);
                        Invoke(action);
                    }
                }
            }
            stopwatch.Stop();
            Action actionBubble = () => label2.Text = stopwatch.ElapsedMilliseconds / 1000 + " секунд";
            Invoke(actionBubble);
        }

        public void InsertSort1(object arr2)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            double x;
            int j;
            for (int i = 1; i < array2.Length; ++i)
            {
                Thread.Sleep(Convert.ToInt32(textBox3.Text));
                x = array2[i]; // Сам 2 элемент
                j = i;
                while (j > 0 && array2[j - 1] < x)
                {
                    double dop = array2[j];
                    array2[j] = array2[j - 1];
                    array2[j - 1] = dop;
                    j -= 1;
                }
                array2[j] = x;
                Action action = () => chart2.Series[0].Points.DataBindY(array2);
                Invoke(action);
                
            }
            stopwatch.Stop();
            Action actionInsert = () => label3.Text = stopwatch.ElapsedMilliseconds / 1000 + " секунд";
            Invoke(actionInsert);
        }

        static void Swap(ref double e1, ref double e2)
        {
            var temp = e1;
            e1 = e2;
            e2 = temp;
        }

        public void ShakerSort1(object arr3) // Шейкерная сортировка
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < array3.Length / 2; ++i)
            {
                Action action = () => chart3.Series[0].Points.DataBindY(array3);
                Invoke(action);

                var swapFlag = false;
                Thread.Sleep(Convert.ToInt32(textBox3.Text));
                for (var j = i; j < array3.Length - i - 1; ++j) // Проход слева направо
                {
                    if (array3[j] < array3[j + 1])
                    {
                        Swap(ref array3[j], ref array3[j + 1]);
                        swapFlag = true;
                    }
                    Invoke(action);
                }
                Thread.Sleep(Convert.ToInt32(textBox3.Text));
                for (var j = array3.Length - 2 - i; j > i; --j) // Проход справа налево
                {
                    if (array3[j - 1] < array3[j])
                    {
                        Swap(ref array3[j - 1], ref array3[j]);
                        swapFlag = true;
                    }
                    Invoke(action);
                }
                
                // Если обменов не было - выходим
                if (!swapFlag)
                {
                    break;
                }
                Invoke(action);
                Thread.Sleep(Convert.ToInt32(textBox3.Text));
            }
            stopwatch.Stop();

            Action actionShake = () => label4.Text = stopwatch.ElapsedMilliseconds / 1000 + " секунд";
            Invoke(actionShake);
        }


        int Partition1(double[] array, int minIndex, int maxIndex) // Метод, возвращающий индекс опорного элемента
        {
            var pivot = minIndex - 1;
            for (int i = minIndex; i < maxIndex; ++i)
            {
                if (array[i] > array[maxIndex])
                {
                    ++pivot;
                    Swap(ref array[pivot], ref array[i]);
                }
            }

            ++pivot;
            Swap(ref array[pivot], ref array[maxIndex]);
            Thread.Sleep(Convert.ToInt32(textBox3.Text));
            return pivot;
        }
        public double[] QuickSort1(double[] array4, int minIndex, int maxIndex) // Быстрая сортировка
        {
            if (minIndex >= maxIndex)
            {
                return array4;
            }

            var pivotIndex = Partition1(array4, minIndex, maxIndex);

            Action action = () => chart4.Series[0].Points.DataBindY(array4);
            Invoke(action);

            QuickSort1(array4, minIndex, pivotIndex - 1); // Левая сторона
            QuickSort1(array4, pivotIndex + 1, maxIndex); // Правая сторона

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            return array4;
        }
        public void QuickSort1(object arr4)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            double[] array4 = (double[])arr4;

            QuickSort1(array4, 0, array4.Length - 1);

            stopwatch.Stop();
            Action actionQuick = () => label5.Text = stopwatch.ElapsedMilliseconds / 1000 + " секунд";
            Invoke(actionQuick);
        }

        static bool IsSorted1(double[] array5) // Метод для проверки упорядоченности массива
        {
            for (int i = 0; i < array5.Length - 1; ++i)
            {
                if (array5[i] < array5[i + 1])
                    return false;
            }
            return true;
        }
        public void Random1() // Метод для перемешивания элементов массива
        {
            Random random = new Random();

            var lenght = array5.Length;
            while (lenght > 1)
            {
                --lenght;
                var i = random.Next(lenght + 1);
                var temp = array5[i];
                array5[i] = array5[lenght];
                array5[lenght] = temp;
            }
        }
        void BogoSort1(object arr5)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (!IsSorted1(array5)) // Пока массив не упорядочен
            {
                Random1();
                Thread.Sleep(Convert.ToInt32(textBox3.Text));
                Action action = () => chart5.Series[0].Points.DataBindY(array5);
                Invoke(action);
            }
            
            stopwatch.Stop();
            var elapsedTime = stopwatch.Elapsed;

            Action actionBogo = () => label6.Text = stopwatch.ElapsedMilliseconds / 1000 + " секунд";
            Invoke(actionBogo);
        }
        #endregion
    }
}
