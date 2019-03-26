using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState {

    public int Hitpoints {get; set;}
    public List<Sequence> Combos {get; set;}
    public Sequence CurrSequence {get; set;}
    public Sequence LastSequence {get; set;}
    public Role CurrRole {get; set;}

    public PlayerState() {
        this.Hitpoints = GameVariables.INITIAL_HITPOINTS;
        this.Combos = new List<Sequence>();
        this.CurrSequence = new Sequence();
        this.LastSequence = null;
        this.CurrRole = Role.NONE;
    }

    public void addCombo(Sequence combo) {
        this.Combos.Add(combo);
        //should add check so that there are no duplicate Combos
    }

    public void removeCombo() {
        this.Combos.RemoveAt(Combos.Count-1);
    }

    public Sequence getCombo(int index) {
        if (index >= this.Combos.Count) {
            return null;
        }
        return this.Combos[index];
    }

    public void addToCurrentSequence(Symbol s) {
        this.CurrSequence.addSymbol(s);
    }

    public void nextSequence() {
        this.LastSequence = this.CurrSequence;
        this.CurrSequence = new Sequence();
    }
}
