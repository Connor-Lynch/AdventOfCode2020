using AdventOfCode.Day1;
using AdventOfCode.Day2;
using Figgle;
using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowMianText();

            var fileReader = new FileReader();
            bool run = true;
            do
            {
                Console.WriteLine("Please Enter The Day:");
                string day = Console.ReadLine();

                if (!string.IsNullOrEmpty(day))
                {
                    switch(day)
                    {
                        case "1":
                            InitDay(day);
                            var day1 = new Day1Solution(fileReader);
                            day1.Solve();
                            break;
                        case "2":
                            InitDay(day);
                            var day2 = new Day2Solution(fileReader);
                            day2.Solve();
                            break;
                        default:
                            Console.WriteLine("Value entered not found, please try again");
                            break;
                    }
                } else
                {
                    run = false;
                }

                Console.WriteLine(Environment.NewLine);
            }
            while (run);            
        }

        private static void ShowMianText()
        {
            Console.WriteLine(FiggleFonts.Standard.Render("2020 Advent Of Code"));
            Console.WriteLine("Enter the day you would like to solve, or press 'ENTER' to exit" + Environment.NewLine);
        }

        private static void InitDay(string day)
        {
            Console.Clear();
            ShowMianText();
            Console.WriteLine(FiggleFonts.Small.Render($"Day {day}"));
        }
    }
}
