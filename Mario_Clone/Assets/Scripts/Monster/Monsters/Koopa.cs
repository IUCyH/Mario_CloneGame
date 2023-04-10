using System;
using System.Collections;
using UnityEngine;

namespace Monster.Monsters
{
    public class Koopa : MonsterAI
    {
        Coroutine currCoroutine;
        
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

        IEnumerator Coroutine_MoveFaster()
        {
            moveSpeed = rollingSpeed;

            while (gameObject.activeSelf)
            {
                MoveFaster();
                
                yield return null;
            }
        }

        protected override void SetMonster()
        {
            base.id = 01;
            base.moveSpeed = speed;
            
            shellCollider.enabled = false;
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
                    if (player != null && currCoroutine != null)
                    {
                        player.SetDie();
                    }
                }

                if (currCoroutine == null)
                {
                    currCoroutine = StartCoroutine(Coroutine_MoveFaster());
                }
            }
        }

        public override void Move()
        {
            base.monster.position += moveSpeed * Time.deltaTime * Vector3.left;
        }
        
        void MoveFaster()
        {
            monster.position += moveSpeed * Time.deltaTime * Vector3.right;
        }
    }
}
