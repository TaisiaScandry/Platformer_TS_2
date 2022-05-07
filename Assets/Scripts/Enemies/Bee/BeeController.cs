using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    public Transform[] points;
    float speed = 3f;
    float waitTime = 3f;
    bool canFly = true;
    public int pointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(points[pointIndex].position.x, points[pointIndex].position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        int nextPoint = pointIndex + 1;
        if (canFly)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[nextPoint].position, speed * Time.deltaTime);
            if (transform.position == points[pointIndex].position)
            {
                if (nextPoint < points.Length - 1)
                {
                    pointIndex++;
                } else
                {
                    pointIndex = 0;
                }
            }
        }
    }
}
