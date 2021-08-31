using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenGasReappear : MonoBehaviour
{
    [SerializeField] int waittime;
    public GameObject gas;
   public float invisibleLength;
    private float invisibleLengthCounter;
    public bool isCountingDown;

    // Start is called before the first frame update
    private void Start()
    {
        invisibleLengthCounter = invisibleLength;
        isCountingDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gas.activeSelf == false) {
            isCountingDown = true;
        }
        
        if (isCountingDown)
            {
                invisibleLengthCounter -= Time.deltaTime;
            }

        if (invisibleLengthCounter <= 0) { 
        
            gas.SetActive(true);
            isCountingDown = false;
            invisibleLengthCounter = invisibleLength;
            
        }
    }

   private void Reappear()
    {
        gas.SetActive(true);
        
    }
}
