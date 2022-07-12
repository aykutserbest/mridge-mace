using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterGeneralController : MonoBehaviour
{
    public static GameObject brickSlotObj;
    public static float _brickSlotObjYPos;
    public static List<GameObject> characterCarryBricks  = new List<GameObject>();
    
    public enum ColorEnum {  Red, Yellow, Blue };

    [SerializeField] public ColorEnum characterEnum;
    
    
    
    private float _multiplier;

    private void Start()
    {
        _multiplier = 1;
        brickSlotObj = GameObject.Find("slot1");
    }

    private void OnEnable()
    {
        EventManager.PutBrickAtBridge += MoveToBridge;
        EventManager.OnBrickEnter += SetEmpySlotPos;
    }

    private void OnDisable()
    {
        EventManager.PutBrickAtBridge -= MoveToBridge;
        EventManager.OnBrickEnter -= SetEmpySlotPos;
    }

    void SetEmpySlotPos()
    {
        _brickSlotObjYPos = brickSlotObj.transform.localPosition.y + (0.6f * _multiplier);
        _multiplier++;
        
        Debug.Log("_brickSlotObjYPos "+_brickSlotObjYPos + " multiplier " + _multiplier);
    }
    
    void MoveToBridge(GameObject targetSlot, int index)
    {
        var obj = characterCarryBricks[index];
        var transform1 = targetSlot.transform;
        obj.transform.parent = targetSlot.transform;
        
        obj.transform.DOLocalMove(new Vector3(
           0,
           0,
           0
        ),1);
        
        /*
        obj.transform.DOLocalMove(new Vector3(
            transform1.localPosition.x,
            transform1.localPosition.y,
            transform1.localPosition.z
        ),1);*/
        
        
        
        obj.transform.rotation = new Quaternion(0, 0, 0,0);
        
    }
    
}
