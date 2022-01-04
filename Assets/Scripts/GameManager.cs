using System.Collections;
using UnityEngine;

public enum State { Spawn, PlayMode, Pause, Win, Lose, EndGame }

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPosition;

    [SerializeField]
    private GameObject pauseGO;

    [SerializeField]
    private GameObject portalGO;

    [SerializeField]
    private PlayerHealth playerGO;

    [SerializeField]
    private GameObject gameOverLabel;

    [SerializeField]
    private GameObject[] stages;

    [SerializeField]
    private GameObject endGameScreen;

    [SerializeField]
    private GameObject[] disableUI;

    private Player player;

    private int currentStage = 0;
    private int enemiesCount = 20;

    public string currentStageName = "Stage1_Tilemap";
    public static GameManager Instance { get; private set; }
    public int Player1Score = 0;
    public int lives = 3;

    public State State;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        player = playerGO.GetComponent<Player>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        ChangeState(State.Spawn);
    }

    public void ChangeState(State newState)
    {
        State = newState;

        switch (newState)
        {
            case State.Spawn: Spawn();
                break;
            case State.PlayMode: PlayMode();
                break;
            case State.Pause: Pause();
                break;
            case State.Win: Win();
                break;
            case State.Lose: Lose();
                break;
            case State.EndGame: EndGame();
                break;
        }
    }

    private void Spawn()
    {
        if(lives > 0)
        {
            StartCoroutine(WaitToRespawn());
        }
        else
        {
            ChangeState(State.Lose);
        }
    }

    private void PlayMode()
    {
    }

    private void Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            player.enabled = false;
            pauseGO.SetActive(true);
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            player.enabled = true;
            pauseGO.SetActive(false);
        }
    }

    private void Lose()
    {
        gameOverLabel.SetActive(true);
        player.StopAllSounds();
        player.enabled = false;
    }

    private void Win()
    {
        Debug.Log("Win");
        StartCoroutine(NextStage());
        ChangeState(State.PlayMode);
    }

    public void DecreaseEnemiesCount()
    {
        enemiesCount--;
        if(enemiesCount <= 0)
        {
            ChangeState(State.Win);
        }
    }

    private IEnumerator WaitToRespawn()
    {
        yield return new WaitForSeconds(1f);
        var playerHealth = Instantiate(playerGO, respawnPosition.position, Quaternion.identity);
        playerHealth.MakeInvulnerable(4);
        player = playerHealth.GetComponent<Player>();
        var portal = Instantiate(portalGO, respawnPosition.position, Quaternion.identity);
        Destroy(portal, 1f);
        ChangeState(State.PlayMode);        
    }

    private IEnumerator NextStage()
    {
        yield return new WaitForSeconds(2f);
        player.StopAllSounds();
        player.gameObject.SetActive(false);
        stages[currentStage].SetActive(false);
        currentStage++;

        if(currentStage > 4)
        {
            ChangeState(State.EndGame);
        }
        else
        {
            yield return new WaitForSeconds(1f);
            stages[currentStage].SetActive(true);
            currentStageName = stages[currentStage].name;
            enemiesCount = 20;
            player.gameObject.transform.position = respawnPosition.position;
            player.gameObject.SetActive(true);
            TilemapUIManager.Instance.ChangeStateNumbersUI();
            TilemapUIManager.Instance.ResetTanksUIAmount();
        }
    }

    private void EndGame()
    {
        foreach(var ui in disableUI)
        {
            ui.SetActive(false);
        }
        endGameScreen.SetActive(true);        
    }
}