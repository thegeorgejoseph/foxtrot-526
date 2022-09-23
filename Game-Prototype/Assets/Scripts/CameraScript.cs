using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public Vector3 Camera_Offset;

    void Start()
    {
        // Default camera offset
        Camera_Offset = new Vector3(0, 0, -1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + Camera_Offset.x, player.position.y + Camera_Offset.y, Camera_Offset.z);
    }
}
