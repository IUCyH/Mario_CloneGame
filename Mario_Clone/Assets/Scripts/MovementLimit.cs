using UnityEngine;

public class MovementLimit : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    float rayCastDistance;
    int boundaryLayer;
    
    public bool IsPlayerCannotMove(float dir)
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(player.position, Vector2.right * dir, rayCastDistance, boundaryLayer);

        if (!ReferenceEquals(hitInfo.collider, null))
        {
            return true;
        }

        return false;
    }

    void Start()
    {
        boundaryLayer = 1 << LayerMask.NameToLayer("Boundary");
    }
}
