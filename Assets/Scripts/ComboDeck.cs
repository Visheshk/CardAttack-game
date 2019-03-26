using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboDeck
{
    private List<Sequence> currStack;
    private List<Sequence> usedStack;
    private static System.Random random = new System.Random();

    public ComboDeck() {
        this.currStack = generateDeck();
        this.usedStack = new List<Sequence>();
        shuffle();
    }

    public Sequence drawCombo() {
        if (currStack.Count == 0) {
            shuffle();
        }
        Sequence combo = this.currStack[random.Next(0, currStack.Count-1)];
        this.currStack.Remove(combo);
        this.usedStack.Add(combo);
        return combo;
    }

    private void shuffle() {
        this.currStack.AddRange(usedStack);
        List<Sequence> newStack = new List<Sequence>();
        foreach (Sequence c in currStack) {
            int maxIndex = (newStack.Count-1 <= 0) ? 0 : newStack.Count-1;
            newStack.Insert(random.Next(0, maxIndex), c);
        }
        this.currStack = newStack;
    }

    public List<Sequence> generateDeck() {
        List<Symbol> symbols = getSymbols();
        List<Sequence> combos = generateCombos(symbols, new Sequence());
        return combos;
    }

    // Returns a list of combinations of symbols using depth first search. Recursive helper function.
    private List<Sequence> generateCombos(List<Symbol> symbols, Sequence builtCombo) {
        List<Sequence> newCombos = new List<Sequence>();
        foreach (Symbol s in symbols) {
            Sequence newBuiltCombo = new Sequence(builtCombo);
            newBuiltCombo.addSymbol(s);
            if (newBuiltCombo.Count >= GameVariables.COMBO_LENGTH) {
                newCombos.Add(newBuiltCombo);
            }
            else {
                newCombos.AddRange(generateCombos(symbols, newBuiltCombo));
            }
        }
        return newCombos;
    }

    private List<Symbol> getSymbols() {
        List<Symbol> symbols = new List<Symbol>((Symbol[])Enum.GetValues(typeof(Symbol)));
        symbols.Remove(Symbol.NONE);
        return symbols;
    }
}
