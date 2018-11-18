using System;
using System.Linq;

namespace XUnitTestProject1 {
  public class FizzBuzz {

    // Write a program that prints the numbers from 1 to 100.
    // But for multiples of three print “Fizz” instead of the number
    // and for the multiples of five print “Buzz”.
    // For numbers which are multiples of both three and five print “FizzBuzz”.
    [Fact]
    public void Main() {
      var container = new Container();
      var printer = container.Printer;
      var printLineNumber = container.PrintLineNumber;
      var language = container.Language;

      _RunFizzBuzz(printer, language, printLineNumber);
    }

    private static void _RunFizzBuzz(
      IPrinter printer,
      ILanguage language,
      PrintLineNumber printLineNumber
    ) {
      foreach (var i in Enumerable.Range(1, 100)) {
        printLineNumber.Print(i);
        if (i % 15 == 0) {
          printer.PrintLine(language.FizzBuzz);
        } else if (i % 3 == 0) {
          printer.PrintLine(language.Fizz);
        } else if (i % 5 == 0) {
          printer.PrintLine(language.Buzz);
        } else {
          printer.PrintLine(i.ToString());
        }
      }
    }
  }

  public class Container { // Composition Root, and it constructs logic
    public IPrinter Printer => new FilePrinter();
    public PrintLineNumber PrintLineNumber => new PrintLineNumber(Printer);
    public ILanguage Language => new EnglishLanguage();
  }

  public class PrintLineNumber {
    private readonly IPrinter _printer;

    public PrintLineNumber(IPrinter printer) {
      _printer = printer;
    }

    public void Print(int x) {
      _printer.Print("line" + x);
    }
  }

  public interface IPrinter {
    void PrintLine(string x);
    void Print(string x);
  }

  public interface ILanguage {
    string FizzBuzz { get; }
    string Fizz { get; }
    string Buzz { get; }
  }

  public class ConsolePrinter : IPrinter {
    public void PrintLine(string x) =>
      Console.WriteLine(x);
    public void Print(string x) =>
      Console.Write(x);
  }

  public class FilePrinter : IPrinter {
    public void PrintLine(string x) =>
      Console.WriteLine("I wrote this to a file: " + x);
    public void Print(string x) =>
      Console.Write("I wrote this to a file: " + x);
  }

  public class Chineselanguage : ILanguage {
    public string FizzBuzz => "!";
    public string Fizz => "@";
    public string Buzz => "!@";
  }

  public class EnglishLanguage : ILanguage {
    public string FizzBuzz => "AB";
    public string Fizz => "A";
    public string Buzz => "B";
  }

}