using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess

{
  class Chess : ITask
  {
    public string[] Run(string[] data)
    {
      int N = Convert.ToInt32(data[0]);
      ulong PosMask = DoForKnight(N);
      int Count = CountOnesTrivial64(PosMask);
      string[] answer = new string[2];
      answer[0] = Count.ToString();
      answer[1] = PosMask.ToString();

      // Console.WriteLine($ "Count({N}) = {PosMask}");
      // Console.WriteLine($"Pos({N}) = {Count}");
      // Console.WriteLine(@"---");

      return answer;
    }

    public static ulong DoForKing(int Position)
    {
      ulong PosMask = 1UL << Position;
      ulong NoAMask = 0xFEFEFEFEFEFEFEFE;
      ulong NoFMask = 0x7F7F7F7F7F7F7F7F;
      ulong ExcludeA = PosMask & NoAMask;
      ulong ExcludeF = PosMask & NoFMask;
      ulong answer =  (ExcludeA << 7) | (PosMask << 8) | (ExcludeF << 9) |
                      (ExcludeA >> 1) |        0        | (ExcludeF << 1) |
                      (ExcludeA >> 9) | (PosMask >> 8) | (ExcludeF >> 7);
      return answer;
    }

    public static ulong DoForKnight(int Position)
    {
      ulong PosMask = 1UL << Position;
      ulong NoAMask = 0xFEFEFEFEFEFEFEFE;
      ulong NoBMask = 0xFDFDFDFDFDFDFDFD;
      ulong NoEMask = 0xBFBFBFBFBFBFBFBF;
      ulong NoFMask = 0x7F7F7F7F7F7F7F7F;
      ulong ExcludeA = PosMask & NoAMask;
      ulong ExcludeAB = ExcludeA & NoBMask;
      ulong ExcludeF = PosMask & NoFMask;
      ulong ExcludeEF = ExcludeF & NoEMask;
      ulong answer =  (ExcludeAB << 6) | (ExcludeEF << 10) | (ExcludeA << 15) | (ExcludeF << 17) |
                      (ExcludeEF >> 6) | (ExcludeAB >> 10) | (ExcludeF >> 15) | (ExcludeA >> 17);
      return answer;
    }

    public static int CountOnesTrivial64(ulong N)
    {
      int answer = 0;
      for(int i = 0; i< 64; i++)
      {
        if((N & 1) != 0)
        {
          answer++;
        }
        N >>= 1;
      }
      return answer;
    }

    public static int CountOnesFast64(ulong N)
    {
      int answer = 0;
      while (N != 0)
      {
        answer++;
        N &= N - 1;
      }
      return answer;
    }

  }
}
