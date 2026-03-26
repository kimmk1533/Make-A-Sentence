using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyManager : ObjectManager<EnemyManager, Enemy>
{
	#region 기본 템플릿
	#region 변수
	private Coroutine m_SpawnEnemyCoroutine = null;
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

		m_SpawnEnemyCoroutine = StartCoroutine(SpawnEnemy("Enemy", 1f));
	}
	/// <summary>
	/// 메인 마무리화 함수 (본인 Main Scene 나갈 시 호출)
	/// </summary>
	public override void FinallizeMain()
	{
		base.FinallizeMain();

		if (m_SpawnEnemyCoroutine != null)
			StopCoroutine(m_SpawnEnemyCoroutine);
	}
	#endregion

	#region 유니티 콜백 함수
	#endregion
	#endregion

	private IEnumerator SpawnEnemy(string key, float interval = 1f)
	{
		while (true)
		{
			yield return new WaitForSeconds(interval);

			Vector3 position = new Vector3();
			position.x = Random.Range(-7f, 7f);
			position.y = Random.Range(-3.5f, 3.5f);
			Enemy enemy = GetBuilder(key)
				.SetActive(true)
				.SetPosition(position)
				.Spawn();

			break;
		}
	}
}