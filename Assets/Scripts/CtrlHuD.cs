using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CtrlHuD : MonoBehaviour
{
    [SerializeField] private Image[] corazones;
    [SerializeField] private TextMeshProUGUI muertesTxt;

    void Start()
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            corazones[i].enabled = i < CtrlJugador.vidas;
        }

        muertesTxt.text = "Muertes: " + CtrlJugador.muertes;
    }
}