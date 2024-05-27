using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GK.Logic;
using Kns;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GK;

public partial class Game : ObservableObject
{
    const int PlayerCount = 2;
    readonly Random random = new Random();
    int A;
    int N;

    [ObservableProperty()]
    public int c;

    [ObservableProperty()]
    public Player? currentPlayer;

    [ObservableProperty()]
    public ObservableCollection<GameNumber> numbers;

    [ObservableProperty()]
    public GameStatus gameStatus;

    [ObservableProperty()]
    public Player? winner;

    [ObservableProperty()]
    public string word;

    [ObservableProperty()]
    public string removedRepetition;

    public StringBuilder wordBuilder = new StringBuilder();

    public bool IsGameFinished => this.GameStatus != GameStatus.OnGoing;

    public Game(int a, int c, int n)
    {
        A = a;
        N = n;
        C = c;

        currentPlayer = random.NextEnum<Player>();

        numbers = new ObservableCollection<GameNumber>(
            Enumerable.Range('A', A).Select(l => (char)l)
            .Order()
            .Select(n => new GameNumber() { Number = n }));
    }

    public void PlayWithAi()
    {
        if (CurrentPlayer == Player.Player2)
        {
            PlayNextAIMove();
        }
    }

    private void NextPlayer()
    {
        C--;
        
        if (C <= 0 && wordBuilder.Length != N)
        {
            Winner = Player.Player1;
            GameStatus = GameStatus.Win;
        }

        if (wordBuilder.Length == N)
        {
            Winner = Player.Player2;
            GameStatus = GameStatus.Win;
        }


        if (GameStatus == GameStatus.OnGoing && CurrentPlayer != null)
            CurrentPlayer = (Player)(((int)CurrentPlayer + 1) % 2);
        else
            CurrentPlayer = null;
    }

    private async void PlayNextAIMove()
 {
     if (CurrentPlayer == Player.Player2)
     {
         await Task.Delay(1000);

         var number = GetBestMove();
         wordBuilder.Append(number);
         RemovedRepetition = RepetitionRemover.RemoveRepetition(wordBuilder);
         if (RemovedRepetition != string.Empty)
             RemovedRepetition = $"Usunięto repetycje {RemovedRepetition}";

         Word = wordBuilder.ToString();
         NextPlayer();
     }
 }

 private char GetBestMove()
 {
     char bestMove = ' ';
     int bestEval = int.MinValue;

     foreach (var gameNumber in Numbers)
     {
         var savedWord = wordBuilder.ToString();
         wordBuilder.Append(gameNumber.Number);
         RemovedRepetition = RepetitionRemover.RemoveRepetition(wordBuilder);
         int eval = Minimax(3, int.MinValue, int.MaxValue, true);
         wordBuilder.Clear();
         wordBuilder.Append(savedWord);

         if (eval > bestEval)
         {
             bestEval = eval;
             bestMove = gameNumber.Number;
         }
     }

     return bestMove;
 }

 private int Minimax(int depth, int alpha, int beta, bool maximizingPlayer)
 {
     if (IsGameFinished || depth == 0)
         return Evaluate();

     if (maximizingPlayer)
     {
         int maxEval = int.MinValue;
         foreach (var gameNumber in Numbers)
         {
             var savedWord = wordBuilder.ToString();
             wordBuilder.Append(gameNumber.Number);
             RemovedRepetition = RepetitionRemover.RemoveRepetition(wordBuilder);
             int eval = Minimax(depth - 1, alpha, beta, false);
             wordBuilder.Clear();
             wordBuilder.Append(savedWord);

             maxEval = Math.Max(maxEval, eval);
             alpha = Math.Max(alpha, eval);
             if (beta <= alpha)
                 break;
         }
         return maxEval;
     }
     else
     {
         int minEval = int.MaxValue;
         foreach (var gameNumber in Numbers)
         {
             var savedWord = wordBuilder.ToString();
             wordBuilder.Append(gameNumber.Number);
             RemovedRepetition = RepetitionRemover.RemoveRepetition(wordBuilder);
             int eval = Minimax(depth - 1, alpha, beta, true);
             wordBuilder.Clear();
             wordBuilder.Append(savedWord);

             minEval = Math.Min(minEval, eval);
             beta = Math.Min(beta, eval);
             if (beta <= alpha)
                 break;
         }
         return minEval;
     }
 }

 private int Evaluate()
 {
     if (wordBuilder.Length >= N)
         return int.MaxValue; // Winning state for the AI
     if (C <= 0)
         return int.MinValue; // Winning state for the human player
     return wordBuilder.Length; // Longer word is better for the AI
 }

    [RelayCommand]
    public void SelectNumber(GameNumber gameNumber)
    {
        if (CurrentPlayer == Player.Player1)
        {
            wordBuilder.Append(gameNumber.Number);
            RemovedRepetition = RepetitionRemover.RemoveRepetition(wordBuilder);
            if (RemovedRepetition != string.Empty)
                RemovedRepetition = $"Usunięto repetycje {RemovedRepetition}";
            Word = wordBuilder.ToString();

            NextPlayer();
            PlayNextAIMove();
        }
    }
}
