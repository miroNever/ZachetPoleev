using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tect_Poleev
{
    public partial class Form1 :Form
    {
        public Form1 ()
        {
            InitializeComponent( );
        }
        static bool Check (string word)
        {
            bool check = false;
            word = word.ToLower( );
            for ( int i = 0; i < word.Length; i++ )
            {
                char c = word [ i ];
                if ( c >= 'а' && c <= 'я' )
                {
                    check = true;
                }
                else
                {
                    check = false;
                    break;
                }
            }
            return check;
        }
        static string ReadingFile (string line)
        {
            StreamReader sr = File.OpenText("file.txt");
            while ( !sr.EndOfStream )
            {
                line = sr.ReadLine( );
            }
            sr.Close( );
            return line;
        }
        static int Search (string word, string line)
        {
            List<string> list = line.Split(' ').ToList( );
            IEnumerable<string> result =
                //берём значение из листа
                from w in list
                //ищем совпадение слова с словом из файла
                where w.Contains(word)
                select w;
            int count = 0;
            foreach ( string w in result )
            {
                count++;
            }
            return count;
        }

        private void button1_Click (object sender, EventArgs e)
        {
            if ( textBox1.TextLength != 0 )
            {
                string word = textBox1.Text;
                //проверка на русские буквы
                if ( Check(word) )
                {
                    string line = "";
                    //чтение из файла
                    line = ReadingFile(line);
                    if ( line.Length != 0 )
                    {
                        int count = Search(word.ToLower( ), line.ToLower( ));
                        if ( count > 0 )
                            MessageBox.Show($"Найдены {count} вхождения(ий) поискового запроса {word}");
                        else
                            MessageBox.Show($"Не найдены входления(ий) поискового запроса {word}");
                    }
                    else
                    {
                        MessageBox.Show("Файл пуст");
                    }
                }
                else
                {
                    MessageBox.Show("Слово должно содержать только русские буквы!");
                }
            }
            else
                MessageBox.Show("Поле пустое");
        }

        double [ ] number = { 5.1, 1.3, 9.2, 2.3, 5.1, 3, 2.3, 1.3, 4.4 };
        private void button2_Click (object sender, EventArgs e)
        {
            listBox1.Items.Clear( );
            var result = number.GroupBy(x => x)//групируем числа из массива без повторов
                .Select(x => new { Number = x.Key, Frequency = x.Count( ) }) //преобразуем в обьект с двумя свойствами:
                                                                             //Number значение, Frequency частота
                .OrderBy(x => x.Number); // сортируем обьект по возростанию
            listBox1.Items.Add("Числа - Частота");
            foreach ( var x in result )
            {
                string item = $"{x.Number} - {x.Frequency}";
                listBox1.Items.Add(item);
            }
        }
        private void button3_Click (object sender, EventArgs e)
        {
            listBox1.Items.Clear( );
            var result = number.GroupBy(x => x)//групируем числа из массива без повторов
                .Select(x => new { Number = x.Key, Frequency = x.Count( ) })//преобразуем в обьект с двумя свойствами:
                                                                            //Number значение, Frequency частота
                .OrderBy(x => x.Number); // сортируем обьект по возростанию
            listBox1.Items.Add("Число * частоту - Частота");
            foreach ( var x in result )
            {
                string item = $"{x.Number * x.Frequency} - {x.Frequency}";
                listBox1.Items.Add(item);
            }
        }

        private void Form1_Load (object sender, EventArgs e)
        {
            //загрузка текста из файла в ListBox
            string line = "";
            line = ReadingFile(line);
            listBox2.Items.Add(line);

            //загрузка массива чисел в ListBox
            foreach ( double num in number)
            {
                listBox1.Items.Add(num.ToString( ));
            }
        }

    }
}
