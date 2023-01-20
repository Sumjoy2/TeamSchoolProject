using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string questName;
    public string description;
    public bool completed;

    public void Complete()
    {
        Debug.Log("Quest Completed");
        Destroy(this);
    }
}
