using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject chracterUI;
    [SerializeField] private GameObject skillUI;
    [SerializeField] private GameObject craftUI;
    [SerializeField] private GameObject optionUI;
    //private bool isPaused = false;

    private void Start()
    {
        SwitchUI(null);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
            SwitchWithKey(chracterUI);
    }

    public void SwitchUI(GameObject _menu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.name != "IngameUI")
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if(_menu != null)
        {
            _menu.SetActive(true);
        }
    }

    public void SwitchWithKey(GameObject _menu)
    {
        if(_menu != null && _menu.activeSelf)
        {
       

            _menu.SetActive(false) ;

            return;
        }

        SwitchUI(_menu);
  

    }
}
 