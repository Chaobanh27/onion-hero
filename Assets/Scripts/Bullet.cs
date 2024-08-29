using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        //ui = GameObject.Find("Canvas").GetComponent<UIController>();
    }
    // Update is called once per frame
    void Update()
    {
        //Dòng này gán hướng của đối tượng để trùng với hướng di chuyển hiện tại của Rigidbody2D, thường được sử dụng để căn chỉnh hướng của đối tượng theo hướng di chuyển.
        rb = GetComponent<Rigidbody2D>();
        transform.up = rb.velocity;
    }

    //phương thức OnTriggerEnter2D(Collider2D collision) được gọi khi một đối tượng khác tiếp xúc với Collider của đối tượng chứa script hiện tại
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Kiểm tra xem đối tượng va chạm có tag là "Target" hay không. Nếu có, điều kiện trong if sẽ được thực thi
        if (collision.tag == "Target")
        {
            //Destroy(gameObject): Hủy đối tượng chứa script hiện tại (ví dụ: viên đạn). Điều này có nghĩa là khi viên đạn va chạm với mục tiêu, viên đạn sẽ bị hủy
            Destroy(gameObject);
            //Destroy(collision.gameObject): Hủy đối tượng mà viên đạn va chạm vào (mục tiêu).
            //Trong trường hợp này, nếu viên đạn va chạm với mục tiêu có tag là "Target", cả viên đạn và mục tiêu đều sẽ bị hủy.
            Destroy(collision.gameObject);

            UIController.Instance.AddScore();
        }
    }
}
