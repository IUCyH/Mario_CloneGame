using UnityEngine;

public class PlayerIntoMap : MonoBehaviour
{
    Vector3 playerViewportPos;
    [SerializeField]
    Camera camera;
    
    [SerializeField]
    float maxPositionX = 0.97f;
    [SerializeField]
    float minPositionX = 0.033f;
    [SerializeField]
    float maxPositionY = 0.96f;
    [SerializeField]
    float minPositionY = 0f;

    public void PositionPlayerIntoMap()
    {
        playerViewportPos = camera.WorldToViewportPoint(transform.position);

        if (IsPlayerXIsOutside() || IsPlayerYIsOutside())
        {
            CorrectionPlayerX();
            CorrectionPlayerY();
            SetPlayerWorldPosition();
        }
    }
    
    bool IsPlayerXIsOutside()
    {
        if (playerViewportPos.x > maxPositionX || playerViewportPos.x < minPositionX)
        {
            return true;
        }

        return false;
    }
    
    bool IsPlayerYIsOutside()
    {
        if (playerViewportPos.y > maxPositionY || playerViewportPos.y < minPositionY)
        {
            return true;
        }

        return false;
    }
    
    void CorrectionPlayerX()
    {
        if (playerViewportPos.x > maxPositionX)
        {
            playerViewportPos.x = maxPositionX;
        }
        else if (playerViewportPos.x < minPositionX)
        {
            playerViewportPos.x = minPositionX;
        }
    }

    void CorrectionPlayerY()
    {
        if (playerViewportPos.y > maxPositionY)
        {
            playerViewportPos.y = maxPositionY;
        }
        else if (playerViewportPos.y < minPositionY)
        {
            playerViewportPos.y = minPositionY;
        }
    }
    
    void SetPlayerWorldPosition()
    {
        var worldPos = camera.ViewportToWorldPoint(playerViewportPos);
        
        transform.position = worldPos;
    }

    void Start()
    {
        camera = Camera.main;
    }
}
