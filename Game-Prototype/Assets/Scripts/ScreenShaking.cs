using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShaking : MonoBehaviour
{
    // Camera Script to be disable when Script is activated
    public GameObject Camera;
    // Desired duration of the shake effect
    private float shakeDuration = 0f;
    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 0.25f;
    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 3.0f;
    // The initial position of the GameObject
    Vector3 iniPosition;


    void Update()
    {
        if (shakeDuration > 0)
        {
            // Debug.Log("Shaking!");
            Camera.transform.localPosition = iniPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            Camera.transform.localPosition = iniPosition;
            Camera.GetComponent<CameraScript>().enabled = true;
        }
    }

    public void TriggerShake()
    {
        iniPosition = Camera.transform.localPosition;
        shakeDuration = 1.0f;
        Camera.GetComponent<CameraScript>().enabled = false; 
    }
}
