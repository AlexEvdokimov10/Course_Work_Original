using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public InventorySlot oldSlot;
    private Transform player;

    private void Start()
    {
        
        FindPlayer(player);
       
        oldSlot = transform.GetComponentInParent<InventorySlot>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        
        if (oldSlot.isEmpty)
        {
            return;
        }
        GetComponent<RectTransform>().position += new Vector3(eventData.delta.x, eventData.delta.y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
        {
            return;
        }
        
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
        
        GetComponentInChildren<Image>().raycastTarget = false;
        
        transform.SetParent(transform.parent.parent);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (oldSlot.isEmpty)
        {
            return;
        }
       
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
        
        GetComponentInChildren<Image>().raycastTarget = true;

        transform.SetParent(oldSlot.transform);
        transform.position = oldSlot.transform.position;
        
        if (eventData.pointerCurrentRaycast.gameObject.name == "UIPanel")
        {
            
            GameObject itemObject = Instantiate(oldSlot.item.itemPrefab, player.position , Quaternion.identity);

            
            itemObject.transform.position = new Vector3(player.position.x, player.position.y, 0.0f);
            itemObject.GetComponent<OpenThings>().amount = oldSlot.amount;
            
            NullifySlotData();
        }
        else if(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>() != null)
        {
            
            ExchangeSlotData(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<InventorySlot>());
        }
       
    }
    public void NullifySlotData()
    {
        
        oldSlot.item = null;
        oldSlot.amount = 0;
        oldSlot.isEmpty = true;
        oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        oldSlot.iconGO.GetComponent<Image>().sprite = null;
        oldSlot.itemAmount.text = "";
    }
    void ExchangeSlotData(InventorySlot newSlot)
    {
        
        Things item = newSlot.item;
        int amount = newSlot.amount;
        bool isEmpty = newSlot.isEmpty;
        GameObject iconGO = newSlot.iconGO;
        TMP_Text itemAmountText = newSlot.itemAmount;

        
        newSlot.item = oldSlot.item;
        newSlot.amount = oldSlot.amount;
        if (oldSlot.isEmpty == false)
        {
            newSlot.SetIcon(oldSlot.iconGO.GetComponent<Image>().sprite);
            newSlot.itemAmount.text = oldSlot.amount.ToString();
        }
        else
        {
            newSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            newSlot.iconGO.GetComponent<Image>().sprite = null;
            newSlot.itemAmount.text = "";
        }
        
        newSlot.isEmpty = oldSlot.isEmpty;

        
        oldSlot.item = item;
        oldSlot.amount = amount;
        if (isEmpty == false)
        {
            oldSlot.SetIcon(iconGO.GetComponent<Image>().sprite);
            oldSlot.itemAmount.text = amount.ToString();
        }
        else
        {
            oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            oldSlot.iconGO.GetComponent<Image>().sprite = null;
            oldSlot.itemAmount.text = "";
        }
        
        oldSlot.isEmpty = isEmpty;
    }
    public void FindPlayer(Transform transform)
    {
        transform = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
