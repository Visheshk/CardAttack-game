using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : IEnumerable {
    private List<Symbol> sequence{get;}
    public int Count { get{return sequence.Count;} } 

    public Sequence() {
        this.sequence = new List<Symbol>();
    }

    public Sequence(List<Symbol> sequence) {
        this.sequence = new List<Symbol>(sequence);
    }

    public Sequence(Sequence s) {
        this.sequence = new List<Symbol>(s.sequence);
    }

    public void addSymbol(Symbol s) {
        this.sequence.Add(s);
    }

    public Symbol getSymbol(int index) {
        if (index >= this.sequence.Count) {
            return Symbol.NONE;
        }
        return this.sequence[index];
    }

    // Returns the difference of two sequences, like a set difference A - B
    public static Sequence getDifference(Sequence seq1, Sequence seq2) {
        if (seq1 == null || seq2 == null || seq1.Count != seq2.Count) {
            return null;
        }
        Sequence newSeq = new Sequence();
        for (int i = 0; i < seq1.Count; i++) {
            if (!seq1.getSymbol(i).Equals(seq2.getSymbol(i))) {
                newSeq.addSymbol(seq1.getSymbol(i));
            }
        }
        return newSeq;
    }

    public static bool containsSubsequence(Sequence seq, Sequence subseq) {
        if (seq == null || subseq == null || subseq.Count > seq.Count) {
            return false;
        }
        for (int i = 0; i < seq.Count; i++) {
            if ((seq.Count - i) < subseq.Count) {
                break;
            }
            bool contains = true;
            for (int j = 0; j < subseq.Count; j++) {
                if (!seq.getSymbol(i+j).Equals(subseq.getSymbol(j))) {
                    contains = false;
                    break;
                }
            }
            if (contains == true) {
                return true;
            }
        }
        return false;
    }

    public IEnumerator GetEnumerator()
    {
        return this.sequence.GetEnumerator();
    }

    public Symbol this[int index] {
        get {
            return this.sequence[index];
        }

        set {
            this.sequence[index] = value;
        }
    }
}
