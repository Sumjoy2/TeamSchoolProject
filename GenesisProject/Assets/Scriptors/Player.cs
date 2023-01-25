using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    //using comments is weird
    //Movement declairations
    public float Speed = 4.5f; //Speed
    float horizontal;
    float vertical;
    //Is Quest false, doesnt have a quest at start
    private bool IsQuest = false;
    Rigidbody2D rigidbody2d;
    //QuestMenu
    public Image QuestMenu;
    public TMP_Text QuestMenuText;

    Vector2 lookDirection = new Vector2(1, 0);

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        QuestMenu.enabled = false;
        QuestMenuText.enabled = false;
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
                    QuestingToggle(hit); //Quest Toggle
                    Debug.Log(hit.collider.tag);
                    character.DisplayDialog();
                }
            }
        }
    }

    //Questing Toggle. For Changing weather there is or isnt the quest active
    void QuestingToggle(RaycastHit2D hit)
    {
        //Should check if NPC has tag QuestGiver
        if (IsQuest == false && hit.collider.tag == "QuestGiver")
        {
            Debug.Log("QuestStarted");
            QuestMenu.enabled = true; QuestMenuText.enabled = true;
            IsQuest = !IsQuest;
        }
        //Should check if NPC has tag QuestReciver
        else if (IsQuest == true && hit.collider.tag == "QuestReceiver")
        {
            Debug.Log("QuestEnded");
            QuestMenu.enabled = false; QuestMenuText.enabled = false;
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