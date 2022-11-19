using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class LevelSelectorCameraScript : MonoBehaviour
{
    public Vector3 Camera_Offset;
   
    public GameObject Player;

    private Animation camAni;
    public static int level_num = 1;

    void Start()
    {
        // Default camera offset
        Camera_Offset = new Vector3(0, 0, -1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x + Camera_Offset.x, Player.transform.position.y + Camera_Offset.y, Camera_Offset.z);
    }

}
