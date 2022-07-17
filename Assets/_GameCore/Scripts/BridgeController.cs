using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    [SerializeField] private GameObject[] bridgeSlotArray;
    private GameObject _zombiBrickSlot;
    private int _zombiBrickSlotChildCount;
    private int _index;
    private bool _isStayCollider;

    private void OnEnable()
    {
        EventManager.ContinueBrickToBridge += StartPut;
    }

    private void OnDisable()
    {
        EventManager.ContinueBrickToBridge -= StartPut;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        _zombiBrickSlot = other.transform.GetChild(2).GetChild(0).gameObject;

        if (_zombiBrickSlot.transform.childCount <= 0) return;

        //Debug.Log(_zombiBrickSlot.transform.IsChildOf(transform));
        
        _isStayCollider = true;
        PutBrickAtBridge();
    }
    
    void PutBrickAtBridge()
    {
        if (_index >= bridgeSlotArray.Length) return;
        if (_zombiBrickSlot.transform.childCount <= 0) return;
       
        EventManager.PutBrickAtBridge?.Invoke(bridgeSlotArray[_index],_index); 
        _index++;
    }
    
    void StartPut()
    {
        if (!_isStayCollider) return;
        PutBrickAtBridge();
    }
    
    private void OnTriggerExit(Collider other)
    {
        _isStayCollider = false;
    }

/*  void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!isTriggerStay) return;

        _zombiBrickSlot = other.transform.GetChild(2).GetChild(0).gameObject;
        
        if (_zombiBrickSlot.transform.IsChildOf(transform)) return;

        StartCoroutine(WaitForStaySeconds());
    }

    IEnumerator WaitForStaySeconds()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("5 sn bekledi");
        PutBrickAtBridge();
    }*/

}
