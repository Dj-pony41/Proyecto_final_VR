using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color hoverColor = Color.gray; // Color cuando el botón está en hover
    public Color normalColor = Color.white; // Color normal del botón

    private Image buttonImage;

    void Start()
    {
        // Obtén el componente Image del botón
        buttonImage = GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = normalColor; // Asegúrate de que el color inicial sea el normal
        }
    }

    // Cuando el cursor entra en el botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            buttonImage.color = hoverColor;
        }
    }

    // Cuando el cursor sale del botón
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            buttonImage.color = normalColor;
        }
    }
}
