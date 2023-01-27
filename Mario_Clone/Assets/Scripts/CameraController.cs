using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    Vector3 nextPosition;
    [SerializeField]
    Vector3 mapEndPosition;

    void Move()
    {
        if (IsCanMove())
        {
            nextPosition.x = player.transform.position.x;
            transform.position = nextPosition;
        }
    }

    bool IsCanMove()
    {
        if (player.transform.position.x < transform.position.x || transform.position.x > mapEndPosition.x)
        {
            return false;
        }

        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        nextPosition = new Vector3(0f, 0f, -10f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Move();
    }
}
