using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    private void Update()
    {
        Vector3 pos = transform.position;
        pos.y = 0f;

        transform.position = pos;
    }
}
