using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : MonoBehaviour
{
    private GameObject newSkillIcon;
    public GameObject skill_Thunder;
    public GameObject skill_FireBall;
    public GameObject skill_StoneBall;
    
    public void DeleteSkillIcon(string skillName)
    {
        switch (skillName)
        {
            case ("Thunder"):
                skill_Thunder.SetActive(false);
                break;
            case ("FireBall"):
                skill_FireBall.SetActive(false);
                break;
            case ("StoneBall"):
                skill_StoneBall.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void AddSkillIcon(string skillName)
    {
        switch (skillName)
        {
            case ("Thunder"):
                newSkillIcon = Instantiate(skill_Thunder);
                newSkillIcon.transform.SetParent(this.gameObject.transform, false);
                newSkillIcon.SetActive(true);
                break;
            case ("FireBall"):
                newSkillIcon = Instantiate(skill_FireBall);
                newSkillIcon.transform.SetParent(this.gameObject.transform, false);
                newSkillIcon.SetActive(true);
                break;
            case ("StoneBall"):
                newSkillIcon = Instantiate(skill_StoneBall);
                newSkillIcon.transform.SetParent(this.gameObject.transform, false);
                newSkillIcon.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void ResetSkillIcon()
    {
        foreach (Transform child in this.gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //ResetSkillIcon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
