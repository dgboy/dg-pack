﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class InventorySaver : MonoBehaviour {
    [SerializeField] private PlayerInventory myInventory = null;

    private void OnEnable() {
        myInventory.myInventory.Clear();
        LoadScriptables();
    }

    private void OnDisable() {
        SaveScriptables();
    }

    public void ResetScriptables() {
        int i = 0;
        string filepath = Application.persistentDataPath + string.Format("/{0}.inv", i);
        while (File.Exists(filepath)) {
            File.Delete(filepath);
            i++;
            filepath = Application.persistentDataPath + string.Format("/{0}.inv", i);
        }
    }

    public void SaveScriptables() {
        ResetScriptables();

        string filepath;
        for (int i = 0; i < myInventory.myInventory.Count; i++) {
            filepath = Application.persistentDataPath + string.Format("/{0}.inv", i);

            FileStream file = File.Create(filepath);
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(myInventory.myInventory[i]);
            binary.Serialize(file, json);
            file.Close();
        }
    }

    public void LoadScriptables() {
        int i = 0;
        string filepath = Application.persistentDataPath + string.Format("/{0}.inv", i);

        while (File.Exists(filepath)) {
            var temp = ScriptableObject.CreateInstance<InventoryItem>();
            FileStream file = File.Open(filepath, FileMode.Open);
            BinaryFormatter binary = new BinaryFormatter();
            JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), temp);
            file.Close();
            myInventory.myInventory.Add(temp);

            i++;
            filepath = Application.persistentDataPath + string.Format("/{0}.inv", i);
        }
    }
}
