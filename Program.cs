using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;//рефлексия
using System.IO;//работа с файлами 

namespace ConsoleApp1
{

    interface ITets
    {
        void Set(int a, int b);
    }
    interface ITest
    {
        double Sum();
        void Info();
    }
    class MyTestClass : ITest, ITets
    {
        public double d;//поле
        public double f;//поле

        public double F//свойства
        {
            get
            {
                return f;
            }
            set
            {
                f = value;
            }
        }
        public string ToConsole(string aye)//метод
        {
            return aye;
        }

        public MyTestClass(double d, double f)//конструктор
        {
            this.d = d;
            this.f = f;
        }


        public double Sum()//метод c ITest
        {
            return d + f;
        }

        public void Info()//метод c ITest
        {
            Console.WriteLine("d = {0} f = {1}", d, f);
        }

        public void Set(int a, int b)//метод с ITets
        {
            d = (double)a;
            f = (double)b;
        }

        public void Set(double a, double b)//метод
        {
            d = a;
            f = b;
        }

        public override string ToString()
        {
            return "MyTestClasssss";
        }
        private void Privat()//метод
        {
            Console.WriteLine("я только тут");
        }
    }

    class Reflect
    {//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*a. выводит всё содержимое класса в текстовый файл (принимает
в качестве параметра имя класса);*/
        public static void MethodA<T>(T obj) where T : class
        {
            FileStream file = new FileStream("D:\\file.txt", FileMode.Create);//FileMode-каким образом будет откр файл
            StreamWriter writer = new StreamWriter(file);
            Type t = typeof(T);//получить информацию о типе
                               /* класс хранит информацию о методе*/
            MethodInfo[] MArr = t.GetMethods();//методы типа в виде массива объектов MethodInfo
            Type[] interNames = t.GetInterfaces();//реализуемые данным типом интерфейсы в виде массива объектов Type
                                                  /*класс хранит информацию о поле*/
            FieldInfo[] fieldNames = t.GetFields();//поля данного типа в виде массива объектов FieldInfo
                                                   /*класс хранит информацию о свойстве*/
            PropertyInfo[] propertyNames = t.GetProperties();//свойства в виде массива объектов PropertyInfo
            foreach (MethodInfo m in MArr)//перебераем
            {
                writer.WriteLine(" - " + m.ReturnType.Name + " \t" + m.Name + "\n");//Name-строка с коротким именем модуля
            }
            foreach (FieldInfo m in fieldNames)
            {
                writer.WriteLine(" -- " + m.ReflectedType.Name + " \t" + m.Name + "\n");
            }
            foreach (PropertyInfo m in propertyNames)
            {
                writer.WriteLine(" --- " + m.ReflectedType.Name + " \t" + m.Name + "\n");
            }
            foreach (Type m in interNames)
            {
                writer.WriteLine(" ---- " + m.Name + "\n");
            }
            writer.Close();
            Console.WriteLine("Запись выполнена!");
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*b. извлекает все общедоступные публичные методы класса
(принимает в качестве параметра имя класса);*/
        public static void MethodB<T>(T obj) where T : class
        {
            Type t = typeof(T);//получить информацию о типе
                               // var MArr = t.GetMethods();///методы типа в виде массива объектов MethodInfo
            Console.WriteLine("Список методов класса {0}\n", obj.ToString());//??OBJ.tOSTRING
            foreach (MethodInfo m in t.GetMethods())
            {
                Console.Write(" -- " + m.ReturnType.Name + " \t" + m.Name + "\n");
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*получает информацию о полях и свойствах класса;*/
        public static void MethodC<T>(T obj) where T : class
        {
            Type t = typeof(T);//получить информацию о типе
            //GetFields();//поля данного типа в виде массива объектов FieldInfo
            //GetProperties();//свойства в виде массива объектов PropertyInfo
            Console.WriteLine("Список полей и свойств класса {0}\n", obj.ToString());//??
            foreach (FieldInfo m in t.GetFields())
            {
                Console.Write(" -- " + m.ReflectedType.Name + " \t" + m.Name + "\n");
            }
            foreach (PropertyInfo m in t.GetProperties())
            {
                Console.Write(" -- " + m.ReflectedType.Name + " \t" + m.Name + "\n");
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*получает все реализованные классом интерфейсы;*/
        public static void MethodD<T>(T obj) where T : class
        {
            Type t = typeof(T);//получить информацию о типе
            //GetInterfaces();//реализуемые данным типом интерфейсы в виде массива объектов Type
            Console.WriteLine("Список реализуемых интерфейсов класса {0}\n", obj.ToString());//??
            foreach (Type m in t.GetInterfaces())
            {
                Console.Write(" -- " + m.Name + "\n");
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*выводит по имени класса имена методов, которые содержат
заданный (пользователем) тип параметра (имя класса
передается в качестве аргумента);*/
        public static void MethodE<T>(T obj, string mth) where T : class
        {
            Type t = typeof(T);//получить информацию о типе
            MethodInfo[] MArr = t.GetMethods();//методы типа в виде массива объектов MethodInfo
            Console.WriteLine("Список методов класса {0}\n", obj.ToString());
            for (int i = 0; i < MArr.Length; i++)
            {
                // класс, хранящий информацию о параметре метода
                ParameterInfo[] PArr = MArr[i].GetParameters();//возвр параметры заданного метода
                for (int j = 0; j < PArr.Length; j++)
                {
                    if (mth == PArr[j].ParameterType.Name)
                    {
                        Console.WriteLine(MArr[i]);
                    }
                }
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /*вызывает некоторый метод класса, при этом значения для его
параметров необходимо прочитать из текстового файла (имя
класса и имя метода передаются в качестве аргументов).*/
        public static void MethodF<T>(T obj, string mth) where T : class
        {
            Console.WriteLine("Читаем из файла параметр и передаем его в метод!");
            FileStream file2 = new FileStream("D:\\tut.txt", FileMode.OpenOrCreate);//создали файл 
            StreamReader reader = new StreamReader(file2);//читаем из файла
            object[] Arro = { reader.ReadLine() };//помещаем в обьект то что на файле(в нашем случае это параметры методов)
            Type t = typeof(T);//получаем информацию о типе
            MethodInfo metod = t.GetMethod(mth);//методы типа в виде массива объектов MethodInfo(определенный)
            object mval = metod.Invoke(obj, Arro);//вызов метода(обьект, параметры)
            Console.WriteLine(mval);//выводим
        }

    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    class Program
    {
        static void Main(string[] args)
        {
            MyTestClass myTestClass = new MyTestClass(12.2, 13.3);
            Reflect.MethodA<MyTestClass>(myTestClass);
            Reflect.MethodB<MyTestClass>(myTestClass);
            Reflect.MethodC<MyTestClass>(myTestClass);
            Reflect.MethodD<MyTestClass>(myTestClass);

            string s;
            Console.WriteLine("Введите тип аргумента(Double, Int32): ");
            s = Console.ReadLine();
            Reflect.MethodE<MyTestClass>(myTestClass, s);
            Reflect.MethodF<MyTestClass>(myTestClass, "ToConsole");

            Console.ReadLine();
        }
    }
}


//System.Type
//Type t = typeof(T);//получить информацию о типе

//MethodInfo[] MArr = t.GetMethods();//методы типа в виде массива объектов MethodInfo//??


/*  определяем тип, создаем обьект класса MethodInfo, и туда помещаем все методы нашего типа 
  */


/*  Console.WriteLine("-------------POSLEDNNIY");
        Student first = new Student("1", "Dowr", 30, "Central", "375", "FIT", 2, 4);
        Reflect.MethodTwo<Student>(first);*/
    