using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyNamespace
{

}

[Serializable]
public class GameData
{
    public int GameDataId;
    public string Title;
    public string IntroText;
    public TeleportData[] TeleportDatas;
    public int[] DeletedTeleportDatas;
    public QuestionData[] QuestionDatas;
    public bool Reset;

}

[Serializable]
public class TeleportData
{
    public int TeleportDataId;
    public string Name;
    public float X;
    public float Y;
    public float Z;
}

[Serializable]
public class QuestionData
{
    public int QuestionDataId;
    public int Position;
    public string Title;
    public string Question;
    public bool KeepPreviousValue;
    public QuestionType QuestionType;
}

[Serializable]
public enum QuestionType
{
    Auto, Fiets, Voetganger, Bus, Deelauto, Geen
}

[Serializable]
public class QuestionResponse
{
    public int QuestionResponseId;
    public QuestionResponseLine[] QuestionResponseLines;
}

[Serializable]
public class QuestionResponseLine
{
    public int QuestionResponseLineId;
    public QuestionType QuestionType;
    public int QuestionResult;
}