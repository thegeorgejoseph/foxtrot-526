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
    public TextMeshProUGUI powerup_hint;

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
        if (LevelselectorScript.spaceship_level == 1)
        {
            hint_text.text = "Travelling to Mercury";
        }
        else if (LevelselectorScript.spaceship_level == 2)
        {
            hint_text.text = "Travelling to Venus";
            powerup_hint.text = "Blue enemy drops freeze powerup";
        }
        else if (LevelselectorScript.spaceship_level == 3)
        {
            hint_text.text = "Travelling to Moon ";
            powerup_hint.text = "Blue enemy drops freeze powerup\n" +
    "Red enemy drops timer freeze powerup";
        }
        else if (LevelselectorScript.spaceship_level == 4)
        {
            hint_text.text = "Travelling to Mars ";
            powerup_hint.text = "Blue enemy drops freeze powerup\n" +
              "Red enemy drops timer freeze powerup\n" +
              "Green enemy drops zoom powerup";
        }
        else if (LevelselectorScript.spaceship_level == 5)
        {
            hint_text.text = "Travelling to Titan";
            powerup_hint.text = "Blue enemy drops freeze powerup\n" +
             "Red enemy drops timer freeze powerup\n" +
             "Green enemy drops zoom powerup";
        }
        else if (LevelselectorScript.spaceship_level == 6)
        {
            hint_text.text = "Travelling to IO";
            powerup_hint.text = "Blue enemy drops freeze powerup\n" +
             "Red enemy drops timer freeze powerup\n" +
             "Green enemy drops zoom powerup";
        }
        else if (LevelselectorScript.spaceship_level == 7)
        {
            hint_text.text = "Travelling to Asteroid Belt";
            powerup_hint.text = "Blue enemy drops freeze powerup\n" +
             "Red enemy drops timer freeze powerup\n" +
             "Green enemy drops zoom powerup";
        }
        else if (LevelselectorScript.spaceship_level == 8)
        {
            hint_text.text = "Travelling to Charon";
            powerup_hint.text = "Blue enemy drops freeze powerup\n" +
             "Red enemy drops timer freeze powerup\n" +
             "Green enemy drops zoom powerup";
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
        if (LevelselectorScript.spaceship_level == 1)
        {
            SceneManager.LoadScene("Level_1");
        }
        else if (LevelselectorScript.spaceship_level == 2)
        {
            SceneManager.LoadScene("Level_2");
        }
        else if (LevelselectorScript.spaceship_level == 3)
        {
            SceneManager.LoadScene("Level_3");
        }
        else if (LevelselectorScript.spaceship_level == 4)
        {
            SceneManager.LoadScene("Level_4");
        }
        else if (LevelselectorScript.spaceship_level == 5)
        {
            SceneManager.LoadScene("Level_5");
        }
        else if (LevelselectorScript.spaceship_level == 6)
        {
            SceneManager.LoadScene("Level_6");
        }
        else if (LevelselectorScript.spaceship_level == 7)
        {
            SceneManager.LoadScene("Level_7");
        }
        else if (LevelselectorScript.spaceship_level == 8)
        {
            SceneManager.LoadScene("Level_8");
        }

    }
}
