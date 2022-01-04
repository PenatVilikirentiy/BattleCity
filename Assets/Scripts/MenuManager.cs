using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    public static MenuManager Instance;

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
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.ChangeState(State.PlayMode);
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            GameManager.Instance.ChangeState(State.Pause);
        }
    }
}
