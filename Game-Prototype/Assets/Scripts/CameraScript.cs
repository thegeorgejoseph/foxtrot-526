using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public Vector3 Camera_Offset;
    public GameObject Enemy;
    public GameObject SpaceText;

    private Animation camAni;

    void Start()
    {
        SpaceText.SetActive(false);
        // Default camera offset
        Camera_Offset = new Vector3(0, 0, -1f);

        // Play starting animation
        camAni = gameObject.GetComponent<Animation>();
        camAni.Play();
        // Stop Enemy from moving before the animation has finished
        Enemy.SetActive(false);
        Debug.Log("Length of Clip = " + Mathf.Ceil(camAni.clip.length));
        StartCoroutine(CountDown((int)Mathf.Ceil(camAni.clip.length)));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + Camera_Offset.x, player.position.y + Camera_Offset.y, Camera_Offset.z);
    }


    // Coroutine for Stoping enemies from moving
    private IEnumerator CountDown(int duration)
    {
        yield return new WaitForSeconds(duration);
        Enemy.SetActive(true);
    }
}
