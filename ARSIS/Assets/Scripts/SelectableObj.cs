using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectableObj : MonoBehaviour
{
    public Text commandName; 
    protected bool selected;
    protected bool highlighted;
    public Image highlightImage;
    public Color highlightColor;
    public Color selectColor; 
    private Color originalColor;
    public UnityEvent selectEvent;
    public bool deselectsAutomatically = false; 

    private string commandText; 


    private void Start()
    {
        gameObject.tag = "selectable";
        selected = false;
        highlighted = false;
        originalColor = highlightImage.color;
        MenuController.s.unhighlight += onUnhighlight;
        MenuController.s.deselect += onUnselect;

        commandText = ""; 
    }

    private void OnEnable()
    {
        if (commandName == null) return; 

        // Ask voice manager to create a command for us 
        VoiceManager.S.addCommand(commandName.text, selectEvent);
        commandText = commandName.text; 
    }

    private void OnDisable()
    {
        VoiceManager.S.removeCommand(commandText);
        commandText = ""; 
    }

    public void resetCommand(string command)
    {
        VoiceManager.S.removeCommand(commandText);
        VoiceManager.S.addCommand(commandName.text, selectEvent);
        commandText = commandName.text;
    }

    public virtual void onHighlight()
    {
        highlighted = true;
        if (!selected)
        {  
            highlightImage.color = highlightColor;
        }
        
        
    }

    public virtual void onUnhighlight()
    {
        highlighted = false;
        if (!selected)
        {
            highlightImage.color = originalColor;
        }
    }

    // This one is called when the button is pressed through "Adele choose" 
    public virtual void onSelect()
    {
        if (highlighted)
        {
            MenuController.s.deselect.Invoke();
            selected = true;
            highlightImage.color = selectColor;
            selectEvent.Invoke();  
            if (deselectsAutomatically)
            {
                Invoke("onUnselect", 1f); 
            }
        }
    }

    // This one is called when the button is pressed through voice 
    public virtual void onSelectInvoked()
    {
        MenuController.s.deselect.Invoke();
        selected = true;
        highlightImage.color = selectColor; 
    }

    public virtual void onUnselect()
    {
        selected = false;
        highlightImage.color = originalColor; 
    }
}
