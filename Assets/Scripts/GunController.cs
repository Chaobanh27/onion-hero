using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    [SerializeField] private Transform gun;
    [SerializeField] private Animator gunAnimation;
    [SerializeField] private float gunDistance;

    private bool gunFacingRight = true;

    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    [SerializeField] private int currentBullets;
    [SerializeField] private int maxBullets = 30;

    private void Start()
    {
        currentBullets = maxBullets;
    }
    // Update is called once per frame
    void Update()
    {
        //Camera.main được sử dụng để truy cập camera chính trong Scene
        //Input.mousePosition trả về vị trí của chuột trên màn hình (tính từ góc dưới bên trái)
        //Camera.main.ScreenToWorldPoint(Input.mousePosition) chuyển đổi vị trí của chuột từ không gian màn hình sang không gian thế giới.
        Vector3 mousePositision = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Tính toán hướng từ vị trí chuột đến vị trí của súng
        //gun.position là vị trí của súng trong không gian thế giới
        //mousePosition - gun.position tính toán vector hướng từ vị trí của chuột đến vị trí của súng.
        //Kết quả là một vector chỉ ra hướng từ súng tới vị trí chuột.
        Vector3 direction = mousePositision - transform.position;


        //Quaternion.Euler được sử dụng để tạo một Quaternion từ các góc Euler (x, y, z)
        //Mathf.Atan2(direction.y, direction.x) tính toán góc giữa trục x và vector direction trong radian
        //Mathf.Rad2Deg được sử dụng để chuyển đổi từ radian sang độ
        //Kết quả này được sử dụng để xác định góc quay quanh trục Z cần thiết để đưa gun về hướng của vector direction
        //Gán giá trị Quaternion mới được tạo từ góc Euler vào gun.rotation, điều này sẽ thay đổi hướng quay của súng.
        gun.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gunDistance = 1.2f;
        //Quaternion.Euler(0, 0, angle): Tạo một Quaternion từ góc angle theo trục z để biểu diễn quay theo góc đó
        //new Vector3(gunDistance, 0, 0): Tạo một vector mới đại diện cho khoảng cách và hướng theo trục x mà khẩu súng sẽ được di chuyển.
        //transform.position + ...: Cộng vector mới này với vị trí hiện tại của đối tượng chứa mã này để xác định vị trí mới của khẩu súng,
        //đảm bảo rằng khẩu súng sẽ được đặt ở một khoảng cách cố định gunDistance và hướng theo angle
        gun.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(gunDistance, 0, 0);

        GunFlipController(mousePositision);

        if (Input.GetKeyDown(KeyCode.Mouse0) && HaveBullets())
        {
            Shoot(direction);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            ReloadGun();
        }
    }
    //hàm xử lý khi nào cần flip súng
    private void GunFlipController(Vector3 mousePositision)
    {
        if (mousePositision.x < gun.position.x && gunFacingRight)
        {
            GunFlip();
        }
        else if (mousePositision.x > gun.position.x && !gunFacingRight)
        {
            GunFlip();
        }
    }

    //hàm flip súng theo đúng hướng
    private void GunFlip()
    {
        gunFacingRight = !gunFacingRight;
        gun.localScale = new Vector3(gun.localScale.x, gun.localScale.y * - 1, gun.localScale.z);
    }

    public void Shoot(Vector3 direction)
    {
        gunAnimation.SetTrigger("Shoot");
        //Tạo một đối tượng mới từ prefab bulletPrefab tại vị trí của khẩu súng (gun.position) mà không xoay (Quaternion.identity).
        GameObject newBullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
        //Lấy component Rigidbody2D từ đối tượng viên đạn mới tạo ra và thiết lập vận tốc của nó theo hướng chuẩn hóa (normalized) của vector direction nhân với bulletSpeed.
        //Điều này sẽ đặt vận tốc của viên đạn để nó di chuyển theo hướng chỉ định với tốc độ xác định.
        newBullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;
        //hủy bullet sau 5s
        Destroy(newBullet, 5);
        UIController.Instance.UpdateAmmoInfo(currentBullets, maxBullets);
    }
    //hàm nạp đạn cho súng
    private void ReloadGun()
    {
        currentBullets = maxBullets;
        UIController.Instance.UpdateAmmoInfo(currentBullets, maxBullets);
    }
  
    //hàm kiểm tra có đạn không
    public bool HaveBullets()
    {
        if(currentBullets <= 0)
        {
            return false;
        }
        currentBullets--;
        return true;
    }
}
