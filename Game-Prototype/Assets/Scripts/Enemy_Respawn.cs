using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Respawn : MonoBehaviour
{  
    
    private Queue<GameObject> enemyQ; // Queue to hold all enemies that need to respawn
    private Color transColor; // Color for defeated enemy

    // Start is called before the first frame update
    void Start()
    {
        enemyQ = new Queue<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableEnemy(GameObject curEnemy)
    {
        
        curEnemy.SetActive(false); // Old respawn logic

        /*  BUG WARNING, Disabled before becoming stable
        // New respawn logic: Defeated enemy will keep moving and become transparent, but will not get into battle again until respawn has passed
        curEnemy.tag = "Defeated";
        transColor = curEnemy.GetComponent<SpriteRenderer>().color;
        transColor.a = 0.2f;
        curEnemy.GetComponent<SpriteRenderer>().color = transColor;
        */
        enemyQ.Enqueue(curEnemy); // Push this enemy into the queue
        // Respawn the enemy, current respawn time: 5 sec
        StartCoroutine(Respawn(Random.Range(10,16))); // Start coroutine that respawns the enemy
    }

    // Coroutine for Enemy's respawn
    private IEnumerator Respawn(int duration)
    {
        yield return new WaitForSeconds(duration);
        enemyQ.Dequeue().SetActive(true); // Enable the enemy after x seconds; Old respawn logic

        /*
        GameObject respawnedEnemy = enemyQ.Dequeue();
        // Reset the respawned enemy
        respawnedEnemy.tag = "Enemy";
        transColor.a = 1f;
        respawnedEnemy.GetComponent<SpriteRenderer>().color = transColor;
        */

    }

}
