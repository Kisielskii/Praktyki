using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
   public Dialogue dialogue;
   private bool isPlayerInRange = false;

   private void Update()
    {
      // Check if the player is in range and has pressed the E key.
      if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
      {
         TriggerDialogue();
      }
    }

   public void TriggerDialogue()
   {
      FindFirstObjectByType<DialogueManager>().StartDialogue(dialogue);
   }

    private void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.CompareTag("Player"))
      {
         Debug.Log("Player In Range");
         isPlayerInRange = true;
      }
    }

    
    private void OnTriggerExit(Collider other)
    {
      if (other.gameObject.CompareTag("Player"))
      {
         Debug.Log("Player Out of Range");
         isPlayerInRange = false;
      }
    }


}
