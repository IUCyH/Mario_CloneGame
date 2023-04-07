using System;
using UnityEngine;

namespace Monster.Monsters
{
    public class Koopa : MonsterAI
    {
        [SerializeField]
        BoxCollider2D koopaCollider;
        [SerializeField]
        BoxCollider2D shellCollider;

        const string PlayerTag = "Player";
        const string MonsterTag = "Monster";
        
        [SerializeField]
        float speed;
        [SerializeField]
        float rollingSpeed;
        bool moveFast;

        protected override void SetMonster()
        {
            base.id = 01;
            shellCollider.enabled = false;
        }

        protected override void ChangeToOppositeDir()
        {
            speed *= -1;
            rollingSpeed *= -1;
        }

        protected override void AdditionalActionsWhenGotDamage()
        {
            var koopa = gameObject;
            
            koopa.tag = "Shell";
            koopa.layer = LayerMask.NameToLayer("Shell");

            shellCollider.enabled = true;
            koopaCollider.enabled = false;
        }

        protected override void AdditionalActionWhenCollided(Collision2D col)
        {
            if (currentState == MonsterState.Die)
            {
                var colTransform = col.transform;
                
                if (colTransform.CompareTag(MonsterTag))
                {
                    var monsterAI = colTransform.GetComponent<MonsterAI>();
                    if (monsterAI != null)
                    {
                        monsterAI.SetDie();
                    }
                }
                
                else if (colTransform.CompareTag(PlayerTag))
                {
                    var player = colTransform.GetComponent<PlayerController>();
                    if (player != null && moveFast == true)
                    {
                        player.SetDie();
                    }
                }

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
