using UnityEngine;

public class HistoriaTrigger : MonoBehaviour
{
    private GameObject parent;
    private GameObject hint;
    private GameObject canvas;
    private bool manager = false;
    private bool exibindo = false;
    private void Start()
    {
        parent = transform.parent.gameObject;
        hint = parent.transform.Find("Hint").gameObject;
        canvas = parent.transform.Find("Canvas").gameObject;
    }

    private void Update()
    {
        if (manager)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (!exibindo)
                {
                    Abre();
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hint.SetActive(true);
            manager = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hint.SetActive(false);
            manager = false;
        }
    }
    private void Abre()
    {
        Time.timeScale = 0f;
        canvas.SetActive(true);
        exibindo = true;
    }

    public void Fecha()
    {
        Time.timeScale = 1f;
        canvas.SetActive(false);
        exibindo = false;
    }
}
