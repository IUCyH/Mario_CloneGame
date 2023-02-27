using UnityEngine;

namespace Monster.Monsters
{
    public class Koopa : MonsterAI
    {
        [SerializeField]
        float speed;

        protected override void SetID()
        {
            base.id = 01;
        }

        public override void Move()
        {
            monster.position += speed * Time.deltaTime * Vector3.left;
        }
    }
}
