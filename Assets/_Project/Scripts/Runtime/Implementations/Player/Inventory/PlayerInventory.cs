﻿using CanvasDEV.Runtime.Core.GameState;
using CanvasDEV.Runtime.Systems.Inventory;
using System;
using UnityEngine;

public class PlayerInventory : InventoryBase
{
    public static PlayerInventory Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }


    public event Action<int> OnUpdateMoney;

    [SerializeField] private int playerStartMoney = 100;
    private int _playerMoney;

    private void Start()
    {
        _playerMoney = playerStartMoney;
    }

    public override void Open()
    {
        GameStateHandler.ChangeState(GameState.Inventory, this);
    }

    public bool HasMoneyTo(int cost)
    {
        return _playerMoney >= cost;
    }

    public void AddMoney(int amount)
    {
        _playerMoney += amount;
        OnUpdateMoney?.Invoke(_playerMoney);
    }

    public void RemoveMoney(int amount)
    {
        _playerMoney -= amount;
        OnUpdateMoney?.Invoke(_playerMoney);
    }

    public int GetMoney()
    {
        return _playerMoney;
    }
}