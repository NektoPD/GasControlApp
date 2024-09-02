using System;
using UnityEngine;

public class OnboardingPresenter : MonoBehaviour
{
    [SerializeField] private OnboardingView _firstScreenView;
    [SerializeField] private OnboardingView _secondScreenView;
    [SerializeField] private MainScreenPresenter _mainScren;
    
    public event Action OnboardingCompleted;
    
    private void Start()
    {
        _secondScreenView.DisableScreen();
        _firstScreenView.EnableScreen();
    }

    private void OnEnable()
    {
        _mainScren.OnboardingCompleted += ProcessSecondScreenButtonClick;
        _firstScreenView.InteractableButtonClicked += ProcessFirstScreenButtonClick;
        _secondScreenView.InteractableButtonClicked += ProcessSecondScreenButtonClick;
    }

    private void OnDisable()
    {
        _mainScren.OnboardingCompleted -= ProcessSecondScreenButtonClick;
        _firstScreenView.InteractableButtonClicked -= ProcessFirstScreenButtonClick;
        _secondScreenView.InteractableButtonClicked -= ProcessSecondScreenButtonClick;
    }

    private void ProcessFirstScreenButtonClick()
    {
        _firstScreenView.DisableScreen();
        _secondScreenView.EnableScreen();
    }

    private void ProcessSecondScreenButtonClick()
    {
        OnboardingCompleted?.Invoke();
        gameObject.SetActive(false);
    }
}
