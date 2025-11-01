using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UIElements;
[DefaultExecutionOrder(-100)]
public class SaveManager: MonoBehaviour {
    public static SaveManager Instance { get; private set; }
    public bool IsLoading { get; private set; } = true;
    //Variable para guardar la ruta, es en el Appdata/LocalLow/DefaultCompany/GlowRancher/
    private string saveAllPath;
    // Lista para registrar los objetos ISavable mediante el metodo RegisterSavable
    private List<ISavable> registeredSavables = new List<ISavable>();

    [Serializable] 
    private class SaveEntry { 
        public string id; 
        public object state; 
    }
    //Genera el singleton si no existe, si existe destruye el objeto duplicado
    // tambien asigna las rutas de guardado
    private void Awake() {
        if(Instance == null) { 
            Instance = this; 
        } 
        else { 
            Destroy(this.gameObject); 
        }
        // AppData / LocalLow / DefaultCompany / GlowRancher / SaveAll.bin 
        saveAllPath = Application.persistentDataPath + "/SaveAll.bin";
    }
    //Se suscribe al evento WriteData para guardar los datos
    private void Start() { 
        UnityEvents.Instance.WriteData.AddListener(SaveCurrentData); 
    }
    //Se desuscribe del evento WriteData para evitar errores
    private void OnDisable() { 
        UnityEvents.Instance.WriteData.RemoveListener(SaveCurrentData); 
    }
    // Registro simple que usan las clases objeto en OnEnable/OnDisable de // la clase que hereda de ISavable para agregarse o quitarse de la lista
    //registeredSavables
    public void RegisterSavable(ISavable savable) { 
        if (savable == null) return; 
        if (!registeredSavables.Contains(savable)) 
            registeredSavables.Add(savable); 
    }
    public void UnregisterSavable(ISavable savable) { 
        if (savable == null) return; 
        registeredSavables.Remove(savable); 
    }
    // Guarda todos los ISavable registrados en un fichero simple (id + payload)
    public void SaveAll(){
        try
        {
            Debug.Log("SaveAll persistent path: " + Application.persistentDataPath);
            var entries = new List<SaveEntry>(); //Crear una lista de SaveEntry que tiene id y state
            // Recorre todos los ISavable registrados y añade su estado a la lista
            foreach (var savable in registeredSavables) {
                entries.Add(new SaveEntry { id = savable.GetSaveID(), state = savable.CaptureState() });
            }
            // Lo convierte en un fichero binario utilizando BinaryFormatter y using FileStream
            using (FileStream fileStream = new FileStream(saveAllPath, FileMode.Create, FileAccess.Write, FileShare.None)) {
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
    public void LoadAll() {
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
                entries = (List<SaveEntry>)formatter.Deserialize(fileStream); //Crea una lista de SaveEntry, que tiene un string para el id y objeto
            }
            var dict = new Dictionary<string, object>();//Creamos el diccionario para guardar objetos de tipo SaveEntry con string id y object objecto
            for (int i = 0; i < entries.Count; i++)
            {
                var e = entries[i];
                if (e == null)
                {
                    Debug.LogWarning($"SaveAll: No se ha encontrado el objeto en el indice: {i}.");
                    continue;
                }
                if (string.IsNullOrEmpty(e.id)) //Si la id es null o empty
                {
                    string payloadType;
                    //Lo que hace es mirar el estado si no tiene id, si el estado es null pone el payloadType a null
                    if (e.state != null)
                    {
                        payloadType = e.state.GetType().FullName; //Busca el nombre
                    }
                    else
                    {
                        payloadType = "null";
                    }
                    //Da un warning ya que no tiene id
                    Debug.LogWarning($"SaveAll: skipped entry with null/empty id at index {i}, payload type: {payloadType}");
                    continue;
                }
                //Si esa entrada no tiene id la agrega
                if (!dict.ContainsKey(e.id))
                {
                    dict[e.id] = e.state;
                }
                else
                {
                    //Si ya existe la id no hace nada
                    Debug.Log($"SaveAll: duplicate entry for id '{e.id}' at index {i} ignored (first-wins).");
                }
            }
            var appliedIds = new List<string>();
            foreach (var s in registeredSavables){
                var sid = s.GetSaveID();
                if (string.IsNullOrEmpty(sid))
                {
                    Debug.LogWarning($"SaveAll: registered savable returned null/empty id: {s.GetType().FullName}");
                    continue;
                }
                if (dict.TryGetValue(sid, out var payload))
                {
                    try { 
                        s.RestoreState(payload); appliedIds.Add(sid); 
                    }
                    catch (Exception ex) { 
                        Debug.LogError($"Error restoring '{sid}': {ex.Message}"); 
                    }
                }
            }
            Debug.Log($"LoadAll completed from: {saveAllPath}. Applied {appliedIds.Count} entries.");
            if (appliedIds.Count > 0) { 
                Debug.Log("Applied IDs: " + string.Join(", ", appliedIds)); 
            }
            // Marca que la fase de carga ha terminado para que otros componentes
            // que dependían de IsLoading en Awake/Start puedan funcionar normalmente.
            IsLoading = false;
        }
        catch(Exception e)
        {
            Debug.LogError("Error in LoadAll: " + e.Message);
        }
    }
    private void SaveCurrentData() { 
        SaveAll(); 
    }
    //Metodo para imprimir todos los mensajes
    [ContextMenu("Log SaveAll contents")]
    public void LogSaveAllContents() {
        try {
            //Si no existe el fichero en la ruta da un warning
            if (!File.Exists(saveAllPath)) { 
                Debug.LogWarning("No SaveAll file found at: " + saveAllPath); 
                return; 
            }
            List<SaveEntry> entries;
            using (FileStream fileStream = new FileStream(saveAllPath, FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter formatter = new BinaryFormatter(); 
                entries = (List<SaveEntry>)formatter.Deserialize(fileStream);
            }
            //Cuenta cuantos objetos va a cargar desde la ruta
            Debug.Log($"SaveAll contains {entries.Count} entries at: {saveAllPath}");
            for (int i = 0; i < entries.Count; i++)
            {
                var e = entries[i];
                //Si alguno de los objetos que se van a cargar es null da un warning
                if (e == null) { 
                    Debug.LogWarning($"[{i}] <null entry>"); 
                    continue; 
                }
                //Si la id es null o empty le asigna
                var id = string.IsNullOrEmpty(e.id) ? "<empty id>" : e.id; // Si la id es null o empty pone empty id, sino es el caso id
                // se asigna al valor de e.id

                var payloadSummary = MessagePayload(e.state); //Guarda el mensaje string generado por MessagePayload en payloadSummary
                Debug.Log($"[{i}] id='{id}' payload={payloadSummary}");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error reading SaveAll: " + ex.Message);
        }
    }
    // Convierte un objeto en un texto, recibe cualquier objeto payload y lo retorna un string
    private string MessagePayload(object payload) {
        //Si no recibe payload no ejecuta no lo de abajo porque hace un return
        if (payload == null) return null;
        try
        {
            // Guarda el tipo de objeto en t, si es int, string, MiClase, etc...
            var type = payload.GetType();
            // Si el tipo de objeto es un tipo basico, eso significa que es primitive como puede ser un int o un boolean
            // entre otros o si es un string o decimal i lo retorna en un string como texto
            if (type.IsPrimitive || payload is string || payload is decimal) { 
                return payload.ToString(); 
            }
            var parts = new List<string>(); //Lista donde guardaremos los campos y propiedades
            //Con esto sabremos el nombre del campo, el tipo de variable, el valor del campo i el tipo de modificador si es un objeto complejo
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var field in fields)
            {
                object value;
                try
                {
                    value = field.GetValue(payload); // intentamos leer el valor
                }
                catch
                {
                    value = "<?>"; // si falla, ponemos marcador
                }
                string valueStr;
                if (value == null) valueStr = "null";
                else if (value.GetType().IsPrimitive || value is string || value is decimal)
                    valueStr = value.ToString();
                else if(value is Vector3 v3)
                {
                    valueStr = $"({v3.x}, {v3.y}, {v3.z})";
                }
                else
                {
                    valueStr = MessagePayload(value);
                }
                parts.Add($"{field.Name}={valueStr}"); // agregamos a la lista como "NombrePropiedad=Valor"
            }
            return $"{type.Name}{{{string.Join(", ", parts)}}}";
        }
        catch(Exception ex) {
            return "<dump-failed: " + ex.Message + ">";
        }



    }

}