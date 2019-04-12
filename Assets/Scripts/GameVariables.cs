using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVariables
{
    public int TimeToAnswer {get;} = 5;
    public int SequenceLength {get;} = 5;
    public int ComboLength {get;} = 3;
    public int InitialHitpoints {get;} = 20;
    public int NormalDamage {get;} = 1;
    public int FinisherDamage {get;} = 3;
    public int NumCombos {get;} = 3;
    public int SymbolTypes {get;} = 3;
    
    public GameVariables(Difficulty difficulty) {
        switch (difficulty) {
            case Difficulty.EASY:
                break;
            case Difficulty.NORMAL:
                this.SequenceLength = 7;
                break;
            case Difficulty.HARD:
                this.SequenceLength = 9;
                break;
        }
    }
}
