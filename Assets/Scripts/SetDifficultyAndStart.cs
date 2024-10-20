using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDifficultyAndStart : MonoBehaviour
{
    Button button;
    public GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        gameState = FindFirstObjectByType<GameState>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        var diff = gameObject.name == "StartEasyBtn" ?
            GameState.GameDifficulty.EASY :
            gameObject.name == "StartMediumBtn" ?
            GameState.GameDifficulty.MEDIUM :
            GameState.GameDifficulty.HARD;
        gameState.currentDifficulty = diff;
        gameState.EnterStageGameplay();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
