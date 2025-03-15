using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
   [SerializeField] List<GameObject> openButtons = new List<GameObject>();

    private void Update()
    {
        if (IsActivated())
        {
            Destroy(gameObject);
        }
    }

    public bool IsActivated()
    {
        for (int i = 0; i < openButtons.Count; i++)
        {
            if (openButtons[i].GetComponent<Button_Plate>() != null)
            {
                ButtonClass button = openButtons[i].GetComponent<Button_Plate>().thisButton;
                if (!button.isPressed)
                {
                    
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        return true;
    }
}
