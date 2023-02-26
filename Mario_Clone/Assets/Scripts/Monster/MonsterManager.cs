using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterManager : SingletonMonoBehaviour<MonsterManager>
{
    [SerializeField]
    List<MonsterAI> monsters;
    Dictionary<ushort, Sprite> monsterDieDictionary = new Dictionary<ushort, Sprite>();
    Sprite[] monsterDieSprites;
    [SerializeField]
    float monsterDisableTime;

    public float MonsterDisableTime
    {
        get { return monsterDisableTime; }
    }

    public Sprite GetMonsterDieSprite(ushort monsterID)
    {
        return monsterDieDictionary[monsterID];
    }

    public void RemoveMonsterFromList(MonsterAI monster)
    {
        monsters.Remove(monster);
    }
    // Start is called before the first frame update
    protected override void OnStart()
    {
        monsters = transform.GetComponentsInChildren<MonsterAI>().ToList();
        monsterDieSprites = Resources.LoadAll<Sprite>("Monster/MonsterDieSprites");
        
        int length = monsterDieSprites.Length;
        
        for (ushort i = 0; i < length; i++)
        {
            monsterDieDictionary.Add(i, monsterDieSprites[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i].IsOutsideTheScreen())
            {
                monsters[i].SetActiveToFalse();
                RemoveMonsterFromList(monsters[i]);
            }
            
            else if (monsters[i].IsCanMove())
            {
                monsters[i].Move();
            }
        }
    }
}
