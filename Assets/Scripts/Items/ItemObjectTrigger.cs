using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectTrigger : MonoBehaviour
{

    private ItemObj item => GetComponentInParent<ItemObj>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
           item.PickUpItem();
        }
    }
}
