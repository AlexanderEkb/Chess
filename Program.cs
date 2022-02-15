using System;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
          ITask task = new Chess();
          Tester tester = new Tester(task, @".\tests\1.Bitboard - King\");
          tester.RunAllTests();
        }
    }
}
