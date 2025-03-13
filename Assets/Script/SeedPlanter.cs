using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region ��ؽ���ת�˶�����
/*��ؽ���ת�˶�����
using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    // �ο���ؽڵ�Transform
    public Transform wristJoint;
    // ����ʱ����ת�Ƕ�
    public float rotationAngle = 90f;
    // ��ת��ʱ��
    public float rotationDuration = 1f;

    private bool isPlanting = false;
    private Quaternion targetRotation;

    void Update()
    {
        // ���¿ո����ʼ���ֹ���
        if (Input.GetKeyDown(KeyCode.Space) && !isPlanting)
        {
            StartPlanting();
        }

        // ������ڲ��֣�������ת��ؽ�
        if (isPlanting)
        {
            RotateWrist();
        }
    }

    private void StartPlanting()
    {
        isPlanting = true;
        // ����Ŀ����ת
        //targetRotation = wristJoint.rotation * Quaternion.Euler(0, rotationAngle, 0);
        targetRotation = wristJoint.rotation * Quaternion.Euler(rotationAngle, 0, 0);
    }

    private void RotateWrist()
    {
        // ����ת��ؽڵ�Ŀ����ת
        wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));

        // ����Ƿ񵽴�Ŀ����ת
        if (Quaternion.Angle(wristJoint.rotation, targetRotation) < 0.1f)
        {
            isPlanting = false; // ��ɲ��ֹ���
        }
    }
}
*/



/*�Լ�����֮ǰ�����޸ĵ�   ��ת90������ת��ȥ
using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    // �ο���ؽڵ�Transform
    public Transform wristJoint;
    // ����ʱ����ת�Ƕ�
    public float rotationAngle = 90f;
    // ��ת��ʱ��
    //   public float rotationDuration = 1f;

    private bool isPlanting = false; // ��ǰ�Ƿ�����ת
    private Quaternion targetRotation; // Ŀ����ת
                                       //   private float rotationProgress = 0f; // ��ת����
    public float wristJointSpeed = 1f; // �±�̧����ٶ�

    private void Update()
    {
        // ���ո��Ƿ���
        if (Input.GetKeyDown(KeyCode.Space)) //���¿ո����ʼ���ֹ���
        {
            // ���û�����ڽ��еĿ��Ѷ�������ʼ�¶���
            if (isPlanting)
            {
                StartCoroutine(PlantingCoroutine());
            }
        }
    }

    private System.Collections.IEnumerator PlantingCoroutine()
    {
        // ��ʼ���ѵĶ���
        isPlanting = true; // ���Ϊ���ڿ���
        float elapsed = 0f;
        float duration = 1f; // ÿ�ζ�������ʱ��
        Quaternion initialWristJointRotation = wristJoint.rotation;
        Quaternion targetWristJointRotation = initialwristJointRotation * Quaternion.Euler(rotationAngle, 0, 0);

        // ̧���±�
        elapsed = 0f;
        while (elapsed < duration)
        {
            wristJoint.rotation = Quaternion.Slerp(initialWristJointRotation, targetWristJointRotation, elapsed / duration);
            elapsed += Time.deltaTime * wristJointSpeed;
            yield return null;
        }

        // ��ԭ�±�
        elapsed = 0f;
        while (elapsed < duration)
        {
            lowerArm.rotation = Quaternion.Slerp(targetWristJointRotation, initialWristJointRotation, elapsed / duration);
            elapsed += Time.deltaTime * wristJointSpeed;
            yield return null;
        }

        // ȷ���±ۻص���ʼλ��
        wristJoint.rotation = initialwristJointRotation;


        isCultivating = false; // ������ɣ����Ϊδ����
    }

}

*/






/*
//��ؽ���ת�˶����� ����ո�һ��������ת���ٵ�һ��������ת
using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    // �ο���ؽڵ�Transform
    public Transform wristJoint;
    // ����ʱ����ת�Ƕ�
    public float rotationAngle = 90f;
    // ��ת��ʱ��
    public float rotationDuration = 1f;

    private bool isPlanting = false; // ��ǰ�Ƿ�����ת
    private Quaternion targetRotation; // Ŀ����ת
    private bool isRotatingBack = false; // �Ƿ�����������ת

    void Update()
    {
        // ���¿ո����ʼ���ֹ���
        if (Input.GetKeyDown(KeyCode.Space) && !isPlanting)
        {
            StartPlanting();
        }

        // ������ڲ��֣�������ת��ؽ�
        if (isPlanting)
        {
            RotateWrist();
        }
    }

    private void StartPlanting()
    {
        isPlanting = true;

        // ���ݵ�ǰ״̬����Ŀ����ת
        if (!isRotatingBack)
        {
            // ����Ŀ����ת
            targetRotation = wristJoint.rotation * Quaternion.Euler(rotationAngle, 0, 0);
        }
        else
        {
            // ����Ѿ���ת��90�ȣ�����Ŀ����ת�ص�ԭλ
            targetRotation = wristJoint.rotation * Quaternion.Euler(-rotationAngle, 0, 0);
        }
    }

    private void RotateWrist()
    {
        // ����ת��ؽڵ�Ŀ����ת
        wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));

        // ����Ƿ񵽴�Ŀ����ת
        if (Quaternion.Angle(wristJoint.rotation, targetRotation) < 0.1f)
        {
            if (!isRotatingBack)
            {
                // ��ɲ��ֹ��̣���ʼ������ת
                isRotatingBack = true;
                isPlanting = false; // ���ò���״̬
            }
            else
            {
                // ���������ת����������״̬
                isRotatingBack = false;
                isPlanting = false; // ���������ת����
            }
        }
    }
}
*/


/*��ؽ���ת�˶����� ����ո�һ��������ת����������ת
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    // �ο���ؽڵ�Transform
    public Transform wristJoint;
    // ����ʱ����ת�Ƕ�
    public float rotationAngle = 90f;
    // ��ת��ʱ��
    public float rotationDuration = 1f;
    // ������תǰ�ĵȴ�ʱ��
    public float delayBeforeRotatingBack = 2f;

    private bool isPlanting = false; // ��ǰ�Ƿ�����ת

    void Update()
    {
        // ���¿ո����ʼ���ֹ���
        if (Input.GetKeyDown(KeyCode.Space) && !isPlanting)
        {
            StartCoroutine(PlantSeeds());
        }
    }

    private IEnumerator PlantSeeds()
    {
        isPlanting = true;

        // ����Ŀ����ת����ؽ���ת90�ȣ�
        Quaternion targetRotation = wristJoint.rotation * Quaternion.Euler(rotationAngle, 0, 0);

        // ��ת��ؽڵ�Ŀ����ת
        float timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));
            timeElapsed += Time.deltaTime;
            yield return null; // �ȴ���һ֡
        }

        // ȷ����ؽ�ȷʵ����Ŀ����ת
        wristJoint.rotation = targetRotation;

        // �ȴ�5�����ٽ���������ת
        yield return new WaitForSeconds(delayBeforeRotatingBack);

        // ����������ת��Ŀ�꣨��ת��ԭλ��
        targetRotation = wristJoint.rotation * Quaternion.Euler(-rotationAngle, 0, 0);

        // ��ת��ؽڻص�ԭλ
        timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));
            timeElapsed += Time.deltaTime;
            yield return null; // �ȴ���һ֡
        }

        // ȷ����ؽ�ȷʵ�ص�ԭλ
        wristJoint.rotation = targetRotation;

        // ���ò���״̬
        isPlanting = false;
    }
}
*/



/*using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    // �ο���ؽڵ�Transform
    public Transform wristJoint;
    // ���ӵ�Transform
    public Transform bagTransform;
    // ����ʱ����ת�Ƕ�
    public float rotationAngle = 90f;
    // ��ת��ʱ��
    public float rotationDuration = 1f;
    // ������תǰ�ĵȴ�ʱ��
    public float delayBeforeRotatingBack = 5f;

    private bool isPlanting = false; // ��ǰ�Ƿ�����ת

    void Update()
    {
        // ���¿ո����ʼ���ֹ���
        if (Input.GetKeyDown(KeyCode.Space) && !isPlanting)
        {
            StartCoroutine(PlantSeeds());
        }
    }

    private IEnumerator PlantSeeds()
    {
        isPlanting = true;

        // ����Ŀ����ת����ؽ���ת90�ȣ�
        Quaternion targetRotation = wristJoint.rotation * Quaternion.Euler(rotationAngle, 0, 0);

        // ��ת��ؽڵ�Ŀ����ת
        float timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));

            timeElapsed += Time.deltaTime;
            yield return null; // �ȴ���һ֡
        }

        // ȷ����ؽ�ȷʵ����Ŀ����ת
        wristJoint.rotation = targetRotation;

        // �ȴ�5�����ٽ���������ת
        yield return new WaitForSeconds(delayBeforeRotatingBack);

        // ����������ת��Ŀ�꣨��ת��ԭλ��
        targetRotation = wristJoint.rotation * Quaternion.Euler(-rotationAngle, 0, 0);

        // ��ת��ؽڻص�ԭλ
        timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));

  
            timeElapsed += Time.deltaTime;
            yield return null; // �ȴ���һ֡
        }

        // ȷ����ؽ�ȷʵ�ص�ԭλ
        wristJoint.rotation = targetRotation;


      
        isPlanting = false;
    }
}
*/



/*//ʵ���ִ������еĴ�����ת�˶�90�Ⱥ��ַ��ص�ԭ��λ��
using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    // �ο���ؽڵ�Transform
    public Transform wristJoint;
    // ���ӵ�Transform
    public Transform bagTransform;
    // ����ʱ����ת�Ƕ�
    public float rotationAngle = 90f;
    // ��ת��ʱ��
    public float rotationDuration = 1f;
    // ������תǰ�ĵȴ�ʱ��
    public float delayBeforeRotatingBack = 1f;

    private bool isPlanting = false; // ��ǰ�Ƿ�����ת

    void Update()
    {
        // ���¿ո����ʼ���ֹ���
        if (Input.GetKeyDown(KeyCode.Space) && !isPlanting)
        {
            StartCoroutine(PlantSeeds());
        }
    }

    private IEnumerator PlantSeeds()
    {
        isPlanting = true;

        // ����Ŀ����ת����ؽ���ת90�ȣ�
        Quaternion targetRotation = wristJoint.rotation * Quaternion.Euler(rotationAngle, 0, 0);

        // ��ת��ؽڵ�Ŀ����ת
        float timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));

            // ͬ�����´��ӵ���ת
            bagTransform.rotation = wristJoint.rotation;

            timeElapsed += Time.deltaTime;
            yield return null; // �ȴ���һ֡
        }

        // ȷ����ؽ�ȷʵ����Ŀ����ת
        wristJoint.rotation = targetRotation;
        bagTransform.rotation = wristJoint.rotation; // ȷ������ͬ��

        // �ȴ�5�����ٽ���������ת
        yield return new WaitForSeconds(delayBeforeRotatingBack);

        // ����������ת��Ŀ�꣨��ת��ԭλ��
        targetRotation = wristJoint.rotation * Quaternion.Euler(-rotationAngle, 0, 0);

        // ��ת��ؽڻص�ԭλ
        timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));

            // ͬ�����´��ӵ���ת
            bagTransform.rotation = wristJoint.rotation;

            timeElapsed += Time.deltaTime;
            yield return null; // �ȴ���һ֡
        }

        // ȷ����ؽ�ȷʵ�ص�ԭλ
        wristJoint.rotation = targetRotation;
        bagTransform.rotation = wristJoint.rotation; // ȷ������ͬ��

        isPlanting = false;
    }
}
*/








/*//ʵ���ִ������еĴ�����ת�˶�90�Ⱥ�����һ�����ӣ��ַ��ص�ԭ��λ��
using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    // �ο���ؽڵ�Transform
    public Transform wristJoint;
    // ���ӵ�Transform
    public Transform bagTransform;
    // ����ʱ����ת�Ƕ�
    public float rotationAngle = 90f;
    // ��ת��ʱ��
    public float rotationDuration = 1f;
    // ������תǰ�ĵȴ�ʱ��
    public float delayBeforeRotatingBack = 1f;

    // ���ӵ�Prefab
    public GameObject seedPrefab; // ����ָ�����ӵ�Prefab
    // ��������ĸ߶�
    public float fallDistance = 1f; // ����߶�

    private bool isPlanting = false; // ��ǰ�Ƿ�����ת

    void Update()
    {
        // ���¿ո����ʼ���ֹ���
        if (Input.GetKeyDown(KeyCode.Space) && !isPlanting)
        {
            StartCoroutine(PlantSeeds());
        }
    }

    private IEnumerator PlantSeeds()
    {
        isPlanting = true;

        // ����Ŀ����ת����ؽ���ת90�ȣ�
        Quaternion targetRotation = wristJoint.rotation * Quaternion.Euler(rotationAngle, 0, 0);

        // ��ת��ؽڵ�Ŀ����ת
        float timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));

            // ͬ�����´��ӵ���ת
            bagTransform.rotation = wristJoint.rotation;

            timeElapsed += Time.deltaTime;
            yield return null; // �ȴ���һ֡
        }

        // ȷ����ؽ�ȷʵ����Ŀ����ת
        wristJoint.rotation = targetRotation;
        bagTransform.rotation = wristJoint.rotation; // ȷ������ͬ��

        // ��������
        StartCoroutine(DropSeed());

        // �ȴ�ָ��ʱ���ٽ���������ת
        yield return new WaitForSeconds(delayBeforeRotatingBack);

        // ����������ת��Ŀ�꣨��ת��ԭλ��
        targetRotation = wristJoint.rotation * Quaternion.Euler(-rotationAngle, 0, 0);

        // ��ת��ؽڻص�ԭλ
        timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));

            // ͬ�����´��ӵ���ת
            bagTransform.rotation = wristJoint.rotation;

            timeElapsed += Time.deltaTime;
            yield return null; // �ȴ���һ֡
        }

        // ȷ����ؽ�ȷʵ�ص�ԭλ
        wristJoint.rotation = targetRotation;
        bagTransform.rotation = wristJoint.rotation; // ȷ������ͬ��

        isPlanting = false;
    }

    private IEnumerator DropSeed()
    {
        // ʵ��������
        GameObject seed = Instantiate(seedPrefab, bagTransform.position, Quaternion.identity);

        // ��ȡ���ӵĳ�ʼλ��
        Vector3 initialPosition = seed.transform.position;

        // ���������Ŀ��λ�ã���ǰ�߶ȼ�ȥ������룩
        Vector3 targetPosition = initialPosition - new Vector3(0, fallDistance, 0);

        float timeElapsed = 0f;
        float fallDuration = fallDistance / 2f; // ���Ե�������ĳ���ʱ��

        // ƽ������Ĺ���
        while (timeElapsed < fallDuration)
        {
            seed.transform.position = Vector3.Lerp(initialPosition, targetPosition, timeElapsed / fallDuration);
            timeElapsed += Time.deltaTime;
            yield return null; // �ȴ���һ֡
        }

        // ȷ�����ӵ���Ŀ��λ��
        seed.transform.position = targetPosition;
    }
}
*/



#endregion




using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    // �ο���ؽڵ�Transform
    public Transform wristJoint;
    // ���ӵ�Transform
    public Transform bagTransform;
    // ����ʱ����ת�Ƕ�
    public float rotationAngle = 90f;
    // ��ת��ʱ��
    public float rotationDuration = 1f;
    // ������תǰ�ĵȴ�ʱ��
    public float delayBeforeRotatingBack = 1f;

    // ���ӵ�Prefab
    public GameObject seedPrefab; // ����ָ�����ӵ�Prefab
    // ��������ĸ߶�
    public float fallDistance = 1f; // ����߶�

    private bool isPlanting = false; // ��ǰ�Ƿ�����ת
    private int seedsPlanted = 0; // �Ѿ���ص�������
    private int score = 0; // ��ǰ����

    void Update()
    {
        // ���¿ո����ʼ���ֹ���
        if (Input.GetKeyDown(KeyCode.Space) && !isPlanting)
        {
            StartCoroutine(PlantSeeds());
        }
    }

    private IEnumerator PlantSeeds()
    {
        isPlanting = true;

        // ����Ŀ����ת����ؽ���ת90�ȣ�
        Quaternion targetRotation = wristJoint.rotation * Quaternion.Euler(rotationAngle, 0, 0);

        // ��ת��ؽڵ�Ŀ����ת
        float timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));

            // ͬ�����´��ӵ���ת
            bagTransform.rotation = wristJoint.rotation;

            timeElapsed += Time.deltaTime;
            yield return null; // �ȴ���һ֡
        }

        // ȷ����ؽ�ȷʵ����Ŀ����ת
        wristJoint.rotation = targetRotation;
        bagTransform.rotation = wristJoint.rotation; // ȷ������ͬ��

        // ��������
        StartCoroutine(DropSeed());

        // �ȴ�ָ��ʱ���ٽ���������ת
        yield return new WaitForSeconds(delayBeforeRotatingBack);

        // ����������ת��Ŀ�꣨��ת��ԭλ��
        targetRotation = wristJoint.rotation * Quaternion.Euler(-rotationAngle, 0, 0);

        // ��ת��ؽڻص�ԭλ
        timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));

            // ͬ�����´��ӵ���ת
            bagTransform.rotation = wristJoint.rotation;

            timeElapsed += Time.deltaTime;
            yield return null; // �ȴ���һ֡
        }

        // ȷ����ؽ�ȷʵ�ص�ԭλ
        wristJoint.rotation = targetRotation;
        bagTransform.rotation = wristJoint.rotation; // ȷ������ͬ��

        isPlanting = false;
    }

    private IEnumerator DropSeed()
    {
        // ʵ��������
        GameObject seed = Instantiate(seedPrefab, bagTransform.position, Quaternion.identity);

        // ��ȡ���ӵĳ�ʼλ��
        Vector3 initialPosition = seed.transform.position;

        // ���������Ŀ��λ�ã���ǰ�߶ȼ�ȥ������룩
        Vector3 targetPosition = initialPosition - new Vector3(0, fallDistance, 0);

        float timeElapsed = 0f;
        float fallDuration = fallDistance / 2f; // ���Ե�������ĳ���ʱ��

        // ƽ������Ĺ���
        while (timeElapsed < fallDuration)
        {
            seed.transform.position = Vector3.Lerp(initialPosition, targetPosition, timeElapsed / fallDuration);
            timeElapsed += Time.deltaTime;
            yield return null; // �ȴ���һ֡
        }

        // ȷ�����ӵ���Ŀ��λ��
        seed.transform.position = targetPosition;

        // ÿ��������أ�������������ӵļ���
        seedsPlanted++;

        // ÿ���������������һ��
        if (seedsPlanted >= 3)
        {
            score++; // ���ӷ���
            seedsPlanted = 0; // �������Ӽ���
            Debug.Log("Score: " + score); // �����ǰ����
        }
    }
}




