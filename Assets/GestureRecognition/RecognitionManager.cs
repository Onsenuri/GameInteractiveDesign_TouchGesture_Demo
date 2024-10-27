using System;
using System.Linq;
using FreeDraw;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecognitionManager : MonoBehaviour
{
    [SerializeField] private Drawable _drawable;
    [SerializeField] private TextMeshProUGUI _recognitionResult;
    [SerializeField] private Button _templateModeButton;
    [SerializeField] private Button _recognitionModeButton;
    [SerializeField] private Button _reviewTemplates;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _CustomizeButton;
    [SerializeField] private TMP_InputField _templateName;
    [SerializeField] private TemplateReviewPanel _templateReviewPanel;
    [SerializeField] private RecognitionPanel _recognitionPanel;
    [SerializeField] private SkillPanel _skillPanel;
    [SerializeField] private GameObject _customizePanels;

    private GestureTemplates _templates => GestureTemplates.Get();
    private static readonly DollarOneRecognizer _dollarOneRecognizer = new DollarOneRecognizer();
    private static readonly DollarPRecognizer _dollarPRecognizer = new DollarPRecognizer();
    private IRecognizer _currentRecognizer = _dollarOneRecognizer;
    private RecognizerState _state = RecognizerState.RECOGNITION;
    private string _recogLogs = " ";
    private bool _isCustomPanelOpened = false;

    public Character char1;
    public Character char2;

    public enum RecognizerState
    {
        TEMPLATE,
        RECOGNITION,
        TEMPLATE_REVIEW
    }

    [Serializable]
    public struct GestureTemplate
    {
        public string Name;
        public DollarPoint[] Points;

        public GestureTemplate(string templateName, DollarPoint[] preparePoints)
        {
            Name = templateName;
            Points = preparePoints;
        }
    }

    public void StartGame()
    {
        GameManager.Instance.isGameStart = true;
        Debug.Log("Game Start!");
    }

    public void CustomizeModeOpen()
    {
        if(!_isCustomPanelOpened)
        {
            _isCustomPanelOpened = true;
            _customizePanels.SetActive(true);
            Debug.Log("Customization Start!");
        }
        else if (_isCustomPanelOpened)
        {
            _isCustomPanelOpened = false;
            _customizePanels.SetActive(false);
            Debug.Log("Customization End!");
        }
    }

    public void CustomizeModeClose()
    {
        _customizePanels.SetActive(false);
        Debug.Log("Customization End!");
    }

    private string TemplateName => _templateName.text;


    private void Start()
    {
        _drawable.OnDrawFinished += OnDrawFinished;
        _templateModeButton.onClick.AddListener(() => SetupState(RecognizerState.TEMPLATE));
        _recognitionModeButton.onClick.AddListener(() => SetupState(RecognizerState.RECOGNITION));
        _reviewTemplates.onClick.AddListener(() => SetupState(RecognizerState.TEMPLATE_REVIEW));
        _recognitionPanel.Initialize(SwitchRecognitionAlgorithm);

        SetupState(_state);
    }

    private void SwitchRecognitionAlgorithm(int algorithm)
    {
        if (algorithm == 0)
        {
            _currentRecognizer = _dollarOneRecognizer;
        }

        if (algorithm == 1)
        {
            _currentRecognizer = _dollarPRecognizer;
        }
    }

    private void SetupState(RecognizerState state)
    {
        _state = state;
        _templateModeButton.image.color = _state == RecognizerState.TEMPLATE ? Color.green : Color.white;
        _recognitionModeButton.image.color = _state == RecognizerState.RECOGNITION ? Color.green : Color.white;
        _reviewTemplates.image.color = _state == RecognizerState.TEMPLATE_REVIEW ? Color.green : Color.white;
        _templateName.gameObject.SetActive(_state == RecognizerState.TEMPLATE);
        _recognitionResult.gameObject.SetActive(_state == RecognizerState.RECOGNITION);

        _drawable.gameObject.SetActive(state != RecognizerState.TEMPLATE_REVIEW);
        _templateReviewPanel.SetVisibility(state == RecognizerState.TEMPLATE_REVIEW);
        _recognitionPanel.SetVisibility(state == RecognizerState.RECOGNITION);
    }

    private void OnDrawFinished(DollarPoint[] points)
    {
        if (_state == RecognizerState.TEMPLATE)
        {
            GestureTemplate preparedTemplate =
                new GestureTemplate(TemplateName, _currentRecognizer.Normalize(points, 64));
            _templates.RawTemplates.Add(new GestureTemplate(TemplateName, points));
            _templates.ProceedTemplates.Add(preparedTemplate);
        }
        else
        {
            //  (string, float) result = _dollarOneRecognizer.DoRecognition(points, 64, _templates.GetTemplates());
            (string, float) result = _currentRecognizer.DoRecognition(points, 64,
                _templates.RawTemplates);
            string resultText = "";
            if (_currentRecognizer is DollarOneRecognizer)
            {
                resultText = $"Recognized: {result.Item1}, Score: {result.Item2}";
                _recogLogs += resultText + "\n";
            }
            else if (_currentRecognizer is DollarPRecognizer)
            {
                resultText = $"Recognized: {result.Item1}, Distance: {result.Item2}";
                _recogLogs += resultText + "\n";
            }

            if (char1.isReadyToCast)
            {
                _recognitionResult.text = resultText;
                Icon.IconType _iconType = Icon.IconType.Thunder; // 아이콘 타입 저장
                int skillRecog = 0; // 인식한 스킬을 인트로 입력받기 위한 변수
                int count = 0;
                bool isSkillFound = false;
                bool isIconFound = false;
                if (result.Item1 == "Thunder")
                {
                    skillRecog = 1;
                    _iconType = Icon.IconType.Thunder;
                }
                else if (result.Item1 == "FireBall")
                {
                    skillRecog = 2;
                    _iconType = Icon.IconType.FireBall;
                }
                else if (result.Item1 == "StoneBall")
                {
                    skillRecog = 3;
                    _iconType = Icon.IconType.StoneBall;
                }

                Debug.Log(count);

                while (!isSkillFound && count < 5)
                {
                    if (SkillManager.Instance.skills[count] == skillRecog)
                    {
                        //count = 0;
                        isSkillFound = true;
                        char1.Skill(result.Item1);
                        SkillManager.Instance.skills[count] = 0;
                        foreach (Transform child in _skillPanel.transform)
                        {
                            if (child.GetComponent<Icon>().iconType == _iconType)
                            {
                                Destroy(child.gameObject);
                                break;
                            }
                        }

                    }
                    else
                    {
                        Debug.Log("No Skill Ready");
                    }

                    count++;
                }

                //_skillPanel.DeleteSkillIcon(result.Item1);
                //char2.Skill(result.Item1);
                Debug.Log(resultText);
            }
        }

    }

    private void OnApplicationQuit()
    {
        LogOut.Instance.WriteLog(_recogLogs);
        _templates.Save();
    }
}