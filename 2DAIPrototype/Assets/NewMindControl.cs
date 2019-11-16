using System.Collections;
using UnityEngine;

public class NewMindControl : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Controllable"))
        {
            Destroy(gameObject);
        }
    }
}
