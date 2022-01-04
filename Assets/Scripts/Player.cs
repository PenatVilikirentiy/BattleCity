using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TankLevel { level1, level2, level3, level4}

public class Player : MonoBehaviour
{
    public TankLevel currentTankLevel = TankLevel.level1;

    public float bulletSpeed = 5f;
    public int damageAmount = 1;
    public float area = 0.8f;

    public bool canDestroySteel = false;

    //[SerializeField]
    //private Tilemap tilemap;

    private AudioSource shotSound;
    private AudioSource idleSound;
    private AudioSource movementSound;

    [SerializeField] 
    private float speed = 2f;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Vector2 input = new Vector2();

    [SerializeField]
    private Bullet bulletPrefab;

    [SerializeField]
    private Transform bulletCreator;

    [SerializeField]
    private Animator anim;

    //private Vector3 minScreenBounds;
    //private Vector3 maxScreenBounds;

    private void Awake()
    {
        shotSound = FindObjectsOfType<AudioSource>().Where(s => s.name == "Shot").First();
        idleSound = FindObjectsOfType<AudioSource>().Where(s => s.name == "Idle").First();
        movementSound = FindObjectsOfType<AudioSource>().Where(s => s.name == "Movement").First();
        //tilemap = FindObjectsOfType<Tilemap>().Where(t => t.name == "Stage1_Tilemap").First();
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var bullet = Instantiate(bulletPrefab, bulletCreator.position, transform.rotation);
            shotSound.Play();
            Destroy(bullet, 5f);
        }

        switch (input.x)
        {
            case 1:
                transform.eulerAngles = new Vector3(0f, 0f, -90f);
                break;

            case -1:
                transform.eulerAngles = new Vector3(0f, 0f, 90f);
                break;
        }

        switch(input.y)
        {
            case 1:
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                break;

            case -1:
                transform.eulerAngles = new Vector3(0f, 0f, -180f);
                break;
        }

        //transform.position = tilemap.CellToWorld(tilemap.WorldToCell(transform.position));
    }

    private void FixedUpdate()
    {
        int isDiagonal = input.x * input.y != 0 ? 0 : 1;
        var velocityX = input * speed * isDiagonal;
        rb.velocity = velocityX;

        if(rb.velocity != Vector2.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if (velocityX == Vector2.zero)
        {
            if (!idleSound.isPlaying)
            {
                movementSound.Stop();
                idleSound.Play();
            }
            
        }
        else
        {
            if (!movementSound.isPlaying)
            {
                idleSound.Stop();
                movementSound.Play();
            }            
        }
        
    }

    public void StopAllSounds()
    {
        if (idleSound.isPlaying) idleSound.Stop();
        if (movementSound.isPlaying) movementSound.Stop();
    }

    private void RestrictMovementOutOfScreen()
    {
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minScreenBounds.x + .5f, maxScreenBounds.x - .5f),
                                         Mathf.Clamp(transform.position.y, minScreenBounds.y + .5f, maxScreenBounds.y - .5f),
                                         transform.position.z);
    }

    public void ChangeTankLevel()
    {
        switch (currentTankLevel)
        {
            case TankLevel.level1: Level1();
                break;
            case TankLevel.level2: Level2();
                break;
            case TankLevel.level3: level3();
                break;
            case TankLevel.level4: Level4();
                break;
        }
    }

    private void Level1()
    {
        speed = 2f;
        bulletSpeed = 5f;
        area = 0.8f;
        damageAmount = 1;
        canDestroySteel = false;
    }

    private void Level2()
    {
        speed = 3f;
        bulletSpeed = 7f;
    }

    private void level3()
    {
        speed = 4f;
        bulletSpeed = 10f;
        canDestroySteel = true;
    }

    private void Level4()
    {
        speed = 6f;
        bulletSpeed = 12f;
    }
}