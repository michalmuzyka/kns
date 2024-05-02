using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kns;

internal static class ConsoleDisplay
{
    public static string GetValue(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }


    public static int GetParameter(string parameter)
    {
        var result = GetValue($"Podaj wartość parametru {parameter}: ");
        return int.Parse( result );
    }

    public static void DisplayPartyInfo(string word, int C, int N)
    {
        Console.Clear();
        Console.WriteLine($"Maksymalna długość słowa: {N}");
        Console.WriteLine($"Pozostało tur: {C}");
        Console.WriteLine($"Utworzone słowo: {word}");
        Console.WriteLine();
    }

    public static char GetNextCharacter(List<char> availableCharacters)
    {
        char c;

        do
        {
            var result = GetValue($"Dostępne litery: {string.Join(", ", availableCharacters)} \nWybierz literę: ");
            c = result[0];
        } while (!availableCharacters.Contains(c));
        
        return c;
    }

    public static void PromptWinner(bool computerPlayerWon)
    {
        Console.WriteLine(computerPlayerWon ? "Wygrał komputer" : "Wygrałeś");
    }
}