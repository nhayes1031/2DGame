using UnityEngine;

namespace Platformer.Scripts.Combat
{
    public class Health : MonoBehaviour
    {
        public float HP;

        public void TakeDamage(float damage)
        {
            HP = Mathf.Max(HP - damage, 0);
        }

        public void Add(float heal)
        {
            HP = HP + heal;
        }

        public void Subtract(float damage)
        {
            HP = Mathf.Max(HP - damage, 0);
        }
    }
}
