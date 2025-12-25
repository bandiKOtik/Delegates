using UnityEngine;

public class WalletExample : MonoBehaviour
{
    [SerializeField] private WalletCanvasInteractor _interactor;
    [SerializeField] private WalletUI _view;

    private Wallet _wallet;

    private void Start()
    {
        _wallet = new Wallet();

        _interactor?.Initialize(_wallet);
        _view?.Initialize(_wallet);
    }
}
