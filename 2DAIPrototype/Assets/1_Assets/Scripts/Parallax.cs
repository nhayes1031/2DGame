using UnityEngine;

public class Parallax : MonoBehaviour {
    private float length, startpos;
    public GameObject cam;
    public SpriteRenderer sprite;
    public float parallaxEffect;

    private void Start()
    {
        length = sprite.bounds.size.x;
        startpos = transform.position.x;
    }

    private void FixedUpdate()
    {
        float distanceRelativeToCamera = (cam.transform.position.x * (1 - parallaxEffect));
        float distanceFromOrigin = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + distanceFromOrigin - (length * 2), transform.position.y, transform.position.z);
        if (distanceRelativeToCamera > startpos + length)
        {
            startpos += length;
        }
        else if (distanceRelativeToCamera < startpos - length)
        {
            startpos -= length;
        }
    }
}
