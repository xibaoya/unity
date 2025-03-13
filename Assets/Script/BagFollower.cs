using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*��������ؽ�ͬ������
public class BagFollower : MonoBehaviour
{
    public Transform wristJoint; // ��ؽڵ�Transform

    void Update()
    {
        if (wristJoint != null)
        {
            // ʹ���ӵ���ת����ؽڵ���ת����һ��
            transform.rotation = wristJoint.rotation;
        }
    }
}
*/


/*
using UnityEngine;

public class BagFollower : MonoBehaviour
{
    // ���ӵ���ת�Ƕ�
    public float rotationAngle = 90f;
    // ��ת�ĳ���ʱ��
    public float rotationDuration = 1f;
    // �ȴ���ʱ��
    public float waitDuration = 5f;

    void Start()
    {
        // ��ʼ��ת����
        StartCoroutine(RotateBag());
    }

    private IEnumerator RotateBag()
    {
        // ��ת���� 90 ��
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, rotationAngle, 0);
        float timeElapsed = 0f;

        while (timeElapsed < rotationDuration)
        {
            // ��ֵ��ת
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));
            timeElapsed += Time.deltaTime;
            yield return null; // �ȴ���һ֡
        }

        // ȷ������ȷʵ����Ŀ����ת
        transform.rotation = targetRotation;

        // �ȴ� 5 ��
        yield return new WaitForSeconds(waitDuration);

        // ������ת���ӻص�ԭλ��
        targetRotation = transform.rotation * Quaternion.Euler(0, -rotationAngle, 0);
        timeElapsed = 0f;

        while (timeElapsed < rotationDuration)
        {
            // ��ֵ��ת
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));
            timeElapsed += Time.deltaTime;
            yield return null; // �ȴ���һ֡
        }

        // ȷ�����ӻص�ԭλ
        transform.rotation = targetRotation;
    }
}
*/




using UnityEngine;

public class BagFollower : MonoBehaviour
{
    // ���ӵ���ת�Ƕ�
    public float rotationAngle = 90f;
    // ��ת�ĳ���ʱ��
    public float rotationDuration = 1f;
    // �ȴ���ʱ��
    public float waitDuration = 5f;

    private bool isRotating = false; // ����Ƿ�������ת

    void Update()
    {
        // ���ո������
        if (Input.GetKeyDown(KeyCode.Space) && !isRotating)
        {
            isRotating = true; // ����Ϊ������ת״̬
            StartCoroutine(RotateBag());
        }
    }

    private IEnumerator RotateBag()
    {
        // ��ת���� 90 ��
        //Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, rotationAngle, 0);
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(rotationAngle, 0, 0);

        float timeElapsed = 0f;

        while (timeElapsed < rotationDuration)
        {
            // ��ֵ��ת
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));
            timeElapsed += Time.deltaTime;
            yield return null; // �ȴ���һ֡
        }

        // ȷ������ȷʵ����Ŀ����ת
        transform.rotation = targetRotation;

        // �ȴ� 5 ��
        yield return new WaitForSeconds(waitDuration);

        // ������ת���ӻص�ԭλ��
 //       targetRotation = transform.rotation * Quaternion.Euler(0, -rotationAngle, 0);
        targetRotation = transform.rotation * Quaternion.Euler(-rotationAngle, 0, 0);

        timeElapsed = 0f;

        while (timeElapsed < rotationDuration)
        {
            // ��ֵ��ת
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));
            timeElapsed += Time.deltaTime;
            yield return null; // �ȴ���һ֡
        }

        // ȷ�����ӻص�ԭλ
        transform.rotation = targetRotation;

        isRotating = false; // ������ת״̬
    }
}
