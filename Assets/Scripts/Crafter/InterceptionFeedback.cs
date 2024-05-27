using TMPro;
using UnityEngine;

public class InterceptionFeedback : MonoBehaviour
{
    [SerializeField] private InterceptionCalculater interceptionCalculater;
    [SerializeField] private TextMeshProUGUI xAnswer;
    [SerializeField] private TextMeshProUGUI yAnswer;

    private Vector2 _correctInterceptionAnswer;

    public void OnConfirmButtonClicked()
    {
        _correctInterceptionAnswer = interceptionCalculater.intersection;
        //TODO: use string builder
        Vector2 answer = new Vector2(float.Parse(xAnswer.text.Substring(0, xAnswer.text.Length - 1)), float.Parse(yAnswer.text.Substring(0, xAnswer.text.Length - 1)));
        Debug.Log(answer[0] + " " + answer[1] + " " + _correctInterceptionAnswer[0] + " " + _correctInterceptionAnswer[1]);
        if (answer == _correctInterceptionAnswer)
        {
            Debug.Log("CorrectAnswer");
            //TODO: use unity event
        }
        else
        {
            Debug.Log("Try Again");
        }
    }
}

