using System;
using DefaultNamespace;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private int _score;
    private int _streak = 1;
    [SerializeField] private TMP_Text Text;
    [SerializeField] private ParticleSystem Particles;
    [SerializeField] private float ParticlesDefaultRate;
    [SerializeField] private float ParticlesMaxRate;

    public static ScoreText Instance;

    private void Start()
    {
        Instance = this;
        Health.Instance.Died += SaveScore;
    }

    private void SaveScore()
    {
        Database.Instance.PlayerData.CurrentScore = _score;
        Database.Instance.PlayerData.UpdateMaxScore();
        Database.Instance.SendPlayerProfileToServer();
    }

    private void OnEnable()
    {
         _score = 0;
         _streak = 1;
        
        Text.SetText("Score: {0}", _score);
    }

    private void UpdateText()
    {
        Text.SetText("Score: {0}", _score);
        transform.DOPunchScale(new Vector3(0.3f, 0.5f, 0), 0.2f).OnComplete(() =>
        {
            transform.localScale=Vector3.one;
        });
    }

    public void AddScore()
    {
        _score += Mathf.Max(1, (_streak / 3));
        UpdateText();
    }

    public void RemoveStreak()
    {
        _streak = 1;

        Particles.Stop();
        var particlesEmission = Particles.emission;
        particlesEmission.rateOverTime = ParticlesDefaultRate;
    }

    public void UpdateStreak()
    {
        _streak += 1;
        if (_streak >= 2)
        {
            Particles.Play();
            var particlesEmission = Particles.emission;
            particlesEmission.rateOverTime = ((particlesEmission.rateOverTime.constant + 5) % ParticlesMaxRate);
        }
    }
}