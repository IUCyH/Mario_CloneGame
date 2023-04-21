using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera mainCam;
    [SerializeField]
    Transform cam;
    [SerializeField]
    Transform player;
    
    Vector3 nextPosition;
    float playerPositionX;
    
    [SerializeField]
    Vector3 mapEndPosition;

    [SerializeField]
    float playerAdditionalPosX;

    public bool IsInsideTheCamera(Transform obj)
    {
        var viewPort = mainCam.WorldToViewportPoint(obj.position);

        bool XisInside = viewPort.x >= 0 && viewPort.x <= 1;
        bool YisInside = viewPort.y >= 0 && viewPort.y <= 1;
        
        if (XisInside && YisInside)
        {
            return true;
        }

        return false;
    }

    void Move()
    {
        if (!IsCanMove()) return;
        
        nextPosition.x = playerPositionX;
        cam.position = nextPosition;
    }

    bool IsCanMove()
    {
        playerPositionX = player.position.x + playerAdditionalPosX;
        var camPos = cam.position;
        bool isCanMove = !(playerPositionX < camPos.x || camPos.x > mapEndPosition.x);
    
        return isCanMove;
    }

    // Start is called before the first frame update
    void Start()
    {
        nextPosition = new Vector3(0f, 0f, -10f);
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Move();
    }
}
