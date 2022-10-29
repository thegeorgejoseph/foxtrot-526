using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public GameObject wayPoints; // A set of waypoints to guide the enemy's movement
    public GameObject hitText; // Image to display when we hit the hitzoom
    public GameObject missText; // Image to display when we miss the hit zoomoom
    public Image currentEnemy; // Current Enemy that the player encountered
    public Image player; // Player Object to be displaied

    private float enemySpeed = 0.7f; // Speed of the enemy
    private float playerSpeed = 0.8f; // Speed of the player

    public bool isFinished; // Bool to tell whether the event has finished

    private bool stopMoving; // Stop the slider movement
    private bool playerWin; // To store the result of the battle
    private int enemyDirection; // Direction of enemy's movement
    private int playerDirection; // Direction of player's movement
    private Transform enemy_nextCoor; // Next waypoint of enemy's movement
    private int curEnemyWaypoint; // Current target waypoint of enemy
    private Transform player_nextCoor; // Next waypoint of player's movement
    private int curPlayerWaypoint; // Current target waypoint of player
    private float[] remainCoorEnemy; // Movement related: 0 for x, 1 for y
    private float[] remainCoorPlayer;
    private bool canAttack;

    private Vector3 playerOriginPos; // The vector where the player should sit when reset
    private Vector3 enemyOriginPos; // The vector where the enemy should sit when reset

    //public Vector3 PlayerPos;
    //public Vector3 EnemyPos;

    [SerializeField] private AudioSource killSoundEffect;
    [SerializeField] private AudioSource missSoundEffect;
    // Start is called before the first frame update
    void Awake()
    {
        remainCoorEnemy = new float[2];
        remainCoorPlayer = new float[2];
        playerOriginPos = this.player.gameObject.transform.position;
        enemyOriginPos = currentEnemy.gameObject.transform.position;
        //Reset(currentEnemy.gameObject, this.player.gameObject);     
    }

    // Public method to set the player / enemy speed using a multiplier
    public void setEnemySpeed(float speedMultiplier)
    {
        enemySpeed *= speedMultiplier;
    }

    public void setPlayerSpeed(float speedMultiplier)
    {
        playerSpeed *= speedMultiplier;
    }

    public bool checkBattleResult()
    {
        return playerWin;
    }

    float getDistance(Transform a, Transform b)
    {
        return Vector3.Distance(a.position, b.position);
    }

    // Reset all vars for next event
    public void Reset(GameObject enemy, GameObject player)
    {
        playerWin = false;
        isFinished = false;
        stopMoving = false;
        hitText.SetActive(false);
        missText.SetActive(false);
        canAttack = true;

        enemyDirection = Random.Range(0, 2); // 0 for clockwise, 1 for counter-clockwise
        playerDirection = (enemyDirection == 0) ? 1 : 0; // Opposite of enemy's direction
        curEnemyWaypoint = 0;   // Current Waypoint: 0, 1, 2, 3
        curPlayerWaypoint = 2;
        enemy_nextCoor = wayPoints.transform.GetChild(curEnemyWaypoint);
        player_nextCoor = wayPoints.transform.GetChild(curPlayerWaypoint);

        // Debug.Log("Player dire: " + playerDirection + "     Enemy dire: " + enemyDirection);

        if (!enemy)
        {
            Debug.Log("Warning: No enemy object found!");
        }
        currentEnemy.sprite = enemy.GetComponent<SpriteRenderer>().sprite;
        this.player.sprite = player.GetComponent<SpriteRenderer>().sprite;

        // Reset Position
        this.player.gameObject.transform.position = playerOriginPos;
        currentEnemy.gameObject.transform.position = enemyOriginPos;

        remainCoorEnemy[0] = enemy_nextCoor.position.x - currentEnemy.gameObject.transform.position.x;
        remainCoorEnemy[1] = enemy_nextCoor.position.y - currentEnemy.gameObject.transform.position.y;
        remainCoorPlayer[0] = player_nextCoor.position.x - this.player.gameObject.transform.position.x;
        remainCoorPlayer[1] = player_nextCoor.position.y - this.player.gameObject.transform.position.y;
    }

    Transform changeMovementDirection(int mode, ref int curWaypoint)
    {
        /* *** !!! The waypoint set is sorted in CCW order !!! *** */
        if (mode == 0) // CW
        {
            if (curWaypoint > 0)
            {
                curWaypoint--;
            }
            else // One cycle is completed, reset
            {
                curWaypoint = wayPoints.transform.childCount - 1;
            }
        }
        else // CCW
        {
            if (curWaypoint < wayPoints.transform.childCount - 1)
            {
                curWaypoint++;
            }
            else // One cycle is completed, reset
            {
                curWaypoint = 0;
            }
        }
        return wayPoints.transform.GetChild(curWaypoint);
    }

    void FixedUpdate()
    {
        if (!stopMoving)
        {
            // Enemy Movement
            //Debug.Log("Enemy Distance Delta: " + getDistance(currentEnemy.gameObject.transform, enemy_nextCoor));
            // Debug.Log("Min delta Enemy: " + Time.deltaTime * enemySpeed * (remainCoorEnemy[0] == 0 ? Mathf.Abs(remainCoorEnemy[1]) : Mathf.Abs(remainCoorEnemy[0])));
            if (getDistance(currentEnemy.gameObject.transform, enemy_nextCoor) <= (Time.deltaTime * enemySpeed * (remainCoorEnemy[0] == 0 ? Mathf.Abs(remainCoorEnemy[1]) : Mathf.Abs(remainCoorEnemy[0]))))
            {
                currentEnemy.gameObject.transform.position = enemy_nextCoor.position; // Line up with current point
                enemy_nextCoor = changeMovementDirection(enemyDirection, ref curEnemyWaypoint); // Set next waypoint
                remainCoorEnemy[0] = enemy_nextCoor.position.x - currentEnemy.gameObject.transform.position.x;
                remainCoorEnemy[1] = enemy_nextCoor.position.y - currentEnemy.gameObject.transform.position.y;
            }
            currentEnemy.gameObject.transform.position = currentEnemy.gameObject.transform.position + new Vector3((remainCoorEnemy[0] == 0) ? 0 : (remainCoorEnemy[0] * Time.deltaTime * enemySpeed), (remainCoorEnemy[1] == 0) ? 0 : (remainCoorEnemy[1] * Time.deltaTime * enemySpeed), 0);

            // Player Movement
            //Debug.Log("Player Distance Delta: " + getDistance(player.gameObject.transform, player_nextCoor));
            // Debug.Log("Min delta Player: " + Time.deltaTime * playerSpeed * (remainCoorPlayer[0] == 0 ? Mathf.Abs(remainCoorPlayer[1]) : Mathf.Abs(remainCoorPlayer[0])));
            if (getDistance(player.gameObject.transform, player_nextCoor) <= (Time.deltaTime * playerSpeed * (remainCoorPlayer[0] == 0 ? Mathf.Abs(remainCoorPlayer[1]) : Mathf.Abs(remainCoorPlayer[0]))))
            {
                player.gameObject.transform.position = player_nextCoor.position; // Line up with current point
                player_nextCoor = changeMovementDirection(playerDirection, ref curPlayerWaypoint); // Set next waypoint
                remainCoorPlayer[0] = player_nextCoor.position.x - player.gameObject.transform.position.x;
                remainCoorPlayer[1] = player_nextCoor.position.y - player.gameObject.transform.position.y;
               
            }
            //Debug.Log("Player offest = " + remainCoorPlayer[0] + ", " + remainCoorPlayer[1]);
            player.gameObject.transform.position = player.gameObject.transform.position + new Vector3((remainCoorPlayer[0] == 0) ? 0 : (remainCoorPlayer[0] * Time.deltaTime * playerSpeed), (remainCoorPlayer[1] == 0) ? 0 : (remainCoorPlayer[1] * Time.deltaTime * playerSpeed), 0);
        }

        //PlayerPos = player.gameObject.transform.position;
        //EnemyPos = currentEnemy.gameObject.transform.position;

        // Hit Pending Part
        if (Input.GetKey(KeyCode.Space) && canAttack)
        {
            stopMoving = true;
            canAttack = false;

            //PlayerPos = player.gameObject.transform.position;

            // Debug.Log("Dist = " + getDistance(player.gameObject.transform, currentEnemy.gameObject.transform));
            if (getDistance(player.gameObject.transform, currentEnemy.gameObject.transform) < 100f)
            {
                Debug.Log("Hit!");
                killSoundEffect.Play();
                hitText.SetActive(true);
                playerWin = true;
            }
            else
            {
                Debug.Log("Missed!");
                missSoundEffect.Play();
                missText.SetActive(true);
                playerWin = false;
            }

            StartCoroutine(CountDown(3));
        }

    }

    // Coroutine for displaying hit/miss image
    private IEnumerator CountDown(int duration)
    {
        yield return new WaitForSeconds(duration);
        isFinished = true;
    }

}
