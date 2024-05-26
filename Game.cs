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

    private char GetAIChar()
    {
        return Numbers[random.Next(Numbers.Count)].number;
    }

    private async void PlayNextAIMove()
    {
        if (CurrentPlayer == Player.Player2)
        {
            await Task.Delay(1000);

            var number = GetAIChar();
            wordBuilder.Append(number);
            RemovedRepetition = RepetitionRemover.RemoveRepetition(wordBuilder);
            if (RemovedRepetition != string.Empty)
                RemovedRepetition = $"Usunięto repetycje {RemovedRepetition}";

            Word = wordBuilder.ToString();
            NextPlayer();
        }
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
