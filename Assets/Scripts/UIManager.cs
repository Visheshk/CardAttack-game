using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private static int padding = 10; // Padding of elements in pixels(?)
    private static int symbolLength = 100;

    [Header("Scene UI Elements")]
    [SerializeField]
    public PlayerUI player1;
    [SerializeField]
    public PlayerUI player2;
    public GameObject statusText;

    [Header("Prefab UI Elements")]
    public GameObject comboPanel;
    public GameObject[] symbols;
    
    [System.Serializable]
    public struct PlayerUI {
        public GameObject nameText;
        public GameObject hitpointsText;
        public GameObject comboArea;
        public GameObject nextSymbolArea;
        public GameObject currentSequenceArea;
        public GameObject lastSequenceArea;
        public GameObject NextSymbol {get; set;}
        public List<GameObject> CurrentSequence {get; set;}
        public List<GameObject> LastSequence {get; set;}
        public List<List<GameObject>> ComboList {get; set;}
    }


    public void Start() {
        initializeUI();
    }

    public void Update() {
        
    }

    private void initializeUI() {
        initializeName(this.player1, "Player 1");
        initializeHitpoints(this.player1);
        initializeComboList(this.player1);
        initializeSequence(this.player1.currentSequenceArea, this.player1.CurrentSequence);
        initializeSequence(this.player1.lastSequenceArea, this.player1.LastSequence);
        initializeName(this.player2, "Player 2");
        initializeHitpoints(this.player2);
        initializeComboList(this.player2);
        initializeSequence(this.player2.currentSequenceArea, this.player2.CurrentSequence);
        initializeSequence(this.player2.lastSequenceArea, this.player2.LastSequence);
    }

    private void initializeComboList(PlayerUI player) {
        RectTransform pTransform = player.comboArea.GetComponent(typeof(RectTransform)) as RectTransform;
        for (int i = 0; i < GameVariables.NUM_COMBOS; i++) {
            GameObject comboPanel = GameObject.Instantiate(this.comboPanel, pTransform);
            comboPanel.name = "Combo" + i.ToString();
            RectTransform transform = comboPanel.GetComponent(typeof(RectTransform)) as RectTransform;
            transform.anchorMin = new Vector2(0.5f, (float)(GameVariables.NUM_COMBOS - i - 1)/GameVariables.NUM_COMBOS);
            transform.anchorMax = new Vector2(0.5f, 1 - (float)i/GameVariables.NUM_COMBOS);
            transform.sizeDelta = new Vector2((float)symbolLength * GameVariables.MAX_COMBO_LENGTH + (GameVariables.MAX_COMBO_LENGTH + 1)*padding,transform.sizeDelta.y);
        }
    }

    private void initializeSequence(GameObject sequenceArea, List<GameObject> sequence) {
        RectTransform pTransform = sequenceArea.GetComponent(typeof(RectTransform)) as RectTransform;
        pTransform.sizeDelta = new Vector2((float)symbolLength * GameVariables.SEQUENCE_LENGTH + (GameVariables.SEQUENCE_LENGTH + 1)*padding, pTransform.sizeDelta.y);
        pTransform.anchoredPosition = new Vector2(-(pTransform.sizeDelta.x / 2), 0);
        float center = ((float)GameVariables.SEQUENCE_LENGTH - 1) / 2;
        for (int i = 0; i < GameVariables.SEQUENCE_LENGTH; i++) {
            GameObject symbol = GameObject.Instantiate(this.symbols[0], pTransform);
            symbol.name = "Symbol" + i.ToString();
            RectTransform transform = symbol.GetComponent(typeof(RectTransform)) as RectTransform;
            transform.anchoredPosition = new Vector2((i-center)*(symbolLength+padding), transform.anchoredPosition.y);
        }
    }

    private static void initializeName(PlayerUI player, string name) {
        initializeText(player.nameText, name);
    }

    private static void initializeHitpoints(PlayerUI player) {
        initializeText(player.hitpointsText, GameVariables.INITIAL_HITPOINTS.ToString());
    }

    private static void initializeText(GameObject container, string s) {
        Text text = container.GetComponent(typeof(Text)) as Text;
        text.text = s;
    }

}
