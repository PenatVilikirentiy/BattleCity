using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform bulletCreator;

    [SerializeField]
    private Transform obstacleCheck;

    [SerializeField]
    private float radiusCheck = 0.2f;

    [SerializeField]
    private LayerMask obstackle;

    private float secondsLeft;
    private float timeToShoot;

    public float speed = 2f;
    public float secondsToRandom = 3f;


    private void Awake()
    {
        timeToShoot = Random.Range(1f, 1.5f);
        secondsLeft = secondsToRandom;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(secondsLeft > 0f)
        {
            secondsLeft -= Time.deltaTime;
        }
        else if(secondsLeft <= 0 || Physics.CheckSphere(obstacleCheck.position, radiusCheck, obstackle))
        {
            RandomDirection();
            secondsLeft = secondsToRandom;
        }

        if(timeToShoot > 0)
        {
            timeToShoot -= Time.deltaTime;
        }
        else
        {
            Shoot();
            timeToShoot = Random.Range(1f, 1.5f);
        }       

        rb.velocity = direction * speed;
        RestrictMovementOutOfScreen();
    }

    private void RandomDirection()
    {
        float xDir = Random.Range(-1f, 1f);
        float yDir = Random.Range(-1f, 1f);
        if(xDir > yDir)
        {
            yDir = 0f;
            xDir = Mathf.Round(Random.Range(-1f, 1f));
            if(xDir == 0)
            {
                xDir = Mathf.Round(Random.Range(-1f, 1f));
            }
        }
        else if(yDir > xDir)
        {
            xDir = 0f;
            yDir = Mathf.Round(Random.Range(-1f, 1f));
            if(yDir == 0)
            {
                yDir = Mathf.Round(Random.Range(-1f, 1f));
            }
        }
        else
        {
            xDir = 0f;
            yDir = -1f;
        }
        direction = new Vector2(xDir, yDir);

        float angle = 0;
        if(direction.x > 0)
        {
            angle = -90;
        }
        else if(direction.x < 0)
        {
            angle = 90;
        }

        if(direction.y > 0)
        {
            angle = 0;
        }
        else if(direction.y < 0)
        {
            angle = 180;
        }
        transform.eulerAngles = new Vector3(transform.localScale.x, transform.localScale.y, angle);
    }

    private void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, bulletCreator.position, transform.rotation);
        Destroy(bullet, 5f);
    }

    private void RestrictMovementOutOfScreen()
    {
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minScreenBounds.x + .5f, maxScreenBounds.x - .5f),
                                         Mathf.Clamp(transform.position.y, minScreenBounds.y + .5f, maxScreenBounds.y - .5f),
                                         transform.position.z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(obstacleCheck.position, radiusCheck);
    }
}
