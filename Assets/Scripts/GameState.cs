using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public enum PlanetType
{
    MERCURY,
    VENUS,
    EARTH,
    MARS,
    JUPITER,
    SATURN,
    URANUS,
    NEPTUNE,
    PLUTO,
    MOON,
}

public class GameState : MonoBehaviour
{
    public enum GameStage
    {
        MENU,
        GAMEPLAY,
        VICTORY,
        GAMEOVER
    }

    public enum GameDifficulty
    {
        EASY,
        MEDIUM,
        HARD
    }

    public SystemController systemController;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI victoryText;
    public TextMeshProUGUI projectilesLeftText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI instructionsText;
    public Button startEasyBtn;
    public Button startMediumBtn;
    public Button startHardBtn;
    public Button toMenuBtnPlaying;
    public Button toMenuBtnGameover;
    public Button toMenuBtnVictory;
    public LineRenderer playerLine;

    public GameStage gameStage;
    public GameDifficulty currentDifficulty;

    public float gravityCoefficient;
    public int projectilesLeft;
    public float turnSpeed;
    public int moonCountMin;
    public int moonCountMax;

    // Start is called before the first frame update
    void Start()
    {
        EnterStageMenu();
        systemController = FindObjectOfType<SystemController>();
    }

    void UpdateUIGivenStage()
    {
        Debug.Log(gameStage);
        ToggleMenuUI(gameStage == GameStage.MENU);
        ToggleGameplayUI(gameStage == GameStage.GAMEPLAY);
        ToggleGameOverUI(gameStage == GameStage.GAMEOVER);
        ToggleVictoryUI(gameStage == GameStage.VICTORY);
    }

    void ToggleMenuUI(bool b)
    {
        titleText.gameObject.SetActive(b);
        instructionsText.gameObject.SetActive(b);
        startEasyBtn.gameObject.SetActive(b);
        startMediumBtn.gameObject.SetActive(b);
        startHardBtn.gameObject.SetActive(b);
    }

    void ToggleGameplayUI(bool b)
    {
        toMenuBtnPlaying.gameObject.SetActive(b);
        projectilesLeftText.gameObject.SetActive(b);
        playerLine.gameObject.SetActive(b);
    }

    void ToggleGameOverUI(bool b)
    {
        gameOverText.gameObject.SetActive(b);
        toMenuBtnGameover.gameObject.SetActive(b);
    }

    void ToggleVictoryUI(bool b)
    {
        victoryText.gameObject.SetActive(b);
        toMenuBtnVictory.gameObject.SetActive(b);
    }

    public void EnterStageMenu()
    {
        var planets = FindObjectsByType<PlanetController>(FindObjectsSortMode.InstanceID);
        foreach (var planet in planets)
        {
            planet.BlowUp();
        }
        var projectiles = FindObjectsByType<ProjectileController>(FindObjectsSortMode.InstanceID);
        foreach (var projectile in projectiles)
        {
            projectile.BlowUp();
        }

        gameStage = GameStage.MENU;
        UpdateUIGivenStage();

    }

    public void EnterStageGameplay()
    {
        gameStage = GameStage.GAMEPLAY;
        UpdateUIGivenStage();

        switch (currentDifficulty)
        {
            case GameDifficulty.EASY:
                projectilesLeft = 9;
                gravityCoefficient = 5.0f;
                turnSpeed = 5.0f;
                moonCountMin = 0;
                moonCountMax = 3;
                break;
            case GameDifficulty.MEDIUM:
                projectilesLeft = 6;
                gravityCoefficient = 5.0f;
                turnSpeed = 10.0f;
                moonCountMin = 0;
                moonCountMax = 2;
                break;
            case GameDifficulty.HARD:
                projectilesLeft = 3;
                gravityCoefficient = 3.0f;
                turnSpeed = 15.0f;
                moonCountMin = 0;
                moonCountMax = 1;
                break;
        }
        UpdateProjectilesLeft(0);
        systemController.InitializeSystem();

    }

    void ExitStageGameplay()
    {
        var planets = FindObjectsByType<PlanetController>(FindObjectsSortMode.InstanceID);
        var projectiles = FindObjectsByType<ProjectileController>(FindObjectsSortMode.InstanceID);
        foreach (var planet in planets)
        {
            planet.BlowUp();
        }
        foreach (var projectile in projectiles)
        {
            projectile.BlowUp();
        }
    }

    void EnterStageVictory()
    {
        gameStage = GameStage.VICTORY;
        UpdateUIGivenStage();
    }

    void EnterStageGameOver()
    {
        gameStage = GameStage.GAMEOVER;
        UpdateUIGivenStage();
    }

    public void UpdateProjectilesLeft(int incr)
    {
        projectilesLeft = System.Math.Max(projectilesLeft + incr, 0);
        projectilesLeftText.text = "Projectiles left: " + projectilesLeft;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStage == GameStage.GAMEPLAY)
        {
            var planets = FindObjectsByType<PlanetController>(FindObjectsSortMode.InstanceID);
            var projectiles = FindObjectsByType<ProjectileController>(FindObjectsSortMode.InstanceID);
            if (planets.Length == 0)
            {
                ExitStageGameplay();
                EnterStageVictory();
            }
            else if (projectilesLeft < 1 && projectiles.Length < 1)
            {
                ExitStageGameplay();
                EnterStageGameOver();
            }
        }
    }
}
