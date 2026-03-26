using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum E_SentenceType
{
	// 정해진 시간마다
	TimeCondition,
	// 대미지를 줄 때 마다
	GiveDamageCondition,
	// 대미지를 받을 때 마다
	TakeDamageCondition,
	// 특정 객체가 스폰될 때 마다
	SpawnCondition,
	// 특정 객체가 디스폰될 때 마다
	DespawnCondition,

	Max
}

public class SentenceManager : ObjectManager<SentenceManager, Sentence>
{
	#region 기본 템플릿
	#region 변수
	// Game
	private Dictionary<float, UtilClass.Timer> m_TimerMap = null;

	// UI
	private Dictionary<E_SentenceType, List<string>> m_SentenceTextMap = null;
	#endregion

	#region 프로퍼티
	[field: SerializeField]
	public RectTransform inventoryPanel { get; set; }
	[field: SerializeField]
	public ScrollRect sentenceScrollView { get; set; }
	public RectTransform content => sentenceScrollView.content;
	#endregion

	#region 이벤트

	#region 이벤트 함수
	#endregion
	#endregion

	#region 매니저
	private static PlayerManager M_Player => PlayerManager.Instance;
	private static EnemyManager M_Enemy => EnemyManager.Instance;
	private static ProjectileManager M_Projectile => ProjectileManager.Instance;
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 기본 초기화 함수 (Init Scene 진입 시, 즉 게임 실행 시 호출)
	/// </summary>
	public override void Initialize()
	{
		base.Initialize();

		// Game
		m_TimerMap = new Dictionary<float, UtilClass.Timer>();

		// UI
		m_SentenceTextMap = new Dictionary<E_SentenceType, List<string>>();
		for (E_SentenceType sentenceType = E_SentenceType.TimeCondition; sentenceType != E_SentenceType.Max; ++sentenceType)
		{
			m_SentenceTextMap.Add(sentenceType, new List<string>());
		}

		m_SentenceTextMap[E_SentenceType.TimeCondition].Add("[SubjectSelectingType]");
		m_SentenceTextMap[E_SentenceType.TimeCondition].Add("{Subject}");
		m_SentenceTextMap[E_SentenceType.TimeCondition].Add("이/가");
		m_SentenceTextMap[E_SentenceType.TimeCondition].Add("[Interval]");
		m_SentenceTextMap[E_SentenceType.TimeCondition].Add("초마다");
		m_SentenceTextMap[E_SentenceType.TimeCondition].Add("[TargetSelectingType]");
		m_SentenceTextMap[E_SentenceType.TimeCondition].Add("{Target}");
		m_SentenceTextMap[E_SentenceType.TimeCondition].Add("에게");
		m_SentenceTextMap[E_SentenceType.TimeCondition].Add("{Magic}");
		m_SentenceTextMap[E_SentenceType.TimeCondition].Add("을/를 사용합니다");

		m_SentenceTextMap[E_SentenceType.GiveDamageCondition].Add("[SubjectSelectingType]");
		m_SentenceTextMap[E_SentenceType.GiveDamageCondition].Add("{Subject}");
		m_SentenceTextMap[E_SentenceType.GiveDamageCondition].Add("이/가 대미지를 줄 때마다");
		m_SentenceTextMap[E_SentenceType.GiveDamageCondition].Add("[TargetSelectingType]");
		m_SentenceTextMap[E_SentenceType.GiveDamageCondition].Add("{Target}");
		m_SentenceTextMap[E_SentenceType.GiveDamageCondition].Add("에게");
		m_SentenceTextMap[E_SentenceType.GiveDamageCondition].Add("{Magic}");
		m_SentenceTextMap[E_SentenceType.GiveDamageCondition].Add("을/를 사용합니다");

		m_SentenceTextMap[E_SentenceType.TakeDamageCondition].Add("[SubjectSelectingType]");
		m_SentenceTextMap[E_SentenceType.TakeDamageCondition].Add("{Subject}");
		m_SentenceTextMap[E_SentenceType.TakeDamageCondition].Add("이/가 대미지를 받을 때마다");
		m_SentenceTextMap[E_SentenceType.TakeDamageCondition].Add("[TargetSelectingType]");
		m_SentenceTextMap[E_SentenceType.TakeDamageCondition].Add("{Target}");
		m_SentenceTextMap[E_SentenceType.TakeDamageCondition].Add("(들)에게");
		m_SentenceTextMap[E_SentenceType.TakeDamageCondition].Add("{Magic}");
		m_SentenceTextMap[E_SentenceType.TakeDamageCondition].Add("을/를 사용합니다");
	}
	/// <summary>
	/// 기본 마무리화 함수 (게임 종료 시 호출)
	/// </summary>
	public override void Finallize()
	{
		// UI
		foreach (var item in m_SentenceTextMap)
		{
			item.Value.Clear();
		}
		m_SentenceTextMap.Clear();
		m_SentenceTextMap = null;

		// Game
		m_TimerMap.Clear();
		m_TimerMap = null;

		base.Finallize();
	}

	/// <summary>
	/// 메인 초기화 함수 (본인 Main Scene 진입 시 호출)
	/// </summary>
	public override void InitializeMain()
	{
		base.InitializeMain();

		#region 디버그용 문장 생성
		List<string> sentenceList = new List<string>()
		{
			"Time Condition Sentence",
			"Give Damage Condition Sentence",
			"Take Damage Condition Sentence",
		};

		foreach (var item in sentenceList)
		{
			for (E_SelectingType subjectSelectingType = E_SelectingType.Nearest; subjectSelectingType != E_SelectingType.Max; ++subjectSelectingType)
			{
				for (E_SelectingType targetSelectingType = E_SelectingType.Nearest; targetSelectingType != E_SelectingType.Max; ++targetSelectingType)
				{
					Sentence sentence = GetBuilder(item)
					.SetParent(content)
					.SetActive(true)
					.SetAutoInit(false)
					.Spawn();

					sentence.subjectSelectingType = subjectSelectingType;
					sentence.targetSelectingType = targetSelectingType;

					sentence.InitializePoolItem();
				}
			}
		}
		#endregion

		inventoryPanel.gameObject.SetActive(false);
	}
	/// <summary>
	/// 메인 마무리화 함수 (본인 Main Scene 나갈 시 호출)
	/// </summary>
	public override void FinallizeMain()
	{
		foreach (var item in m_TimerMap)
		{
			item.Value.Clear();
		}

		base.FinallizeMain();
	}
	#endregion

	#region 유니티 콜백 함수
	private void OnInventory(InputValue inputValue)
	{
		inventoryPanel.gameObject.SetActive(!inventoryPanel.gameObject.activeSelf);
	}

	private void Update()
	{
		foreach (var item in m_TimerMap)
		{
			item.Value.Update();
		}
	}
	#endregion
	#endregion

	public void AddTimeSentence(float interval, System.Action action)
	{
		if (m_TimerMap.TryGetValue(interval, out UtilClass.Timer timer) == false)
		{
			timer = new UtilClass.Timer(interval);
			m_TimerMap.Add(interval, timer);
		}

		timer.onTime += action;
	}
	public void RemoveTimeSentence(float interval, System.Action action)
	{
		if (m_TimerMap.TryGetValue(interval, out UtilClass.Timer timer) == false)
			return;

		timer.onTime -= action;
	}

	public List<IWordObject> GetWordObjectList(E_SelectingType selectingType, Word word)
	{
		IWordObject player = M_Player.player;
		List<IWordObject> wordObjectList = new List<IWordObject>();
		LayerMask layerMask;

		switch (word.wordType)
		{
			case E_WordType.Player:
				wordObjectList.Add(player);
				break;
			case E_WordType.Enemy:
				layerMask = LayerMask.GetMask("Enemy");

				wordObjectList.AddRange(player.GetNearbyWordObjectList(selectingType, layerMask));
				break;
			case E_WordType.Magic:
				layerMask = LayerMask.GetMask("Player Magic", "Enemy Magic");

				wordObjectList.AddRange(player.GetNearbyWordObjectList(selectingType, layerMask));
				break;
		}

		return wordObjectList;
	}

	public List<string> GetSentenceText(E_SentenceType sentenceType)
	{
		if (m_SentenceTextMap.TryGetValue(sentenceType, out List<string> textList) == false)
			return null;

		return textList;
	}
	public List<string> GetSentenceText(Sentence sentence)
	{
		if (sentence as TimeConditionSentence != null)
			return GetSentenceText(E_SentenceType.TimeCondition);
		if (sentence as GiveDamageConditionSentence != null)
			return GetSentenceText(E_SentenceType.GiveDamageCondition);
		if (sentence as TakeDamageConditionSentence != null)
			return GetSentenceText(E_SentenceType.TakeDamageCondition);

		return null;
	}
}