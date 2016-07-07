using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterStatusHandler : MonoBehaviour
{
    private Character character;
    private List<Status> statuses = new List<Status>();
    private bool processing;


    void Awake()
    {
        character = GetComponent<Character>();  
    }

    public void ApplyStatus(StatusData data)
    {
        GameObject effect = StatusConfig.instance.CreateEffect(character, data);
        statuses.Add(new Status(data, effect));

        if (!processing)
            StartCoroutine(ProcessStatuses());
    }

    IEnumerator ProcessStatuses()
    {
        processing = true;
        float passedTime = 0f;

        while (statuses.Count > 0f)
        {
            passedTime += GameStateManager.instance.gameTime * Time.deltaTime;

            if (passedTime >= StatusConfig.instance.statusTickRate)
            {
                for (int i = statuses.Count - 1; i >= 0; i--)
                    ProcessStatus(statuses[i]);

                passedTime = 0f;
            }

            yield return null;
        }

        processing = false;
    }

    void ProcessStatus(Status status)
    {
        if (status.Process(character))
        {
            status.Cleanup();
            statuses.Remove(status);
        }
           

    }

    public bool HasStatus(StatusData.Type type)
    {
        foreach (Status status in statuses)
            if (status.type == type)
                return true;

        return false;
    }
	

}
