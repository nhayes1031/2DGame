using Platformer.Scripts.CameraManagement;
using System;
using UnityEngine;

namespace Platformer.Scripts.Abilities.Mindcontrol
{
    public class Mindcontrol : MonoBehaviour
    {
        [SerializeField]
        private float speed = 20f;

        private Rigidbody2D rb;
        public Action<string> iHitSomething = delegate { };

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void SetDirection(Vector2 dir)
        {
            rb.velocity = dir * speed;
        }

        void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.tag != "Player")
            {
                iHitSomething(coll.tag);
                if (coll.tag == "Controllable")
                {
                    IControllable controller = coll.gameObject.GetComponent<IControllable>();
                    controller.Possessed();

                    GameObject camera = GameObject.Find("CameraManager");
                    CameraManager cm = camera.GetComponent<CameraManager>();
                    cm.Follow(coll.gameObject.transform);
                }
                Destroy(gameObject);
            }
        }
    }
}
