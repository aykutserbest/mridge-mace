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
    private int _index;
    private GameObject _lastItem;

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
    }
    
    void MoveToBridge(GameObject targetSlot, int index)
    {
        var lastItemIndex = characterCarryBricks.Count-1;
        if (lastItemIndex <= -1) return;
        
      //  Debug.Log("ilk index count -1 li : " + lastItemIndex);
        
        var obj = characterCarryBricks[lastItemIndex];
        var transform1 = targetSlot.transform;
        
        _lastItem = characterCarryBricks[lastItemIndex];
        
        obj.transform.parent = targetSlot.transform;
        
        obj.transform.DOLocalMove(new Vector3(
           0,
           0,
           0
        ),1).OnComplete(BridgeBrickProcess);
        
        obj.transform.rotation = new Quaternion(0, 0, 0,0);
        
        
        
        characterCarryBricks.RemoveAt(characterCarryBricks.Count-1);
        
        _multiplier--;
       // StartCoroutine(BridgeBrickProcess(lastItem));
    }

    void BridgeBrickProcess()
    {
        
        
        _lastItem.transform.GetComponent<BrickController>().enabled = false;
        _lastItem.transform.GetComponent<BoxCollider>().enabled = true;
        _lastItem.transform.GetComponent<BoxCollider>().isTrigger = false;
        
        if (characterCarryBricks != null) EventManager.ContinueBrickToBridge?.Invoke();
    }
}
