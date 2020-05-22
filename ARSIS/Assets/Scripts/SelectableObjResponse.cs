using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectableObjResponse : SelectableObj
{
    public Response response;
    public UnityEvent<Response> selectEventWithResponse; 

    public override void onSelect()
    {
        if (highlighted)
        {
            MenuController.s.deselect.Invoke();
            selected = true;
            highlightImage.color = selectColor;
            selectEventWithResponse.Invoke(response);
        }
    }
}
