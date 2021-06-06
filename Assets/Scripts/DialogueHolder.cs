using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHolder : MonoBehaviour
{
    [TextArea]
    [SerializeField] private List<string> dialogue;
    [SerializeField] private Text content;
    [SerializeField] private Transform canvas;

    private int i;

    public void PlayDialogue()
    {
        StartCoroutine(LoopDialogue());
    }

    private IEnumerator LoopDialogue()
    {
        canvas.gameObject.SetActive(true);
        while(i < dialogue.Count)
        {
            content.text = dialogue[i];
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump")));
            i++;
        }
        canvas.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            PlayDialogue();
        }
    }
}
