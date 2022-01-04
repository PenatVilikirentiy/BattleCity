using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private GameObject startObjectPosition;

    public bool skip = true;
    public float followSpeed = 5f;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * followSpeed);

        if(Input.anyKey && skip)
        {
            transform.position = target.position;
        }

        if(transform.position == target.position)
        {
            startObjectPosition.SetActive(true);
        }
    }
}
