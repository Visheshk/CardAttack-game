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

    // Returns the difference of two sequences, like a set difference A - B
    public static Sequence getDifference(Sequence seq1, Sequence seq2) {
        if (seq1 == null || seq2 == null || seq1.Count != seq2.Count) {
            return null;
        }
        Sequence newSeq = new Sequence();
        for (int i = 0; i < seq1.Count; i++) {
            if (seq1[i] != seq2[i]) {
                newSeq.addSymbol(seq1[i]);
            }
        }
        return newSeq;
    }

    public static int subsequenceOccurences(Sequence seq, Sequence subseq) {
        int occurrences = 0;
        if (seq == null || subseq == null || subseq.Count > seq.Count) {
            return 0;
        }
        for (int i = 0; i < seq.Count; i++) {
            if ((seq.Count - i) < subseq.Count) {
                break;
            }
            bool contains = true;
            for (int j = 0; j < subseq.Count; j++) {
                if (seq[i+j] != subseq[j]) {
                    contains = false;
                    break;
                }
            }
            if (contains == true) {
                occurrences++;
            }
        }
        return occurrences;
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
