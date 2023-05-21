using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortablePlatform : MonoBehaviour
{
	[Tooltip("ƽ̨�ƶ��Ľ���λ��")]
	public Vector3 stopPosiiton;
	[Tooltip("ƽ̨�ƶ�һ�ε�ʱ��")]
	public float moveTime;
	[Tooltip("ƽ̨���߽���ͣ��ʱ��")]
	public float stayTime;

	private bool toStop = true;         // �Ƿ񳯽���λ���ƶ�
	private float speed;                // �ƶ����ٶ�
	private Vector3 startPostion;       // ��ʼλ��

	public bool on;           // ƽ̨�ƶ����أ��Ƿ�����ƽ̨�ƶ�
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
	/// ƽ̨�ƶ�����
	/// </summary>
	/// <param name="on">ƽ̨�ƶ�����</param>
	void PlatformMoveOn(bool on)
	{
		if (!on) { return; }
		StartCoroutine(PlatformMove(stopPosiiton));
	}

	/// <summary>
	/// ����ƽ̨�ƶ�����
	/// </summary>
	/// <param name="stopPosiiton">ֹͣλ��</param>
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
