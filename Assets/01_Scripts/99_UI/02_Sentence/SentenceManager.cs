using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SentenceManager : ObjectManager<SentenceManager, Sentence>
{
	#region 기본 템플릿
	#region 변수
	#endregion

	#region 프로퍼티
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


	}
	/// <summary>
	/// 기본 마무리화 함수 (게임 종료 시 호출)
	/// </summary>
	public override void Finallize()
	{
		base.Finallize();


	}

	/// <summary>
	/// 메인 초기화 함수 (본인 Main Scene 진입 시 호출)
	/// </summary>
	public override void InitializeMain()
	{
		base.InitializeMain();


	}
	/// <summary>
	/// 메인 마무리화 함수 (본인 Main Scene 나갈 시 호출)
	/// </summary>
	public override void FinallizeMain()
	{
		base.FinallizeMain();


	}
	#endregion

	#region 유니티 콜백 함수
	#endregion
	#endregion
	
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
				layerMask = LayerMask.NameToLayer("Enemy");

				wordObjectList.AddRange(player.GetNearbyWordObjectList(selectingType, layerMask));
				break;
			case E_WordType.Magic:
				layerMask = LayerMask.NameToLayer("PlayerMagic");

				wordObjectList.AddRange(player.GetNearbyWordObjectList(selectingType, layerMask));
				break;
		}

		return wordObjectList;
	}
}