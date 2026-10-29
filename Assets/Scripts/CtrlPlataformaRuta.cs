using UnityEngine;

public class CtrlPlataformaRuta : MonoBehaviour
{
    [SerializeField] private Transform puntoInicio;
    [SerializeField] private Transform puntoFin;
    [SerializeField] private float profundidad = 6f;
    [SerializeField] private float forma = 0.4f;
    [SerializeField] private float velocidad = 10f;
    [SerializeField] private int resolucion = 40;
    [SerializeField] private Transform jugador;
    [SerializeField] private float distanciaActivacion = 3f;
    [SerializeField] private Transform[] zonas;

    private Vector3[] ruta;
    private int indice = 0;
    private int direccion = 0;

    void Start()
    {
        ruta = new Vector3[resolucion + 1];
        for (int i = 0; i <= resolucion; i++)
        {
            ruta[i] = PosicionEnRuta((float)i / resolucion);
        }
        transform.position = ruta[0];
    }

    void Update()
    {
        bool cerca = false;
        foreach (Transform zona in zonas)
        {
            if (Vector2.Distance(jugador.position, zona.position) < distanciaActivacion) cerca = true;
        }

        if (direccion == 0 && cerca && Input.GetKeyDown(KeyCode.Space))
        {
            if (indice == 0) direccion = 1;
            else direccion = -1;
        }

        float paso = velocidad * Time.deltaTime;
        while (direccion != 0 && paso > 0)
        {
            Vector3 destino = ruta[indice + direccion];
            float distancia = Vector3.Distance(transform.position, destino);

            if (distancia > paso)
            {
                transform.position = Vector3.MoveTowards(transform.position, destino, paso);
                paso = 0;
            }
            else
            {
                transform.position = destino;
                paso -= distancia;
                indice += direccion;
                if (indice == 0 || indice == ruta.Length - 1) direccion = 0;
            }
        }
    }

    private Vector3 PosicionEnRuta(float valor)
    {
        Vector3 recta = Vector3.Lerp(puntoInicio.position, puntoFin.position, valor);
        float bajada = Mathf.Pow(Mathf.Abs(Mathf.Sin(valor * Mathf.PI)), forma) * profundidad;
        return recta + Vector3.down * bajada;
    }

    private void OnDrawGizmos()
    {
        if (puntoInicio == null || puntoFin == null) return;

        Gizmos.color = Color.cyan;
        Vector3 anterior = PosicionEnRuta(0f);
        for (int i = 1; i <= 20; i++)
        {
            Vector3 siguiente = PosicionEnRuta(i / 20f);
            Gizmos.DrawLine(anterior, siguiente);
            anterior = siguiente;
        }

        if (zonas == null) return;
        Gizmos.color = Color.yellow;
        foreach (Transform zona in zonas)
        {
            if (zona != null) Gizmos.DrawWireSphere(zona.position, distanciaActivacion);
        }
    }
}