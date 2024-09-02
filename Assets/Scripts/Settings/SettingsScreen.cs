using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] private SettingsScreenView _view;

    public event Action FeedbackButtonClicked;
    public event Action VersionButtonClicked;
    public event Action TermsOfUseClicked;
    public event Action PrivicyButtonClicked;
    public event Action BackButtonClicked;
    
    private void Start()
    {
        _view.Disable();
    }

    private void OnEnable()
    {
        _view.FeedbackButtonClicked += ProcessFeedbackButtonClicked;
        _view.VersionButtonClicked += ProcessVersionButtonClicked;
        _view.TermsOfUseButtonClicked += ProcessTermsOfUseButtonClicked;
        _view.PrivacyPolicyButtonClicked += ProcessPrivicyPolicyButtonClicked;
        _view.BackButtonClicked += ProcessBackButtonClicked;
    }

    private void OnDisable()
    {
        _view.FeedbackButtonClicked -= ProcessFeedbackButtonClicked;
        _view.VersionButtonClicked -= ProcessVersionButtonClicked;
        _view.TermsOfUseButtonClicked -= ProcessTermsOfUseButtonClicked;
        _view.PrivacyPolicyButtonClicked -= ProcessPrivicyPolicyButtonClicked;
        _view.BackButtonClicked -= ProcessBackButtonClicked;
    }

    private void ProcessBackButtonClicked()
    {
        BackButtonClicked?.Invoke();
        _view.Disable();
    }

    public void ShowScreen()
    {
        _view.Enable();
    }

    private void ProcessPrivicyPolicyButtonClicked()
    {
        PrivicyButtonClicked?.Invoke();
        _view.Disable();
    }

    private void ProcessTermsOfUseButtonClicked()
    {
        TermsOfUseClicked?.Invoke();
        _view.Disable();
    }

    private void ProcessVersionButtonClicked()
    {
        VersionButtonClicked?.Invoke();
        _view.Disable();
    }

    private void ProcessFeedbackButtonClicked()
    {
        FeedbackButtonClicked?.Invoke();
        _view.Disable();
    }
}
