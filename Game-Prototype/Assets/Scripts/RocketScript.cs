using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RocketScript : MonoBehaviour
{
    public float velX = 5f;
    float velY = 0f;
    Rigidbody2D rb;
    public float lifetime = 5f;
    public TextMeshProUGUI hint_text;

    private void Awake()
    {
        Vector3 localscale = transform.localScale;
        if (velX < 0)
        {
            localscale.x *= -1;
            transform.localScale = localscale;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(Exit_Script.level_num == 1)
        {
            hint_text.text = "Travelling to planet Jupiter";
        }
        else if (Exit_Script.level_num == 2)
        {
            hint_text.text = "Travelling to planet Venus ";
        }
        else if (Exit_Script.level_num == 3)
        {
            hint_text.text = "Travelling to planet Neptune ";
        }
        else if (Exit_Script.level_num == 4)
        {
            hint_text.text = "Travelling to planet Mercury";
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velX, velY);
        Invoke("destroyGameobject", lifetime);
    }

    void destroyGameobject()
    {
        Debug.Log("as");
        foreach(Transform child in transform)
        {
            if (child.name == "FireParticles")
            {
                child.GetComponent<ParticleSystem>().Stop();
                Destroy(child.gameObject, 3f);
            }
        }
        transform.DetachChildren();
        Destroy(gameObject);
        if (Exit_Script.level_num == 1)
        {
            SceneManager.LoadScene("Level_2");
        }
        else if (Exit_Script.level_num == 2)
        {
            SceneManager.LoadScene("Level_3");
        }
        else if (Exit_Script.level_num == 3)
        {
            SceneManager.LoadScene("Level_4");
        }
        else if (Exit_Script.level_num == 4)
        {
            SceneManager.LoadScene("Level_5");
        }
        else
        {
            SceneManager.LoadScene("Level_1");
        }
    }
}