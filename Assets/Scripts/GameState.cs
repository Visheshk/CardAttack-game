using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : ScriptableObject {
    private ComboDeck deck;
    private PlayerState player1State;
    private PlayerState player2State;
    private int currentSequenceLength;

    public GameState() {
        this.deck = new ComboDeck();
        // Players instanced, HP set to initial starting HP.
        this.player1State = new PlayerState();
        this.player2State = new PlayerState();

        // Players recieve starting number of combos.
        for (int i = 0; i < GameVariables.NUM_COMBOS; i++) {
            this.player1State.addCombo(deck.getCombo());
            this.player2State.addCombo(deck.getCombo());
        }
    }

    public GameState(ComboDeck deck, PlayerState p1, PlayerState p2) {
        this.deck = deck;
        this.player1State = p1;
        this.player2State = p2;
    }

    public PlayerState getPlayer1State() {
        return this.player1State;
    }

    public PlayerState getPlayer2State() {
        return this.player2State;
    }

    public int getCurrentSequenceLength() {
        return this.player1State.getCurrentSequence().getLength();
    }

    public void setPlayer1State(PlayerState state) {
        this.player1State = state;
    }

    public void setPlayer2State(PlayerState state) {
        this.player2State = state;
    }

    public ComboDeck getDeck() {
        return this.deck;
    }

    // Returns 1 if player1 won, -1 if player 2 won, 0 otherwise.
    public int isWon() {
        if (this.player1State.getHitpoints() == 0) {
            return 1;
        }
        if (this.player2State.getHitpoints() == 0) {
            return -1;
        }
        return 0;
    }

    public static GameState nextState(GameState game, GameInput input) {
        if (game.getCurrentSequenceLength() == GameVariables.SEQUENCE_LENGTH) {
            // Players deal damage to each other
            PlayerState attacker = (game.getPlayer1State().getRole() == Role.ATTACKER) ? game.getPlayer1State() : game.getPlayer2State();
            PlayerState defender = (game.getPlayer2State().getRole() == Role.DEFENDER) ? game.getPlayer1State() : game.getPlayer2State();
            Sequence difference = Sequence.getDifference(defender.getCurrentSequence(), attacker.getCurrentSequence());
            int damage = 0;
            foreach (Sequence combo in attacker.getCombos()) {
                if (Sequence.containsSubsequence(difference, combo)) {
                    damage += GameVariables.FINISHER_DAMAGE;
                }
            }
            damage += difference.getLength() * GameVariables.NORMAL_DAMAGE;
            defender.setHitpoints(defender.getHitpoints() - damage);

            // Players switch roles
            attacker.setRole(Role.DEFENDER);
            defender.setRole(Role.ATTACKER);

            // Attacker removes a combo, recieves a combo
            attacker.removeCombo();
            attacker.addCombo(game.getDeck().getCombo());
            return game;
        } else {
            // Players add their new symbol to their sequence
            game.getPlayer1State().addToCurrentSequence(input.getPlayer1Symbol());
            game.getPlayer2State().addToCurrentSequence(input.getPlayer2Symbol());
            return game;
        }
    }
}
