using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialougueManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI dialogText;
    [SerializeField] public GameObject dialouguebox;
    public String[] texts;
    public int index = 0;
    public static DialougueManager main;
    
    public bool isWriting = false;

    private void Awake()
    {
        if (main!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            main = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isWriting)
            {
                StopCoroutine("SlowWrite");
                dialogText.text = texts[index];
                isWriting = false;
            }
            else
            {
                dialouguebox.SetActive(false);
            }
        }
    }

    public void DisplayDialougue()
    {
        dialouguebox.SetActive(true);
        dialogText.text = texts[index];
       // StartCoroutine("SlowWrite");
        
        
    }

    public IEnumerable<WaitForSeconds> SlowWrite()
    {
        isWriting = true;
        for (int i = 0; i < texts[index].Length; i++)
        {
            string text = texts[index];
            dialogText.text += text[i];
            yield return new WaitForSeconds(0.1f);
        }
        isWriting = false;
    }
}
