using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGenome : MonoBehaviour
{
    List<GoapAction> skills = new List<GoapAction>();
    public int mutationOdds;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GoapAction a in gameObject.GetComponents<GoapAction>())
        {
            a.cost = Random.Range(-5f, 10f);
            skills.Add(a);
        }
        mutationOdds = Random.Range(2, 7);
        Mutate();
    }

    void Mutate()
    {
        foreach(var a in skills)
            if(Random.Range(1,mutationOdds)==1)
                a.cost = Random.Range(-5f, 10f);
    }

    void Breed(SkillGenome other)
    {
        for(int i = 0; i < skills.Count;i++)
        if (Random.Range(1, mutationOdds) == 1)
            skills[i].cost = Random.Range(0,2)==1?skills[i].cost:other.skills[i].cost;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Mutate();
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            var allGenomes = GameObject.FindObjectsOfType<SkillGenome>();
            Breed(allGenomes[Random.Range(0, allGenomes.Length)]);
        }
    }
}
