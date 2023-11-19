using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Assembly a = Assembly.LoadFrom("TextManipulationLibrary.dll");

            string sentence = "Роботу виконав Купчик Віталій, Student of the group IPZ-20006B, 4th year.";
            int spaceCount = StringManipulation.CountSpaces(sentence);
            string upperCaseSentence = TextTransformation.ConvertToUppercase(sentence);

            Console.WriteLine($"Кількість пробілів: {spaceCount}");
            Console.WriteLine($"Функція, яка перетворює всі латинські символи рядка до\r\nверхнього регістра (a =>A, … d =>D )и: {upperCaseSentence}");

            Console.ReadLine();
        }
    }
}
