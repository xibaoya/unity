using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections.Generic; // ��Ӵ�������ʹ�� List





/*
// ���ο��ѣ���һ�֣��ڿ������ز���仯����С���κ�պú��ʣ�����������Ч
// ֮���������ͷ�����е�һ���ڶ�����ƶ� X �����򣬵�������ƶ� Z �����򣬵��ġ������ƶ� X ������

public class Rake_Spin : MonoBehaviour
{
    public Camera mainCamera;  // �������������

    public Transform lowerArm; // �±۵� Transform
    public Transform elbow;     // ��ؽڵ� Transform
    public List<Transform> lands; // ���ص� Transform �б�������6�����أ�
    public GameObject cultivationEffect; // ������Ч�� Prefab
    public float raiseAngle = 30f; // �±�̧��ĽǶ�
    public float lowerArmSpeed = 1f; // �±�̧����ٶ�
    public float initialScaleIncrement = 0.1f; // ��ʼ�������ӵ����ش�С
    public float scaleDuration = 0.05f; // �������ص�ʱ��

    private bool isCultivating = false; // ���ڿ����Ƿ����ڿ���
    private int cultivateCount = 0; // ���Ѵ���
    private int score = 0; // ��ҵ÷�
    private bool hasScored = false; // ���ڱ���Ƿ��Ѿ��÷�
    private float currentScaleIncrement; // ��ǰ�������ӵ����ش�С

    private int currentLandIndex = 0; // ��ǰ��������������

    private void Start()
    {
        // ��ʼ����ǰ����Ϊ��ʼֵ
        currentScaleIncrement = 2*initialScaleIncrement;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isCultivating && currentLandIndex < lands.Count)
            {
                StartCoroutine(CultivateCoroutine());
            }
        }
    }

    private System.Collections.IEnumerator CultivateCoroutine()
    {
        isCultivating = true;
        float elapsed = 0f;
        float duration = 1f;
        Quaternion initialLowerArmRotation = lowerArm.rotation;
        Quaternion targetLowerArmRotation = initialLowerArmRotation * Quaternion.Euler(0, 0, raiseAngle);

        // ���ض�����ʼ
        elapsed = 0f;
        while (elapsed < duration)
        {
            lowerArm.rotation = Quaternion.Slerp(initialLowerArmRotation, targetLowerArmRotation, elapsed / duration);
            elapsed += Time.deltaTime * lowerArmSpeed;
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < duration)
        {
            lowerArm.rotation = Quaternion.Slerp(targetLowerArmRotation, initialLowerArmRotation, elapsed / duration);
            elapsed += Time.deltaTime * lowerArmSpeed;
            yield return null;
        }

        lowerArm.rotation = initialLowerArmRotation;

        // ÿ�����صĳ����߼�
        if (!hasScored)
        {
            Transform currentLand = lands[currentLandIndex];

            // ������Ч
            Vector3 initialScale = currentLand.localScale;
            Vector3 targetScale = initialScale + new Vector3(currentScaleIncrement, currentScaleIncrement, currentScaleIncrement);

            if (cultivationEffect != null)
            {
                Vector3 effectPosition = currentLand.position + Vector3.up * -0.4f;
                Instantiate(cultivationEffect, effectPosition, Quaternion.identity);
            }

            // ��ʼ���صĹ��̣��������صĳߴ磩
            elapsed = 0f;
            while (elapsed < scaleDuration)
            {
                currentLand.localScale = Vector3.Lerp(initialScale, targetScale, elapsed / scaleDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            currentLand.localScale = targetScale;

            cultivateCount++;

            // �����������γ���
            if (cultivateCount >= 3)
            {
                score++;
                Debug.Log("�÷�: " + score);
                cultivateCount = 0;
                hasScored = true;

                // �������ص�˳��ƽ�ƣ��ֱ��� X �� Z �����ƶ�
                Vector3 targetPosition = transform.position;

                // ��˳��ƽ�ƣ���һ���ڶ�����ƶ� X �����򣬵�������ƶ� Z �����򣬵��ġ������ƶ� X ������
                if (currentLandIndex == 0 || currentLandIndex == 1)
                {
                    targetPosition += new Vector3(1.5f, 0f, 0f); // X ������
                }
                else if (currentLandIndex == 2)
                {
                    targetPosition += new Vector3(0f, 0f, -1.5f); // Z ������
                }
                else if (currentLandIndex == 3 || currentLandIndex == 4)
                {
                    targetPosition += new Vector3(-1.5f, 0f, 0f); // X ������
                }

                // �ƶ�ʱ��
                float moveDuration = 1f;
                float moveElapsed = 0f;

                // �ƶ���ɫ�Ͱ���
                while (moveElapsed < moveDuration)
                {
                    transform.position = Vector3.Lerp(transform.position, targetPosition, moveElapsed / moveDuration);
                    moveElapsed += Time.deltaTime;
                    yield return null;
                }
                transform.position = targetPosition; // ȷ���ﵽĿ��λ��

                // ���ز�����ɺ������һ������
                currentLandIndex++;
                if (currentLandIndex < lands.Count)
                {
                    // ����������һ������
                    hasScored = false; // ���õ÷ֱ�ǣ�׼������һ�����ؽ��в���
                }
            }
        }

        isCultivating = false;
    }
}
*/





// ���ο��ѣ���һ�֣��ڿ������ز���仯����С���κ�պú��ʣ�����������Ч
// ֮���������ͷ�����е�һ���ڶ�����ƶ� X �����򣬵�������ƶ� Z �����򣬵��ġ������ƶ� X ������
public class Rake_Spin : MonoBehaviour
{
    public Camera mainCamera;  // �������������

    public Transform lowerArm; // �±۵� Transform
    public Transform elbow;     // ��ؽڵ� Transform
    public List<Transform> lands; // ���ص� Transform �б�������6�����أ�
    public GameObject cultivationEffect; // ������Ч�� Prefab
    public float raiseAngle = 30f; // �±�̧��ĽǶ�
    public float lowerArmSpeed = 1f; // �±�̧����ٶ�
    public float initialScaleIncrement = 0.1f; // ��ʼ�������ӵ����ش�С
    public float scaleDuration = 0.05f; // �������ص�ʱ��

    private bool isCultivating = false; // ���ڿ����Ƿ����ڿ���
    private int cultivateCount = 0; // ���Ѵ���
    private int score = 0; // ��ҵ÷�
    private bool hasScored = false; // ���ڱ���Ƿ��Ѿ��÷�
    private float currentScaleIncrement; // ��ǰ�������ӵ����ش�С

    private int currentLandIndex = 0; // ��ǰ��������������

    private void Start()
    {
        // ��ʼ����ǰ����Ϊ��ʼֵ
        currentScaleIncrement = 2 * initialScaleIncrement;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isCultivating && currentLandIndex < lands.Count)
            {
                StartCoroutine(CultivateCoroutine());
            }
        }
    }

    private System.Collections.IEnumerator CultivateCoroutine()
    {
        isCultivating = true;
        float elapsed = 0f;
        float duration = 1f;
        Quaternion initialLowerArmRotation = lowerArm.rotation;
        Quaternion targetLowerArmRotation = initialLowerArmRotation * Quaternion.Euler(0, 0, raiseAngle);

        // ���ض�����ʼ
        elapsed = 0f;
        while (elapsed < duration)
        {
            lowerArm.rotation = Quaternion.Slerp(initialLowerArmRotation, targetLowerArmRotation, elapsed / duration);
            elapsed += Time.deltaTime * lowerArmSpeed;
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < duration)
        {
            lowerArm.rotation = Quaternion.Slerp(targetLowerArmRotation, initialLowerArmRotation, elapsed / duration);
            elapsed += Time.deltaTime * lowerArmSpeed;
            yield return null;
        }

        lowerArm.rotation = initialLowerArmRotation;

        // ÿ�����صĳ����߼�
        if (!hasScored)
        {
            Transform currentLand = lands[currentLandIndex];

            // ������Ч
            Vector3 initialScale = currentLand.localScale;
            Vector3 targetScale = initialScale + new Vector3(currentScaleIncrement, currentScaleIncrement, currentScaleIncrement);

            if (cultivationEffect != null)
            {
                Vector3 effectPosition = currentLand.position + Vector3.up * -0.4f;
                Instantiate(cultivationEffect, effectPosition, Quaternion.identity);
            }

            // ��ʼ���صĹ��̣��������صĳߴ磩
            elapsed = 0f;
            while (elapsed < scaleDuration)
            {
                currentLand.localScale = Vector3.Lerp(initialScale, targetScale, elapsed / scaleDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            currentLand.localScale = targetScale;

            cultivateCount++;

            // �����������γ���
            if (cultivateCount >= 3)
            {
                score++;
                Debug.Log("�÷�: " + score);
                cultivateCount = 0;
                hasScored = true;

                // �������ص�˳��ƽ�ƣ��ֱ��� X �� Z �����ƶ�
                Vector3 targetPosition = transform.position;

                // ��˳��ƽ�ƣ���һ���ڶ�����ƶ� X �����򣬵�������ƶ� Z �����򣬵��ġ������ƶ� X ������
                if (currentLandIndex == 0 || currentLandIndex == 1)
                {
                    targetPosition += new Vector3(1.5f, 0f, 0f); // X ������
                }
                else if (currentLandIndex == 2)
                {
                    targetPosition += new Vector3(0f, 0f, -1.5f); // Z ������
                }
                else if (currentLandIndex == 3 || currentLandIndex == 4)
                {
                    targetPosition += new Vector3(-1.5f, 0f, 0f); // X ������
                }

                // �ƶ�ʱ��
                float moveDuration = 2f;
                float moveElapsed = 0f;

                // �ƶ���ɫ�Ͱ���
                while (moveElapsed < moveDuration)
                {
                    transform.position = Vector3.Lerp(transform.position, targetPosition, moveElapsed / moveDuration);

                    // ͬ�����λ�ã�ʹ�����ɫһ���ƶ�
                    if (mainCamera != null)
                    {
                        mainCamera.transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y, transform.position.z);
                    }

                    moveElapsed += Time.deltaTime;
                    yield return null;
                }
                transform.position = targetPosition; // ȷ���ﵽĿ��λ��

                // ���ز�����ɺ������һ������
                currentLandIndex++;
                if (currentLandIndex < lands.Count)
                {
                    // ����������һ������
                    hasScored = false; // ���õ÷ֱ�ǣ�׼������һ�����ؽ��в���
                }
            }
        }

        isCultivating = false;
    }

}

