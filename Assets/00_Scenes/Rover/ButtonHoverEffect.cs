using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color hoverColor = Color.gray; // Color cuando el bot�n est� en hover
    public Color normalColor = Color.white; // Color normal del bot�n

    private Image buttonImage;

    void Start()
    {
        // Obt�n el componente Image del bot�n
        buttonImage = GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = normalColor; // Aseg�rate de que el color inicial sea el normal
        }
    }

    // Cuando el cursor entra en el bot�n
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            buttonImage.color = hoverColor;
        }
    }

    // Cuando el cursor sale del bot�n
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            buttonImage.color = normalColor;
        }
    }
}
