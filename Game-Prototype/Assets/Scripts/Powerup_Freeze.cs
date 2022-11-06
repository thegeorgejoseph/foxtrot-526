using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Freeze : MonoBehaviour
{
    // Attach the "Enemy" object to this
    public GameObject Enemies;
    // Freezing time for enemies
    private float freezeTime;

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
        freezeTime = 5f;
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
    }
}
