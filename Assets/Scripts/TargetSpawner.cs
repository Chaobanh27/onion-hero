using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private Sprite[] targetSprite;
    [SerializeField] private BoxCollider2D cd;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private float cooldown;
    public float timer;

    [SerializeField] private int sushiCreated;
    [SerializeField] private int sushiMilestone = 20;

    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime;

        if(timer < 0)
        {
            timer = cooldown;
            sushiCreated++;

            if(sushiCreated > sushiMilestone && cooldown > 0.5f)
            {
                sushiMilestone = sushiMilestone + 20;
                cooldown = cooldown - 0.1f;
            }

            //targetPrefab: Đây là prefab mà mình muốn tạo một bản sao
            //Instantiate(targetPrefab): Hàm này tạo một bản sao của prefab được chuyển vào và trả về một tham chiếu đến đối tượng mới được tạo ra.
            //biến newTarget sẽ chứa tham chiếu đến đối tượng mới được tạo ra từ prefab targetPrefab
            GameObject newTarget = Instantiate(targetPrefab);

            //cd.bounds.min/max.x: Đây là giá trị x tối thiểu/tối đa của ranh giới của một Collider
            float randomX = Random.Range(cd.bounds.min.x, cd.bounds.max.x);

            //vị trí đối tượng mới sẽ là vector2 với x là ngẫu nhiên còn y giữ nguyên tọa độ của đối tượng gốc.
            newTarget.transform.position = new Vector2(randomX, transform.position.y);

            //lấy index ngẫu nhiên trong khoảng
            int randomIndex = Random.Range(0, targetSprite.Length);

            //.sprite: Đây là thuộc tính của SpriteRenderer được sử dụng để gán sprite mới cho đối tượng SpriteRenderer
            //targetSprite: Đây là mảng chứa các sprite mà bạn muốn chọn một sprite từ đó
            //thay đổi hình ảnh của đối tượng mới được tạo ra thành một sprite ngẫu nhiên từ mảng targetSprite.
            newTarget.GetComponent<SpriteRenderer>().sprite = targetSprite[randomIndex];
        }
    }
}
