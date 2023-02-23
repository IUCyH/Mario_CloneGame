using System;
using UnityEngine;

public abstract class InteractionTrigger : MonoBehaviour
{
    protected abstract void Interact(Collider2D otherCollider); 
    
    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Interact(col);
    }
}
