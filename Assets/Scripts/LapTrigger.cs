using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapTrigger : MonoBehaviour
{
    public GameObject trigger;
    public GameObject trigger1;
    public GameObject trigger2;
    public GameObject trigger3;
    public GameObject finishLap;
       [Header("Info")]
     public float triggerCount;
     public float lapsComplete;
    
   void update()
   { 
    void OnTriggerEnter()
    {
        trigger.SetActive(false);
            Debug.Log("triggered");
                triggerCount += 1;
            /* if (triggerCount == 0)
             {
                 trigger.SetActive(false);
                 triggerCount += 1;
             }
             else if(triggerCount == 1)
             {
                 trigger1.SetActive(false);
                 triggerCount += 1;
             }
             else if (triggerCount == 2)
             {
                 trigger2.SetActive(false);
                 triggerCount += 1;
             }
             else if (triggerCount == 3)
             {
                 trigger3.SetActive(false);
                 triggerCount += 1;
             }
            */
        }
   }
}
