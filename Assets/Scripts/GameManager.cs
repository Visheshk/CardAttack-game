using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static int padding = 10; // Padding of elements in pixels(?)
    private static int symbolLength = 100; // Size of symbol edge in pixels(?)
    private bool gameRunning = true;

    [Header("Scene UI Elements")]
    [SerializeField]
    public PlayerUI player1;
    [SerializeField]
    public PlayerUI player2;
    public GameObject statusText;
    public GameObject resumeButton;

    [Header("Prefab UI Elements")]
    public GameObject comboPanel;
    public GameObject[] symbolPrefabs;
    
    [System.Serializable]
    public struct PlayerUI {
        public GameObject nameText;
        public GameObject hitpointsText;
        public GameObject roleText;
        public GameObject comboArea;
        public GameObject nextSymbolArea;
        public GameObject currentSequenceArea;
        public Symbol LastSymbol {get; set;}
        public GameObject NextSymbol {get; set;}
        public List<GameObject> CurrentSequence {get; set;}
        public List<List<GameObject>> ComboList {get; set;}
    }
    [Header("Game Difficulty")]
    public Difficulty difficulty;

    private InputManager input;
    private GameState game;
    private GameVariables variables;
    private float timer;

    void Start() {
        this.input = new InputManager();
        this.variables = new GameVariables(this.difficulty);
        this.game = new GameState(variables);
        initializeUI();
        replaceGameState(this.game);
        this.timer = 0;
    }

    public void toggleGameState() {
        this.gameRunning = !this.gameRunning;
    }

    void Update() {
        if (!gameRunning) {
            return;
        }
        this.input.getInput();
        this.timer += Time.deltaTime;
        this.placeNextSymbols(this.input);
        placeTime(timer);
        if (this.timer >= variables.TimeToAnswer) {
            this.game = GameState.nextState(this.game, this.input);
            replaceGameState(this.game);
            if (this.game.isWon() != 0) {
                if (this.game.isWon() == -1) {
                    SceneManager.LoadScene("Player1WinScene");
                } else {
                    SceneManager.LoadScene("Player2WinScene");
                }
            }
            this.input.clear();
            timer = 0;
            toggleGameState();
            resumeButton.SetActive(true);
        }
    }

    private void initializeUI() {
        initializeFields(this.player1);
        initializeName(this.player1, "Player 1");
        initializeHitpoints(this.player1);
        player1.ComboList = initializeComboList(this.player1);
        player1.CurrentSequence = initializeSequence(this.player1.currentSequenceArea, this.player1.CurrentSequence);
        initializeFields(this.player2);
        initializeName(this.player2, "Player 2");
        initializeHitpoints(this.player2);
        player2.ComboList = initializeComboList(this.player2);
        player2.CurrentSequence = initializeSequence(this.player2.currentSequenceArea, this.player2.CurrentSequence);
    }

    public void placeNextSymbols(InputManager input) {
        this.player1 = placeNextSymbol(input.Player1Symbol, this.player1);
        this.player2 = placeNextSymbol(input.Player2Symbol, this.player2);
    }

    private PlayerUI placeNextSymbol(Symbol symbol, PlayerUI player) {
        if (symbol != player.LastSymbol) {
            GameObject.Destroy(player.NextSymbol);
            RectTransform pTransform = player.nextSymbolArea.GetComponent(typeof(RectTransform)) as RectTransform;
            player.NextSymbol = placeSymbol(pTransform, symbol);
            player.LastSymbol = symbol;
        }
        return player;
    }

    private void placeTime(float timer) {
        placeText(this.statusText, "Remaining\nTime\nTo Place:\n" + Mathf.Floor(this.variables.TimeToAnswer+1-timer).ToString());
    }

    private void replaceGameState(GameState state) {
        replaceComboLists(state);
        replaceCurrentSequences(state);
        replaceHitpoints(state);
        replaceRole(state);
    }

    private void replaceRole(GameState state) {
        this.player1.roleText = placeText(this.player1.roleText, state.Player1State.CurrRole.ToString());
        this.player2.roleText = placeText(this.player2.roleText, state.Player2State.CurrRole.ToString());
    }

    private void replaceHitpoints(GameState state) {
        this.player1.hitpointsText = placeText(this.player1.hitpointsText, state.Player1State.Hitpoints.ToString());
        this.player2.hitpointsText = placeText(this.player2.hitpointsText, state.Player2State.Hitpoints.ToString());
    }

    private void replaceCurrentSequences(GameState state) {
        this.player1.CurrentSequence = replaceCurrentSequence(state.Player1State.CurrSequence, this.player1.CurrentSequence);
        this.player2.CurrentSequence = replaceCurrentSequence(state.Player2State.CurrSequence, this.player2.CurrentSequence);
    }

    private List<GameObject> replaceCurrentSequence(Sequence seq, List<GameObject> symbolObjects) {
        return replaceSymbols(seq, symbolObjects);
    }

    private List<GameObject> replaceLastSequence(Sequence seq, List<GameObject> symbolObjects) {
        return replaceSymbols(seq, symbolObjects);
    }

    private void replaceComboLists(GameState state) {
        this.player1 = replaceComboList(state.Player1State.Combos, this.player1);
        this.player2 = replaceComboList(state.Player2State.Combos, this.player2);
    }

    private PlayerUI replaceComboList(List<Sequence> combos, PlayerUI player) {
        List<List<GameObject>> newComboList = new List<List<GameObject>>();
        for (int i = 0; i < combos.Count; i++) {
            newComboList.Add(replaceSymbols(combos[i], player.ComboList[i]));
        }
        player.ComboList = newComboList;
        return player;
    }

    private List<GameObject> replaceSymbols(Sequence seq, List<GameObject> symbolObjects) {
        List<GameObject> newSymbolObjects = new List<GameObject>();
        for (int i = 0; i < symbolObjects.Count; i++) {
            if (i < seq.Count) {
                newSymbolObjects.Add(replaceSymbol(symbolObjects[i], seq[i]));
            } else {
                newSymbolObjects.Add(replaceSymbol(symbolObjects[i], Symbol.NONE));
            }
        }
        return newSymbolObjects;
    }

    private GameObject replaceSymbol(GameObject oldSymbolObject, Symbol s) {
        RectTransform transform = oldSymbolObject.GetComponent(typeof(RectTransform)) as RectTransform;
        GameObject newSymbolObject = GameObject.Instantiate(this.symbolPrefabs[(int)s], transform);
        newSymbolObject.name = "Symbol";
        newSymbolObject.transform.SetParent(oldSymbolObject.transform.parent);
        GameObject.Destroy(oldSymbolObject);
        return newSymbolObject;
    }

    private List<List<GameObject>> initializeComboList(PlayerUI player) {
        List<List<GameObject>> comboList = new List<List<GameObject>>();
        RectTransform pTransform = player.comboArea.GetComponent(typeof(RectTransform)) as RectTransform;
        for (int i = 0; i < this.variables.NumCombos; i++) {
            GameObject comboPanel = GameObject.Instantiate(this.comboPanel, pTransform);
            comboPanel.name = "Combo" + i.ToString();
            RectTransform transform = comboPanel.GetComponent(typeof(RectTransform)) as RectTransform;
            transform.anchorMin = new Vector2(0.5f, (float)(this.variables.NumCombos - i - 1)/this.variables.NumCombos);
            transform.anchorMax = new Vector2(0.5f, 1 - (float)i/this.variables.NumCombos);
            transform.sizeDelta = new Vector2((float)symbolLength * this.variables.ComboLength + (this.variables.ComboLength+1)*padding,transform.sizeDelta.y);
            comboList.Add(intitializeSymbols(this.variables.ComboLength, transform));
        }
        return comboList;
    }

    private List<GameObject> initializeSequence(GameObject sequenceArea, List<GameObject> sequence) {
        RectTransform pTransform = sequenceArea.GetComponent(typeof(RectTransform)) as RectTransform;
        pTransform.sizeDelta = new Vector2((float)symbolLength * this.variables.SequenceLength + (this.variables.SequenceLength)*padding, pTransform.sizeDelta.y);
        pTransform.anchoredPosition = new Vector2(pTransform.sizeDelta.x / 2 + padding, 0);
        return intitializeSymbols(this.variables.SequenceLength, pTransform);
    }

    private static void initializeName(PlayerUI player, string name) {
        player.nameText = placeText(player.nameText, name);
    }

    private void initializeHitpoints(PlayerUI player) {
        player.hitpointsText = placeText(player.hitpointsText, this.variables.InitialHitpoints.ToString());
    }

    private static GameObject placeText(GameObject container, string s) {
        Text text = container.GetComponent(typeof(Text)) as Text;
        text.text = s;
        return container;
    }

    private List<GameObject> intitializeSymbols(int numSymbols, RectTransform pTransform) {
        List<GameObject> symbolObjects = new List<GameObject>();
        float center = ((float)numSymbols - 1) / 2;
        for (int i = 0; i < numSymbols; i++) {
            GameObject symbol = placeSymbol(pTransform, Symbol.NONE);
            RectTransform transform = symbol.GetComponent(typeof(RectTransform)) as RectTransform;
            transform.anchoredPosition = new Vector2((i-center)*(symbolLength+padding), transform.anchoredPosition.y);
            symbolObjects.Add(symbol);
        }
        return symbolObjects;
    }

    private void initializeFields(PlayerUI player) {
        player.LastSymbol = Symbol.NONE;
        player.NextSymbol = null;
        player.ComboList = new List<List<GameObject>>();
        player.CurrentSequence = new List<GameObject>();
    }

    private GameObject placeSymbol(RectTransform pTransform, Symbol s) {
        GameObject o = GameObject.Instantiate(this.symbolPrefabs[(int)s], pTransform);
        o.name = "Symbol";
        return o;
    }

    private List<GameObject> placeSymbols(List<RectTransform> transforms, Sequence symbols) {
        List<GameObject> symbolObjects = new List<GameObject>();
        for (int i = 0; i < transforms.Count; i++) {
            symbolObjects.Add(placeSymbol(transforms[i], symbols[i]));
        }
        return symbolObjects;
    }

}
