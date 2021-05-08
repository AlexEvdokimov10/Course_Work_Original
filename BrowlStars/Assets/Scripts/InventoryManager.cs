using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InventoryManager : MonoBehaviour
{
    public GameObject UIpanel;
    public Gamer gamer;
    public Transform inventoryPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    public bool isOpened;
    private Camera mainCamera;
    public float reachDistance=10f;
    RaycastHit2D hit;
    public Things things;
   

    private void Awake()
    {
        UIpanel.SetActive(true);
    }
    void Start()
    {
        mainCamera = Camera.main;
        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            if (inventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
        UIpanel.SetActive(false);
    }

  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpened = !isOpened;
            if (isOpened)
            {
                UIpanel.SetActive(true);
                inventoryPanel.gameObject.SetActive(true);
            }
            else
            {
                UIpanel.SetActive(false);
                inventoryPanel.gameObject.SetActive(false);
            }
        }
        hit = Physics2D.Raycast(gamer.transform.position, Vector2.right * gamer.transform.localScale.x, reachDistance);
        if (Input.GetKeyDown(KeyCode.E)){
            if (Physics2D.Raycast(gamer.transform.position, Vector2.right * gamer.transform.localScale.x, reachDistance))
            {
                Debug.DrawRay(gamer.transform.position, Vector3.right * gamer.transform.localScale.x * reachDistance, Color.green);
                if (hit.collider.gameObject.GetComponent<OpenThings>() != null)
                {
                    AddItem(hit.collider.gameObject.GetComponent<OpenThings>().itemOpen, hit.collider.gameObject.GetComponent<OpenThings>().amount);
                    Destroy(hit.collider.gameObject);
                    Debug.DrawRay(gamer.transform.position, Vector3.right * gamer.transform.localScale.x * reachDistance, Color.black);
                }
            }
            else
            {

                Debug.DrawRay(gamer.transform.position, Vector3.right * gamer.transform.localScale.x * reachDistance, Color.red);
            }
        }
       
    }
    private void AddItem(Things tempItem,int tempAmount)
    {
        
        foreach(InventorySlot slot in slots)
        {
            if (slot.item==tempItem)
            {
                if (slot.amount + tempAmount <= tempItem.maxAmount)
                {
                    slot.amount += tempAmount;
                    slot.itemAmount.text = slot.amount.ToString();
                    return;
                }
                break;
            }

        }
        foreach(InventorySlot slot in slots)
        {
            if (slot.item == null)
            {
                slot.item = tempItem;
                slot.amount = tempAmount;
                slot.isEmpty = false;
                slot.SetIcon(tempItem.icon);
                slot.itemAmount.text = tempAmount.ToString();
                return;
            }
        }
    }
   
}
