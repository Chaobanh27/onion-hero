using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    //hàm được gọi khi một trigger collider bắt đầu va chạm với Collider khác
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Target")
        {
            UIController.Instance.OpenEndScreen();
        }
    }
}
