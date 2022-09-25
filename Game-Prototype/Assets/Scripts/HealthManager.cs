using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static float health = 1.0f;
    private float prev = health;
    public GameObject heartPrefab;
    List<HealthHeart> hearts = new List<HealthHeart> ();

    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHeart>();
    }

    public void CreateHeart(HeartStatus status)
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeartImage(status);
        hearts.Add(heartComponent);
    }

    public void DrawHearts()
    {
        ClearHearts();

        for (int i = 0; i<Math.Floor(health); i++)
        {
            CreateHeart(HeartStatus.Full);
        }

        if(Math.Floor(health) < health)
        {
            CreateHeart(HeartStatus.Half);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        DrawHearts();
    }

    // Update is called once per frame
    void Update()
    {
        if (health != prev)
        {
            DrawHearts ();
            prev = health;
        }

    }
}
