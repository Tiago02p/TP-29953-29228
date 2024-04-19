using UnityEngine;

public class FollowBall : MonoBehaviour
{
    public GameObject bola;

    void Update()
    {
        if (bola != null)
        {
            transform.position = bola.transform.position;
        }
    }
}
