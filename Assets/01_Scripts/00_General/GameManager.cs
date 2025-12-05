using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : SerializedSingleton<GameManager>
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
	// Game Managers
	private static PlayerManager M_Player => PlayerManager.Instance;
	private static EnemyManager M_Enemy => EnemyManager.Instance;
	private static ProjectileManager M_Projectile => ProjectileManager.Instance;
	private static MagicManager M_Magic => MagicManager.Instance;

	// UI Managers
	private static WordManager M_Word => WordManager.Instance;
	private static SentenceManager M_Sentence => SentenceManager.Instance;
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 초기화 함수 (Init Scene 진입 시, 즉 게임 실행 시 호출)
	/// </summary>
	public override void Initialize()
	{
		base.Initialize();
		base.InitializeMain();

		M_Player.Initialize();
		M_Enemy.Initialize();
		M_Projectile.Initialize();
		M_Magic.Initialize();

		M_Player.InitializeMain();
		M_Enemy.InitializeMain();
		M_Projectile.InitializeMain();
		M_Magic.InitializeMain();

		M_Word.Initialize();
		M_Sentence.Initialize();

		M_Word.InitializeMain();
		M_Sentence.InitializeMain();
	}
	/// <summary>
	/// 마무리화 함수 (게임 종료 시 호출)
	/// </summary>
	public override void Finallize()
	{


		base.FinallizeMain();
		base.Finallize();
	}

	/// <summary>
	/// Main Menu Scene 초기화 함수 (Main Menu Scene 진입 시 호출)
	/// </summary>
	public void InitializeOnMainMenuScene()
	{

	}
	/// <summary>
	/// Main Menu Scene 마무리화 함수 (Main Menu Scene 나갈 시 호출)
	/// </summary>
	public void FinallizeOnMainMenuScene()
	{

	}

	/// <summary>
	/// Game Scene 초기화 함수 (Game Scene 진입 시 호출)
	/// </summary>
	public void InitializeOnGameScene()
	{

	}
	/// <summary>
	/// Game Scene 마무리화 함수 (Game Scene 나갈 시 호출)
	/// </summary>
	public void FinallizeOnGameScene()
	{

	}
	#endregion

	#region 유니티 콜백 함수
	protected override void Awake()
	{
		base.Awake();

		Initialize();
	}
	protected void OnApplicationQuit()
	{
		Finallize();
	}
	#endregion
	#endregion

}