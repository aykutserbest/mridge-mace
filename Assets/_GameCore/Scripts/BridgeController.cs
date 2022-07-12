using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    [SerializeField] private GameObject[] bridgeSlotArray;
    private GameObject _zombiBrickSlot;
    private int _index;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        _zombiBrickSlot = other.transform.GetChild(2).GetChild(0).gameObject;

        //bool haveChild = _zombiBrickSlot.transform.GetChild(0).gameObject;
        if (_zombiBrickSlot.transform.IsChildOf(transform)) return;

        PutBrickAtBridge();
    }
    
    void PutBrickAtBridge()
    {
        if (_index > bridgeSlotArray.Length) return;
        EventManager.PutBrickAtBridge?.Invoke(bridgeSlotArray[_index],_index); 
        _index++;
    }
    
    
}
