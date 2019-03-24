using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState {
    private PlayerState player1State;
    private PlayerState player2State;
    private int currentSequenceLength;

    public GameState() {
        this.player1State = new PlayerState();
        this.player2State = new PlayerState();
        this.currentSequenceLength = 0;
    }

    public GameState(PlayerState p1, PlayerState p2, int currSeqLen) {
        this.player1State = p1;
        this.player2State = p2;
        this.currentSequenceLength = currSeqLen;
    }

    public PlayerState getPlayer1State() {
        return this.player1State;
    }

    public PlayerState getPlayer2State() {
        return this.player2State;
    }

    public int getCurrentSequenceLength() {
        return this.currentSequenceLength;
    }

    public void setCurrentSequenceLength(int currSeqLen) {
        this.currentSequenceLength = currSeqLen;
    }

    public void setPlayer1State(PlayerState state) {
        this.player1State = state;
    }

    public void setPlayer2State(PlayerState state) {
        this.player2State = state;
    }

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
            return game;
        } else {
            game.getPlayer1State().addToCurrentSequence(input.getPlayer1Symbol());
            game.getPlayer2State().addToCurrentSequence(input.getPlayer2Symbol());
            game.setCurrentSequenceLength(game.getCurrentSequenceLength() + 1);
            return game;
        }
    }
}
