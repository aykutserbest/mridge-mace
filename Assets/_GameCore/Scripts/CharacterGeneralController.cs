using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGeneralController : MonoBehaviour
{
    public static GameObject brickSlotObj;
    public static float _brickSlotObjYPos;
    
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
        
        EventManager.OnBrickEnter += SetEmpySlotPos;
    }

    private void OnDisable()
    {
        EventManager.OnBrickEnter -= SetEmpySlotPos;
    }

    void SetEmpySlotPos()
    {
        _brickSlotObjYPos = brickSlotObj.transform.localPosition.y + (0.6f * _multiplier);
        _multiplier++;
        
        Debug.Log("_brickSlotObjYPos "+_brickSlotObjYPos + " multiplier " + _multiplier);
    }
    
    
}
