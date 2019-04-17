using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState {
    private static System.Random random = new System.Random();
    private ComboDeck Deck {get; set;}
    public PlayerState Player1State {get;}
    public PlayerState Player2State {get;}
    public bool roundRunning;

    private GameVariables variables;
    public int seqStep;
    public int comboCheck;

    public GameState(GameVariables variables) {
        this.variables = variables;
        this.Deck = new ComboDeck(this.variables.ComboLength);
        // Players instanced, HP set to initial starting HP.
        this.Player1State = new PlayerState(variables.InitialHitpoints);
        this.Player2State = new PlayerState(variables.InitialHitpoints);

        Player1State.CurrRole = random.Next(0,2) == 0 ? Role.ATTACKER : Role.DEFENDER;
        Player2State.CurrRole = Player1State.CurrRole == Role.ATTACKER ? Role.DEFENDER : Role.ATTACKER;

        // Players recieve starting number of combos.
        for (int i = 0; i < variables.NumCombos; i++) {
            this.Player1State.addCombo(this.Deck.drawCombo());
            this.Player2State.addCombo(this.Deck.drawCombo());
        }
        this.roundRunning = true;
        this.seqStep = 0;
        this.comboCheck = 0;
    }

    public int CurrSequenceLength() {
        return this.Player1State.CurrSequence.Count;
    }

    public bool isRoundEnd() {
        return !roundRunning;
    }

    // Returns 1 if player 1 won, -1 if player 2 won, 0 otherwise.
    public int isWon() {
        if (this.Player1State.Hitpoints <= 0) {
            return 1;
        }
        if (this.Player2State.Hitpoints <= 0) {
            return -1;
        }
        return 0;
    }

    public static GameState nextState(GameState game, InputManager input) {
        game.Player1State.addToCurrentSequence(input.Player1Symbol);
        game.Player2State.addToCurrentSequence(input.Player2Symbol);
        if (game.CurrSequenceLength() == game.variables.SequenceLength) {
            /*
            // Players deal damage to each other
            PlayerState attacker = (game.Player1State.CurrRole == Role.ATTACKER) ? game.Player1State : game.Player2State;
            PlayerState defender = (game.Player1State.CurrRole == Role.DEFENDER) ? game.Player1State : game.Player2State;
            */

            //////////////////////////////
            game.roundRunning = false;
            game.seqStep = 0;
            game.comboCheck = 0;

            /*
            Sequence difference = Sequence.getDifference(attacker.CurrSequence, defender.CurrSequence);

            int damage = 0;
            foreach (Sequence combo in attacker.Combos) {
                damage += game.variables.FinisherDamage * Sequence.subsequenceOccurences(difference, combo);
            }
            damage += difference.Count * game.variables.NormalDamage;
            defender.Hitpoints -= damage;
            */

            //////////////////////////////

            /*

            // Players switch roles
            game.Player1State.CurrRole = (game.Player1State.CurrRole == Role.ATTACKER) ? Role.DEFENDER : Role.ATTACKER;
            game.Player2State.CurrRole = (game.Player1State.CurrRole == Role.ATTACKER) ? Role.DEFENDER : Role.ATTACKER;

            // Players get new sequences
            game.Player1State.nextSequence();
            game.Player2State.nextSequence();

            // Attacker removes a combo, recieves a combo
            attacker.removeCombo();
            attacker.addCombo(game.Deck.drawCombo());

            */

        }
        return game;
    }

    public static GameState nextAttack(GameState game) {
        PlayerState attacker = (game.Player1State.CurrRole == Role.ATTACKER) ? game.Player1State : game.Player2State;
        PlayerState defender = (game.Player1State.CurrRole == Role.DEFENDER) ? game.Player1State : game.Player2State;
        if (game.seqStep < attacker.CurrSequence.Count){
            if (attacker.CurrSequence.getSymbol(game.seqStep) == defender.CurrSequence.getSymbol(game.seqStep)) {
                attacker.CurrSequence.makeInactive(game.seqStep);
            }
            else {
                //cause damage
                defender.Hitpoints -= 1;
            }
            game.seqStep += 1;
        }
        else{
            if(game.comboCheck < attacker.Combos.Count) {
                //cause damage
                Sequence difference = Sequence.getDifference(attacker.CurrSequence, defender.CurrSequence);
                if (Sequence.subsequenceOccurences(difference, attacker.Combos[game.comboCheck]) > 0) {
                    defender.Hitpoints -= game.variables.FinisherDamage;
                }
                game.comboCheck += 1;
            }
            else {
                game.roundRunning = true;
                game.seqStep = 0;
                game.comboCheck = 0;

                // Players switch roles
                game.Player1State.CurrRole = (game.Player1State.CurrRole == Role.ATTACKER) ? Role.DEFENDER : Role.ATTACKER;
                game.Player2State.CurrRole = (game.Player1State.CurrRole == Role.ATTACKER) ? Role.DEFENDER : Role.ATTACKER;

                // Players get new sequences
                game.Player1State.nextSequence();
                game.Player2State.nextSequence();

                // Attacker removes a combo, recieves a combo
                attacker.removeCombo();
                attacker.addCombo(game.Deck.drawCombo());

            }
        }
        return game;
    }

}
