using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldNoteDisplay : MonoBehaviour
{
    public GameObject skipButton;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public Text promptText;
    public GameObject instructionsText;

    public void setQuestion(string text, string[] options, bool skippable)
    {
        promptText.text = text;
        if (options.Length == 2)
        {
            button3.SetActive(false);
            button1.SetActive(true);
            button2.SetActive(true); 
            instructionsText.SetActive(true); 
            button1.GetComponentInChildren<Text>().text = options[0];
            button1.GetComponent<SelectableObj>().resetCommand(options[0]); 
            button2.GetComponentInChildren<Text>().text = options[1];
            button2.GetComponent<SelectableObj>().resetCommand(options[1]);
        } else if (options.Length == 3)
        {
            button3.SetActive(true);
            button1.SetActive(true);
            button2.SetActive(true);
            instructionsText.SetActive(true);
            button1.GetComponentInChildren<Text>().text = options[0];
            button1.GetComponent<SelectableObj>().resetCommand(options[0]);
            button2.GetComponentInChildren<Text>().text = options[1];
            button2.GetComponent<SelectableObj>().resetCommand(options[1]);
            button3.GetComponentInChildren<Text>().text = options[2];
            button3.GetComponent<SelectableObj>().resetCommand(options[2]);
        }
        if (skippable)
        {
            skipButton.SetActive(true); 
        } else
        {
            skipButton.SetActive(false); 
        }
    }

    public void displayFinalQuestion()
    {
        skipButton.SetActive(false);
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
        instructionsText.SetActive(false); 
        promptText.text = "Field note recorded!"; 
    }
}
