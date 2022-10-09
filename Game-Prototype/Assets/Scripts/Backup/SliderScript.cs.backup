using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public Slider slider;
    public GameObject hitImg; // Image to display when we hit the hitzoom
    public GameObject missImg; // Image to display when we miss the hit zoom
    public Transform hitZoom; // Determine the position of the hit zoom
    public bool isFinished; // Global bool to tell whether the event has finished

    public float speed = 1.5f; // Speed of the handle
    private bool isForward; // Used for judging which side the handle goes
    private float minHit; // Min value of hit zoom
    private float maxHit; // Max value of hit zoom
    private bool stopMoving; // Stop the handle movement
    private RectTransform slider_Rect;
    private RectTransform hitzone_Rect;
    private bool playerWin;
    
 
    // Start is called before the first frame update
    void Start()
    {
        slider_Rect = (RectTransform)slider.transform;
        hitzone_Rect = (RectTransform)hitZoom;
        Reset();     
    }

    public bool checkBattleResult()
    {
        return playerWin;
    }

    // Reset all vars for next event
    public void Reset()
    {
        float hitzoneHalf = hitzone_Rect.rect.width / 2.0f;

        playerWin = false;
        slider.value = 0;
        isForward = true;
        isFinished = false;
        stopMoving = false;
        hitImg.SetActive(false);
        missImg.SetActive(false);
        float randomPosX = Random.Range(0, slider_Rect.rect.width*1.0f); // width of the slider (e.g, width = 100, range = (0, 100))
        // Calculate min and max for hitzone
        // Note that randomRosX is the center pos of hitzone
        // Both minHit and maxHit need to be divided by slider_Rect.rect.width to normalize their value in between [0, 1]
        if (randomPosX >= (slider_Rect.rect.width - hitzoneHalf))
        {
            // Special Case 1: maxHit is outside the slide area
            minHit = (slider_Rect.rect.width - hitzone_Rect.rect.width) / slider_Rect.rect.width;
            maxHit = slider_Rect.rect.width / slider_Rect.rect.width;
            hitZoom.localPosition = new Vector2(slider_Rect.rect.width / 2 - hitzoneHalf, 0);
        }
        else if (randomPosX <= hitzoneHalf)
        {
            // Special Case 2: minHit is outside the slide area
            minHit = 0;
            maxHit = hitzone_Rect.rect.width / slider_Rect.rect.width;
            hitZoom.localPosition = new Vector2(-slider_Rect.rect.width / 2 + hitzoneHalf, 0);
        }
        else
        {
            // Normal Case
            minHit = (randomPosX - hitzoneHalf) / slider_Rect.rect.width;
            maxHit = (randomPosX + hitzoneHalf) / slider_Rect.rect.width;
            hitZoom.localPosition = new Vector2(randomPosX - slider_Rect.rect.width / 2, 0);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Slider Moving Part
        if (isForward && !stopMoving)
        {
            if (slider.value < slider.maxValue)
            {
                slider.value += speed;
            }
            else
            {
                isForward = !isForward;
            }
        }
        else if(!stopMoving)
        {
            if (slider.value > slider.minValue)
            {
                slider.value -= speed;
            }
            else
            {
                isForward = !isForward;
            }
        }

        // Hit Pending Part
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Min: " + minHit + "/n Max: " + maxHit);
            stopMoving = true;

            if (slider.normalizedValue >= minHit && slider.normalizedValue <= maxHit)
            {
                Debug.Log("Hit!");
                hitImg.SetActive(true);
                playerWin = true;
            }
            else
            {
                Debug.Log("Missed!");
                missImg.SetActive(true);
                playerWin = false;
            }
            Debug.Log("Hitzone Pos: " + hitZoom.localPosition);
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
