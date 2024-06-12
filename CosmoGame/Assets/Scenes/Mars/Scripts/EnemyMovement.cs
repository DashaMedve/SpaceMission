using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f; // �������� �������� ����������
    private Transform target; // ���� (���������)
    private int flag_died = 0; // ���� ��� ������������ �������� ����������

    void Start()
    {
        // ������� ������ � ����� "Player" � ������������� ��� ��� ����
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // ���������, ����������� �� ����
        if (target != null)
        {
            // ������������ ����������� � ���� � ����������� ���
            Vector3 direction = (target.position - transform.position).normalized;
            // ���������� ���������� � ����������� ���� � �������� ���������
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ���������, ��������� �� ������������ � ����������� (��� ������� �������� "Marsian")
        if (collision.gameObject.name.Contains("Marsian"))
        {
            // ���� ������� ������ - ��� �� ������������ ��������� (��� �� "Marsian")
            if (gameObject.name != "Marsian")
            {
                // ������� ������� ������ (���������)
                Destroy(gameObject);
                // ������������� ����, �����������, ��� ������� ������ ��� ������
                flag_died = 1;
            }
            // ���� ������ ������������ - ��� �� ������������ ��������� (��� �� "Marsian") � ������� ������ ��� �� ��� ������
            if (collision.gameObject.name != "Marsian" && flag_died == 0)
            {
                // ������� ������ ������������ (������ ���������)
                Destroy(collision.gameObject);
            }
            // ���������� ���� ����� ��������� ������������
            flag_died = 0;
        }
    }
}
