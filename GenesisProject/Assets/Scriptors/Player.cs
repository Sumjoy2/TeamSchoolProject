using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float Speed = 4.5f;
    float horizontal;
    float vertical;
    Rigidbody2D rigidbody2d;

    Vector2 lookDirection = new Vector2(1, 0);

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        Vector2 position = transform.position;
        position.x = position.x + Speed * horizontal * Time.deltaTime;
        position.y = position.y + Speed * vertical * Time.deltaTime;
        transform.position = position;

        if (Input.GetKeyDown("escape"))
        {
            LoadScene("Menu");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NPC character = hit.collider.GetComponent<NPC>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }

    }
}