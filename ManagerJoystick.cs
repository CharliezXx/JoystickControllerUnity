using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ManagerJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler,IPointerUpHandler
{
    
private Image imgJoystickBG;
private Image imgJoystick;
private Vector2 posInput;

    void Start()
    {
        imgJoystickBG = GetComponent<Image>();
        imgJoystick = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imgJoystickBG.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out posInput))
            {
                posInput.x = posInput.x / (imgJoystickBG.rectTransform.sizeDelta.x);
                posInput.y = posInput.y / (imgJoystickBG.rectTransform.sizeDelta.y);
                //Debug.Log(posInput.x.ToString() + "/" +  posInput.y.ToString());

                // แก้จอย Joystick ลอยเกิน JoystickBG
                if (posInput.magnitude > 1.0f){
                    posInput = posInput.normalized;
                }

                // ขยับ Joystick
                imgJoystick.rectTransform.anchoredPosition = new Vector2(
                    posInput.x * (imgJoystickBG.rectTransform.sizeDelta.x /4), 
                    posInput.y * (imgJoystickBG.rectTransform.sizeDelta.y /4));
            }
    }

    public void OnPointerDown(PointerEventData eventData){
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData){
        posInput = Vector2.zero;
        imgJoystick.rectTransform.anchoredPosition = Vector2.zero;
    }


    public float inputHorizontal(){
        if (posInput.x != 0)
            return posInput.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float inputVertical(){
        if (posInput.x != 0)
            return posInput.y;
        else
            return Input.GetAxis("Vertical");
    }
}
