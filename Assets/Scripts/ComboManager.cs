using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> symbols;
    public Sequence thisCombo;

    void Start()
    {
        
    }

    public void DisplayCombo(Sequence combo) {
        thisCombo = combo;
        for (int i = 0; i < thisCombo.Count; i++) {
            // Debug.Log(i);
            // Debug.Log((int) thisCombo.getSymbol(i));
            symbols[i].GetComponent<SymbolManager>().SetImage((int) thisCombo.getSymbol(i) - 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
