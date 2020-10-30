/*
 * Anthony Wessel
 * Assignment 6
 * Displays tutorial messages to the player
 */

using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public TutorialMessage[] messages;
    GameObject lastMessage;
    
    void Start()
    {
        // Hide all messages at the start
        for (int i = 0; i < messages.Length; i++)
        {
            messages[i].text.SetActive(false);
        }
        StartCoroutine(displayMessages());
    }

    IEnumerator displayMessages()
    {
        int i = 0;
        while (i < messages.Length)
        {
            messages[i].text.SetActive(true);
            yield return new WaitForSeconds(messages[i].displayTime);
            messages[i].text.SetActive(false);

            i++;
        }
    }
}

[System.Serializable]
public class TutorialMessage
{
    public GameObject text;
    public float displayTime;
}
