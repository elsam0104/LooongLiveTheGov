using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool shuttingDown = false;
    private static object locker = new object();
    //�ι� ������� �ʰ� �ϱ� ����

    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (shuttingDown)
            {
                Debug.LogWarning("[Instance] instance" + typeof(T) + "�� �̹� �ı��Ǿ����ϴ�. NULL�� ��ȯ�մϴ�.");
            }
            lock (locker)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                        DontDestroyOnLoad(instance);
                        //typeof(T).ToString() ToString���� T�� �̸��� �����ͼ�(��ȯ) ���ӿ�����Ʈ�� �����
                        //�̷��Ը� ���� ������ = ���� ������ ���ӿ�����Ʈ�� �ڻ쳪�µ� ������Ʈ�� �����
                    }
                }

                return instance;
            }
        }
    }

    //22��° ���� ������ �ذ�, ���� ���� �� ������Ʈ�� ���ϸ� ����� �α׸� ��� 
    private void OnApplicationQuit()
    {
        shuttingDown = true;
    }
    private void OnDestroy()
    {
        shuttingDown = true;
    }
}
