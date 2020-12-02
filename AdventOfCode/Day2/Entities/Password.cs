namespace AdventOfCode.Day2.Entities
{
    public class Password
    {
        public char RequiredLetter { get; set; }
        public int MinTimes { get; set; }
        public int MaxTimes { get; set; }
        public string Text { get; set; }
        public bool Valid { get; set; }
    }
}
