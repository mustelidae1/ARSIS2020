﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FieldNotesListDisplay : MonoBehaviour
{
    public GameObject fieldNotesListGrid;
    public GameObject fieldNoteEntryPrefab;

    public GameObject detailPanel;
    public Text detailTitle;
    public Text detailBody; 

    public void showFieldNotes(ResponseRepository notes)
    {
        fieldNotesListGrid.SetActive(true);
        detailPanel.SetActive(false); 
        foreach (Transform child in fieldNotesListGrid.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Response r in notes.responses)
        {
            Debug.Log("Creating a field note label"); 
            GameObject label = GameObject.Instantiate(fieldNoteEntryPrefab);
            FieldNoteLabel components = label.GetComponent<FieldNoteLabel>();
            components.dateText.text = r.date.ToString();
            UnityEvent<Response> newEvent = new FieldNoteCallback(); 
            newEvent.AddListener(showFieldNoteDetail);
            label.GetComponent<SelectableObjResponse>().response = r; 
            label.GetComponent<SelectableObjResponse>().selectEventWithResponse = newEvent; 
            label.transform.SetParent(fieldNotesListGrid.transform, false); 
        }
    }

    public void showFieldNoteDetail(Response response)
    {
        fieldNotesListGrid.SetActive(false);
        detailPanel.SetActive(true);

        detailTitle.text = response.date.ToString();

        string body = ""; 
        foreach (Entry e in response.entries)
        {
            body += e.prompt + "\t" + e.response + "\n"; 
        }

        detailBody.text = body; 
    }

    // TODO remove all listeners on deactivate?
}

public class FieldNoteCallback : UnityEvent<Response> {

}