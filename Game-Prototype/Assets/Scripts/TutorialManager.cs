using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{   
    public GameObject[] popUps;
    static int popUpIndex;
    private Dictionary<int, bool> dict = new Dictionary<int, bool>();
    // Start is called before the first frame update
    void Start()
    {
        popUpIndex = 0;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && popUpIndex < popUps.Length && popUps[popUpIndex].active == true)
        {
            Time.timeScale = 1;
            popUps[popUpIndex].SetActive(false);
            dict[popUpIndex] = false;
        }

        if (popUpIndex < popUps.Length && !dict.ContainsKey(popUpIndex)){
            
        if (popUpIndex == 0 ) {
            popUps[0].SetActive(true);
            popUps[1].SetActive(false);
            popUps[2].SetActive(false);
            popUps[3].SetActive(false);
            popUps[4].SetActive(false);
            popUps[5].SetActive(false);
        }
        else if (popUpIndex == 1) {
            popUps[1].SetActive(true);
            popUps[0].SetActive(false);
            popUps[2].SetActive(false);
            popUps[3].SetActive(false);
            popUps[4].SetActive(false);
            popUps[5].SetActive(false);
        } 
        else if (popUpIndex == 2) {
            popUps[2].SetActive(true);
            popUps[0].SetActive(false);
            popUps[1].SetActive(false);
            popUps[3].SetActive(false);
            popUps[4].SetActive(false);
            popUps[5].SetActive(false);
        }
        else if (popUpIndex == 3) {
            popUps[3].SetActive(true);
            popUps[0].SetActive(false);
            popUps[2].SetActive(false);
            popUps[1].SetActive(false);
            popUps[4].SetActive(false);
            popUps[5].SetActive(false);
        }
        else if (popUpIndex == 4) {
            popUps[4].SetActive(true);
            popUps[0].SetActive(false);
            popUps[2].SetActive(false);
            popUps[3].SetActive(false);
            popUps[1].SetActive(false);
            popUps[5].SetActive(false);
        }
        else if (popUpIndex == 5) {
            popUps[5].SetActive(true);
            popUps[0].SetActive(false);
            popUps[2].SetActive(false);
            popUps[3].SetActive(false);
            popUps[1].SetActive(false);
            popUps[4].SetActive(false);
        }

        }
    }
    private void OnCollisionEnter2D(Collision2D other){
        popUpIndex++;
        Destroy(gameObject);
        Time.timeScale = 0;

    }
}
