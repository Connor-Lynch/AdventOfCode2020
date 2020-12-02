using AdventOfCode.Interfaces;
using Figgle;
using System;

namespace AdventOfCode
{
    public class ConsoleHelper : IConsoleHelper
    {
        public string GetDayFromUser()
        {
            Console.WriteLine("Please Enter The Day:");
            return Console.ReadLine();
        }

        public void ShowMianText()
        {
            Console.WriteLine(FiggleFonts.Standard.Render("2020 Advent Of Code"));
            Console.WriteLine("Enter the day you would like to solve, or press 'ENTER' to exit" + Environment.NewLine);
        }

        public void InitDay(string day)
        {
            Console.Clear();
            ShowMianText();
            Console.WriteLine(FiggleFonts.Small.Render($"Day {day}"));
        }

        public void IterationComplete()
        {
            Console.WriteLine(Environment.NewLine);
        }
    }
}
