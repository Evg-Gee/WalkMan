using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public   class UserScoreInfo : MonoBehaviour
{
    public static UserScoreInfo instance;
    private int _scoreBloodHeart;
    private int _scoreSilverHeart;
    private int _scoreGoldHeart;
    private int _scoreMonstersKill;              

    private void Awake()
    {
        instance = this;
        SetStartUIInfoUser();
    }
    private void Start()
    {
    }

    // !!!! ¬–≈»≈ÕÕŒ !!!!!
    public void PlayButton()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        SaveInfoUser();
    }
    public int PlusScoreHeart(int plusInt)
    {
        return
        _scoreBloodHeart += plusInt;
    }
    public void SaveInfoUser()
    {
       PlayerPrefs.SetInt("ScoreHeart", _scoreBloodHeart);
       PlayerPrefs.SetInt("ScoreSilverHeart", _scoreSilverHeart);
       PlayerPrefs.SetInt("ScoreGoldHeart", _scoreGoldHeart);
    }
    private void SetStartUIInfoUser()
    {
        _scoreBloodHeart = PlayerPrefs.GetInt("ScoreHeart", 0);
        _scoreSilverHeart = PlayerPrefs.GetInt("ScoreSilverHeart", 0);
        _scoreGoldHeart = PlayerPrefs.GetInt("ScoreGoldHeart", 0);
        _scoreMonstersKill = 0;
    }
    public int SetMonstersKill(int plulKill)
    {
        return
        _scoreMonstersKill += plulKill;
    }
    public int GetBloodHeartInt()
    {
        return _scoreBloodHeart;
    }
    public int GetSilverHeartInt()
    {
        return _scoreSilverHeart;
    }  
    public int GetGoldHeartInt()
    {
        return _scoreGoldHeart;
    }
    public int GetMonstersKill()
    {
        return _scoreMonstersKill;
    }
}