using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Powerup_Zoom : MonoBehaviour
{
    public Camera camera;
    public static int pauseTime;
    public GameObject gameObject;
    private Dictionary<string, float> scalingFactor = new Dictionary<string, float>
    {
        { "Level_1", 4.0f },
        { "Level_2", 3.5f },
        { "Level_3", 3.0f },
        { "Level_4", 3.0f },
        { "Level_5", 4.0f }
    };

    // dropping status
    private bool droppingEnabled;
    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        pauseTime = 10;   
        droppingEnabled = true; // enabled by default
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public getter for enemy_battle_script
    public bool getDroppingStatus()
    {
        return droppingEnabled;
    }

    // function to change dropping status
    public void changeDroppingStatus(bool newStatus)
    {
        droppingEnabled = newStatus;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Powerup!");
        if (collision.gameObject.tag == "ZoomPowerup")
        {
            collision.gameObject.SetActive(false);
            gameObject.GetComponent<Image>().color = Color.white;
            changeDroppingStatus(false);
            StartCoroutine(CountDown(pauseTime));
        }
    }
    

    private IEnumerator CountDown(int duration)
    {
        float currZoom = camera.orthographicSize;
        Debug.Log("Initial Camera object " + camera.orthographicSize);
        camera.orthographicSize = currZoom + scalingFactor[SceneManager.GetActiveScene().name];
        Debug.Log("Changed Camera object " + camera.orthographicSize);
        yield return new WaitForSeconds(duration);
        camera.orthographicSize = currZoom;
        Debug.Log("restore Camera object " + camera.orthographicSize);
        gameObject.GetComponent<Image>().color = Color.grey;
        changeDroppingStatus(true);
    }
}
