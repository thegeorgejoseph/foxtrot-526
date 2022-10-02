using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static float health = 1.0f;
    private float mxHealth = 1.0f;
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
        int hearts = 0;
        for (int i = 0; i<Math.Floor(health); i++)
        {
            CreateHeart(HeartStatus.Full);
            ++hearts;
        }

        if(health > 0 && Math.Floor(health) < health)
        {
            CreateHeart(HeartStatus.Half);
            ++hearts;
        }

        for(int i=hearts; i<mxHealth; i++)
        {
            CreateHeart (HeartStatus.Empty);
        }
    }

    public void EnlargeHeart()
    {
        for (int i = 0; i < 20; i++)
        {
            hearts[0].transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
        }
    }

    public void ShrinkHearts()
    {
        for (int i = 0; i < 20; i++)
        {
            hearts[0].transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
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
        if(health <= 0)
        {
            DrawHearts();
        }
        else if (health != prev && health > 0)
        {
            DrawHearts ();
            prev = health;
            mxHealth = Math.Max(mxHealth, health);
        }
    }
}
