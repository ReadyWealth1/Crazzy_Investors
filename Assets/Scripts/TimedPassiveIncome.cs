using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedPassiveIncome : MonoBehaviour
{
    private Assets assetsManager;
    private Dictionary<string, List<TimedIncome>> timedIncomes = new Dictionary<string, List<TimedIncome>>();

    private void Start()
    {
        assetsManager = GetComponent<Assets>();
        if (assetsManager == null)
        {
            Debug.LogError("Assets component not found on the same GameObject.");
            return;
        }
        StartCoroutine(ProcessTimedIncomes());
    }

    public void AddTimedIncome(string assetType, int amount, float duration)
    {
        if (!timedIncomes.ContainsKey(assetType))
        {
            timedIncomes[assetType] = new List<TimedIncome>();
        }
        timedIncomes[assetType].Add(new TimedIncome(amount, duration));
    }

    private IEnumerator ProcessTimedIncomes()
    {
        while (true)
        {
            foreach (var asset in timedIncomes.Keys)
            {
                timedIncomes[asset].RemoveAll(income => income.RemainingTime <= 0);
            }
            yield return new WaitForSeconds(1f);
            foreach (var asset in timedIncomes.Keys)
            {
                foreach (var income in timedIncomes[asset])
                {
                    income.RemainingTime -= 1f;
                    if (income.RemainingTime <= 0)
                    {
                        RemovePassiveIncome(asset, income.Amount);
                    }
                }
            }
        }
    }

    private void RemovePassiveIncome(string assetType, int amount)
    {
        switch (assetType)
        {
            case "Stock":
                assetsManager.TotalStockPassiveIncome -= amount;
                break;
                // Add cases for other asset types as needed
        }
        GameManager.PassiveIncome -= amount;
        assetsManager.UpdateUI();
    }

    private class TimedIncome
    {
        public int Amount { get; private set; }
        public float RemainingTime { get; set; }

        public TimedIncome(int amount, float duration)
        {
            Amount = amount;
            RemainingTime = duration;
        }
    }
}