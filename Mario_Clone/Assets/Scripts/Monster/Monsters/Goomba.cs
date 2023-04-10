using UnityEngine;

namespace Monster.Monsters
{
    public class Goomba : MonsterAI
    {
        [SerializeField]
        float speed;
        int dieMonsterLayer;

        protected override void SetMonster()
        {
            base.id = 00;
            base.moveSpeed = speed;
            
            dieMonsterLayer = LayerMask.NameToLayer("DieMonster");
        }

        public override void Move()
        {
            base.monster.position += moveSpeed * Time.deltaTime * Vector3.left;
        }

        protected override void AdditionalActionsWhenGotDamage()
        {
            gameObject.layer = dieMonsterLayer;
            Invoke(nameof(HideSprite), MonsterManager.Instance.MonsterDisableTime);
        }

        void HideSprite()
        {
            monsterSprRenderer.sprite = null;
        }
    }
}
