using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerup_Greedy : MonoBehaviour
{
    private int crystalMultiplier;

    // dropping status
    private bool droppingEnabled;
    public GameObject GreedyIcon;

    // Start is called before the first frame update
    void Start()
    {
        crystalMultiplier = 1;
        droppingEnabled = true; // enabled by default
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getCrystalMultiplier()
    {
        return crystalMultiplier;
    }

    public void changeCrystalMultiplier(int newMultiplier)
    {
        crystalMultiplier = newMultiplier;
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
        Debug.Log("Entered powerup");
        Debug.Log("player is " + collider.gameObject.tag);
        // Using tags to check if the player has actually met enemy
        if (collider.gameObject.tag == "Powerup")
        {
            collider.gameObject.SetActive(false);
            if (getCrystalMultiplier() == 1)
            {
                GreedyIcon.GetComponent<Image>().color = Color.white;
                changeCrystalMultiplier(2);
                changeDroppingStatus(false);
                StartCoroutine(CountDown(30));
            }
        }

    }

    private IEnumerator CountDown(int duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("finished waiting for powerup");
        GreedyIcon.GetComponent<Image>().color = Color.grey;
        changeCrystalMultiplier(1);
        changeDroppingStatus(true);
    }
}
