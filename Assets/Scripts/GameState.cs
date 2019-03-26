using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState {
    private static System.Random random = new System.Random();
    private ComboDeck Deck {get; set;}
    public PlayerState Player1State {get;}
    public PlayerState Player2State {get;}

    public GameState() {
        this.Deck = new ComboDeck();
        // Players instanced, HP set to initial starting HP.
        this.Player1State = new PlayerState();
        this.Player2State = new PlayerState();

        Player1State.CurrRole = random.Next(0,1) == 0 ? Role.ATTACKER : Role.DEFENDER;
        Player2State.CurrRole = Player1State.CurrRole == Role.ATTACKER ? Role.DEFENDER : Role.ATTACKER;

        // Players recieve starting number of combos.
        for (int i = 0; i < GameVariables.NUM_COMBOS; i++) {
            this.Player1State.addCombo(this.Deck.drawCombo());
            this.Player2State.addCombo(this.Deck.drawCombo());
        }
    }

    public int CurrSequenceLength() {
        return this.Player1State.CurrSequence.Count;
    }

    // Returns 1 if player1 won, -1 if player 2 won, 0 otherwise.
    public int isWon() {
        if (this.Player1State.Hitpoints == 0) {
            return 1;
        }
        if (this.Player2State.Hitpoints == 0) {
            return -1;
        }
        return 0;
    }

    public static GameState nextState(GameState game, InputManager input) {
        if (game.CurrSequenceLength() == GameVariables.SEQUENCE_LENGTH) {
            // Players deal damage to each other
            PlayerState attacker = (game.Player1State.CurrRole == Role.ATTACKER) ? game.Player1State : game.Player2State;
            PlayerState defender = (game.Player2State.CurrRole == Role.DEFENDER) ? game.Player1State : game.Player2State;
            Sequence difference = Sequence.getDifference(defender.CurrSequence, attacker.CurrSequence);
            int damage = 0;
            foreach (Sequence combo in attacker.Combos) {
                if (Sequence.containsSubsequence(difference, combo)) {
                    damage += GameVariables.FINISHER_DAMAGE;
                }
            }
            damage += difference.Count * GameVariables.NORMAL_DAMAGE;
            defender.Hitpoints -= damage;

            // Players switch roles
            attacker.CurrRole = Role.DEFENDER;
            defender.CurrRole = Role.ATTACKER;

            // Attacker removes a combo, recieves a combo
            attacker.removeCombo();
            attacker.addCombo(game.Deck.drawCombo());
            return game;
        } else {
            // Players add their new symbol to their sequence
            game.Player1State.addToCurrentSequence(input.Player1Symbol);
            game.Player2State.addToCurrentSequence(input.Player2Symbol);
            return game;
        }
    }
}
