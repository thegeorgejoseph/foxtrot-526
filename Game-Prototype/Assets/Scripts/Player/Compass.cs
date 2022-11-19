using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    // public GameObject arrow; // The compass arrow
    public GameObject target; // The target where the arrow will point to
    public GameObject player;
    public float angle; // The rotation angle
    private Quaternion quaternion;
    private float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rotateSpeed = 5f;
    }

    float v2Angle(Vector2 v1, Vector2 v2)
    {
        Vector2 diff = v2 - v1;
        float sign = (v2.y < v1.y) ? -1f : 1f;
        return Vector2.Angle(Vector2.right, diff) * sign;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        angle = v2Angle(player.transform.position, target.transform.position) - 90f; // Angle From target to player
        // quaternion = Quaternion.Euler(0, 0, angle);
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
