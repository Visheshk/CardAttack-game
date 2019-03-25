using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboDeck
{
    private List<Sequence> deck;
    private static System.Random random = new System.Random();

    public ComboDeck() {
        this.deck = generateDeck();
    }

    public Sequence getCombo() {
        return this.deck[random.Next(0, deck.Count-1)];
    }

    public List<Sequence> generateDeck() {
        List<Symbol> symbols = getSymbols();
        List<Sequence> combos = new List<Sequence>();
        generateCombos(symbols, combos, new Sequence(), 0);
        return combos;
    }

    // Returns a list of combinations of symbols using depth first search. Recursive helper function.
    private void generateCombos(List<Symbol> symbols, List<Sequence> combos, Sequence builtCombo, int depth) {
        if (depth == symbols.Count) {
            for (int i = 0; i < symbols.Count; i++) {
                Sequence combo = new Sequence(builtCombo.getSequence());
                combo.addSymbol(symbols[i]);
                combos.Add(combo);
            }
        } else {
            for (int i = 0; i < symbols.Count; i++) {
                Sequence newBuiltCombo = new Sequence();
                newBuiltCombo.addSymbol(symbols[i]);
                generateCombos(symbols, combos, newBuiltCombo, depth+1);
            }
        }
    }

    private List<Symbol> getSymbols() {
        return new List<Symbol>((Symbol[])Enum.GetValues(typeof(Symbol)));
    }
}
