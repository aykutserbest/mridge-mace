using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BrickController : MonoBehaviour
{
    [SerializeField] public CharacterGeneralController.ColorEnum brickColor;
   
     
    private GameObject _thisBrickClone;
    private Transform _thisBrickCloneParent;
    
    private void Start()
    {
        var o = gameObject;
        _thisBrickClone = o;
        _thisBrickCloneParent = o.transform.parent;
        DOTween.Init();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        var playerColor = other.GetComponent<CharacterGeneralController>().characterEnum;

        if (playerColor != brickColor) return;

        GenerateBrick();
        
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

        CharacterGeneralController.characterCarryBricks.Add(obj);
    }

    IEnumerator DisableTrailEffect()
    {
       yield return new WaitForSeconds(1);
       gameObject.GetComponent<TrailRenderer>().enabled = false;
    }

    void GenerateBrick()
    {
        var disabledCloneBrick = Instantiate(_thisBrickClone,_thisBrickCloneParent);
        disabledCloneBrick.SetActive(false);
        StartCoroutine(SpawnBrick(disabledCloneBrick));
    }

    IEnumerator SpawnBrick(GameObject cloneBrick)
    {
        yield return new WaitForSeconds(3);
        cloneBrick.SetActive(true);
    }
    
   
    
    
}
