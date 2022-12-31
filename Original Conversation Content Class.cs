using System.Collections.Generic;

public class OriginalConversationContentClass
{
    private string _talkType;
    private string _talkContent;
    private string _expression;
    private readonly List<int> _nextIndex;

    public OriginalConversationContentClass(string talkType,string talkContent, string expression, List<int> nextIndex)
    {
        this._talkType = talkType;
        this._expression = expression;
        this._nextIndex = nextIndex;
        this._talkContent = talkContent;

    }

    public void ResetTalkContent(string tempContent)
    {
        _talkContent = tempContent;
    }

    public void ResetExpression(string tempExpression)
    {
        _expression = tempExpression;
    }

    public void ResetNextIndex(int serial,int index)
    {
        _nextIndex.Reverse(serial,index);
    }

    public void ResetType(string type)
    {
        _talkType = type;
    }



    public int GetIndex(int serial)
    {
        return _nextIndex[serial];
    }

    public string GetTalkContent()
    {
        return _talkContent;
    }

    public string GetExpression()
    {
        return _expression;
    }

    public string GetTalkType()
    {
        return _talkType;
    } 
}
