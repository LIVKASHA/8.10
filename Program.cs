using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using App;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace App
{
    /// <summary>
    /// На междугородной телефонной станции картотека абонентов, содержащая сведения
    /// о телефонах и их владельцах, организована в виде линейного списка.
    /// Написать программу, которая:
    ///   обеспечивает начальное формирование картотеки в виде линейного списка;
    ///   производит вывод всей картотеки;
    ///   вводит номер телефона и время разговора;
    ///   выводит извещение на оплату телефонного разговора.
    /// Программа должна обеспечивать диалог с помощью меню и контроль ошибок при
    /// вводе.Оценка снижается на 3 балла, если в работе используется не список, а массив.
    /// </summary>
    
        class Abonent
        {
            string fio;
            string tel;

            public string Fio
            {
                get
            {
                return fio;
            }
                set
                {
                    if (value != null)
                        fio = value;
                }
            }

            public string Tel
            {
                get
            {
                return tel;
            }
                set
                {
                    if (value != null)
                        tel = value;
                }
            }
            
        }

    //Ячейка списка
    public class Item<T> 
    {   //Данные хранимые в ячейке списка
        private T date = default(T);

        public T Data
        {
            get { return date; }
            set
            {
                if (value != null)
                    date = value;
                else
                    throw new ArgumentNullException(nameof(value));
            }
        }
        //Следующая ячейка списка
        public Item<T> Next { get; set; }

        public Item(T data)
        {
            Data = data;
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
    //Линейный список
    public class LinkedList <T>  : IEnumerable
    {
        const string path = "allAbonents.txt";
        StreamReader sr = new StreamReader(path);

        //Первый элемент списка
        public Item<T> Head { get; private set; }
        //Последний элемент списка
        public Item<T> Tail { get; private set; }
        //Количество элементов в списке
        public int Count { get; private set; }
        //Создание пустого списка
        public LinkedList() //Конструктор
        {
            Head = null;
            Tail = null;
            Count = 0;
        }
        //Спичок с начальным элементом
        public LinkedList(T data) //Перегруженный конструктор
        {
            var item = new Item<T>(data);
            SetHeadAndTail(item);
        }
        //Добавить данные в конец списка
       public void Add(T data)
        {
            var item = new Item<T>(data);

            if(Tail != null)
            {
                Tail.Next = item;
                Tail = item;
                Count++;
            }
            else
            {

            }
        }

        /*
        //Удалить данные элемента в списке
        public void Delete(T data)
        {
            if(Head != null)
            {
                if(Head.Data.Equals(data))
                {
                    Head = Head.Next;
                        Count--;
                    return;
                }

                var current = Head.Next;
                var previous = Head;


                while(current != null)
                {
                    if(current.Data.Equals(data))
                    {
                        previous.Next = current.Next;
                        Count--;
                        return;
                    }

                    previous = current;
                    current = current.Next;
                }
            }
        }*/

        private void SetHeadAndTail(Item<T> item)
        {
            Head = item;
            Tail = item;
            Count = 1;
        }
        //Получение всех элементов списка
        public IEnumerator GetEnumerator()
        {
            var current = Head;
            while(current != null)
            {
                yield return current.Data; //!!!!!!
                current = current.Next;
            }
        }
    }
    

    class Call
    {   
        int time;
        int data;
        
        public int Time { get; set; }
        public int Data { get; set; }
    }

    class Program
    {   private const string path = "allAbonents.txt";
        public bool result = false;
            static string[] stringArray()
            {
                List<string> lineOfAbonents = new List<string>();
                StreamReader sr;
                try
                {
                    sr = new StreamReader(path, Encoding.Default);
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lineOfAbonents.Add(line); //заносим в список
                    }
                    sr.Close();
                    string[] arrayLine = lineOfAbonents.ToArray(); //переносим в массив строк
                    return arrayLine;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Файл с именем {1} не найден!\n\n", path);
                    return null;
                }
            }
    //Проверка на наличие данного абонента в картотеке
    public void Check(string tel)
    {

        string[] arrayLine = stringArray();



        for (int i = 0; i < arrayLine.Length; i++)
        {
            arrayLine[i] = arrayLine[i].Trim(' ');
            string[] line = Regex.Split(arrayLine[i], " | ");

            if (result = Regex.IsMatch(path, tel))
            {
                return;
            }
            else
            {
                tel = null;
                return;
            }
        }
    }

        static void Main(string[] args)

        {
            string path_1 = "allAbonents.txt";
            string path_2 = "allCalls.txt";

            do
            {
                Console.WriteLine("Вывести картотеку - 1\n" +
                                  "Добавить абонента - 2\n" +
                                  "Добавить разговор и вывести счет на оплату  - 3\n" +
                                  "Выход из программы - 0");
                string num = Console.ReadLine();
                var list = new LinkedList<string>();

                switch (num)
                {   
                    //Выход из программы
                    case "0":
                        return;
                    
                    //Вывести картотеку
                    case "1":
                        string read;
                        using (StreamReader sr1 = new StreamReader(path_1))
                        while (sr1.EndOfStream != true)
                            {
                                read = sr1.ReadLine();
                                Console.WriteLine(read);
                            }    
                            
                        Console.ReadKey();
                        return;

                    //Добавить абонента
                    case "2":

                        Abonent a = new Abonent();
                        Console.WriteLine("ФИО абонента:");
                        a.Fio = Console.ReadLine();

                        while (true)
                        {                        
                            Console.WriteLine("Номер телефона");
                            a.Tel = Console.ReadLine();

                            if (a.Tel.Length.Equals(11))
                            {
                                string oneLine = a.Fio + " | " + a.Tel;
                                using (StreamWriter sw1 = new StreamWriter(path_1, true))
                                    sw1.WriteLine(oneLine);
                                Console.WriteLine("Абонент успешно добавлен.");
                                break;
                            }

                            else
                            {
                                Console.WriteLine("Недопустимая длина номера телефона./n**Телефон должен начинаться с 8 и содержать 11 цифр**");
                                
                            }

                        }
                        Console.ReadKey();
                        return;

                    //Добавить разговор и вывести счет на оплату
                    case "3":
                        Call tcall = new Call();
                        Console.WriteLine("Введите телефон абонента");
                        string phone =  Console.ReadLine();
                        //int phone1 = Convert.ToInt32(phone);
                        tcall.Check(phone);
                        if (phone != null)
                        {
                             Console.WriteLine("Введите дату разговора(формат ГГГГММДД)");
                             tcall.Data = Convert.ToInt32(Console.ReadLine());
                             Console.WriteLine("Введите продолжительность разговора(в минутах)");
                             tcall.Time = Convert.ToInt32(Console.ReadLine());
                             string oneLine = phone + " | " + tcall.Data + " | " + tcall.Time;
                             using (StreamWriter sw1 = new StreamWriter(path_2, true))
                                sw1.WriteLine(oneLine);
                             Console.WriteLine("Разговор успешно добавлен.");

                            int cost = 3;
                            int min = tcall.Time;
                            int bill = cost * min;
                            //Тариф задан в прграмме, пользователь не может изменить тариф
                            Console.WriteLine("Цена за минуту разговора составляет 3 руб./nСчет на оплату телефонной связи для абонента {1}/nпо тарифу 3 руб./мин. составляет {2}", phone, bill ) ;
                            Console.ReadKey();
                        }
                        else
                        {
                             Console.WriteLine("Введенный абонент не найден!");
                        }

                        Console.ReadKey();
                        return;
                }

            } while (true);

        }
    }
}