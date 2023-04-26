using UnityEngine;

public class HeartBullet : MonoBehaviour
{
    public float Speed = 10f;

    public float commonSpeed = 10f;

    private void Start()
    {
        //생성으로부터 N초 후 삭제
        Destroy(gameObject, 20f);
    }

    private void Update()
    {
        //두번째 파라미터에 Space.World를 해줌으로써 Rotation에 의한 방향 오류를 수정함
        transform.Translate(Vector2.right * (Speed * Time.deltaTime) * commonSpeed, Space.Self);
    }
}
