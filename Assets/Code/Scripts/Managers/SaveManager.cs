using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
[DefaultExecutionOrder(-100)]
public class SaveManager: MonoBehaviour{

    public static SaveManager Instance { get; private set; }
    public bool IsLoading { get; private set; } = true;
    //Variable para guardar la ruta, es en el Appdata/LocalLow/DefaultCompany/GlowRancher/
    private string savePath;
    private string saveAllPath;

    // Lista para registrar los objetos ISavable mediante el metodo RegisterSavable
    private List<ISavable> registeredSavables = new List<ISavable>();

    [Serializable]
    private class SaveEntry
    {
        public string id;
        public object state;
    }
    //Genera el singleton si no existe, si existe destruye el objeto duplicado, 
    // tambien asigna las rutas de guardado
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }   
        savePath = Application.persistentDataPath + "/SaveData.bin";
        saveAllPath = Application.persistentDataPath + "/SaveAll.bin";
    }

    //Se suscribe al evento WriteData para guardar los datos
    private void Start()
    {
        UnityEvents.Instance.WriteData.AddListener(SaveCurrentPlayerData);
    }
    //Se desuscribe del evento WriteData para evitar errores
    private void OnDisable()
    {
        UnityEvents.Instance.WriteData.RemoveListener(SaveCurrentPlayerData);

    }
    // Registro simple que usan los componentes en OnEnable/OnDisable de 
    // la clase que hereda de ISavable para agregarse o quitarse de la lista 
    //   registeredSavables
    public void RegisterSavable(ISavable savable)
    {
        if (savable == null) return;
        if (!registeredSavables.Contains(savable)) registeredSavables.Add(savable);
    }

    public void UnregisterSavable(ISavable savable)
    {
        if (savable == null) return;
        registeredSavables.Remove(savable);
    }

    // Guarda todos los ISavable registrados en un fichero simple (id + payload)
    public void SaveAll()
    {
        try
        {
            Debug.Log("SaveAll persistent path: " + Application.persistentDataPath);
            var entries = new List<SaveEntry>(); //Crear una lista de SaveEntry que tiene 
            // id y state
            //Recorre todos los ISavable registrados y añade su estado a la lista
            foreach (var savable in registeredSavables)
            {
                entries.Add(new SaveEntry { id = savable.GetSaveID(), state = savable.CaptureState() });
            }
            //Lo convierte en un fichero binario utilizando BinaryFormatter y using FileStream
            using (FileStream fileStream = new FileStream(saveAllPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, entries);
            }
            Debug.Log("SaveAll saved in: " + saveAllPath);
        }
        catch (Exception e)
        {
            Debug.LogError("Error in SaveAll: " + e.Message);
        }
    }

    // Carga el fichero SaveAll.bin y llama RestoreState en los registrados cuando exista payload
    public void LoadAll()
    {
        try
        {
            //Da un warning si no existe el fichero en la ruta
            if (!File.Exists(saveAllPath))
            {
                Debug.LogWarning("No SaveAll file found at: " + saveAllPath);
                return;
            }

            List<SaveEntry> entries;
            using (FileStream fileStream = new FileStream(saveAllPath, FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                entries = (List<SaveEntry>)formatter.Deserialize(fileStream);
            }

            var dict = new Dictionary<string, object>();
            // Build a dictionary but skip any malformed entries (null entry or null/empty id)
            for (int i = 0; i < entries.Count; i++)
            {
                var e = entries[i];
                if (e == null)
                {
                    Debug.LogWarning($"SaveAll: skipped null entry at index {i}.");
                    continue;
                }
                if (string.IsNullOrEmpty(e.id))
                {
                    var payloadType = e.state != null ? e.state.GetType().FullName : "null";
                    Debug.LogWarning($"SaveAll: skipped entry with null/empty id at index {i}, payload type: {payloadType}");
                    continue;
                }
                if (!dict.ContainsKey(e.id))
                {
                    dict[e.id] = e.state;
                }
                else
                {
                    // Keep first occurrence (first-wins). Log for debugging.
                    Debug.Log($"SaveAll: duplicate entry for id '{e.id}' at index {i} ignored (first-wins).");
                }
            }

            var appliedIds = new List<string>();
            foreach (var s in registeredSavables)
            {
                var sid = s.GetSaveID();
                if (string.IsNullOrEmpty(sid))
                {
                    Debug.LogWarning($"SaveAll: registered savable returned null/empty id: {s.GetType().FullName}");
                    continue;
                }
                if (dict.TryGetValue(sid, out var payload))
                {
                    try
                    {
                        s.RestoreState(payload);
                        appliedIds.Add(sid);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Error restoring '{sid}': {ex.Message}");
                    }
                }
            }

            Debug.Log($"LoadAll completed from: {saveAllPath}. Applied {appliedIds.Count} entries.");
            if (appliedIds.Count > 0)
            {
                Debug.Log("Applied IDs: " + string.Join(", ", appliedIds));
            }

            // Marca que la fase de carga ha terminado para que otros componentes
            // que dependían de IsLoading en Awake/Start puedan funcionar normalmente.
            IsLoading = false;
        }
        catch (Exception e)
        {
            Debug.LogError("Error in LoadAll: " + e.Message);
        }
    }
    //Metodo que se ejecuta en el Player para guardar los datos creando un nuevo objeto
    private void SaveCurrentPlayerData(){
        //ApplyTempData();
        //SaveGame();
        SaveAll();
    }


    // Guarda el estado actual usando referencias encontradas en escena (KISS)
    public void SaveGame()
    {
        try
        {
            Player player = FindObjectOfType<Player>();
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            Farm farm = FindObjectOfType<Farm>();

            SaveData saveData = new SaveData(player, enemies, farm);

            using (FileStream fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, saveData);
            }
            Debug.Log("Data saved in: " + savePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Error al guardar: " + e.Message);
        }
    }

    // Context menu method to print the contents of SaveAll.bin (id + simple payload dump)
    [ContextMenu("Log SaveAll contents")]
    public void LogSaveAllContents()
    {
        try
        {
            if (!File.Exists(saveAllPath))
            {
                Debug.LogWarning("No SaveAll file found at: " + saveAllPath);
                return;
            }

            List<SaveEntry> entries;
            using (FileStream fileStream = new FileStream(saveAllPath, FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                entries = (List<SaveEntry>)formatter.Deserialize(fileStream);
            }

            Debug.Log($"SaveAll contains {entries.Count} entries at: {saveAllPath}");
            for (int i = 0; i < entries.Count; i++)
            {
                var e = entries[i];
                if (e == null)
                {
                    Debug.Log($"[{i}] <null entry>");
                    continue;
                }
                var id = string.IsNullOrEmpty(e.id) ? "<empty id>" : e.id;
                var payloadSummary = DumpPayload(e.state);
                Debug.Log($"[{i}] id='{id}' payload={payloadSummary}");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error reading SaveAll: " + ex.Message);
        }
    }

    // Simple safe dump: primitives and strings are printed directly; for objects we list up to 10 fields/props
    private string DumpPayload(object payload)
    {
        if (payload == null) return "null";
        try
        {
            var t = payload.GetType();
            // Common simple case
            if (t.IsPrimitive || payload is string || payload is decimal)
            {
                return payload.ToString();
            }

            var parts = new List<string>();
            int count = 0;
            var fields = t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var f in fields)
            {
                object val;
                try { val = f.GetValue(payload); } catch { val = "<?>"; }
                parts.Add($"{f.Name}={val}");
                if (++count >= 10) break;
            }
            if (count < 10)
            {
                var props = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (var p in props)
                {
                    if (!p.CanRead) continue;
                    object val;
                    try { val = p.GetValue(payload, null); } catch { val = "<?>"; }
                    parts.Add($"{p.Name}={val}");
                    if (++count >= 10) break;
                }
            }
            return $"{t.Name}{{{string.Join(", ", parts)}}}";
        }
        catch (Exception ex)
        {
            return "<dump-failed: " + ex.Message + ">";
        }
    }
}
