using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boulder_Spawner : MonoBehaviour
{
   public enum Logic
    {
        Or,
        And
    }

    public Logic logic;
   public GameObject boulder_prefab;
    List<GameObject> boulders = new List<GameObject>();
    public int boulders_to_spawn=1;
    
    public Button_Plate[] plates;

    public Button_Behaviour[] buttons;
    private bool isActivated=false;
    
    
    bool AllPlatesActivated()
    {
        if (logic == Logic.And)
        {
            foreach (Button_Plate plate in plates)
            {
                if (!plate.isPressed)
                    return false;
            }

        
            foreach (Button_Behaviour button in buttons)
            {
                if (!button.isPressed)
                    return false;
            }
        
           
            return true;
        }
        else
        {
            foreach (Button_Plate plate in plates)
            {
                if (plate.isPressed)
                    return true;
            }

        
            foreach (Button_Behaviour button in buttons)
            {
                if (button.isPressed)
                    return true;
            }
            return false;
        }
        
    }

    private void Update()
    {
        if (AllPlatesActivated())
        {
            if (boulders.Count==0&&!isActivated)
            {
                for (int i = 0; i < boulders_to_spawn; i++)
                {
                    StartCoroutine("SpawnBoulder");
                   
                }
                isActivated = true;
            }
            else if(boulders.Count>0)
            {
                foreach (GameObject boulder in boulders)
                {
                    Destroy(boulder);
                }
                boulders.Clear();
                isActivated = false;
            }
            
        }
        

        
    }

    public IEnumerator SpawnBoulder()
    {
        yield return new WaitForSeconds(2f);
        GameObject a = Instantiate(boulder_prefab, transform.position, Quaternion.identity);
        boulders.Add(a);
    }
}
