using System;
using UnityEngine;

namespace Monster.Monsters
{
    public class Koopa : MonsterAI
    {
        enum KoopaState
        {
            None = -1,
            Default,
            Shell
        }

        [SerializeField]
        BoxCollider2D koopaCollider;
        [SerializeField]
        BoxCollider2D shellCollider;
        KoopaState koopaState;

        const string playerTag = "Player";
        const string monsterTag = "Monster";
        
        [SerializeField]
        float speed;
        [SerializeField]
        float rollingSpeed;
        bool moveFast;

        protected override void SetMonster()
        {
            base.id = 01;
            shellCollider.enabled = false;
            SetState(KoopaState.Default);
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

            SetState(KoopaState.Shell);
        }

        protected override void AdditionalActionWhenCollided(Collision2D col)
        {
            if (CompareState(KoopaState.Shell))
            {
                var colTransform = col.transform;
                
                if (colTransform.CompareTag(monsterTag))
                {
                    var monsterAI = colTransform.GetComponent<MonsterAI>();
                    if (monsterAI != null)
                    {
                        monsterAI.SetDie();
                    }
                }
                
                else if (colTransform.CompareTag(playerTag))
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

        void SetState(KoopaState state)
        {
            koopaState = state;
        }

        bool CompareState(KoopaState state)
        {
            return koopaState == state;
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
