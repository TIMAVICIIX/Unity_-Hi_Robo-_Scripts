using System.Collections.Generic;
using UnityEngine;

public class Test_Class : MonoBehaviour
{
    public string fileLocal;
    private CollectionText _testCollection;
    private List<OriginalConversationContentClass> _getContent;
    
    void Awake()
    {
        _testCollection = new CollectionText(fileLocal);
        _getContent = _testCollection.GetOriginList();
        
        PrintLog();

    }

    private void PrintLog()
    {
        Debug.Log("Content Role:"+_testCollection.GetFileName());

        foreach (var tempContent in _getContent)
        {
            Debug.Log(tempContent.GetTalkType()+":"+tempContent.GetTalkContent()+"Expression:"+tempContent.GetExpression()+" "+tempContent.GetIndex(0)+" "+tempContent.GetIndex(1));

            if (tempContent.GetIndex(0) > 0 && tempContent.GetIndex(1) > 0)
            {
                Debug.Log("Next Content:"+tempContent.GetIndex(0)+" "+tempContent.GetIndex(1));
            }else if (tempContent.GetIndex(0) == 0)
            {
                Debug.Log("End Content");
            }else if (tempContent.GetIndex(0) == -1)
            {
                Debug.Log("Next Content:"+tempContent.GetIndex(1));
            }else if (tempContent.GetIndex(1) == -1)
            {
                Debug.Log("Next Content:"+tempContent.GetIndex(0));
            }
            else
            {
                Debug.Log("Content Formatting Error!!!");
                Application.Quit();
            }

        }
        
    }

    
   
}
