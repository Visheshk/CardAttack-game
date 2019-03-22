using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerState thisPlayer = new PlayerState();
    public List<GameObject> comboPanels;
    public Dictionary<Symbol, Sprite> SymbolImages;
    // private List<GameObject> symbolBoxes;
    private Sequence newcombo;
    void Start()
    {
        AssignCombos();
        // Debug.Log((Symbol)1);
        // Debug.Log((Symbol)2);
        // Debug.Log((Symbol)3);

    }

    public void AssignCombos() {
        foreach (GameObject cPanel in comboPanels){
            newcombo = new Sequence();
            for (int i = 0; i < GameVariables.COMBO_LENGTH; i++) {
                Debug.Log(Random.Range(1, (GameVariables.SYMBOL_TYPES + 1)));
                
                newcombo.addSymbol((Symbol)Random.Range(1, (GameVariables.SYMBOL_TYPES + 1)));
            }
            thisPlayer.addCombo(newcombo);
            PopulateComboPanel(cPanel, newcombo);
        }
    }

    public void PopulateComboPanel(GameObject cPanel, Sequence newcombo) {
        // Transform s1 = cPanel.transform.GetChild("Symbol1");
        // Transform s1 = cPanel.transform.GetChild("Symbol1");
        // GameObject s1 = GameObject.Find("/Symbol1");
        cPanel.GetComponent<ComboDisplayer>().DisplayCombo(newcombo);
        // Transform s2 = cPanel.transform.GetChild("Symbol2");
        // Transform s3 = cPanel.transform.GetChild("Symbol3");
        // s1.transform.GetComponent<UnityEngine.UI.Image>() = SymbolImages[newcombo.getSymbol(0)];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
