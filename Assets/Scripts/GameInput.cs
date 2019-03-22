using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private Symbol player1Symbol;
    private Symbol player2Symbol;

    public Symbol getPlayer1Symbol() {
        return this.player1Symbol;
    }

    public Symbol getPlayer2Symbol() {
        return this.player2Symbol;
    }

    public GameInput getInput() {
        throw new System.NotImplementedException();
    }
}
