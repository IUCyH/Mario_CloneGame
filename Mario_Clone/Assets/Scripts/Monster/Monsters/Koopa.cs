using System;
using UnityEngine;

namespace Monster.Monsters
{
    public class Koopa : MonsterAI
    {
        [SerializeField]
        float speed;
        [SerializeField]
        float rollingSpeed;
        bool moveFast;

        protected override void SetMonster()
        {
            base.id = 01;
        }

        protected override void ChangeToOppositeDir()
        {
            speed *= -1;
        }

        protected override void AdditionalActionsWhenGotDamage()
        {
            gameObject.tag = "Shell";
        }

        protected override void AdditionalActionWhenCollided()
        {
            if (CompareTag("Shell"))
            {
                Debug.Log("It is shell");
                moveFast = true;
            }
        }

        public override void Move()
        {
            base.monster.position += speed * Time.deltaTime * Vector3.left;
        }
        
        void MoveFaster()
        {
            monster.position += rollingSpeed * Time.deltaTime * Vector3.right;
        }

        void Update()
        {
            if (moveFast)
            {
                MoveFaster();
            }
        }
    }
}
