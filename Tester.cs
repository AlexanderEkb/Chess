namespace Chess

{
  internal class Tester
  {
    private ITask task;
    private string path;
    private string[] data = {};
    private string[] expect = {};
    public Tester(ITask task, string path)
    {
      this.task = task;
      this.path = path;
    }
    public void RunAllTests()
    {
      int count = 0;
      while(true)
      {
        bool result = RunTest(count);
        if(!result)
        {
          break;
        }
        count++;
      }
    }

    public bool RunTest(int count)
    {
      string inFile = $"{path}test.{count}.in";
      string outFile = $"{path}test.{count}.out";
      if(!File.Exists(inFile) || !File.Exists(outFile))
      {
        Console.WriteLine(@"No more tests.");
        return false;
      }
      RunTest(inFile, outFile, count);
      return true;
    }
    bool RunTest(string inFile, string outFile, int count)
    {
      string[] actual = {};
      try
      {
        data = File.ReadAllLines(inFile);
        expect = File.ReadAllLines(outFile);
        actual = task.Run(data);
        bool result = true;
        for(int i = 0; i<2; i++)
        {
          if(actual[i].Trim() != expect[i].Trim())
          {
            result = false;
            break;
          }
        }
        if(result)
        {
          Console.WriteLine($"Test #{count} - PASS");
        }
        else
        {
          Console.WriteLine($"Test #{count} - FAIL");
        }
        return actual == expect;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return false;
      }
    }
  }
}