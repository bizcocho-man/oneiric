using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Enemy : MonoBehaviour {

    public float speed;
    public float dstY;
    public float dstX;
    public float timeToStart;

    private Vector3 dest;
    private Vector3 start;
    private float fraction = 0f;
    private bool hasStarted;

    private Vector3 startPosition;

    private void Start()
    {
        start = transform.position;
        startPosition = start;
        dest = new Vector3(transform.position.x + dstX, transform.position.y + dstY, transform.position.z);
        StartCoroutine(StartDelayed());
    }

    private void Update()
    {
        if (!hasStarted || UI_Manager.Instance.isPausedGame)
        {
            return;
        }

        if (fraction < 1)
        {
            fraction += Time.deltaTime * speed;
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

    private IEnumerator StartDelayed()
    {
        yield return new WaitForSeconds(timeToStart);
        hasStarted = true;
    }

    public void Restart()
    {
        fraction = 0f;
        transform.position = startPosition;
        start = startPosition;
        dest = new Vector3(transform.position.x + dstX, transform.position.y + dstY, transform.position.z);

        StartDelayed();
    }
}
