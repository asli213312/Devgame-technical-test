using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class AbstractCounter : MonoBehaviour
{
	[SerializeField] protected TextMeshProUGUI counterText;
	[SerializeField] private string preText;
	[SerializeField] private string postText;

	public event Action<int> CountChanged;
	protected int Counter
	{
		get => _counter;
		private set
		{
			_counter = value;
			CountChanged?.Invoke(_counter);
		}
	}
	private int _counter;

	public void Initialize(int counter) => Counter = counter;
	public void ChangeValue(int value) => Counter += value;
	public void ChangeValueByMultiplier(float multiplier) => Counter = Mathf.RoundToInt(Counter * multiplier);
	public int GetValue() => Counter;

	protected virtual void Start() 
	{
		CountChanged += UpdateText;

		UpdateText(0);
	}

	protected virtual void OnDestroy() 
	{	
		CountChanged -= UpdateText;
	}

	protected void InvokeCount(int value) 
	{
		Counter += value;

		Counter = Counter >= 0 ? Counter : 0;
	}

	protected virtual bool NeedIncrease() => true;

	private void UpdateText(int count) 
	{
		counterText.text = $"{preText} {count} {postText}";
	}
}
