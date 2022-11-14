using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerup_Freeze : MonoBehaviour
{
    // Attach the "Enemy" object to this
    public GameObject Enemies;
    // Freezing time for enemies
    private float freezeTime;

    private bool droppingEnabled;
    public GameObject FreezeIcon;

    [SerializeField] private AudioSource freezeSoundEffect;

    public void setFreezeTime(float newTime)
    {
        freezeTime = newTime;
    }

    // Public method for calling to freeze enemy from other scripts
    public void freezeEnemy()
    {
        for (int i = 0; i < Enemies.transform.childCount; i++)
        {
            Transform curEnemy = Enemies.transform.GetChild(i);
            curEnemy.gameObject.GetComponent<Enemy_Movement_Script_Basic>().enabled = false;
        }
        CountDown(freezeTime);
    }

    void unFreezeEnemy()
    {
        for (int i = 0; i < Enemies.transform.childCount; i++)
        {
            Transform curEnemy = Enemies.transform.GetChild(i);
            curEnemy.gameObject.GetComponent<Enemy_Movement_Script_Basic>().enabled = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Default freezing time
        freezeTime = 10f;
        droppingEnabled = true; // enabled by default
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Coroutine for Enemy freezing time
    private IEnumerator CountDown(float duration)
    {
        yield return new WaitForSeconds(duration);
        unFreezeEnemy();
        FreezeIcon.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1);
        changeDroppingStatus(true);
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

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Using tags to check if the player has actually met enemy
        if (collider.gameObject.tag == "FreezePowerup")
        {
            collider.gameObject.SetActive(false);
            FreezeIcon.GetComponent<Image>().color = Color.white;
            freezeEnemy();
            changeDroppingStatus(false);
            StartCoroutine(CountDown(freezeTime));
        }

    }
}
