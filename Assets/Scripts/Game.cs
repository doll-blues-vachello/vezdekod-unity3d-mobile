using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Game : MonoBehaviour
{
    public enum Level
    {
        Easy,
        Medium,
        Hard
    }

    public static Level Lvl;
    public static uint SkillPoints = 0;
    public static int Status = 0;
    [SerializeField] public Transform LeftWall;
    [SerializeField] public Transform RightWall;
    [SerializeField] public Transform TopWall;
    [SerializeField] public Transform BottomWall;
    [SerializeField] private Transform _player;
    [SerializeField] private Text _remainLabel;
    [SerializeField] private Text _scoreLabel;
    [SerializeField] private Text _shieldsLabel;
    [SerializeField] private Text _degreeLabel;
    [SerializeField] private Text _protectionLabel;
    [SerializeField] private Text _skillPointsLabel;
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private int _chance;
    private Stack<GameObject> _gameObjects;
    private int _totalObjects;
    private uint _score = 0;
    private float _wallOffset = 50f;
    private float _interval;

    private void Start()
    {
        _gameObjects = new Stack<GameObject>();

        if (Lvl == Level.Easy)
        {
            _totalObjects = 25;
            _interval = 2f;
            Asteroid.Speed = 0.5f;
            Asteroid.Damage = 10;
        }
        else if (Lvl == Level.Medium)
        {
            _totalObjects = 40;
            _interval = 1.9f;
            Asteroid.Speed = 0.6f;
            Asteroid.Damage = 11;
        }
        else if (Lvl == Level.Hard)
        {
            _totalObjects = 55;
            _interval = 1.8f;
            Asteroid.Speed = 0.7f;
            Asteroid.Damage = 12;
        }

        GenerageObjects();

        StartCoroutine(Spawn());
    }

    private void Update()
    {
        _scoreLabel.text = $"Score: {_score}";
        _shieldsLabel.text = $"Shields: {Player.Shields}";
        _degreeLabel.text = $"Degree: {PlayerController.Degree}";
        _protectionLabel.text = $"Protection: {Player.Protection}";
        _skillPointsLabel.text = $"Skill Points: {SkillPoints}";

        if (Status != 0)
            End();
    }

    private IEnumerator Spawn()
    {
        int remain = _gameObjects.Count;

        foreach (GameObject obj in _gameObjects)
        {
            _remainLabel.text = $"Remain: {remain--}";

            obj.transform.position = new Vector3(
                Random.Range(
                    LeftWall.position.x + _wallOffset,
                    RightWall.position.x - _wallOffset
                ),
                0f,
                Random.Range(
                    BottomWall.position.z + _wallOffset,
                    TopWall.position.z - _wallOffset
                )
            );

            if (obj.CompareTag("Asteroid"))
            {
                Asteroid asteroid = obj.GetComponent<Asteroid>();
                asteroid.IsActive = true;
                Asteroid.Speed += 0.05f;
            }

            CountScore();

            yield return new WaitForSeconds(_interval);
        }

        _remainLabel.text = $"Remain: {remain}";

        if (remain == 0)
            Game.Status = 1;
    }

    private void CountScore()
    {
        var player = _player.gameObject.GetComponent<Player>();

        if (player.Hp != null)
            _score += player.Hp.Get();

        if (_score > 1000)
        {
            _score = 0;
            SkillPoints++;
        }
    }

    private void End()
    {
        Time.timeScale = 0f;
        UIBehaviour.GameIsPaused = true;
    }

    private void GenerageObjects()
    {
        for (int i = 0; i < _totalObjects; i++)
        {
            int randomed = Random.Range(0, _enemies.Length);

            GameObject enemy = Instantiate(
                _enemies[randomed],
                Vector3.zero,
                Quaternion.identity,
                transform
            );

            _gameObjects.Push(enemy);
        }
    }
}
