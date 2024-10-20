using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToMenuBtnController : MonoBehaviour
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
        gameState.EnterStageMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
