using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : SingletonMonoBehaviour<MonsterManager>
{
    [SerializeField]
    MonsterAI[] monsters;
    
    // Start is called before the first frame update
    protected override void OnStart()
    {
        monsters = transform.GetComponentsInChildren<MonsterAI>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var monster in monsters)
        {
            if (!monster.IsOutsideTheScreen() && monster.IsCanMove())
            {
                monster.Move();
            }
            
            else if (monster.IsOutsideTheScreen())
            {
                monster.SetCannotMove();
            }
        }
    }
}
