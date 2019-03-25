using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState {

    private int hitpoints;
    private List<Sequence> combos;
    private Sequence currSequence;
    private Sequence lastSequence;
    private Role role;

    public PlayerState() {
        this.hitpoints = GameVariables.INITIAL_HITPOINTS;
        this.combos = new List<Sequence>();
        this.currSequence = new Sequence();
        this.lastSequence = null;
    }

    public int getHitpoints() {
        return hitpoints;
    }

    public void setHitpoints(int hitpoints) {
        this.hitpoints = hitpoints;
    }

    public void addCombo(Sequence combo) {
        this.combos.Add(combo);
        //should add check so that there are no duplicate combos
    }

    public void removeCombo() {
        this.combos.RemoveAt(combos.Count-1);
    }

    public Sequence getCombo(int index) {
        if (index >= this.combos.Count) {
            return null;
        }
        return this.combos[index];
    }

    public List<Sequence> getCombos() {
        return this.combos;
    }

    public Sequence getCurrentSequence() {
        return this.currSequence;
    }

    public void addToCurrentSequence(Symbol s) {
        this.currSequence.addSymbol(s);
    }

    public void nextSequence() {
        this.lastSequence = this.currSequence;
        this.currSequence = new Sequence();
    }

    public Sequence getLastSequence() {
        return this.lastSequence;
    }

    public Role getRole() {
        return this.role;
    }

    public void setRole(Role role) {
        this.role = role;
    }
}
