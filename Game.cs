using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kns;

internal class Game
{
    const int PlayerCount = 2;
    readonly Random random = new Random();
    int A;
    int N;
    int C;
    List<char> AvailableCharacters;


    public Game()
    {
        A = ConsoleDisplay.GetParameter("a");
        N = ConsoleDisplay.GetParameter("n");
        C = ConsoleDisplay.GetParameter("c");

        AvailableCharacters = Enumerable.Range('A', A).Select(l => (char)l).ToList();
    }

    public void Play()
    {
        var word = new StringBuilder();
        int player = random.Next(PlayerCount);

        while (C > 0 && word.Length < N)
        {
            ConsoleDisplay.DisplayPartyInfo(word.ToString(), C, N);
            char nextLetter = GetNextChar(player);
            word.Append(nextLetter);
            RepetitionRemover.RemoveRepetition(word);

            C--;

            player = (player + 1) % PlayerCount;
        }

        ConsoleDisplay.DisplayPartyInfo(word.ToString(), C, N);
        ConsoleDisplay.PromptWinner(word.Length == N);
    }


    private char GetNextChar(int player)
    {
        return player == 0 ? ConsoleDisplay.GetNextCharacter(AvailableCharacters) : GetEnemyChar();
    }

    private char GetEnemyChar()
    {
        return AvailableCharacters[random.Next(AvailableCharacters.Count)];
    }

}
