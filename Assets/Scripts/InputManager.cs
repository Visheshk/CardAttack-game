using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {
    public Symbol Player1Symbol {get; set;}
    public Symbol Player2Symbol {get; set;}

    public InputManager() {
        this.Player1Symbol = Symbol.NONE;
        this.Player2Symbol = Symbol.NONE;
    }

    public void clear() {
        this.Player1Symbol = Symbol.NONE;
        this.Player2Symbol = Symbol.NONE;
    }

    public void getInput() {
        if (Input.GetButtonDown("Player1 Button1")) {
            this.Player1Symbol = Symbol.TYPE1;
        } else if (Input.GetButtonDown("Player1 Button2")) {
            this.Player1Symbol = Symbol.TYPE2;
        } else if (Input.GetButtonDown("Player1 Button3")) {
            this.Player1Symbol = Symbol.TYPE3;
        }

        if (Input.GetButtonDown("Player2 Button1")) {
            this.Player2Symbol = Symbol.TYPE1;
        } else if (Input.GetButtonDown("Player2 Button2")) {
            this.Player2Symbol = Symbol.TYPE2;
        } else if (Input.GetButtonDown("Player2 Button3")) {
            this.Player2Symbol = Symbol.TYPE3;
        }
    }
}
