using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages field notes data.  
/// </summary>
public class FieldNotesManager : MonoBehaviour
{
    public Question firstQuestion;
    public Question currentQuestion; 
    public static FieldNotesManager s;

    int currentSelectionIndex = -1; 

    void Start()
    {
        s = this; 

        Question first = new Question("What is the type of the sample?", new string[] { "rock", "regolith" });
        firstQuestion = first;
        currentQuestion = first; 

        Question rockFirst = new Question("Color/tone", new string[] { "gray", "light red" });
        Question regolithFirst = new Question("First regolith question", new string[] { "option1", "option2" });

        first.setNextQuestions(new Question[] { rockFirst, regolithFirst }); 
    }

    public void nextQuestion()
    {
        if (currentSelectionIndex == -1) return; 
        if (currentQuestion.variableNextQuestion())
        {
            currentQuestion = currentQuestion.nextQuestions[currentSelectionIndex];
            currentSelectionIndex = -1; 
        } else
        {
            currentQuestion = currentQuestion.nextQuestion;
            currentSelectionIndex = -1; 
        }
    }
}

[System.Serializable]
public class Question 
{
    [Header("Option")]
    public string prompt;
    public string[] options;
    public Question[] nextQuestions; // used for branching options - note that in this case the question will be non-skippable
    public Question nextQuestion; // used if next question is always the same 

    public Question(string text, string[] options)
    {
        this.prompt = text;
        this.options = options;
        nextQuestions = null;
        nextQuestion = null; 
    }

    public string getPrompt()
    {
        return prompt; 
    }

    public string getOption(int index)
    {
        if (options.Length > index)
        {
            return null; 
        }
        else
        {
            return options[index]; 
        }
    }

    public bool variableNextQuestion()
    {
        return nextQuestions == null; 
    }

    public void setNextQuestions(Question[] q)
    {
        nextQuestion = null; // only one should be set at a time 
        nextQuestions = q; 
    }

    public void setNextQuestion(Question q)
    {
        nextQuestions = null; // only one should be set at a time 
        nextQuestion = q; 
    }
}
