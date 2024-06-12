using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SC_PuzzleGenerator : MonoBehaviour
{
    public Texture[] elements; // ������� ��������� �����
    public int totalColumns = 7; // ���������� �������� � �����
    public int totalRows = 7; // ���������� ����� � �����
    public Texture textureBackground; // �������� ��� ���� �����

    // ����� ��� ������������� �������� �����
    [System.Serializable] //� Unity ������� [System.Serializable] ������������ ��� ����, ����� ���� ������ ���� ������ � ������������ � ���������� Unity
    public class PuzzleElement
    {
        public Texture texture; // �������� ��������
        public Vector2 position; // ������� ��������
    }

    // ������ ��� �������� �������� � �� ���������
    List<List<PuzzleElement>> columns = new List<List<PuzzleElement>>();

    int selectedColumn = -1; // ������ ���������� ������� 
    int selectedRow = -1; // ������ ��������� ������ 
    int score; // ������� ���� ������
    int steps = 30; // ���������� ���������� �����


    // ��������� �������������
    void Start()
    {
        // ������������� �������
        for (int x = 0; x < totalColumns; x++)
        {
            List<PuzzleElement> column = new List<PuzzleElement>();
            // ������������� �����
            for (int y = 0; y < totalRows; y++)
            {
                PuzzleElement element = new PuzzleElement();
                element.texture = elements[Random.Range(0, elements.Length)]; // ��������� ��������� ��������
                element.position = new Vector2(x, y);
                column.Add(element);
            }
            columns.Add(column);
        }

        StartCoroutine(RestockEnumrator());
    }

    void OnGUI()
    {
        if (steps > 0) // ���������, ���� ���������� ����� ������ 0
        {
            for (int x = 0; x < columns.Count; x++)
            {
                for (int y = 0; y < columns[x].Count; y++)
                {
                    if (columns[x][y].texture)
                    {
                        // ����� �������� �������...
                        columns[x][y].position = Vector2.Lerp(columns[x][y].position, new Vector2((Screen.width / 2 - (columns.Count * 100) / 2) + x * 100 - 350, (Screen.height / 2 - (columns[x].Count * 100) / 2) + y * 100), Time.deltaTime * 7);
                        // ������ ����� �������������
                        Rect elementRect = new Rect(columns[x][y].position.x, columns[x][y].position.y, 100, 100);

                        // ���������, ���� ������� �������� � ���������
                        if ((x == selectedColumn && (y == selectedRow - 1 || y == selectedRow + 1)) || ((x == selectedColumn - 1 || x == selectedColumn + 1) && y == selectedRow))
                        {
                            // ���� ������ ������ �� ��������
                            if (GUI.Button(elementRect, columns[x][y].texture))
                            {
                                // �������� �������� �������
                                PuzzleElement tmpElement = columns[x][y];
                                columns[x][y] = columns[selectedColumn][selectedRow];
                                columns[selectedColumn][selectedRow] = tmpElement;
                                selectedColumn = -1;
                                selectedRow = -1;
                                StopCoroutine(DetectCombos());
                                StartCoroutine(DetectCombos());
                                steps--;  // ��������� ���������� �����
                            }
                        }
                        else
                        {
                            // ���������, �������� �� ������������� �������� ������� ������� ����
                            if (elementRect.Contains(Event.current.mousePosition))
                            {
                                // ���� ������� ���� ��������� ������ ��������������, ��������� GUI �������
                                GUI.enabled = false;
                                // ���� ���� ������ ����� ������ ����
                                if (Input.GetMouseButtonDown(0))
                                {
                                    // ���������� ������� ���������� ������� � ������
                                    selectedColumn = x;
                                    selectedRow = y;
                                }
                            }
                            // ���� ������� ������� �������� ���������
                            if (x == selectedColumn && y == selectedRow)
                            {
                                // ��������� GUI ������� ��� ���������� ��������
                                GUI.enabled = false;
                            }
                            // ������������ GUI Box ��� �������� �������� ����� � �������������� ��� ��������
                            GUI.Box(elementRect, columns[x][y].texture);
                            // �������� ������� GUI �������
                            GUI.enabled = true;

                        }
                    }
                }
            }

            // ����������� ����, ����� � ���������� �����
            GUI.Box(new Rect(Screen.width - 600, 350, 400, 400), textureBackground);
            GUI.Label(new Rect(Screen.width - 600, 300, 400, 300), "Score: " + score.ToString(), new GUIStyle("Label")
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 80
            });
            GUI.Label(new Rect(Screen.width - 600, 450, 400, 400), "Steps: " + steps.ToString(), new GUIStyle("Label")
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 80
            });
        }
        else
        {
            // ���������� ��������� "Game Over"
            GUI.Label(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 300, 600, 200), "Game Over", new GUIStyle("Label")
            {
                alignment = TextAnchor.MiddleCenter, // ������������ ������ �� ������
                fontSize = 100 // ������ ������
            });

            // ���������� ��������� ���� ��� ���������� "Game Over"
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 100, 300, 100), "Final Score: " + score.ToString(), new GUIStyle("Label")
            {
                alignment = TextAnchor.MiddleCenter, // ������������ ������ �� ������
                fontSize = 50 // ������ ������
            });

            // ����� ��� ������
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = 30; // ������������� ������ ������ ������

            // ������ �����������
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 120, 200, 70), "Restart", buttonStyle))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            // ������ ������
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 200, 200, 70), "Exit", buttonStyle))
            {
                SceneManager.LoadScene("Space");
            }
        }
    }

    // ���������, ����� �� �������� ������ ���������
    IEnumerator CompressElements()
    {
        bool compressionNeeded = false;
        // �������� �� ������� �������
        for (int x = 0; x < columns.Count; x++)
        {
            // �������� �� ������� �������� � �������
            for (int y = 1; y < columns[x].Count; y++)
            {
                // ���� ������� ������� ������, � ���������� ��������, ������, ������ ���������
                if (!columns[x][y].texture && columns[x][y - 1].texture)
                {
                    compressionNeeded = true;
                }
            }
        }

        // ���� ������ ���������
        if (compressionNeeded)
        {
            // ���� 0.25 ������� ����� ������� ������ (��� ����������� �������)
            yield return new WaitForSeconds(0.25f);

            // �������� �� ������� �������
            for (int x = 0; x < columns.Count; x++)
            {
                int referenceIndex = -1;
                // �������� �� ������� �������� � ������� (����� �����)
                for (int y = columns[x].Count - 1; y >= 0; y--)
                {
                    // ���� ������� ������� ������
                    if (!columns[x][y].texture)
                    {
                        // ���� ��� ������ ������ ������� � �������, ���������� ��� ������
                        if (referenceIndex == -1)
                        {
                            referenceIndex = y;
                        }
                    }
                    // ���� ������� ������� ��������
                    else
                    {
                        // ���� ���� ������ �������� ���� ��������, ������� �������� ����
                        if (referenceIndex != -1)
                        {
                            // ���������� ������� ������� � ������ ����� ����
                            columns[x][referenceIndex].texture = columns[x][y].texture;
                            columns[x][referenceIndex].position = columns[x][y].position;
                            // ������� ������� �������
                            columns[x][y].texture = null;
                            // ��������� � ���������� ������� ����� ����
                            referenceIndex--;
                        }
                    }
                }
            }
        }

        // ��������� ������������ ��������� ����� ������
        StartCoroutine(RestockEnumrator());
    }


    // ���������� ����� ��������� � ������ ������
 
    IEnumerator RestockEnumrator()
    {
        // ���� 0.25 ������� ����� ������� ���������� (��� ����������� �������)
        yield return new WaitForSeconds(0.25f);

        // �������� �� ������� �������
        for (int x = 0; x < columns.Count; x++)
        {
            // �������� �� ������� �������� � �������
            for (int y = 0; y < columns[x].Count; y++)
            {
                // ���� ������� ������� ������
                if (!columns[x][y].texture)
                {
                    // ���������� ��������� ������ �������� �� ���������� ������� ���������
                    int randomElement = Random.Range(0, (elements.Length - 1) * 2);
                    // ���� ������ ��������� ���������� ���������, ������������ ���
                    if (randomElement > elements.Length - 1)
                    {
                        randomElement -= elements.Length - 1;
                    }
                    // ������� ����� ������� �����
                    PuzzleElement element = new PuzzleElement();
                    // ����������� ��������� �������� � ������� ��������
                    element.texture = elements[randomElement];
                    element.position = new Vector2((Screen.width / 2 - (totalColumns * 64) / 2) + x * 64, (-Screen.height - (totalRows * 64) / 2) + y * 64);
                    // �������� ������ ������� ����� ���������
                    columns[x][y] = element;
                }
            }
        }

        // ��������� ������� ����������� ���������� ����� ���������� ���������
        StartCoroutine(DetectCombos());
    }


    // ����������� ���������� ��������� ���������
    IEnumerator DetectCombos()
    {
        yield return new WaitForSeconds(0.25f); // ��������� ��������� ����� ��� ������ ��������

        List<List<int>> combinedLines = new List<List<int>>(); // ������ ��� �������� ��������������� �����
        bool combosDetected = false; // ���� ��� ����������� ������������ ����������

        // ����������� ������������ ����������
        for (int x = 0; x < columns.Count; x++) // ������� ���� ��������
        {
            combinedLines.Add(new List<int>()); // ���������� ����� ������ ����� � ��������������� �����
            List<int> line = new List<int>(); // �������� ����� �����
            for (int y = 0; y < columns[x].Count; y++) // ������� ���� ��������� � �������
            {
                if (line.Count == 0) // ���� ����� �����
                {
                    line.Add(y); // �������� ������� ������� � �����
                }
                else // ���� ����� �� �����
                {
                    if (columns[x][line[0]].texture == columns[x][y].texture) // ���� �������� �������� �������� ��������� � ��������� ������� �������� � �����
                    {
                        line.Add(y); // �������� ������� ������� � �����
                    }
                    if (columns[x][line[0]].texture != columns[x][y].texture || y == columns[x].Count - 1) // ���� �������� �������� �������� �� ��������� � ��������� ������� �������� � �����, ���� ��������� ����� �������
                    {
                        if (line.Count >= 3) // ���� ����� �������� ��� ��� ����� ��������
                        {
                            combinedLines[x].AddRange(line); // �������� ����� � ��������������� �����
                        }
                        line.Clear(); // �������� �����
                        line.Add(y); // �������� ������� ������� � ����� �����
                    }
                }
            }
        }

        for (int x = 0; x < combinedLines.Count; x++) // ������� ���� ��������������� �����
        {
            for (int y = 0; y < combinedLines[x].Count; y++) // ������� ���� ��������� � ��������������� �����
            {
                columns[x][combinedLines[x][y]].texture = null; // ������� �������� � �������� � ��������������� �����
                score += 5; // ��������� ���� �� 5
                combosDetected = true; // ���������� ���� ����������� ���������� � true
            }
        }

        // ����������� �������������� ����������
        combinedLines = new List<List<int>>(); // �������� ������ ��������������� �����
        for (int y = 0; y < columns[0].Count; y++) // ������� ���� �����
        {
            combinedLines.Add(new List<int>()); // ���������� ����� ������ ����� � ��������������� �����
            List<int> line = new List<int>(); // �������� ����� �����
            for (int x = 0; x < columns.Count; x++) // ������� ���� ��������� � ������
            {
                if (line.Count == 0) // ���� ����� �����
                {
                    line.Add(x); // �������� ������� ������� � �����
                }
                else // ���� ����� �� �����
                {
                    if (columns[line[0]][y].texture == columns[x][y].texture) // ���� �������� �������� �������� ��������� � ��������� ������� �������� � �����
                    {
                        line.Add(x); // �������� ������� ������� � �����
                    }
                    if (columns[line[0]][y].texture != columns[x][y].texture || x == columns.Count - 1) // ���� �������� �������� �������� �� ��������� � ��������� ������� �������� � �����, ���� ��������� ����� ������
                    {
                        if (line.Count >= 3) // ���� ����� �������� ��� ��� ����� ��������
                        {
                            combinedLines[y].AddRange(line); // �������� ����� � ��������������� �����
                        }
                        line.Clear(); // �������� �����
                        line.Add(x); // �������� ������� ������� � ����� �����
                    }
                }
            }
        }

        for (int x = 0; x < combinedLines.Count; x++) // ������� ���� ��������������� ����� �� ����������� � ���������
        {
            for (int y = 0; y < combinedLines[x].Count; y++) // ������� ���� ��������� � ������� ��������������� �����
            {
                // �������� �������� � ��������, ������� ��������� � ������� ��������������� ����� �� ������� x
                columns[combinedLines[x][y]][x].texture = null;
                score += 5; // ���������� ����� �� 5 �� ������ ��������� �������
                combosDetected = true; // ��������� ����� ����������� ���������� � true
            }
        }

        if (combosDetected) // ���� ���������� ����������
        {
            StartCoroutine(CompressElements()); // ������ �������� ������ ���������
        }
    }
}
