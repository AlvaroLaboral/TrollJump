using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CtrlJugador : MonoBehaviour
{
    [SerializeField] private int velocidad;
    [SerializeField] private Rigidbody2D fisica;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int fuerzaSalto;
    private Animator animacion;
    public static int vidas = 5;
    public static int muertes = 0;
    [SerializeField] private string primerNivel = "Nivel1";
    private bool vulnerable = true;

    void Awake()
    {
        fisica = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animacion = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float entradaX = Input.GetAxis("Horizontal");
        fisica.linearVelocity = new Vector2(entradaX * velocidad, fisica.linearVelocity.y);

        if (fisica.linearVelocity.x > 0) spriteRenderer.flipX = false;
        else if (fisica.linearVelocity.x < 0) spriteRenderer.flipX = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && TocarSuelo())
        {
            fisica.linearVelocity = new Vector2(fisica.linearVelocity.x, fuerzaSalto);
        }
        AnimarJugador();
    }

    private bool TocarSuelo()
    {
        Vector2 origen = fisica.position + new Vector2(0, -1.1f);
        Debug.DrawRay(origen, Vector2.down * 0.1f, Color.red);

        RaycastHit2D toca = Physics2D.Raycast(origen, Vector2.down, 0.1f);
        return toca.collider != null;
    }

    private void AnimarJugador()
    {
        if (!TocarSuelo())
        {
            if (fisica.linearVelocity.y > 0) animacion.Play("Saltando");
            else animacion.Play("Cayendo");
        }
        else if (Mathf.Abs(fisica.linearVelocity.x) > 0.1f)
        {
            animacion.Play("Corriendo");
        }
        else
        {
            animacion.Play("Quieto");
        }
    }

    public void QuitarVida()
    {
        if (!vulnerable) return;
        vulnerable = false;

        vidas--;
        muertes++;

        if (vidas <= 0)
        {
            vidas = 5;
            SceneManager.LoadScene(primerNivel);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}