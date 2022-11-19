using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderScript : MonoBehaviour
{
    public GameObject wayPoints; // A set of waypoints to guide the enemy's movement
    public Image currentEnemy; // Current Enemy that the player encountered
    public Image player; // Player Object to be displaied
    public Animator BPTransition; // Battle transistion animation & UI
    public Image BPTransitionBackGround;
    public TextMeshProUGUI BPTransitionText;

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

    void Start()
    {
        // Increase player scale
        this.player.gameObject.transform.localScale *= 1.5f;
        currentEnemy.gameObject.transform.localScale *= 1.8f;
    }

    // Public method to set the player / enemy speed using a multiplier
    public void setEnemySpeed(float speedMultiplier)
    {
        enemySpeed *= speedMultiplier;
        Debug.Log("Enemy Speed: " + enemySpeed);
    }

    public void setPlayerSpeed(float speedMultiplier)
    {
        playerSpeed *= speedMultiplier;
        Debug.Log("Player Speed: " + playerSpeed);
    }

    public bool checkBattleResult()
    {
        return playerWin;
    }

    float getDistance(Transform a3, Transform b3)
    {
        Vector2 a2 = a3.position;
        Vector2 b2 = b3.position;
        return Vector2.Distance(a2, b2);
    }

    // Reset all vars for next event
    public void Reset(GameObject enemy, GameObject player)
    {
        playerWin = false;
        isFinished = false;
        stopMoving = false;
        canAttack = true;

        enemyDirection = Random.Range(0, 2); // 0 for clockwise, 1 for counter-clockwise
        playerDirection = (enemyDirection == 0) ? 1 : 0; // Opposite of enemy's direction
        // playerDirection = Random.Range(0, 2);
        curEnemyWaypoint = 0;   // Current Waypoint: 0, 1, 2, 3
        curPlayerWaypoint = 2;
        enemy_nextCoor = wayPoints.transform.GetChild(curEnemyWaypoint);
        player_nextCoor = wayPoints.transform.GetChild(curPlayerWaypoint);

        // Debug.Log("Player dire: " + playerDirection + "     Enemy dire: " + enemyDirection);

        if (!enemy)
        {
            Debug.Log("Warning: No enemy object found!");
        }
        // Sync the sprite and color of enemy & player
        currentEnemy.sprite = enemy.GetComponent<SpriteRenderer>().sprite;
        currentEnemy.color = enemy.GetComponent<SpriteRenderer>().color;
        this.player.sprite = player.GetComponent<SpriteRenderer>().sprite;
        this.player.color = player.GetComponent<SpriteRenderer>().color;
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
            // Debug.Log("Enemy Distance Delta: " + getDistance(currentEnemy.gameObject.transform, enemy_nextCoor));
            // Debug.Log("Min delta Enemy: " + Time.deltaTime * enemySpeed * (remainCoorEnemy[0] == 0 ? Mathf.Abs(remainCoorEnemy[1]) : Mathf.Abs(remainCoorEnemy[0])));
            if (getDistance(currentEnemy.gameObject.transform, enemy_nextCoor) <= (Time.deltaTime * enemySpeed * ( Mathf.Abs(remainCoorEnemy[0]) <= 0.1f ? Mathf.Abs(remainCoorEnemy[1]) : Mathf.Abs(remainCoorEnemy[0]))))
            {
                currentEnemy.gameObject.transform.position = enemy_nextCoor.position; // Line up with current point
                enemy_nextCoor = changeMovementDirection(enemyDirection, ref curEnemyWaypoint); // Set next waypoint
                remainCoorEnemy[0] = enemy_nextCoor.position.x - currentEnemy.gameObject.transform.position.x;
                remainCoorEnemy[1] = enemy_nextCoor.position.y - currentEnemy.gameObject.transform.position.y;
            }
            currentEnemy.gameObject.transform.position = currentEnemy.gameObject.transform.position + new Vector3(Mathf.Abs(remainCoorEnemy[0]) <= 0.1f ? 0 : (remainCoorEnemy[0] * Time.deltaTime * enemySpeed), (remainCoorEnemy[1] == 0) ? 0 : (remainCoorEnemy[1] * Time.deltaTime * enemySpeed), 0);

            // Player Movement
            // Debug.Log("Player Distance Delta: " + getDistance(player.gameObject.transform, player_nextCoor));
            // Debug.Log("Min delta Player: " + Time.deltaTime * playerSpeed * (remainCoorPlayer[0] == 0 ? Mathf.Abs(remainCoorPlayer[1]) : Mathf.Abs(remainCoorPlayer[0])));
            if (getDistance(player.gameObject.transform, player_nextCoor) <= (Time.deltaTime * playerSpeed * (Mathf.Abs(remainCoorPlayer[0]) <= 0.1f ? Mathf.Abs(remainCoorPlayer[1]) : Mathf.Abs(remainCoorPlayer[0]))))
            {
                player.gameObject.transform.position = player_nextCoor.position; // Line up with current point
                player_nextCoor = changeMovementDirection(playerDirection, ref curPlayerWaypoint); // Set next waypoint
                remainCoorPlayer[0] = player_nextCoor.position.x - player.gameObject.transform.position.x;
                remainCoorPlayer[1] = player_nextCoor.position.y - player.gameObject.transform.position.y;
               
            }
            //Debug.Log("Player offest = " + remainCoorPlayer[0] + ", " + remainCoorPlayer[1]);
            player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(Mathf.Abs(remainCoorPlayer[0]) <= 0.1f ? 0 : (remainCoorPlayer[0] * Time.deltaTime * playerSpeed), (remainCoorPlayer[1] == 0) ? 0 : (remainCoorPlayer[1] * Time.deltaTime * playerSpeed), 0);
        }

        // Hit Pending Part
        if (Input.GetKey(KeyCode.Space) && canAttack)
        {
            stopMoving = true;
            canAttack = false;

            // Debug.Log("Dist = " + getDistance(player.gameObject.transform, currentEnemy.gameObject.transform));
            if (getDistance(player.gameObject.transform, currentEnemy.gameObject.transform) < 100f)
            {
                Debug.Log("Hit!");
                BPTransitionText.text = "Battle Won";
                BPTransitionBackGround.color = new Color(0.23f, 0.68f, 0.23f); // Custom Green Color
                BPTransition.Play("Base Layer.BP_Finished_Start");
                killSoundEffect.Play();
                playerWin = true;
            }
            else
            {
                Debug.Log("Missed!");
                BPTransitionText.text = "Battle Lost";
                BPTransitionBackGround.color = new Color(0.68f, 0.23f, 0.23f); // Custom Red Color
                BPTransition.Play("Base Layer.BP_Finished_Start");
                missSoundEffect.Play();
                playerWin = false;
            }

            StartCoroutine(CountDown(4));
        }

    }

    // Coroutine for displaying hit/miss image
    private IEnumerator CountDown(int duration)
    {
        yield return new WaitForSeconds(duration);
        isFinished = true;
    }

}
