using UnityEngine;

public class HeartBullet : MonoBehaviour
{
    public float Speed = 10f;

    public float commonSpeed = 10f;

    private void Start()
    {
        //�������κ��� N�� �� ����
        Destroy(gameObject, 20f);
    }

    private void Update()
    {
        //�ι�° �Ķ���Ϳ� Space.World�� �������ν� Rotation�� ���� ���� ������ ������
        transform.Translate(Vector2.right * (Speed * Time.deltaTime) * commonSpeed, Space.Self);
    }
}
