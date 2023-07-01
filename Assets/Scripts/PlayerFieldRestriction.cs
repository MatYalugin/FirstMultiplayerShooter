using UnityEngine;
using UnityEngine.UI;

public class PlayerFieldRestriction : MonoBehaviour
{
    private InputField inputField;

    private void Start()
    {
        inputField = GetComponent<InputField>();
    }

    private void Update()
    {
        // Получаем текущий текст в InputField
        string text = inputField.text;

        // Проверяем каждый символ в тексте
        for (int i = 0; i < text.Length; i++)
        {
            char character = text[i];

            // Разрешаем латиницу, цифры и специальные символы
            if (!IsLatin(character) && !char.IsDigit(character) && !IsSpecialCharacter(character))
            {
                // Если символ не разрешен, удаляем его из текста
                text = text.Remove(i, 1);
                i--;
            }
        }

        // Устанавливаем обновленный текст в InputField
        inputField.text = text;
    }

    // Проверка, является ли символ латинской буквой
    private bool IsLatin(char character)
    {
        return (character >= 'a' && character <= 'z') || (character >= 'A' && character <= 'Z');
    }

    // Проверка, является ли символ специальным символом
    private bool IsSpecialCharacter(char character)
    {
        // Здесь можно расширить список разрешенных специальных символов по своему усмотрению
        char[] specialCharacters = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '{', '}', '[', ']', ':', ';', '<', '>', ',', '.', '/', '?', '\\', '|', '`', '~', ' ', '=', '+' };

        return System.Array.IndexOf(specialCharacters, character) >= 0;
    }
}
