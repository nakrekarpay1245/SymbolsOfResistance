using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    //PARAMS
    #region PARAMS
    [Header("MenuManager Params")]
    [Header("Menu Params")]
    [SerializeField]
    private Button _playButton;

    [SerializeField]
    private Button _settingsButton;

    [SerializeField]
    private Button _creditsButton;

    [SerializeField]
    private Button _quitButton;

    [Header("Settings Params")]
    [SerializeField]
    private Transform _settingsThings;
    [SerializeField]
    private Button _closeSettingsButton;

    [Header("Credits Params")]
    [SerializeField]
    private Transform _creditsThings;
    [SerializeField]
    private Button _closeCreditsButton;

    [Header("Quit Params")]
    [SerializeField]
    private Transform _quitThings;
    [SerializeField]
    private Button _closeQuitButton;
    #endregion

    private void Awake()
    {
        _playButton.onClick.AddListener(() => StartLevel());

        _settingsButton.onClick.AddListener(() => OpenSettings());
        _closeSettingsButton.onClick.AddListener(() => CloseSettings());

        _creditsButton.onClick.AddListener(() => OpenCredits());
        _closeCreditsButton.onClick.AddListener(() => CloseCredits());

        _quitButton.onClick.AddListener(() => OpenQuit());
        _closeQuitButton.onClick.AddListener(() => CloseQuit());
        _quitButton.onClick.AddListener(() => Quit());
    }

    //GAME
    #region GAME
    private void StartLevel()
    {

    }
    #endregion

    //CREDITS
    #region CREDITS
    private void OpenCredits()
    {
        Sequence creditsMenuSequence = DOTween.Sequence();
        creditsMenuSequence.AppendCallback(() =>
        {
            _creditsThings.gameObject.SetActive(true);
        });

        creditsMenuSequence.Append(_creditsThings.transform.DOScale(1f, GlobalBinder.singleton.TimeManager.MenuAnimationDuration));

        creditsMenuSequence.AppendInterval(GlobalBinder.singleton.TimeManager.MenuAnimationDelay);

        foreach (Transform child in _creditsThings.GetChild(2).transform)
        {
            creditsMenuSequence.Append(child.DOScale(1f, GlobalBinder.singleton.TimeManager.MenuAnimationDuration));
        }

        creditsMenuSequence.Append(_closeCreditsButton.transform.DOScale(1f,
            GlobalBinder.singleton.TimeManager.MenuAnimationDuration));
    }

    private void CloseCredits()
    {
        Sequence creditsMenuSequence = DOTween.Sequence();

        creditsMenuSequence.Append(_closeCreditsButton.transform.DOScale(0f,
            GlobalBinder.singleton.TimeManager.MenuAnimationDuration));

        foreach (Transform child in _creditsThings.GetChild(2).transform)
        {
            creditsMenuSequence.Append(child.DOScale(0f, GlobalBinder.singleton.TimeManager.MenuAnimationDuration));
        }

        creditsMenuSequence.Append(_creditsThings.transform.DOScale(0f, GlobalBinder.singleton.TimeManager.MenuAnimationDuration));

        creditsMenuSequence.AppendCallback(() =>
        {
            _creditsThings.gameObject.SetActive(false);
        });
    }

    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }
    #endregion

    //SETTINGS
    #region SETTINGS
    private void OpenSettings()
    {
        Sequence settingsMenuSequence = DOTween.Sequence();
        settingsMenuSequence.AppendCallback(() =>
        {
            _settingsThings.gameObject.SetActive(true);
        });

        settingsMenuSequence.Append(_settingsThings.transform.DOScale(1f, GlobalBinder.singleton.TimeManager.MenuAnimationDuration));

        settingsMenuSequence.AppendInterval(GlobalBinder.singleton.TimeManager.MenuAnimationDelay);

        foreach (Transform child in _settingsThings.GetChild(1).transform)
        {
            settingsMenuSequence.Append(child.DOScale(1f, GlobalBinder.singleton.TimeManager.MenuAnimationDuration));
        }

        settingsMenuSequence.Append(_closeSettingsButton.transform.DOScale(1f,
            GlobalBinder.singleton.TimeManager.MenuAnimationDuration));
    }

    private void CloseSettings()
    {
        Sequence settingsMenuSequence = DOTween.Sequence();

        settingsMenuSequence.Append(_closeSettingsButton.transform.DOScale(0f,
            GlobalBinder.singleton.TimeManager.MenuAnimationDuration));

        foreach (Transform child in _settingsThings.GetChild(1).transform)
        {
            settingsMenuSequence.Append(child.DOScale(0f, GlobalBinder.singleton.TimeManager.MenuAnimationDuration));
        }

        settingsMenuSequence.Append(_settingsThings.transform.DOScale(0f, GlobalBinder.singleton.TimeManager.MenuAnimationDuration));

        settingsMenuSequence.AppendCallback(() =>
        {
            _settingsThings.gameObject.SetActive(false);
        });
    }
    #endregion

    //QUIT
    #region QUIT
    private void OpenQuit()
    {
        Sequence quitMenuSequence = DOTween.Sequence();
        quitMenuSequence.AppendCallback(() =>
        {
            _quitThings.gameObject.SetActive(true);
        });

        quitMenuSequence.Append(_quitThings.transform.DOScale(1f, GlobalBinder.singleton.TimeManager.MenuAnimationDuration));

        quitMenuSequence.AppendInterval(GlobalBinder.singleton.TimeManager.MenuAnimationDelay);

        foreach (Transform child in _quitThings.transform)
        {
            quitMenuSequence.Append(child.DOScale(1f, GlobalBinder.singleton.TimeManager.MenuAnimationDuration));
        }
    }

    private void CloseQuit()
    {
        Sequence quitMenuSequence = DOTween.Sequence();

        foreach (Transform child in _quitThings.transform)
        {
            quitMenuSequence.Append(child.DOScale(0f, GlobalBinder.singleton.TimeManager.MenuAnimationDuration));
        }

        quitMenuSequence.Append(_quitThings.transform.DOScale(0f, GlobalBinder.singleton.TimeManager.MenuAnimationDuration));

        quitMenuSequence.AppendCallback(() =>
        {
            _quitThings.gameObject.SetActive(false);
        });
    }

    private void Quit()
    {
        Application.Quit();
    }
    #endregion
}