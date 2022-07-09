using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BrickController : MonoBehaviour
{
    [SerializeField] private CharacterGeneralController.ColorEnum brickColor;
    
    private void Start()
    {
        DOTween.Init();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerColor = other.GetComponent<CharacterGeneralController>().characterEnum;
            if (playerColor==brickColor)
            {
                StartCoroutine(DisableTrailEffect());
                gameObject.GetComponent<BoxCollider>().enabled = false;

                EventManager.OnBrickEnter?.Invoke();
            
                var obj = gameObject;
                obj.transform.DOLocalMove(new Vector3(
                    CharacterGeneralController.brickSlotObj.transform.localPosition.x,
                    CharacterGeneralController._brickSlotObjYPos,
                    CharacterGeneralController.brickSlotObj.transform.localPosition.z
                ),1);
            
                obj.transform.parent = CharacterGeneralController.brickSlotObj.transform;
                obj.transform.rotation = new Quaternion(0, 0, 0,0);
            }
        }
    }

    IEnumerator DisableTrailEffect()
    {
       yield return new WaitForSeconds(1);
       gameObject.GetComponent<TrailRenderer>().enabled = false;
    }
    
}
