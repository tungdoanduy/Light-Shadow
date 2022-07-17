using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Mouse : MonoBehaviour
{
    GameObject mouseVFX;
    private void Start()
    {
        mouseVFX = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseVFX.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }
}
