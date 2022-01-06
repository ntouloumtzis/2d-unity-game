using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : MonoBehaviour 
{

    private void Start()
    {
        // the actual cursor is hidden
        Cursor.visible = false;
    }

    private void Update()
    {
        // the cursor's UI position equal to our mouse position
        transform.position = Input.mousePosition;
    }
}