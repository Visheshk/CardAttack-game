using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : MonoBehaviour
{

    private GameState game;

    void Start()
    {
        game = ScriptableObject.CreateInstance(typeof(GameState)) as GameState;
    }

    void Update()
    {
        
        //Calculate next game state
        //Update UI to gamestate
    }
}
