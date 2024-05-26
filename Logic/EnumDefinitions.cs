using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GK;

public enum GameMode
{
    PlayWithAi,
    WatchAi,
}

public enum Player
{
    Player1 = 0,
    Player2 = 1
}

public enum GameStatus
{
    OnGoing,
    Draw,
    Win
}

public enum Strategy
{
    Random
}