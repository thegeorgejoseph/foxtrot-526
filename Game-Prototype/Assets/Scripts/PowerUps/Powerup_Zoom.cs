using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Powerup_Zoom : MonoBehaviour
{
    public Camera myCamera;
    public static int pauseTime;
    public GameObject zoomoutIcon;
    private Dictionary<string, float> scalingFactor = new Dictionary<string, float>
    {
        { "Level_1", 4.0f },
        { "Level_2", 3.5f },
        { "Level_3", 3.0f },
        { "Level_4", 3.0f },
        { "Level_5", 4.0f },
        { "Level_6", 7.0f },
        { "Level_7", 4.0f },
        { "Level_8", 3.0f }
    };

    [SerializeField] private AudioSource zoomSoundEffect;

    // dropping status
    private bool droppingEnabled;
    void Start()
    {
        myCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
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
            zoomoutIcon.GetComponent<Image>().color = Color.white;
            changeDroppingStatus(false);
            zoomSoundEffect.Play();
            StartCoroutine(CountDown(pauseTime));
        }
    }
    

    private IEnumerator CountDown(int duration)
    {
        float currZoom = myCamera.orthographicSize;
        Debug.Log("Initial Camera object " + myCamera.orthographicSize);
        myCamera.orthographicSize = currZoom + scalingFactor[SceneManager.GetActiveScene().name];
        Debug.Log("Changed Camera object " + myCamera.orthographicSize);
        yield return new WaitForSeconds(duration);
        myCamera.orthographicSize = currZoom;
        Debug.Log("restore Camera object " + myCamera.orthographicSize);
        zoomoutIcon.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);
        changeDroppingStatus(true);
    }
}
