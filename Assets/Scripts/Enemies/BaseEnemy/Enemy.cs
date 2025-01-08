using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected EnemyStateManager enemyStateManager;
    protected BaseEnemyState deathStateToTrigger;

    // Start is called before the first frame update
    public virtual void Start()
    {
        enemyStateManager = new EnemyStateManager();
        enemyStateManager.Start(this);

        int areas = GetAreasCleared();

        MaxHealth = MaxHealthList[areas];
        CurrentHealth = MaxHealth;
        MinMoneyOnKill = MinMoneyOnKillList[areas];
        MaxMoneyOnKill = MaxMoneyOnKillList[areas];

        deathStateToTrigger = new GenericEnemyDeathState();
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        enemyStateManager.FixedUpdate();
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        enemyStateManager.OnColliderEnter2D(other);
    }

    public EnemyStateManager GetEnemyStateManager()
    {
        return enemyStateManager;
    }

    public int GetAreasCleared()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

        int areas = player.CurrentAreasCleared;
        if (areas < 0) areas = 0;
        if (areas > 5) areas = 5;

        return areas;
    }

    public virtual void TakeDamage(int baseDamage, bool skipArmor = false)
    {
        if (skipArmor || Armor.Count == 0)
        {
            CurrentHealth -= baseDamage;

            if (CurrentHealth <= 0)
            {
                enemyStateManager.TransitionToState(deathStateToTrigger);
            }
        }
        else
        {
            Armor[Armor.Count - 1] -= baseDamage;

            if (Armor[Armor.Count - 1] <= 0)
            {
                Armor.RemoveAt(Armor.Count - 1);
            }
        }
    }

    // initial min/max properties
    [SerializeField] protected List<int> maxHealthList = new List<int>() { 100, 100, 100, 100, 100, 100 };
    public List<int> MaxHealthList
    {
        get { return maxHealthList; }
        set { maxHealthList = value; }
    }

    [SerializeField] protected List<int> minMoneyOnKillList = new List<int>() { 1, 1, 1, 1, 1, 1 };
    public List<int> MinMoneyOnKillList
    {
        get { return minMoneyOnKillList; }
        set { minMoneyOnKillList = value; }
    }

    [SerializeField] protected List<int> maxMoneyOnKillList = new List<int>() { 5, 5, 5, 5, 5, 5 };
    public List<int> MaxMoneyOnKillList
    {
        get { return maxMoneyOnKillList; }
        set { maxMoneyOnKillList = value; }
    }

    [SerializeField] protected List<int> armor = new List<int>();
    public List<int> Armor
    {
        get { return armor; }
        set { armor = value; }
    }

    // properties that represent the current state
    private int _maxHealth;
    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    private int _currentHealth;
    public int CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    private int _minMoneyOnKill;
    public int MinMoneyOnKill
    {
        get { return _minMoneyOnKill; }
        set { _minMoneyOnKill = value; }
    }

    private int _maxMoneyOnKill;
    public int MaxMoneyOnKill
    {
        get { return _maxMoneyOnKill; }
        set { _maxMoneyOnKill = value; }
    }
}