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
            dieMonsterLayer = LayerMask.NameToLayer("DieMonster");
        }
        
        protected override void ChangeToOppositeDir()
        {
            speed *= -1;
        }

        public override void Move()
        {
            base.monster.position += speed * Time.deltaTime * Vector3.left;
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
