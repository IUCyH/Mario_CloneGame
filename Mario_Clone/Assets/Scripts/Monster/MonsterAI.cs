using System;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    Transform monster;
    float speed;
    
    void Start()
    {
        monster = transform;
    }

    void Update()
    {
        var dir = Vector3.left;
        monster.position += speed * Time.deltaTime * dir;
    }
}
