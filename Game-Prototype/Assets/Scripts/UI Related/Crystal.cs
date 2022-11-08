using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Crystal : MonoBehaviour
{
    public TextMeshProUGUI display;
    public Animator animator;

    private int crystalNum;

    public GameObject player;

    private Powerup_Greedy powerup_Greedy;

    // Public method to retrive the number of crystal that the player has gained (Level-independent)
    public int getCrystalNum()
    {
        return crystalNum;
    }

    public void gainCrystal(int gainNum)
    {
        int crystalMultiplier = powerup_Greedy.getCrystalMultiplier();
        Debug.Log("Printing current multiplier: " + crystalMultiplier);
        crystalNum += crystalMultiplier * gainNum;
        // Play animation
        animator.Play("Base Layer.Crystal_Gained");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        crystalNum = 0;
        powerup_Greedy = player.GetComponent<Powerup_Greedy>();
    }

    // Update is called once per frame
    void Update()
    {
        display.text = "" + crystalNum;
    }
}
