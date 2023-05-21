using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortablePlatform : MonoBehaviour
{
	[Tooltip("平台移动的结束位置")]
	public Vector3 stopPosiiton;
	[Tooltip("平台移动一次的时间")]
	public float moveTime;
	[Tooltip("平台到边界后的停留时间")]
	public float stayTime;

	private bool toStop = true;         // 是否朝结束位置移动
	private float speed;                // 移动的速度
	private Vector3 startPostion;       // 开始位置

	public bool on;           // 平台移动开关，是否允许平台移动
	void Start()
	{
		startPostion = transform.position;
		speed = Vector3.Distance(transform.position, stopPosiiton) / moveTime;
	}
	void Update()
	{
		PlatformMoveOn(on);
	}

	/// <summary>
	/// 平台移动控制
	/// </summary>
	/// <param name="on">平台移动开关</param>
	void PlatformMoveOn(bool on)
	{
		if (!on) { return; }
		StartCoroutine(PlatformMove(stopPosiiton));
	}

	/// <summary>
	/// 具体平台移动控制
	/// </summary>
	/// <param name="stopPosiiton">停止位置</param>
	/// <returns></returns>
	IEnumerator PlatformMove(Vector3 stopPosiiton)
	{
		Vector3 tempPosition = transform.position;
		if (toStop)
		{
			tempPosition = Vector3.MoveTowards(tempPosition, stopPosiiton, speed * Time.deltaTime);
			transform.position = tempPosition;
			if (transform.position == stopPosiiton)
			{
				yield return new WaitForSeconds(stayTime);
				toStop = false;
			}
		}
		else if (!toStop)
		{
			tempPosition = Vector3.MoveTowards(tempPosition, startPostion, speed * Time.deltaTime);
			transform.position = tempPosition;
			if (transform.position == startPostion)
			{
				yield return new WaitForSeconds(stayTime);
				toStop = true;
			}
		}
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
		collision.transform.SetParent(transform);
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.SetParent(null);
	}

}
