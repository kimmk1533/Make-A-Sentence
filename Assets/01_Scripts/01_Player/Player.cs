using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : SerializedMonoBehaviour
{
	#region 기본 템플릿
	#region 변수
	private Vector2 m_MovingDirection;
	[SerializeField]
	private float m_MovingSpeed = 1f;
	#endregion

	#region 프로퍼티
	#endregion

	#region 매니저
	#endregion

	#region 이벤트

	#region 이벤트 함수
	#endregion
	#endregion

	#region 초기화 & 마무리화 함수
	/// <summary>
	/// 초기화 함수
	/// </summary>
	public void Initialize()
	{

	}
	/// <summary>
	/// 마무리화 함수
	/// </summary>
	public void Finallize()
	{

	}
	#endregion

	#region 유니티 콜백 함수
	private void OnMove(InputValue inputValue)
	{
		m_MovingDirection = inputValue.Get<Vector2>();
	}

	private void Update()
	{
		transform.position += (Vector3)(m_MovingDirection * m_MovingSpeed * Time.deltaTime);
	}
	private void OnValidate()
	{
		m_MovingSpeed = Mathf.Max(0f, m_MovingSpeed);
	}
	#endregion
	#endregion
}