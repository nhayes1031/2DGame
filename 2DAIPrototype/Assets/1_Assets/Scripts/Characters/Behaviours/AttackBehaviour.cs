using Platformer.Scripts.Characters.Behaviours;
using System.Collections;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    public float cooldown = 1f;

    private bool isAttacking = false;

    private Animator anim;
    private IController ctrl;

    private void Start()
    {
        ctrl = gameObject.GetComponentInChildren<IController>();
        if (ctrl == null)
        {
            Debug.LogError("Interface not found!");
            return;
        }

        ctrl.OnAttack += HandleOnAttack;

        anim = gameObject.GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        if (ctrl == null)
        {
            return;
        }

        ctrl.OnAttack -= HandleOnAttack;
    }

    private void HandleOnAttack()
    {
        if (!isAttacking) anim.SetTrigger("Attack");
        StartCoroutine(Attacking());
    }

    IEnumerator Attacking()
    {
        isAttacking = true;
        yield return new WaitForSeconds(cooldown);
        isAttacking = false;
    }
}
