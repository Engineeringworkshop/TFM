using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    [Header("Fade configuration")]
    [SerializeField] private float startGameFadeTime;
    [SerializeField] private float endGameFadeTime;
    [SerializeField] [Range(0.0f, 1.0f)] private float endGameFadeValue;

    [Header("References")]
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private BlackPanelController blackPanelController;
    [SerializeField] private EndGamePanelController endGamePanelController;

    [SerializeField] private List<Robot_Controller> playerList = new List<Robot_Controller>();

    [SerializeField] private Robot_Controller currentWinner;
    public List<Robot_Controller> PlayerList 
    {
        get
        {
            return playerList;
        }
        private set
        {
            playerList = value;
        } 
    }

    [Header("Time config")]
    [SerializeField] private float roundTextTime;
    [SerializeField] private float roundFightTime;
    [SerializeField] private float roundTime;
    [SerializeField] private float roundEndRoundTextTime;
    [SerializeField] private float roundEndGameTextTime;

    // Timers
    private WaitForSecondsRealtime roundTextTimer;
    private WaitForSecondsRealtime roundFightTimer;
    private WaitForSecondsRealtime roundEndRoundTextTimer;

    [Header("Debug")]
    [SerializeField] private int roundNumber;

    static private bool isPaused;
    static public bool IsPaused
    {
        get 
        {
            return isPaused;
        }
        private set
        {
            isPaused = value;

            if (OnGamePaused != null)
            {
                OnGamePaused();
            } 
        } 
    }

    private bool roundTimeComplete;
    private bool hasWinner;

    // Events
    // Events
    public delegate void GamePaused();
    public static event GamePaused OnGamePaused;

    private void OnValidate()
    {
        if (blackPanelController == null)
        {
            blackPanelController = GetComponent<BlackPanelController>();
        }

        if (endGamePanelController == null)
        {
            endGamePanelController = GetComponent<EndGamePanelController>();
        }
    }

    private void OnEnable()
    {
        Robot_Controller.OnDefeated += PlayerDefeated;
    }

    private void OnDisable()
    {
        Robot_Controller.OnDefeated -= PlayerDefeated;
    }

    private void Awake()
    {
        // Set round flags to 0
        roundTimeComplete = false;
        hasWinner = false;

        IsPaused = true;

        roundNumber = 0;

        messageText.text = string.Empty;

        timeText.text = "00";

        // Create timmers
        roundTextTimer = new WaitForSecondsRealtime(roundTextTime);
        roundFightTimer = new WaitForSecondsRealtime(roundFightTime);
        roundEndRoundTextTimer = new WaitForSecondsRealtime(roundEndRoundTextTime);
    }

    private void Start()
    {
        StartCoroutine(GameStartCoroutine());
    }

    #region Gameplay logic

    /// <summary>
    /// This coroutine will only executed once on game start
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameStartCoroutine()
    {
        // Start Fade in
        yield return StartCoroutine(blackPanelController.FadeOut(1, 0, startGameFadeTime));

        StartCoroutine(GameLoop());
    }

    /// <summary>
    /// This is the game loop logic, this will be executed each game round
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameLoop()
    {
        // Start off by running the 'RoundStarting' coroutine but don't return until it's finished
        yield return StartCoroutine(RoundStarting());

        // Once the 'RoundStarting' coroutine is finished, run the 'RoundPlaying' coroutine but don't return until it's finished
        yield return StartCoroutine(RoundPlaying());

        // Once execution has returned here, run the 'RoundEnding' coroutine, again don't return until it's finished
        yield return StartCoroutine(RoundEnding());

        // Check if a player wins the game
        if (currentWinner.WinCount >= 2)
        {
            StartCoroutine(GameEndCoroutine());
        }
        else
        {
            // Clear the winner from the previous round
            currentWinner = null;

            StartCoroutine(GameLoop());
        }
    }

    private IEnumerator RoundStarting()
    {
        // As soon as the round starts reset the robots
        ResetRobots();

        // Reset all cameras to the players
        //SetCameraOnActiveTanks();

        // Set time count
        timeText.text = ((int)roundTime).ToString();

        // Increment the round number
        roundNumber++;

        // Display round text and wait to show it
        messageText.text = "Round " + roundNumber;
        yield return roundTextTimer;

        // Display fight text and wait to show it
        messageText.text = "Fight!";
        yield return roundFightTimer;
    }

    private IEnumerator RoundPlaying()
    {
        // Reset flag checks
        roundTimeComplete = false;
        hasWinner = false;

        // Unpause game
        IsPaused = false;

        // Clean text message
        messageText.text = string.Empty;

        // Start timer coroutine
        var coroutine = StartCoroutine(TimeCounter());

        while (!hasWinner && !roundTimeComplete)
        {
            yield return null;
        }

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    private IEnumerator RoundEnding()
    {
        // Pause game
        IsPaused = true;

        // Show round winner
        messageText.text = currentWinner.playerName + " wins";

        // Update HUD win counter
        foreach (var player in playerList)
        {
            player.playerHUD.UpdateWinCounter(player.WinCount);
        }

        // Wait for the specified length of time until yielding control back to the game loop
        yield return roundEndRoundTextTimer;
    }

    private IEnumerator GameEndCoroutine()
    {
        // Debug
        print("Game finished!");

        // Start Fade in
        yield return StartCoroutine(blackPanelController.FadeIn(0, endGameFadeValue, endGameFadeTime));

        endGamePanelController.LoadEndGamePanel(currentWinner.playerName);
    }

    IEnumerator TimeCounter()
    {
        var oneSecondTimer = new WaitForSecondsRealtime(1f);

        for (int i = 0; i < roundTime + 1; i++)
        {
            timeText.text = ((int)roundTime - i).ToString();
            yield return oneSecondTimer;
        }

        roundTimeComplete = true;

        CheckMaxPlayerHealth();
    }

    #endregion

    #region Auxiliar methods

    private void ResetRobots()
    {
        foreach (var player in playerList)
        {
            player.ResetRobot();
        }
    }

    #endregion

    private void PlayerDefeated(Robot_Controller defeatedPlayer)
    {
        print("Player " + name + " defeated.");

        Robot_Controller winner = CheckWinner();

        if (winner != null)
        {
            currentWinner = winner;
            hasWinner = true;
        }
    }

    private Robot_Controller CheckWinner()
    {
        int aliveCounter = 0;
        Robot_Controller winner = default;

        foreach (var player in playerList)
        {
            if (!player.IsDefeated)
            {
                winner = player;
                aliveCounter++;
            }
        }

        if (aliveCounter == 1)
        {
            winner.WinCount++;

            return winner;
        }

        return null;
    }

    private void CheckMaxPlayerHealth()
    {
        Robot_Controller currMaxHealthRobot = default;
        float currMaxHealth = 0;

        foreach (var player in playerList)
        {
            if (player.Health > currMaxHealth)
            {
                currMaxHealth = player.Health;
                currMaxHealthRobot = player;
            }
        }

        currentWinner = currMaxHealthRobot;

        currentWinner.WinCount++;
    }
}
