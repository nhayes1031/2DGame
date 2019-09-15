using UnityEngine;

namespace Platformer.Scripts.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private FloatVariable HP;
        [SerializeField] private bool resetHP;
        [SerializeField] private FloatReference startingHP;

        private void Start()
        {
            if (resetHP)
            {
                HP.SetValue(startingHP);
            }
        }

        public void TakeDamage(float damage)
        {
            HP.ApplyChange(Mathf.Max(HP.Value - damage, 0));
        }
    }
}
