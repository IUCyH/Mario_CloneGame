using UnityEngine;

namespace Monster.Monsters
{
    public class Goomba : MonsterAI
    {
        [SerializeField]
        Collider2D monsterColl;
        [SerializeField]
        Rigidbody2D monsterRB;
        [SerializeField]
        float speed;

        protected override void SetMonster()
        {
            base.id = 00;
        }

        public override void Move()
        {
            base.monster.position += speed * Time.deltaTime * Vector3.left;
        }

        protected override void AdditionalActionsWhenGotDamage()
        {
            StopPhysics();
            Invoke(nameof(base.SetActiveToFalse), MonsterManager.Instance.MonsterDisableTime);
        }

        void StopPhysics()
        {
            monsterRB.bodyType = RigidbodyType2D.Kinematic;  
            monsterColl.enabled = false;
        }

        protected override void OnStart()
        {
            monsterRB = GetComponent<Rigidbody2D>();
            monsterColl = GetComponent<Collider2D>();
        }
    }
}
