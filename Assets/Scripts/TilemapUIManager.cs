using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapUIManager : MonoBehaviour
{
    public static TilemapUIManager Instance;

    [SerializeField]
    private Tilemap tilemapUI;

    [SerializeField]
    private Tile grayTile;

    [SerializeField]
    private Tile tankUITile;

    [SerializeField]
    private Tile[] numbersTiles;

    [SerializeField]
    private Vector3Int[] tanksUIPositions;

    [SerializeField]
    private Vector3Int playerLivesPosition;

    [SerializeField]
    private Vector3Int[] stageNumbersPositions;

    private int currentTankUI = 0;
    private int playerLives = 2;
    private int currentStage1 = 0;
    private int currentStage2 = 1;

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

        ResetTanksUIAmount();
        SetPlayerLivesUI();
        SetStageNumbers();
    }

    public void ChangeTanksUIAmount()
    {
        tilemapUI.SetTile(tanksUIPositions[currentTankUI], grayTile);
        currentTankUI++;
    }

    public void ResetTanksUIAmount()
    {
        foreach(var pos in tanksUIPositions)
        {
            tilemapUI.SetTile(pos, tankUITile);
        }
        currentTankUI = 0;
    }

    public void SetPlayerLivesUI()
    {        
        tilemapUI.SetTile(playerLivesPosition, numbersTiles[playerLives]);
    }

    public void RemovePlayerLivesUI()
    {
        playerLives--;
        if (playerLives < 0) return;
        tilemapUI.SetTile(playerLivesPosition, numbersTiles[playerLives]);
    }

    public void AddPlayerLivesUI()
    {
        playerLives++;
        tilemapUI.SetTile(playerLivesPosition, numbersTiles[playerLives]);
    }

    public void ChangeStateNumbersUI()
    {
        currentStage2++;

        if (currentStage2 > 9)
        {
            currentStage2 = 0;
            currentStage1++;
        }
        tilemapUI.SetTile(stageNumbersPositions[0], numbersTiles[currentStage1]);
        tilemapUI.SetTile(stageNumbersPositions[1], numbersTiles[currentStage2]);
    }

    private void SetStageNumbers()
    {
        tilemapUI.SetTile(stageNumbersPositions[0], numbersTiles[currentStage1]);
        tilemapUI.SetTile(stageNumbersPositions[1], numbersTiles[currentStage2]);
    }
}
