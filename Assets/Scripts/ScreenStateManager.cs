using System;
using UnityEngine;

public class ScreenStateManager : MonoBehaviour
{
    [SerializeField] private MainScreenPresenter _mainScreenPresenter;
    [SerializeField] private AddTripScreenPresenter _addTripScreenPresenter;
    [SerializeField] private OnboardingPresenter _onboardingPresenter;
    [SerializeField] private EditTripInfoPresenter _addStartTripPresenter;
    [SerializeField] private EditTripInfoPresenter _addEndTripPresenter;
    [SerializeField] private OpenTripScreen _openTripScreen;
    [SerializeField] private EditFilledTripInfoScreen _editFilledTripInfoScreen;
    [SerializeField] private EditTripInfoPresenter _editStartTripPresenter;
    [SerializeField] private EditTripInfoPresenter _editEndTripPresenter;
    [SerializeField] private Calculator _calculator;
    [SerializeField] private SettingsScreen _settingsScreen;
    [SerializeField] private FeedbackScreenView _feedbackScreen;
    [SerializeField] private VersionView _versionScreen;
    [SerializeField] private TermsOfUseView _termsOfUseScreen;
    [SerializeField] private PrivacyPolicyView _privacyPolicyScreen;
    
    private void Start()
    {
        _onboardingPresenter.OnboardingCompleted += _mainScreenPresenter.ShowMenuScreen;
        
        _mainScreenPresenter.AddTripClicked += _addTripScreenPresenter.ShowScreen;
        _mainScreenPresenter.SeeMoreButtonClicked += _openTripScreen.InitializeWindow;
        _mainScreenPresenter.CalculatorClicked += _calculator.ShowScreen;
        
        _addTripScreenPresenter.BackToMenuButtonClicked += _mainScreenPresenter.ShowMenuScreen;
        _addTripScreenPresenter.AddStartTripClicked += _addStartTripPresenter.ShowScreen;
        _addTripScreenPresenter.AddEndTripClicked += _addEndTripPresenter.ShowScreen;
        
        _addStartTripPresenter.BackButtonClicked += _addTripScreenPresenter.ShowScreen;
        _addEndTripPresenter.BackButtonClicked += _addTripScreenPresenter.ShowScreen;

        _openTripScreen.BackButtonClicked += _mainScreenPresenter.ShowMenuScreen;
        _openTripScreen.EditButtonClicked += _editFilledTripInfoScreen.InitializeScreen;

        _editFilledTripInfoScreen.BackButtonClicked += _openTripScreen.ShowScreen;
        _editFilledTripInfoScreen.EditStartTripClicked += _editStartTripPresenter.ShowScreen;
        _editFilledTripInfoScreen.EditEndTripClicked += _editEndTripPresenter.ShowScreen;

        _editStartTripPresenter.BackButtonClicked += _editFilledTripInfoScreen.ShowScreen;
        _editEndTripPresenter.BackButtonClicked += _editFilledTripInfoScreen.ShowScreen;

        _calculator.BackButtonClicked += _mainScreenPresenter.ShowMenuScreen;

        _mainScreenPresenter.SettingsClicked += _settingsScreen.ShowScreen;
        
        _settingsScreen.BackButtonClicked += _mainScreenPresenter.ShowMenuScreen;
        _settingsScreen.TermsOfUseClicked += _termsOfUseScreen.Enable;
        _settingsScreen.VersionButtonClicked += _versionScreen.Enable;
        _settingsScreen.PrivicyButtonClicked += _privacyPolicyScreen.Enable;
        _settingsScreen.FeedbackButtonClicked += _feedbackScreen.Enable;

        _feedbackScreen.BackButtonClicked += _settingsScreen.ShowScreen;
        _termsOfUseScreen.BackButtonClicked += _settingsScreen.ShowScreen;
        _versionScreen.BackButtonClicked += _settingsScreen.ShowScreen;
        _privacyPolicyScreen.BackButtonClicked += _settingsScreen.ShowScreen;
    }

    private void OnDisable()
    {
        _onboardingPresenter.OnboardingCompleted -= _mainScreenPresenter.ShowMenuScreen;
        
        _mainScreenPresenter.AddTripClicked -= _addTripScreenPresenter.ShowScreen;
        _mainScreenPresenter.SeeMoreButtonClicked -= _openTripScreen.InitializeWindow;
        _mainScreenPresenter.CalculatorClicked -= _calculator.ShowScreen;
        _mainScreenPresenter.SettingsClicked -= _settingsScreen.ShowScreen;
        
        _addTripScreenPresenter.BackToMenuButtonClicked -= _mainScreenPresenter.ShowMenuScreen;
        _addTripScreenPresenter.AddStartTripClicked -= _addStartTripPresenter.ShowScreen;
        _addTripScreenPresenter.AddEndTripClicked -= _addEndTripPresenter.ShowScreen;
        
        _addStartTripPresenter.BackButtonClicked -= _addTripScreenPresenter.ShowScreen;
        _addEndTripPresenter.BackButtonClicked -= _addTripScreenPresenter.ShowScreen;
        
        _openTripScreen.BackButtonClicked -= _mainScreenPresenter.ShowMenuScreen;
        _openTripScreen.EditButtonClicked -= _editFilledTripInfoScreen.InitializeScreen;
        
        _editFilledTripInfoScreen.BackButtonClicked -= _openTripScreen.ShowScreen;
        _editFilledTripInfoScreen.EditStartTripClicked -= _editStartTripPresenter.ShowScreen;
        _editFilledTripInfoScreen.EditEndTripClicked -= _editEndTripPresenter.ShowScreen;
        
        _editStartTripPresenter.BackButtonClicked -= _editFilledTripInfoScreen.ShowScreen;
        _editEndTripPresenter.BackButtonClicked -= _editFilledTripInfoScreen.ShowScreen;
        
        _calculator.BackButtonClicked -= _mainScreenPresenter.ShowMenuScreen;

        _settingsScreen.BackButtonClicked -= _mainScreenPresenter.ShowMenuScreen;
        _settingsScreen.TermsOfUseClicked -= _termsOfUseScreen.Enable;
        _settingsScreen.VersionButtonClicked -= _versionScreen.Enable;
        _settingsScreen.PrivicyButtonClicked -= _privacyPolicyScreen.Enable;
        _settingsScreen.FeedbackButtonClicked -= _feedbackScreen.Enable;
        
        _feedbackScreen.BackButtonClicked -= _settingsScreen.ShowScreen;
        _termsOfUseScreen.BackButtonClicked -= _settingsScreen.ShowScreen;
        _versionScreen.BackButtonClicked -= _settingsScreen.ShowScreen;
        _privacyPolicyScreen.BackButtonClicked -= _settingsScreen.ShowScreen;
    }
}
