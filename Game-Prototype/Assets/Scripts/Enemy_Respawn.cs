using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Respawn : MonoBehaviour
{  
    
    private Queue<GameObject> enemyQ; // Queue to hold all enemies that need to respawn
    
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
        curEnemy.SetActive(false);
        enemyQ.Enqueue(curEnemy); // Push this enemy into the queue
        // Respawn the enemy, current respawn time: 5 sec
        StartCoroutine(Respawn(5)); // Start coroutine that respawns the enemy
    }

    // Coroutine for Enemy's respawn
    private IEnumerator Respawn(int duration)
    {
        yield return new WaitForSeconds(duration);
        enemyQ.Dequeue().SetActive(true); // Enable the enemy after x seconds
    }

}
