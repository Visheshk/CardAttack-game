using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private static int padding = 10; // Padding of elements in pixels(?)
    private static int symbolLength = 100; // Size of symbol edge in pixels(?)

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
        public Symbol LastSymbol {get; set;}
        public GameObject NextSymbol {get; set;}
        public List<GameObject> CurrentSequence {get; set;}
        public List<GameObject> LastSequence {get; set;}
        public List<List<GameObject>> ComboList {get; set;}
    }


    public void Start() {
        initializeUI();
    }

    public void Update() {
        ;
    }

    public void setNextSymbols(InputManager input) {
        setNextSymbol(input.Player1Symbol, this.player1);
        setNextSymbol(input.Player2Symbol, this.player2);
    }

    private void setNextSymbol(Symbol symbol, PlayerUI player) {
        if (symbol != player.LastSymbol) {
            GameObject.Destroy(player.NextSymbol);
            RectTransform pTransform = player.nextSymbolArea.GetComponent(typeof(RectTransform)) as RectTransform;
            GameObject nextSymbol = symbols[(int)symbol];
            player.NextSymbol = GameObject.Instantiate(nextSymbol, pTransform);
            player.LastSymbol = symbol;
        }
    }

    private void initializeUI() {
        initializeName(this.player1, "Player 1");
        initializeHitpoints(this.player1);
        initializeComboList(this.player1);
        initializeSequence(this.player1.currentSequenceArea, this.player1.CurrentSequence);
        initializeSequence(this.player1.lastSequenceArea, this.player1.LastSequence);
        initializeFields(this.player1);
        initializeName(this.player2, "Player 2");
        initializeHitpoints(this.player2);
        initializeComboList(this.player2);
        initializeSequence(this.player2.currentSequenceArea, this.player2.CurrentSequence);
        initializeSequence(this.player2.lastSequenceArea, this.player2.LastSequence);
        initializeFields(this.player2);
    }

    private void initializeComboList(PlayerUI player) {
        RectTransform pTransform = player.comboArea.GetComponent(typeof(RectTransform)) as RectTransform;
        for (int i = 0; i < GameVariables.NUM_COMBOS; i++) {
            GameObject comboPanel = GameObject.Instantiate(this.comboPanel, pTransform);
            comboPanel.name = "Combo" + i.ToString();
            RectTransform transform = comboPanel.GetComponent(typeof(RectTransform)) as RectTransform;
            transform.anchorMin = new Vector2(0.5f, (float)(GameVariables.NUM_COMBOS - i - 1)/GameVariables.NUM_COMBOS);
            transform.anchorMax = new Vector2(0.5f, 1 - (float)i/GameVariables.NUM_COMBOS);
            transform.sizeDelta = new Vector2((float)symbolLength * GameVariables.COMBO_LENGTH + (GameVariables.COMBO_LENGTH)*padding,transform.sizeDelta.y);
            intitializeSymbols(GameVariables.COMBO_LENGTH, transform);
        }
    }

    private void initializeSequence(GameObject sequenceArea, List<GameObject> sequence) {
        RectTransform pTransform = sequenceArea.GetComponent(typeof(RectTransform)) as RectTransform;
        pTransform.sizeDelta = new Vector2((float)symbolLength * GameVariables.SEQUENCE_LENGTH + (GameVariables.SEQUENCE_LENGTH)*padding, pTransform.sizeDelta.y);
        pTransform.anchoredPosition = new Vector2(-(pTransform.sizeDelta.x / 2), 0);
        intitializeSymbols(GameVariables.SEQUENCE_LENGTH, pTransform);
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

    private void intitializeSymbols(int numSymbols, RectTransform pTransform) {
        float center = ((float)numSymbols - 1) / 2;
        for (int i = 0; i < numSymbols; i++) {
            GameObject symbol = GameObject.Instantiate(this.symbols[0], pTransform);
            symbol.name = "Symbol" + i.ToString();
            RectTransform transform = symbol.GetComponent(typeof(RectTransform)) as RectTransform;
            transform.anchoredPosition = new Vector2((i-center)*(symbolLength+padding), transform.anchoredPosition.y);
        }
    }

    private void initializeFields(PlayerUI player) {
        player.LastSymbol = Symbol.NONE;
        player.NextSymbol = null;
        player.ComboList = new List<List<GameObject>>();
        player.CurrentSequence = new List<GameObject>();
        player.LastSequence = null;
    }

}
