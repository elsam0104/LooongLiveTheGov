using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool shuttingDown = false;
    private static object locker = new object();
    //두번 실행되지 않게 하기 위함

    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (shuttingDown)
            {
                Debug.LogWarning("[Instance] instance" + typeof(T) + "는 이미 파괴되었습니다. NULL을 반환합니다.");
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
                        //typeof(T).ToString() ToString으로 T의 이름을 가져와서(변환) 게임오브젝트를 만든다
                        //이렇게만 쓰면 문제점 = 게임 꺼질때 게임오브젝트가 박살나는데 컴포넌트를 만든다
                    }
                }

                return instance;
            }
        }
    }

    //22번째 줄의 문제점 해결, 만약 꺼질 때 오브젝트를 구하면 디버그 로그를 띄움 
    private void OnApplicationQuit()
    {
        shuttingDown = true;
    }
    private void OnDestroy()
    {
        shuttingDown = true;
    }
}
