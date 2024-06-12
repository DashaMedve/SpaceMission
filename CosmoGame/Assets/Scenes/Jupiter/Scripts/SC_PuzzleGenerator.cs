using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SC_PuzzleGenerator : MonoBehaviour
{
    public Texture[] elements; // Префабы элементов пазла
    public int totalColumns = 7; // Количество столбцов в пазле
    public int totalRows = 7; // Количество строк в пазле
    public Texture textureBackground; // Текстура для фона счета

    // Класс для представления элемента пазла
    [System.Serializable] //В Unity атрибут [System.Serializable] используется для того, чтобы поля класса были видимы и редактируемы в инспекторе Unity
    public class PuzzleElement
    {
        public Texture texture; // Текстура элемента
        public Vector2 position; // Позиция элемента
    }

    // Список для хранения столбцов и их элементов
    List<List<PuzzleElement>> columns = new List<List<PuzzleElement>>();

    int selectedColumn = -1; // Индекс выбранного столбца 
    int selectedRow = -1; // Индекс выбранной строки 
    int score; // Текущий счет игрока
    int steps = 30; // Количество оставшихся ходов


    // Начальная инициализация
    void Start()
    {
        // Инициализация колонок
        for (int x = 0; x < totalColumns; x++)
        {
            List<PuzzleElement> column = new List<PuzzleElement>();
            // Инициализация строк
            for (int y = 0; y < totalRows; y++)
            {
                PuzzleElement element = new PuzzleElement();
                element.texture = elements[Random.Range(0, elements.Length)]; // Назначаем случайную текстуру
                element.position = new Vector2(x, y);
                column.Add(element);
            }
            columns.Add(column);
        }

        StartCoroutine(RestockEnumrator());
    }

    void OnGUI()
    {
        if (steps > 0) // Проверяем, если количество шагов больше 0
        {
            for (int x = 0; x < columns.Count; x++)
            {
                for (int y = 0; y < columns[x].Count; y++)
                {
                    if (columns[x][y].texture)
                    {
                        // Очень страшная формула...
                        columns[x][y].position = Vector2.Lerp(columns[x][y].position, new Vector2((Screen.width / 2 - (columns.Count * 100) / 2) + x * 100 - 350, (Screen.height / 2 - (columns[x].Count * 100) / 2) + y * 100), Time.deltaTime * 7);
                        // Рисуем новый прямоугольник
                        Rect elementRect = new Rect(columns[x][y].position.x, columns[x][y].position.y, 100, 100);

                        // Проверяем, если элемент соседний с выбранным
                        if ((x == selectedColumn && (y == selectedRow - 1 || y == selectedRow + 1)) || ((x == selectedColumn - 1 || x == selectedColumn + 1) && y == selectedRow))
                        {
                            // Если нажата кнопка на элементе
                            if (GUI.Button(elementRect, columns[x][y].texture))
                            {
                                // Поменять элементы местами
                                PuzzleElement tmpElement = columns[x][y];
                                columns[x][y] = columns[selectedColumn][selectedRow];
                                columns[selectedColumn][selectedRow] = tmpElement;
                                selectedColumn = -1;
                                selectedRow = -1;
                                StopCoroutine(DetectCombos());
                                StartCoroutine(DetectCombos());
                                steps--;  // Уменьшаем количество шагов
                            }
                        }
                        else
                        {
                            // Проверяем, содержит ли прямоугольник элемента текущую позицию мыши
                            if (elementRect.Contains(Event.current.mousePosition))
                            {
                                // Если позиция мыши находится внутри прямоугольника, отключаем GUI элемент
                                GUI.enabled = false;
                                // Если была нажата левая кнопка мыши
                                if (Input.GetMouseButtonDown(0))
                                {
                                    // Запоминаем индексы выбранного столбца и строки
                                    selectedColumn = x;
                                    selectedRow = y;
                                }
                            }
                            // Если текущий элемент является выбранным
                            if (x == selectedColumn && y == selectedRow)
                            {
                                // Отключаем GUI элемент для выбранного элемента
                                GUI.enabled = false;
                            }
                            // Отрисовываем GUI Box для текущего элемента пазла с использованием его текстуры
                            GUI.Box(elementRect, columns[x][y].texture);
                            // Включаем обратно GUI элемент
                            GUI.enabled = true;

                        }
                    }
                }
            }

            // Отображение фона, счета и оставшихся шагов
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
            // Отображаем сообщение "Game Over"
            GUI.Label(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 300, 600, 200), "Game Over", new GUIStyle("Label")
            {
                alignment = TextAnchor.MiddleCenter, // Выравнивание текста по центру
                fontSize = 100 // Размер шрифта
            });

            // Отображаем финальный счет под сообщением "Game Over"
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 100, 300, 100), "Final Score: " + score.ToString(), new GUIStyle("Label")
            {
                alignment = TextAnchor.MiddleCenter, // Выравнивание текста по центру
                fontSize = 50 // Размер шрифта
            });

            // Стиль для кнопок
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = 30; // Устанавливаем размер шрифта кнопки

            // Кнопка перезапуска
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 120, 200, 70), "Restart", buttonStyle))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            // Кнопка выхода
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 200, 200, 70), "Exit", buttonStyle))
            {
                SceneManager.LoadScene("Space");
            }
        }
    }

    // Проверяем, нужно ли провести сжатие элементов
    IEnumerator CompressElements()
    {
        bool compressionNeeded = false;
        // Проходим по каждому столбцу
        for (int x = 0; x < columns.Count; x++)
        {
            // Проходим по каждому элементу в столбце
            for (int y = 1; y < columns[x].Count; y++)
            {
                // Если текущий элемент пустой, а предыдущий непустой, значит, сжатие требуется
                if (!columns[x][y].texture && columns[x][y - 1].texture)
                {
                    compressionNeeded = true;
                }
            }
        }

        // Если сжатие требуется
        if (compressionNeeded)
        {
            // Ждем 0.25 секунды перед началом сжатия (для визуального эффекта)
            yield return new WaitForSeconds(0.25f);

            // Проходим по каждому столбцу
            for (int x = 0; x < columns.Count; x++)
            {
                int referenceIndex = -1;
                // Проходим по каждому элементу в столбце (снизу вверх)
                for (int y = columns[x].Count - 1; y >= 0; y--)
                {
                    // Если текущий элемент пустой
                    if (!columns[x][y].texture)
                    {
                        // Если это первый пустой элемент в столбце, запоминаем его индекс
                        if (referenceIndex == -1)
                        {
                            referenceIndex = y;
                        }
                    }
                    // Если текущий элемент непустой
                    else
                    {
                        // Если есть пустые элементы ниже текущего, сжимаем элементы вниз
                        if (referenceIndex != -1)
                        {
                            // Перемещаем текущий элемент в пустое место ниже
                            columns[x][referenceIndex].texture = columns[x][y].texture;
                            columns[x][referenceIndex].position = columns[x][y].position;
                            // Очищаем текущий элемент
                            columns[x][y].texture = null;
                            // Переходим к следующему пустому месту ниже
                            referenceIndex--;
                        }
                    }
                }
            }
        }

        // Запускаем перезагрузку элементов после сжатия
        StartCoroutine(RestockEnumrator());
    }


    // Добавление новых элементов в пустые ячейки
 
    IEnumerator RestockEnumrator()
    {
        // Ждем 0.25 секунды перед началом пополнения (для визуального эффекта)
        yield return new WaitForSeconds(0.25f);

        // Проходим по каждому столбцу
        for (int x = 0; x < columns.Count; x++)
        {
            // Проходим по каждому элементу в столбце
            for (int y = 0; y < columns[x].Count; y++)
            {
                // Если текущий элемент пустой
                if (!columns[x][y].texture)
                {
                    // Генерируем случайный индекс элемента из доступного массива элементов
                    int randomElement = Random.Range(0, (elements.Length - 1) * 2);
                    // Если индекс превышает количество элементов, корректируем его
                    if (randomElement > elements.Length - 1)
                    {
                        randomElement -= elements.Length - 1;
                    }
                    // Создаем новый элемент пазла
                    PuzzleElement element = new PuzzleElement();
                    // Присваиваем случайную текстуру и позицию элементу
                    element.texture = elements[randomElement];
                    element.position = new Vector2((Screen.width / 2 - (totalColumns * 64) / 2) + x * 64, (-Screen.height - (totalRows * 64) / 2) + y * 64);
                    // Заменяем пустой элемент новым элементом
                    columns[x][y] = element;
                }
            }
        }

        // Запускаем процесс обнаружения комбинаций после пополнения элементов
        StartCoroutine(DetectCombos());
    }


    // Обнаружение комбинаций совпавших элементов
    IEnumerator DetectCombos()
    {
        yield return new WaitForSeconds(0.25f); // Подождать некоторое время для показа анимации

        List<List<int>> combinedLines = new List<List<int>>(); // Список для хранения комбинированных линий
        bool combosDetected = false; // Флаг для обозначения обнаруженных комбинаций

        // Обнаружение вертикальных комбинаций
        for (int x = 0; x < columns.Count; x++) // Перебор всех столбцов
        {
            combinedLines.Add(new List<int>()); // Добавление новой пустой линии в комбинированные линии
            List<int> line = new List<int>(); // Создание новой линии
            for (int y = 0; y < columns[x].Count; y++) // Перебор всех элементов в столбце
            {
                if (line.Count == 0) // Если линия пуста
                {
                    line.Add(y); // Добавить текущий элемент в линию
                }
                else // Если линия не пуста
                {
                    if (columns[x][line[0]].texture == columns[x][y].texture) // Если текстура текущего элемента совпадает с текстурой первого элемента в линии
                    {
                        line.Add(y); // Добавить текущий элемент в линию
                    }
                    if (columns[x][line[0]].texture != columns[x][y].texture || y == columns[x].Count - 1) // Если текстура текущего элемента не совпадает с текстурой первого элемента в линии, либо достигнут конец столбца
                    {
                        if (line.Count >= 3) // Если линия содержит три или более элемента
                        {
                            combinedLines[x].AddRange(line); // Добавить линию в комбинированные линии
                        }
                        line.Clear(); // Очистить линию
                        line.Add(y); // Добавить текущий элемент в новую линию
                    }
                }
            }
        }

        for (int x = 0; x < combinedLines.Count; x++) // Перебор всех комбинированных линий
        {
            for (int y = 0; y < combinedLines[x].Count; y++) // Перебор всех элементов в комбинированной линии
            {
                columns[x][combinedLines[x][y]].texture = null; // Удалить текстуру у элемента в комбинированной линии
                score += 5; // Увеличить счет на 5
                combosDetected = true; // Установить флаг обнаружения комбинации в true
            }
        }

        // Обнаружение горизонтальных комбинаций
        combinedLines = new List<List<int>>(); // Очистить список комбинированных линий
        for (int y = 0; y < columns[0].Count; y++) // Перебор всех строк
        {
            combinedLines.Add(new List<int>()); // Добавление новой пустой линии в комбинированные линии
            List<int> line = new List<int>(); // Создание новой линии
            for (int x = 0; x < columns.Count; x++) // Перебор всех элементов в строке
            {
                if (line.Count == 0) // Если линия пуста
                {
                    line.Add(x); // Добавить текущий элемент в линию
                }
                else // Если линия не пуста
                {
                    if (columns[line[0]][y].texture == columns[x][y].texture) // Если текстура текущего элемента совпадает с текстурой первого элемента в линии
                    {
                        line.Add(x); // Добавить текущий элемент в линию
                    }
                    if (columns[line[0]][y].texture != columns[x][y].texture || x == columns.Count - 1) // Если текстура текущего элемента не совпадает с текстурой первого элемента в линии, либо достигнут конец строки
                    {
                        if (line.Count >= 3) // Если линия содержит три или более элемента
                        {
                            combinedLines[y].AddRange(line); // Добавить линию в комбинированные линии
                        }
                        line.Clear(); // Очистить линию
                        line.Add(x); // Добавить текущий элемент в новую линию
                    }
                }
            }
        }

        for (int x = 0; x < combinedLines.Count; x++) // Перебор всех комбинированных линий по горизонтали и вертикали
        {
            for (int y = 0; y < combinedLines[x].Count; y++) // Перебор всех элементов в текущей комбинированной линии
            {
                // Удаление текстуры у элемента, который находится в текущей комбинированной линии на позиции x
                columns[combinedLines[x][y]][x].texture = null;
                score += 5; // Увеличение счета на 5 за каждый удаленный элемент
                combosDetected = true; // Установка флага обнаружения комбинации в true
            }
        }

        if (combosDetected) // Если обнаружены комбинации
        {
            StartCoroutine(CompressElements()); // Запуск процесса сжатия элементов
        }
    }
}
