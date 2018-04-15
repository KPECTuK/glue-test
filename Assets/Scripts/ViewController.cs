using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once CheckNamespace
public class ViewController : MonoBehaviour, IExecutor
{
	private const int LOGS_I = 20;
	private const float TRANSITION_DURATION_F = .6f;

	private Button _buttonTurn;
	//
	private Button _buttonOpt0;
	private Image _imageOpt0;
	private Button _buttonOpt1;
	private Image _imageOpt1;
	private Button _buttonOpt2;
	private Image _imageOpt2;
	//
	private Image _iconBetOwn;
	private Image _iconBetFoe;
	//
	private Text _textCount;
	private Text _textLog;

	//
	private PlaygroundController _controller;
	private PlaygroundRepository _repository;
	private readonly string[] _log = new string[LOGS_I];
	private int _logCurrent;

	private void OnTurn()
	{
		if(_controller.IsInWaitState)
		{
			_controller.SetWaitStage();
		}
	}

	private void OnBet(IOption option)
	{
		if(_controller.IsInWaitState)
		{
			_controller.Repository.SetOwnOption(option);
			_controller.SetNextStage();
		}
	}

	public IEnumerator PerformSwitchOwn()
	{
		_iconBetOwn.CrossFadeAlpha(0f, TRANSITION_DURATION_F / 2f, false);
		yield return new WaitForSeconds(TRANSITION_DURATION_F / 2f);
		_iconBetOwn.sprite = _controller.Repository.OwnOption.Sprite;
		_iconBetOwn.CrossFadeAlpha(1f, TRANSITION_DURATION_F / 2f, false);
		yield return new WaitForSeconds(TRANSITION_DURATION_F / 2f);
	}

	public IEnumerator PerformSwitchFoe()
	{
		_iconBetFoe.CrossFadeAlpha(0f, TRANSITION_DURATION_F / 2f, false);
		yield return new WaitForSeconds(TRANSITION_DURATION_F / 2f);
		_iconBetFoe.sprite = _controller.Repository.FoeOption.Sprite;
		_iconBetFoe.CrossFadeAlpha(1f, TRANSITION_DURATION_F / 2f, false);
		yield return new WaitForSeconds(TRANSITION_DURATION_F / 2f);
	}

	public IEnumerator PerformWin()
	{
		_iconBetOwn.CrossFadeColor(Color.blue, TRANSITION_DURATION_F / 2f, false, false);
		yield return new WaitForSeconds(TRANSITION_DURATION_F / 2f);
		_iconBetOwn.CrossFadeColor(Color.white, TRANSITION_DURATION_F / 2f, false, false);
		yield return new WaitForSeconds(TRANSITION_DURATION_F / 2f);
	}

	public IEnumerator PerformLoss()
	{
		_iconBetFoe.CrossFadeColor(Color.red, TRANSITION_DURATION_F / 2f, false, false);
		yield return new WaitForSeconds(TRANSITION_DURATION_F / 2f);
		_iconBetFoe.CrossFadeColor(Color.white, TRANSITION_DURATION_F / 2f, false, false);
		yield return new WaitForSeconds(TRANSITION_DURATION_F / 2f);
	}

	public IEnumerator PerformDraw()
	{
		_iconBetOwn.CrossFadeColor(Color.gray, TRANSITION_DURATION_F / 2f, false, false);
		_iconBetFoe.CrossFadeColor(Color.gray, TRANSITION_DURATION_F / 2f, false, false);
		yield return new WaitForSeconds(TRANSITION_DURATION_F / 2f);

		_iconBetOwn.CrossFadeColor(Color.white, TRANSITION_DURATION_F / 2f, false, false);
		_iconBetFoe.CrossFadeColor(Color.white, TRANSITION_DURATION_F / 2f, false, false);
		yield return new WaitForSeconds(TRANSITION_DURATION_F / 2f);
	}

	public IEnumerator PerformCounterChange()
	{
		_textCount.text = $"<color=blue>{_repository.OwnCounter}</color> : <color=red>{_repository.FoeCounter}</color>";
		yield return new WaitForEndOfFrame();
	}

	public void LogMessage(string message)
	{
		_log[_logCurrent] = message;
		_logCurrent = ++_logCurrent % _log.Length;
		RenderLog();
	}

	private void RenderLog()
	{
		var builder = new StringBuilder();
		for(var index = 0; index < _log.Length; index++)
		{
			builder.AppendLine(_log[(_logCurrent + index) % _log.Length]);
		}
		_textLog.text = builder.ToString();
	}

	private void InitializeView()
	{
		// prototyping

		_buttonTurn = transform.FindOnAllChildren<Button>(_ => _.name.Contains("turn")).SingleOrDefault();
		_buttonTurn.onClick.AddListener(OnTurn);
		//
		using(var enumerator = _repository.GetAllOptions().GetEnumerator())
		{
			enumerator.MoveNext();
			var option_00 = enumerator.Current;
			_buttonOpt0 = transform.FindOnAllChildren<Button>(_ => _.name.Contains("opt_00")).SingleOrDefault();
			_buttonOpt0.onClick.AddListener(() => OnBet(option_00));
			_imageOpt0 = _buttonOpt0.transform.FindOnAllChildren<Image>(_ => _.name.Contains("icon")).SingleOrDefault();
			_imageOpt0.sprite = option_00.Sprite;
			//
			enumerator.MoveNext();
			var option_01 = enumerator.Current;
			_buttonOpt1 = transform.FindOnAllChildren<Button>(_ => _.name.Contains("opt_01")).SingleOrDefault();
			_buttonOpt1.onClick.AddListener(() => OnBet(option_01));
			_imageOpt1 = _buttonOpt1.transform.FindOnAllChildren<Image>(_ => _.name.Contains("icon")).SingleOrDefault();
			_imageOpt1.sprite = option_01.Sprite;
			//
			enumerator.MoveNext();
			var option_02 = enumerator.Current;
			_buttonOpt2 = transform.FindOnAllChildren<Button>(_ => _.name.Contains("opt_02")).SingleOrDefault();
			_buttonOpt2.onClick.AddListener(() => OnBet(option_02));
			_imageOpt2 = _buttonOpt2.transform.FindOnAllChildren<Image>(_ => _.name.Contains("icon")).SingleOrDefault();
			_imageOpt2.sprite = option_02.Sprite;
		}
		//
		_iconBetOwn = transform.FindOnAllChildren<Image>(_ => _.name.Contains("bet_own")).SingleOrDefault();
		_iconBetFoe = transform.FindOnAllChildren<Image>(_ => _.name.Contains("bet_foe")).SingleOrDefault();
		//
		_textCount = transform.FindOnAllChildren<Text>(_ => _.name.Contains("counter")).SingleOrDefault();
		_textLog = transform.FindOnAllChildren<Text>(_ => _.name.Contains("log")).SingleOrDefault();

		RenderLog();
	}

	// ReSharper disable once UnusedMember.Local
	private void OnEnable()
	{
		if(!Application.isPlaying)
		{
			return;
		}

		_repository = new PlaygroundRepository();
		InitializeView();
		_controller = new PlaygroundController(this, _repository, this);
	}

	// ReSharper disable once UnusedMember.Local
	private void OnDisable()
	{
		_buttonTurn?.onClick.RemoveAllListeners();
		_buttonOpt0?.onClick.RemoveAllListeners();
		_buttonOpt1?.onClick.RemoveAllListeners();
		_buttonOpt2?.onClick.RemoveAllListeners();
		_controller?.Release();
		_repository?.Release();
	}
}