using System.Collections.Generic;

namespace AdventOfCode.Interfaces
{
    public interface IConsoleHelper
    {
        public string GetDayFromUser();
        public void ShowMianText();
        public void InitDay(string day);
        public void IterationComplete();
        public void PrintTable(List<List<string>> table);
    }
}
