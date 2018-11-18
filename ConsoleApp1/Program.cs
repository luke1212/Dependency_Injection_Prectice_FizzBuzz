using System;
using System.Linq;

namespace ConsoleApp1 {
  internal class Program {
    private static void Main(string[] args) {
      Container container = new Container();
      IPrinter Printer = container.Printer;
      ILanguage Language = container.Language;
      IPrintLineNumber PrintLineNumber = container.PrintLineNumber;
      IConditionals Conditionals = container.Conditionals;
      IConfiguration Configuration = container.Configuration;
      _RunFizzBuzz(Printer, Language, PrintLineNumber, Conditionals, Configuration);
      Console.Read();
    }

    private static void _RunFizzBuzz(
      IPrinter Printer,
      ILanguage Language,
      IPrintLineNumber PrintLineNumber,
      IConditionals Conditionals,
      IConfiguration Configuration
      ) {
      foreach (var i in Enumerable.Range(
        Configuration.FristNumber,
        Configuration.LastNumber)) {
        PrintLineNumber._PrintLineNumber(i);
        if (Conditionals.IsFizzBuzz(i)) {
          Printer.PrintLine(Language.FizzBuzz);
        } else if (Conditionals.IsFizz(i)) {
          Printer.PrintLine(Language.Fizz);
        } else if (Conditionals.IsBuzz(i)) {
          Printer.PrintLine(Language.Buzz);
        } else {
          Printer.PrintLine(i.ToString());
        }
      }
    }
  }

  public class Container {
    public IPrinter Printer => new ConsolePrinter();
    public ILanguage Language => new Chinese();
    public IConfiguration Configuration => new Configuration();
    public IConditionals Conditionals => new Conditionals(Configuration);
    public IPrintLineNumber PrintLineNumber => new PrintLineNumber(Printer, Configuration);
  }
  public interface IConfiguration {
    int FizzNumber { get; }
    int BuzzNumber { get; }
    int FizzBuzzNumber { get; }
    string Line { get; }
    string Delimiter { get; }
    int FristNumber { get; }
    int LastNumber { get; }
  }

  public class Configuration : IConfiguration {
    public int FizzNumber => 3;
    public int BuzzNumber => 5;
    public int FizzBuzzNumber => 15;
    public string Line => "line";
    public string Delimiter => ": ";
    public int FristNumber => 1;
    public int LastNumber => 100;
  }
  public interface IPrinter {
    void Print(string s);
    void PrintLine(string s);
  }
  internal class FilePrinter : IPrinter {
    public void PrintLine(string s) =>
        Console.WriteLine("Print to file " + s);
    public void Print(string s) =>
        Console.Write("Print to file " + s);
  }
  internal class ConsolePrinter : IPrinter {
    public void PrintLine(string s) =>
        Console.WriteLine(s);
    public void Print(string s) =>
        Console.Write(s);

  }
  public interface IPrintLineNumber {
    void _PrintLineNumber(int i);
  }
  public class PrintLineNumber : IPrintLineNumber {
    private readonly IPrinter _printer;
    private readonly IConfiguration _configuration;
    public PrintLineNumber(IPrinter printer, IConfiguration configuration) {
      _printer = printer;
      _configuration = configuration;
    }
    public void _PrintLineNumber(int i) =>
    _printer.Print(_configuration.Line + i + _configuration.Delimiter);
  }
  public interface IConditionals {
    bool IsFizz(int i);
    bool IsBuzz(int i);
    bool IsFizzBuzz(int i);
  }

  public class Conditionals : IConditionals {
    private readonly IConfiguration _configuration;
    public Conditionals(IConfiguration configuration) {
      _configuration = configuration;
    }
    public bool IsFizz(int i) => i % _configuration.FizzNumber == 0;
    public bool IsBuzz(int i) => i % _configuration.BuzzNumber == 0;
    public bool IsFizzBuzz(int i) => i % _configuration.FizzBuzzNumber == 0;
  }
  public interface ILanguage {
    string Fizz { get; }
    string Buzz { get; }
    string FizzBuzz { get; }
  }
  internal class Chinese : ILanguage {
    public string Fizz => "!";
    public string Buzz => "@";
    public string FizzBuzz => "!@";
  }
  internal class English : ILanguage {
    public string Fizz => "A";
    public string Buzz => "B";
    public string FizzBuzz => "AB";
  }
}
