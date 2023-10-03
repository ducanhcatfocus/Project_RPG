using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parallaxEffect;

    private float xPos;

    private void Start()
    {
        cam = GameObject.Find("Main Camera");
        xPos = transform.position.x;
        
    }

    private void FixedUpdate()
    {
        float distanceToMove = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(xPos + distanceToMove, transform.position.y);
    }
}
