using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Enemy : MonoBehaviour {

    public float speed;
    public float dstY;
    public float dstX;

    private Vector3 dest;
    private Vector3 start;
    private float fraction = 0f;

    private void Start()
    {
        start = transform.position;
        dest = new Vector3(transform.position.x + dstX, transform.position.y + dstY, transform.position.z);
    }

    private void FixedUpdate()
    {
        if (fraction < 1)
        {
            fraction += Time.fixedDeltaTime * speed;
            transform.position = Vector3.Lerp(start, dest, fraction);
        }
        else
        {
            Vector3 aux = start;
            start = dest;
            dest = aux;

            fraction = 0;
        }
    }
}
