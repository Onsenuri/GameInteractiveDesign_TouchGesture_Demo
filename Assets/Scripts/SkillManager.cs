using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // 1 T 2 F 3 S 4 N
    public int[] skills = {0,0,0,0,0};
    public Queue<int> skillQueue = new Queue<int>();
    public float skillGenRate = 1.0f;
    private float _skillCoolTime = 0.0f;
    private static SkillManager instance = null;
    private SkillPanel _skillPanel;

    public void InitializeSkills()
    {
        for (int i = 0; i < 5; i++)
        {
            int randomSkill = Random.Range(1, 4);
            skills[i] = randomSkill;
            if (randomSkill == 1)
            {
                Debug.Log("T");
                _skillPanel.AddSkillIcon("Thunder");
            }
            else if (randomSkill == 2)
            {
                _skillPanel.AddSkillIcon("FireBall");
            }
            else if (randomSkill == 3)
            {
                _skillPanel.AddSkillIcon("StoneBall");
            }
        }
    }

    void AddSkill()
    {
        for (int i = 0; i < 5; i++)
        {
            if (skills[i] == 0)
            {
                int randomSkill = Random.Range(1, 4);
                if (randomSkill == 1)
                {
                    Debug.Log("T");
                    _skillPanel.AddSkillIcon("Thunder");
                }
                else if (randomSkill == 2)
                {
                    _skillPanel.AddSkillIcon("FireBall");
                }
                else if (randomSkill == 3)
                {
                    _skillPanel.AddSkillIcon("StoneBall");
                }
                skills[i] = randomSkill;
                break;
            }
        }
    }
    private void PrintSkills()
    {
        Debug.Log(skills[0] +","+ skills[1] +","+ skills[2] +","+ skills[3] +","+ skills[4]);
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        _skillPanel = GameObject.FindWithTag("SkillPanel").GetComponent<SkillPanel>();
        InitializeSkills();
    }

    public static SkillManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_skillCoolTime >= skillGenRate)
        {
            AddSkill();
            _skillCoolTime = 0.0f;
        }
        else
        {
            _skillCoolTime += Time.deltaTime;
        }
        
        PrintSkills();
    }
}
