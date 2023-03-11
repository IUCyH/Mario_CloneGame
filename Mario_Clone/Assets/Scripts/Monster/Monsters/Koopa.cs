using UnityEngine;

namespace Monster.Monsters
{
    public class Koopa : MonsterAI
    {
        [SerializeField]
        Koopa_Rolling koopaRolling;
        [SerializeField]
        BoxCollider2D monsterCollider;
        [SerializeField]
        float speed;
        [SerializeField]
        float rollingSpeed;

        protected override void SetMonster()
        {
            base.id = 01;
            koopaRolling.SetActive(false);
        }

        protected override void AdditionalActionsWhenGotDamage()
        {
            monsterCollider.enabled = false;
            koopaRolling.SetActive(true);
        }

        public override void Move()
        {
            monster.position += speed * Time.deltaTime * Vector3.left;
        }
        
        public void MoveFaster()
        {
            monster.position += rollingSpeed * Time.deltaTime * Vector3.left;
        }
    }
}
