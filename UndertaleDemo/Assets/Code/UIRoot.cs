using UnityEngine;

public class UIRoot : MonoBehaviour
{
    //Po to ¿eby wszystkie UI dzia³a³y miêdzy scenami
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}