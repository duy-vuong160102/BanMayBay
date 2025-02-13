using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    public float speed = 5f; // Tốc độ rơi
    private float rotationSpeed; // Tốc độ xoay ngẫu nhiên

    void Start()
    {
        // Tạo tốc độ xoay ngẫu nhiên (-300 đến 300)
        rotationSpeed = Random.Range(-300f, 300f);
    }

    void Update()
    {
        // Di chuyển thiên thạch xuống dưới
        transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);

        // Xoay thiên thạch quanh trục Z
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
