using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Random = System.Random;

/*
 *.Txt Write format
 *First Line:Role Name
 * Second Line & then:
 * Talk/Request#Talk Content#Talk Expressions#Next Content#Ex Next Content
 *
 * if don't have anymore Content,must write "#0#0" to replace #Next Content#Ex Next Content
 * 
 */


public class CollectionText
{
    private readonly string _fileLocal;
    private readonly List<OriginalConversationContentClass> _originalConversationContentClasses;

    public CollectionText(string fileLocal)
    {
        this._fileLocal = fileLocal;
        _originalConversationContentClasses = new List<OriginalConversationContentClass>();
        FindNote();
    }

    private void FindNote()
    {
        try
        {
           

            if (File.Exists(_fileLocal))
            {
                StreamReader str = new StreamReader(@_fileLocal,Encoding.UTF8);
                string tempStream = str.ReadLine();
                List<int> selections;

                while ((tempStream = str.ReadLine()) != null)
                {
                    string[] splitStream = tempStream.Split("#");

                    if (splitStream.Length != 5)
                    {
                        Debug.Log("Content Formatting Error!!!");
                        Application.Quit();
                    }
                    
                    selections=new List<int>();
                    selections.Add(Int32.Parse(splitStream[3]));
                    selections.Add(Int32.Parse(splitStream[4]));

                    _originalConversationContentClasses.Add(new OriginalConversationContentClass(splitStream[0],splitStream[1],splitStream[2],selections));
                }
                
                str.Close();
            }
            else
            {
                Debug.Log("The File Don't Exists!");
                Application.Quit();
            }

        }
        catch (IOException ex)
        {
            Debug.Log(ex.Message);
        }


    }

    public List<OriginalConversationContentClass> GetOriginList()
    {
        return _originalConversationContentClasses;
    }

    public string GetFileName()
    {
        if (File.Exists(_fileLocal))
        {
            return Path.GetFileNameWithoutExtension(_fileLocal);
        }
        return "Don't Exists";
    }

    public int NextContent(int serial)
    {
        int tempNext01 = _originalConversationContentClasses[serial].GetIndex(0);
        int tempNext02 = _originalConversationContentClasses[serial].GetIndex(1);

        if (tempNext01==0)
        {
            return tempNext01;
        }

        if (tempNext01 > 0 && tempNext02 > 0)
        {

            if (new Random().Next(0, 100) % 2 == 0)
            {
                return tempNext01;
            }

            return tempNext02;

        }

        if (tempNext01 == -1)
        {
            return tempNext02;
        }

        if (tempNext02 == -1)
        {
            return tempNext01;
        }
        
        //Debug.Log("Content Formatting Error!!!");
        Application.Quit();
        
        return 0;
    }


}
