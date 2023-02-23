using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : SingletonMonoBehaviour<MonsterManager>
{
    [SerializeField]
    MonsterAI[] monsters;
    Sprite[] dieSprites;
    Dictionary<string, Sprite> monsterDieSprDic = new Dictionary<string, Sprite>();

    public Sprite GetMonsterDieSprite(string monsterName)
    {
        return monsterDieSprDic[monsterName];
    }
    // Start is called before the first frame update
    protected override void OnStart()
    {
        monsters = transform.GetComponentsInChildren<MonsterAI>();
        dieSprites = Resources.LoadAll<Sprite>("Monster/MonsterDieSprites");
        
        int length = dieSprites.Length;
        
        for (int i = 0; i < length; i++)
        {
            monsterDieSprDic.Add(dieSprites[i].name, dieSprites[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var monster in monsters)
        {
            if (monster.IsOutsideTheScreen())
            {
                monster.SetActiveToFalse();
            }
            
            else if (monster.IsCanMove())
            {
                monster.Move();
            }
        }
    }
}
