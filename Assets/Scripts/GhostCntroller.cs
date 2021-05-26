using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCntroller : MonoBehaviour
{
    public Vector3 back;
    public Vector3 forth;
    float phase=0;
    public float speed;
    float phaseDirection = 1;





    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {//Makes the ghost character go back and forth along the X Axis.

        transform.position = Vector3.Lerp(back, forth, phase);
        phase += Time.deltaTime * speed * phaseDirection;
        if(phase>=1|| phase <= 0)
        {//This reverses the direction the character moves.
            phaseDirection *= -1;
        }
        

        }


}