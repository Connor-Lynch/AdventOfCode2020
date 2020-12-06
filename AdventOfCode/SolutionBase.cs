using AdventOfCode.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AdventOfCode
{
    public class SolutionBase
    {
        private static IConsoleHelper _consoleHelper;
        public Stopwatch Timer;
        public List<string> Answers;
        public List<string> Times;

        public SolutionBase()
        {
            _consoleHelper = new ConsoleHelper();
            Timer = new Stopwatch();
        }

        public void ResetSolution()
        {
            Answers = new List<string>();
            Times = new List<string>();
        }

        public void StartTime()
        {
            Timer.Reset();
            Timer.Start();
        }

        public void StopTime()
        {
            Timer.Stop();
            Times.Add($"{Timer.Elapsed.TotalMilliseconds}ms");
            Timer.Reset();
        }

        public void SetAnswer(object answer)
        {
            StopTime();
            Answers.Add(answer.ToString());
        }

        public void PrintAnswers()
        {
            var table = new List<List<string>>()
            {
                new List<string>() {"Part", "Answer", "Time"}
            };

            var currentIndex = 0;
            foreach(var answer in Answers)
            {
                table.Add(new List<string>()
                {
                    (currentIndex + 1).ToString(), answer, Times[currentIndex]
                });

                currentIndex++;
            }

            _consoleHelper.PrintTable(table);
        }
    }
}
