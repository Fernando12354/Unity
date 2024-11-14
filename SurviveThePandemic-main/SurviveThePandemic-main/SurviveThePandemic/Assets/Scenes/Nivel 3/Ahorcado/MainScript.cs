using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;
using SimpleJSON;

/*public class MainScript : MonoBehaviour
{
    public Text QuestionText, DashesText, ResultsText;
    public Image HangmanImage, FinalImage;
    public Sprite[] HangmanSprites;
    public Sprite WinSprite, LoseSprite;

    public GameObject MainDialogue, FinalDialogue, Escenario, Ahorcado;

    private int currentHangmanSprite = 0;
    private const int TOTAL_HANGMAN_SPRITES = 8;
    private const char PLACEHOLDER = '*';

    private Dictionary<string, string> gameDict;
    private string answer, userInput;

    void Start()
    {
        gameDict = new Dictionary<string, string>();
        LoadDictionary("FruitsDictionary", gameDict);
        LoadDictionary("SportsDictionary", gameDict);
        PickRandomQuestion();
    }

    public void OnRestartClicked()
    {
        MainDialogue.SetActive(true);
        FinalDialogue.SetActive(false);
        currentHangmanSprite = 0;
        HangmanImage.sprite = HangmanSprites[currentHangmanSprite];
        PickRandomQuestion();
    }

    public void OnGuessSubmitted(Button button)
{
    // Agregamos una línea de depuración para saber si se ha pulsado el botón
    Debug.Log("Botón pulsado: " + button.GetComponentInChildren<Text>().text);

    // Obtener la letra del botón
    char letter = button.GetComponentInChildren<Text>().text.ToCharArray()[0];
    
    // Verificamos si la respuesta contiene la letra
    if (answer.Contains(letter))
    {
        UpdateAnswerText(letter);
        if (CheckWinCondition())
        {
            Debug.Log("Has ganado el juego!");
            ShowFinalDialogue(true);
        }
    }
    else
    {
        if (CheckLoseCondition())
        {
            Debug.Log("Has perdido el juego");
            ShowFinalDialogue(false);
        }
        else
        {
            DrawNextHangmanPart();
        }
    }
}

      // Método para activar y desactivar objetos
    public void Continuar()
    {
        Ahorcado.SetActive(false);
       Escenario.SetActive(true);
       
    }

    private void PickRandomQuestion()
    {
        int randInt = Random.Range(0, gameDict.Count);
        QuestionText.text = gameDict.ElementAt(randInt).Key;
        answer = gameDict.ElementAt(randInt).Value.ToUpper();
        StringBuilder sb = new StringBuilder("");
        for (int i = 0; i < answer.Length; i++) { sb.Append(PLACEHOLDER); }
        DashesText.text = sb.ToString();
        userInput = sb.ToString();
        Debug.Log("Answer: " + answer);
    }

    private void LoadDictionary(string dictFileName, Dictionary<string, string> outputDict)
    {
        TextAsset ta = Resources.Load(dictFileName) as TextAsset;
        JSONObject jsonObj = (JSONObject)JSON.Parse(ta.text);

        foreach (var keyValuePair in jsonObj)
        {
            string key = keyValuePair.Key;
            string value = keyValuePair.Value;
            outputDict[key] = value;
        }
    }

    private void UpdateAnswerText(char letter)
    {
        char[] userInputArray = userInput.ToCharArray();
        for (int i = 0; i < answer.Length; i++)
        {
            if (userInputArray[i] != PLACEHOLDER) { continue; } // already guessed
            if (answer[i] == letter) { userInputArray[i] = letter; }
        }
        userInput = new string(userInputArray);
        DashesText.text = userInput;
    }

    private void DrawNextHangmanPart()
    {
        currentHangmanSprite = ++currentHangmanSprite % TOTAL_HANGMAN_SPRITES;
        HangmanImage.sprite = HangmanSprites[currentHangmanSprite];
    }

    private bool CheckWinCondition() { return answer.Equals(userInput); }
    private bool CheckLoseCondition() { return currentHangmanSprite == TOTAL_HANGMAN_SPRITES - 1; }

    private void ShowFinalDialogue(bool win)
    {
        MainDialogue.SetActive(false);
        FinalDialogue.SetActive(true);
        FinalImage.sprite = win ? WinSprite : LoseSprite;
        ResultsText.text = win ? "Victoria !" : "Derrota !!!";
    }

  
}*/



public class MainScript : MonoBehaviour
{
    public Text QuestionText, DashesText, ResultsText;
    public Image HangmanImage, FinalImage;
    public Sprite[] HangmanSprites;
    public Sprite WinSprite, LoseSprite;

    public GameObject MainDialogue, FinalDialogue, Escenario, Ahorcado;
    public MessageManager messageManager; // Referencia al script MessageManager
    
    // Nuevos botones
    public Button WinButton1, WinButton2, LoseButton;

    private int currentHangmanSprite = 0;
    private const int TOTAL_HANGMAN_SPRITES = 8;
    private const char PLACEHOLDER = '*';

    private Dictionary<string, string> gameDict;
    private string answer, userInput;

    void Start()
    {
        gameDict = new Dictionary<string, string>();
        LoadDictionary("FruitsDictionary", gameDict);
        LoadDictionary("SportsDictionary", gameDict);
        PickRandomQuestion();
        
        // Desactiva los botones al iniciar el juego
        WinButton1.gameObject.SetActive(false);
        WinButton2.gameObject.SetActive(false);
        LoseButton.gameObject.SetActive(false);

        // Asegura que MessageManager esté desactivado al inicio
        messageManager.gameObject.SetActive(false);
    }

    public void OnRestartClicked()
    {
        MainDialogue.SetActive(true);
        FinalDialogue.SetActive(false);
        currentHangmanSprite = 0;
        HangmanImage.sprite = HangmanSprites[currentHangmanSprite];
        PickRandomQuestion();

        // Desactiva los botones al reiniciar el juego
        WinButton1.gameObject.SetActive(false);
        WinButton2.gameObject.SetActive(false);
        LoseButton.gameObject.SetActive(false);
        
        // Asegura que MessageManager esté desactivado al reiniciar
        messageManager.gameObject.SetActive(false);
    }

    public void OnGuessSubmitted(Button button)
    {
        Debug.Log("Botón pulsado: " + button.GetComponentInChildren<Text>().text);
        char letter = button.GetComponentInChildren<Text>().text.ToCharArray()[0];
        
        if (answer.Contains(letter))
        {
            UpdateAnswerText(letter);
            if (CheckWinCondition())
            {
                Debug.Log("Has ganado el juego!");
                ShowFinalDialogue(true);
            }
        }
        else
        {
            if (CheckLoseCondition())
            {
                Debug.Log("Has perdido el juego");
                ShowFinalDialogue(false);
            }
            else
            {
                DrawNextHangmanPart();
            }
        }
    }

    // Método para regresar al escenario y activar MessageManager
    public void Continuar()
    {
        Ahorcado.SetActive(false);
        Escenario.SetActive(true);

        // Activa MessageManager
        messageManager.gameObject.SetActive(true);
    }

    private void PickRandomQuestion()
    {
        int randInt = Random.Range(0, gameDict.Count);
        QuestionText.text = gameDict.ElementAt(randInt).Key;
        answer = gameDict.ElementAt(randInt).Value.ToUpper();
        StringBuilder sb = new StringBuilder("");
        for (int i = 0; i < answer.Length; i++) { sb.Append(PLACEHOLDER); }
        DashesText.text = sb.ToString();
        userInput = sb.ToString();
        Debug.Log("Answer: " + answer);
    }

    private void LoadDictionary(string dictFileName, Dictionary<string, string> outputDict)
    {
        TextAsset ta = Resources.Load(dictFileName) as TextAsset;
        JSONObject jsonObj = (JSONObject)JSON.Parse(ta.text);

        foreach (var keyValuePair in jsonObj)
        {
            string key = keyValuePair.Key;
            string value = keyValuePair.Value;
            outputDict[key] = value;
        }
    }

    private void UpdateAnswerText(char letter)
    {
        char[] userInputArray = userInput.ToCharArray();
        for (int i = 0; i < answer.Length; i++)
        {
            if (userInputArray[i] != PLACEHOLDER) { continue; } // already guessed
            if (answer[i] == letter) { userInputArray[i] = letter; }
        }
        userInput = new string(userInputArray);
        DashesText.text = userInput;
    }

    private void DrawNextHangmanPart()
    {
        currentHangmanSprite = ++currentHangmanSprite % TOTAL_HANGMAN_SPRITES;
        HangmanImage.sprite = HangmanSprites[currentHangmanSprite];
    }

    private bool CheckWinCondition() { return answer.Equals(userInput); }
    private bool CheckLoseCondition() { return currentHangmanSprite == TOTAL_HANGMAN_SPRITES - 1; }

    private void ShowFinalDialogue(bool win)
    {
        MainDialogue.SetActive(false);
        FinalDialogue.SetActive(true);
        FinalImage.sprite = win ? WinSprite : LoseSprite;
        ResultsText.text = win ? "Victoria !" : "Derrota !!!";
        
        // Activa botones según el resultado
        if (win)
        {
            WinButton1.gameObject.SetActive(true);
            WinButton2.gameObject.SetActive(true);
        }
        else
        {
            LoseButton.gameObject.SetActive(true);
        }
    }
}

