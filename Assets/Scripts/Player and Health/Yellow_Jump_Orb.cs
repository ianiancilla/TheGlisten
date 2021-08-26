using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow_Jump_Orb : MonoBehaviour
{
    [SerializeField] int waittime;
    public GameObject gas;
    
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (gas.activeSelf==false)
        {
            
            Invoke("Reappear", waittime);
        }
    }

   private void Reappear()
    {
        gas.SetActive(true);
        
    }
}
