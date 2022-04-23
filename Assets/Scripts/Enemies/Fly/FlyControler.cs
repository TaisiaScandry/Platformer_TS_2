using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyControler : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    float velocity = 2f;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(point1.position.x, point1.position.y, point1.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, point1.position, velocity * Time.deltaTime);
        if (transform.position == point1.position)
        {
            Transform tmp_point = point1;
            point1 = point2;
            point2 = tmp_point;
        }
    }
}
