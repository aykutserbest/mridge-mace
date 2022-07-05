using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGeneralController : MonoBehaviour
{
    public static Vector3 EmptySlotPos;
    public static GameObject brickSlotObj;
    private float _brickSlotObjYPos;
    [SerializeField] private float multiplier;

    private void Start()
    {
        multiplier = 1;
        brickSlotObj = GameObject.Find("BrickSlot");
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
        //valuable for y / digerlerini sabitleme
        
        EmptySlotPos = brickSlotObj.transform.position;
        _brickSlotObjYPos = EmptySlotPos.y + (0.1f * multiplier);
        EmptySlotPos = new Vector3(EmptySlotPos.x, _brickSlotObjYPos, EmptySlotPos.z);
        multiplier++;
    }
    
    
}
