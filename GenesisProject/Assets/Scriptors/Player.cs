using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Movement declairations
    public float Speed = 4.5f; //Speed
    float horizontal;
    float vertical;
    //Is Quest false
    private bool IsQuest = false;
    Rigidbody2D rigidbody2d;

    Vector2 lookDirection = new Vector2(1, 0);

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    //Scene loading
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void Update()
    {
        //Movement things
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

        //Return to menu after hitting ESC
        if (Input.GetKeyDown("escape"))
        {
            LoadScene("Menu");
        }

        //Talk to Non Player Characters
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NPC character = hit.collider.GetComponent<NPC>();
                if (character != null)
                {
                    QuestingToggle(); //Quest Toggle
                    character.DisplayDialog();
                }
            }
        }
    }

    //Questing Toggle. For Changing weather there is or isnt the quest active
    void QuestingToggle()
    {
        //Should check if NPC has tag QuestGiver
        if (IsQuest == false && GameObject.FindWithTag("QuestGiver"))
        {
            Debug.Log("QuestStarted");
            IsQuest = !IsQuest;
        }
        //Should check if NPC has tag QuestReciver
        else if (IsQuest == true && GameObject.FindWithTag("QuestReceiver"))
        {
            Debug.Log("QuestEnded");
            IsQuest = !IsQuest;
            Invoke ("DeltaLevel", 2); //Loads Level after X ish seconds. X is the Number after the text
        }
    }

    //For incase we want to go to next level later
    void DeltaLevel()
    {
        LoadScene("Menu");
    }
}