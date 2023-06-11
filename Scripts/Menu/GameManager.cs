using UnityEngine;


public class GameManager : MonoBehaviour, IObserver
{
    [SerializeField] Subject _playerSubject;
    [SerializeField] Subject _finalFase;
    public GameObject hud;
    public GameObject GameOver;
    public GameObject Win;

    public void OnNotify(PlayerActions action)
    {
        if(action == PlayerActions.Morreu)
        {
            EndGame();
        }
        if(action == PlayerActions.Vitoria)
        {
            FinalGame();
        }
    }

    private void EndGame()
    {
        hud.SetActive(false);
        GameOver.SetActive(true);
    }

    private void FinalGame()
    {
        hud.SetActive(false);
        Win.SetActive(true);
    }

    private void OnEnable()
    {
        _playerSubject.addObserver(this);
        _finalFase.addObserver(this);
    }

    private void OnDisable()
    {
        _playerSubject.removeObserver(this);
        _finalFase.removeObserver(this);
    }
}
