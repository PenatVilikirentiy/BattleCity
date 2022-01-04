using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapDestroyer : MonoBehaviour
{
    [SerializeField]
    private AudioSource explosionSound;

    [SerializeField]
    private AudioSource wallDestroySound;

    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private Tile destroyedBaseTile;

    [SerializeField]
    private GameObject explosionFX;

    public static TilemapDestroyer Instance { get; private set; }

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Instance = this;
            //Destroy(gameObject);
        }
        
    }

    public void DestroyTiles(Vector3 position, float area, Direction direction, bool canDestrouSteel)
    {
        Vector3Int tilePos = Vector3Int.zero;
        for (float i = -0.5f; i < 0.75f; i += 0.25f)
        {
            if(direction == Direction.Right)
            {
                tilePos = tilemap.WorldToCell(position + new Vector3(0, i, 0));
            }
            else if(direction == Direction.Up)
            {
                tilePos = tilemap.WorldToCell(position + new Vector3(i, 0, 0));
            }

            TileBase tile = tilemap.GetTile(tilePos);
            if (tile)
            {
                if (tile.name == "Base" || tile.name == "Base 1")
                {
                    tilemap.SetTile(new Vector3Int(16, -21, 0), destroyedBaseTile);
                    var explosionPosition = tilemap.CellToWorld(new Vector3Int(18, -19, 0));
                    var explosionGO = Instantiate(explosionFX, explosionPosition, Quaternion.identity);
                    var sound = Instantiate(explosionSound);
                    Destroy(sound, 0.5f);
                    Destroy(explosionGO, .6f);
                    GameManager.Instance.ChangeState(State.Lose);                    
                }

                if (tile.name == "Steel" && !canDestrouSteel || tile.name == "Forest" || tile.name == "Water") return;

                DestroyTile(tilePos);
                wallDestroySound.Play();
            }
        }
    }

    public void DestroyTile(Vector3Int tilePos)
    {
        tilemap.SetTile(tilePos, null);
    }
}
