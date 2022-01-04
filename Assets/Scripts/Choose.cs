using UnityEngine;
using UnityEngine.SceneManagement;

public class Choose : MonoBehaviour
{
    [SerializeField]
    private Transform tank;

    [SerializeField]
    private float offset = .5f;

    [SerializeField]
    private Animator anim;

    private int currentState = 0;

    private void Awake()
    {
        anim.SetBool("isMoving", true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentState++;
            if (currentState > 2)
            {
                currentState = 2;
                return;
            }

            tank.transform.position = new Vector3(tank.transform.position.x, tank.transform.position.y - offset, tank.transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            currentState--;
            if (currentState < 0)
            {
                currentState = 0;
                return;
            }
            tank.transform.position = new Vector3(tank.transform.position.x, tank.transform.position.y + offset, tank.transform.position.z);
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("enter") || Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Stage1");
        }
    }
}
