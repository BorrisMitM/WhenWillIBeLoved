using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestRiddle : MonoBehaviour
{


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            FindObjectOfType<RiddlePanel>().Activate();
        }

    }
}
