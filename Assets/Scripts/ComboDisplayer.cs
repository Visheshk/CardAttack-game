using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboDisplayer : MonoBehaviour
{   
    [System.Serializable]
    public struct symbolImage {
        public string name;
        public Sprite image;
    }
    public symbolImage[] symbolImagesDict;
    public List<GameObject> symbols;
    // public Dictionary<String, Sprite> symbolImagesDict;
    public List<Sprite> symbolImages;

    public Sequence thisCombo;
    // public GameObject symbol1;
    // public GameObject symbol1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DisplayCombo(Sequence combo) {
        thisCombo = combo;
        for (int i = 0; i < thisCombo.getLength(); i++) {
            // Debug.Log(thisCombo.getSymbol(i));
            // Debug.Log( (int) thisCombo.getSymbol(i));
            symbols[i].GetComponent<SymbolDisplayer>().SetImage((int) thisCombo.getSymbol(i) - 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
