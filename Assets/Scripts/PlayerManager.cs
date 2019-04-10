using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerState thisPlayer = new PlayerState();
    public List<GameObject> comboPanels;
    
    private Sequence newcombo;
    void Start()
    {
        AssignCombos();

    }

    public void AssignCombos() {
        foreach (GameObject cPanel in comboPanels){
            newcombo = new Sequence();
            for (int i = 0; i < GameVariables.COMBO_LENGTH; i++) {
                // Debug.Log(Random.Range(1, (GameVariables.SYMBOL_TYPES + 1)));
                
                newcombo.addSymbol((Symbol)Random.Range(1, (GameVariables.SYMBOL_TYPES + 1)));
            }
            thisPlayer.addCombo(newcombo);
            PopulateComboPanel(cPanel, newcombo);
        }
    }

    public void PopulateComboPanel(GameObject cPanel, Sequence newcombo) {
        cPanel.GetComponent<ComboManager>().DisplayCombo(newcombo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
