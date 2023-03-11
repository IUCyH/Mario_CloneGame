using System;
using System.Collections;
using System.Collections.Generic;
using Monster.Monsters;
using UnityEngine;

public class Koopa_Rolling : MonoBehaviour
{
    [SerializeField]
    Koopa koopa;
    bool rolling;

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player")) 
            rolling = true;
    }

    void Update()
    {
        if (rolling)
        {
            koopa.MoveFaster();
        }
    }
}
