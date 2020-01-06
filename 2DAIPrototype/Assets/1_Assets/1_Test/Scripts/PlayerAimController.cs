using System.Collections;
using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    private Animator animator;
    private int weaponLayer;

    public bool isFiring = false;
    [SerializeField] private GameObject claw;
    private int aimAngle = 0;

    #region AnimationNames
    private readonly static int negNinety = Animator.StringToHash("Neg90");
    private readonly static int negFourtyFive = Animator.StringToHash("Neg45");
    private readonly static int zero = Animator.StringToHash("0");
    private readonly static int fourtyFive = Animator.StringToHash("45");
    private readonly static int ninety = Animator.StringToHash("90");
    private readonly static int firing = Animator.StringToHash("Firing");
    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
        weaponLayer = animator.GetLayerIndex("Weapon Layer");
    }

    private void Update()
    {
        Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        FlipSprite(mouseScreenPosition);
        Aim(mouseScreenPosition);
        Attack();
    }

    private void Aim(Vector2 msp)
    {
        if (isFiring) return;

        float rotationZ = Mathf.Rad2Deg * Mathf.Atan2(Mathf.Abs(msp.y - transform.position.y), Mathf.Abs(msp.x - transform.position.x));
        float angle = rotationZ * (msp.y >= transform.position.y ? 1 : -1);

        if (angle < 22.5 && angle > -22.5)
            aimAngle = 0;
        else if (angle > 22.5 && angle < 67.5)
            aimAngle = 45;
        else if (angle > 67.5)
            aimAngle = 90;
        else if (angle < -22.5 && angle > -67.5)
            aimAngle = -45;
        else if (angle < -67.5)
            aimAngle = -90;

        switch (aimAngle)
        {
            case 0:
                animator.Play(zero, weaponLayer);
                break;
            case -90:
                animator.Play(negNinety, weaponLayer);
                break;
            case -45:
                animator.Play(negFourtyFive, weaponLayer);
                break;
            case 90:
                animator.Play(ninety, weaponLayer);
                break;
            case 45:
                animator.Play(fourtyFive, weaponLayer);
                break;
        }
    }

    private void FlipSprite(Vector2 msp)
    {
        if (msp.x < transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Left Click") && !isFiring)
        {
            Instantiate(claw, transform.position, Quaternion.Euler(0, transform.localRotation.y * 180, aimAngle));
            isFiring = true;
            animator.Play(firing, weaponLayer);
        }
    }
}
