using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShovelBonus : MonoBehaviour
{
    private Tilemap tilemap;

    [SerializeField]
    private Tile steelTile;

    //[SerializeField]
    ///private Tile brickTile;

    [SerializeField]
    private Vector3Int[] steelTilePositions;

    [SerializeField]
    private Vector3Int[] brickTilePositions;

    private void Awake()
    {
        // TODO: make in GameManager field with the name of current level and pass it below
        tilemap = FindObjectsOfType<Tilemap>().Where(t => t.name == GameManager.Instance.currentStageName).First();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if(player)
        {
            Debug.Log(tilemap.name);
            ChangeBlocksOnSteel();
            Destroy(gameObject);
        }
    }

    private void ChangeBlocksOnSteel()
    {
        ClearTiles();
        foreach(var tilePos in steelTilePositions)
        {
            tilemap.SetTile(tilePos, steelTile);
        }        
    }

    //private void ChangeBlocksOnBrick()
    //{
    //    ClearTiles();
    //    foreach(var tilePos in brickTilePositions)
    //    {
    //        tilemap.SetTile(tilePos, brickTile);
    //    }
    //}

    private void ClearTiles()
    {
        foreach(var tilePos in brickTilePositions)
        {
            tilemap.SetTile(tilePos, null);
        }
    }
}
