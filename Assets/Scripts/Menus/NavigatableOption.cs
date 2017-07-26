using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIType
{
    Button,
    Slider,
    DropDown,
    Checkbox
}

public class NavigatableOption : MonoBehaviour {

    [SerializeField]
    private UIType m_UIType = UIType.Button;
    public UIType UIType { get { return m_UIType; } }
    private bool m_isSelected = false;
    public bool IsSelected { set { m_isSelected = value; } }
    private Selectable m_UIElement;

    void Start()
    {
        switch (m_UIType)
        {
            case UIType.Button:
                m_UIElement = GetComponent<Button>();
                break;
            case UIType.Slider:
                m_UIElement = GetComponent<Slider>();
                break;
            case UIType.DropDown:
                m_UIElement = GetComponent<Dropdown>();
                break;
            case UIType.Checkbox:
                m_UIElement = GetComponent<Toggle>();
                break;
        }
    }

    void Update()
    {
        if(m_isSelected)
        {
            switch(m_UIType)
            {
                case UIType.Button:
                    ((Button)m_UIElement).Select();
                    break;
                case UIType.Slider:
                    ((Slider)m_UIElement).Select();
                    break;
                case UIType.DropDown:
                    ((Dropdown)m_UIElement).Select();
                    break;
                case UIType.Checkbox:
                    ((Toggle)m_UIElement).Select();
                    break;
            }
        }
    }

    public void Activate()
    {
        switch (m_UIType)
        {
            case UIType.Button:
                Debug.Log("Button Click");
                ((Button)m_UIElement).onClick.Invoke();
                break;
            case UIType.Slider:

                break;
            case UIType.DropDown:
                ((Dropdown)m_UIElement).Show();
                Debug.Log("Show Drop down");
                break;
            case UIType.Checkbox:
                ((Toggle)m_UIElement).isOn = !((Toggle)m_UIElement).isOn;
                break;
        }
    }

    public void Deactivate()
    {
        switch (m_UIType)
        {
            case UIType.Button:

                break;
            case UIType.Slider:

                break;
            case UIType.DropDown:
                ((Dropdown)m_UIElement).Hide();
                Debug.Log("Hiding Drop down");
                break;
            case UIType.Checkbox:

                break;
        }
    }

    public void Slide(float direction)
    {
        if(m_UIType == UIType.Slider)
        {
            ((Slider)m_UIElement).value += direction / Mathf.Abs(direction);
        }
    }

    public void NavigateDropDown(int direction)
    {
        if(m_UIType == UIType.DropDown)
        {
            ((Dropdown)m_UIElement).value += direction / Mathf.Abs(direction);
            ((Dropdown)m_UIElement).RefreshShownValue();
        }
    }
}
