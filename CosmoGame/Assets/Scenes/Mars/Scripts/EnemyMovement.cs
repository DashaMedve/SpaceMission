using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f; // �������� ��������
    private Transform target; // ���� (���������)

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �������� �� ��� Marsian(Clone)
        if (collision.gameObject.name == "Marsian(Clone)")
        {
            Debug.Log("�������� �����������!");

            // �������� �� ��, ��� ������� ������ �� �������� ������������ �����������
            if (gameObject.name != "Marsian")
            {
                Destroy(gameObject);
            }

            // ������� ���� ����������
            Destroy(collision.gameObject);
        }
    }
}
