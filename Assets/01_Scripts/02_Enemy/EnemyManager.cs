using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyManager : ObjectManager<EnemyManager, Enemy>
{
	#region 기본 템플릿
	#region 변수
	private UtilClass.Timer<string> m_SpawnEnemyTimer = null;
	#endregion

	#region 프로퍼티
	#endregion

	#region 이벤트

	#region 이벤트 함수
	#endregion
	#endregion

	#region 매니저
	private static PlayerManager M_Player => PlayerManager.Instance;
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 기본 초기화 함수 (Init Scene 진입 시, 즉 게임 실행 시 호출)
	/// </summary>
	public override void Initialize()
	{
		base.Initialize();

		m_SpawnEnemyTimer = new UtilClass.Timer<string>();
		m_SpawnEnemyTimer.onTime += SpawnEnemy;
		m_SpawnEnemyTimer.Pause();
	}
	/// <summary>
	/// 기본 마무리화 함수 (게임 종료 시 호출)
	/// </summary>
	public override void Finallize()
	{
		m_SpawnEnemyTimer.Pause();
		m_SpawnEnemyTimer.onTime -= SpawnEnemy;
		m_SpawnEnemyTimer = null;

		base.Finallize();
	}

	/// <summary>
	/// 메인 초기화 함수 (본인 Main Scene 진입 시 호출)
	/// </summary>
	public override void InitializeMain()
	{
		base.InitializeMain();

		// 디버깅
		SpawnEnemy("Enemy");
		// 기존 코드
		//m_SpawnEnemyTimer.interval = 1.5f; // 적 생성 주기
		//m_SpawnEnemyTimer.Resume();
	}
	/// <summary>
	/// 메인 마무리화 함수 (본인 Main Scene 나갈 시 호출)
	/// </summary>
	public override void FinallizeMain()
	{
		m_SpawnEnemyTimer.Clear();
		m_SpawnEnemyTimer.Pause();

		base.FinallizeMain();
	}
	#endregion

	#region 유니티 콜백 함수
	//private void Update()
	//{
	//	m_SpawnEnemyTimer.Update();
	//	m_SpawnEnemyTimer.TimeCheck("Enemy");
	//}
	#endregion
	#endregion

	private void SpawnEnemy(string key)
	{
		Vector3 position = new Vector3();
		position.x = Random.Range(-7f, 7f);
		position.y = Random.Range(-3.5f, 3.5f);

		Enemy enemy = GetBuilder(key)
			.SetActive(true)
			.SetPosition(position)
			.Spawn();
	}
}