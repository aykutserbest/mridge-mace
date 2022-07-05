using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            EventManager.OnBrickEnter?.Invoke();
            
            var obj = gameObject;
            
           
            StartCoroutine (ToBrickSlotMove (CharacterGeneralController.EmptySlotPos));
            obj.transform.parent = CharacterGeneralController.brickSlotObj.transform;
            
            obj.transform.rotation = new Quaternion(0, 0, 0,0);
          
        }
 
        IEnumerator  ToBrickSlotMove(Vector3 target)
        {
            float way = 0;
            Vector3 startPos = transform.position;
            while (way <= 1f) {
                transform.position = Vector3.Lerp (startPos, target, way);
                way += 15*Time.deltaTime;
                yield return null;
            }
        }
       
    }
    
    // obj.transform.position = Vector3.Lerp(obj.transform.position, CharacterGeneralController.EmptySlotPos, Time.deltaTime);
    // obj.transform.position = CharacterGeneralController.EmptySlotPos;
    
}
