using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class GUM11_Conver : MonoBehaviour
{
    public NPCConversation myConversation;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ConversationManager.Instance.StartConversation(myConversation);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ConversationManager.Instance.EndConversation();
        }
    }
}
