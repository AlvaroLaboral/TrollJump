using UnityEngine;

public class Personajes : MonoBehaviour
{


    [SerializeField] private int velocidad;
    [SerializeField] private Rigidbody2D fisica;


    void Awake()
    {
        fisica=GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        float entradaX = Input.GetAxis("Horizontal");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
