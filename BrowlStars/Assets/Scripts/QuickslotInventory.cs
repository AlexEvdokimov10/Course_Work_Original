using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickslotInventory : MonoBehaviour
{
  
    public Transform quickslotParent;
    public InventoryManager inventoryManager;
    public int currentQuickslotID = 0;
    public Sprite selectedSprite;
    public Sprite notSelectedSprite;
    public Gamer gamer;
    public Transform transform;
    public bool test = false;
    public InventorySlot activeSlot = null;
    public Transform allWeapons;

    
    void Update()
    {
        float mw = Input.GetAxis("Mouse ScrollWheel");
        
        if (mw > 0.1)
        {
            
            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
            
            if (currentQuickslotID >= quickslotParent.childCount-1)
            {
                currentQuickslotID = 0;
            }
            else
            {
                
                currentQuickslotID++;
            }
            
            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
           
            activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
            ShowItemInHand();

        }
        if (mw < -0.1)
        {
            
            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
            
            if (currentQuickslotID <= 0)
            {
                currentQuickslotID = quickslotParent.childCount-1;
            }
            else
            {
                
                currentQuickslotID--;
            }
            
            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
           
            activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
            ShowItemInHand();
        }
        
        for(int i = 0; i < quickslotParent.childCount; i++)
        {
            
            if (Input.GetKeyDown((i + 1).ToString())) {
                
                if (currentQuickslotID == i)
                {

                   
                    if (quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite == notSelectedSprite)
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
                        activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();

                        ShowItemInHand();
                    }
                    else
                    {
                        
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                        activeSlot = null;
                        HideItemsInHand();

                    }
                    
                }
               
                else
                {
                    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                    currentQuickslotID = i;
                    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
                    activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
                    ShowItemInHand();
                }
            }
        }
       
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().item != null)
            {
               
                if (quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().item.isConsumeable && quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite == selectedSprite)
                {
                    
                    
                    ChangeCharacteristics();

                    if (quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().amount <= 1)
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponentInChildren<DragAndDropItem>().NullifySlotData();
                    }
                    else
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().amount--;
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().itemAmount.text = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().amount.ToString();
                    }
                }
            }
        }
       
        
    }

    private void ChangeCharacteristics()
    {
         
        if( gamer.currentfood<100)
        {
            float newHealth = gamer.currentfood + quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().item.changeHealth;
            gamer.currentfood = (int)newHealth;
        }
       
        else
        {
            gamer.currentlives += 20;
        }
    }
    public void CheckItemInHand()
    {
        if (activeSlot != null)
        {
            ShowItemInHand();
        }
        else
        {
            HideItemsInHand();
        }
    }
    private void ShowItemInHand()
    {
        HideItemsInHand();
        if (activeSlot.item == null)
        {
            return;
        }
        for (int i = 0; i < allWeapons.childCount; i++)
        {
            if (activeSlot.item.inHandName == allWeapons.GetChild(i).name)
            {
                allWeapons.GetChild(i).gameObject.SetActive(true);
               
            }
        }
    }
    private void HideItemsInHand()
    {
        for (int i = 0; i < allWeapons.childCount; i++)
        {
            allWeapons.GetChild(i).gameObject.SetActive(false);
        }
    }
   
   
   
}
