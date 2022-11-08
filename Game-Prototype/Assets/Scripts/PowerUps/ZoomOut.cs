using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOutScript : MonoBehaviour
{
    public Camera camera;
    public static int pauseTime;

    void Start()
    {
        
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        pauseTime = 10;
        StartCoroutine(CountDown(pauseTime));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator CountDown(int duration)
    {
        float currZoom = camera.orthographicSize;
        Debug.Log("Initial Camera object " + camera.orthographicSize);
        camera.orthographicSize = currZoom + 6.0f;
        Debug.Log("Changed Camera object " + camera.orthographicSize);
        yield return new WaitForSeconds(duration);
        camera.orthographicSize = currZoom;
        Debug.Log("restore Camera object " + camera.orthographicSize);
    }
}
