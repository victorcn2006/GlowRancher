/// <summary>
/// ISavable obliga a un objeto a exponer un ID único para guardado y 
/// proporciona métodos para capturar y restaurar su estado.
/// </summary>
public interface ISavable{
    //Identificador único del objeto para guardado/carga.
    string GetSaveID();
    //Captura el estado actual del objeto para guardarlo.
    object CaptureState();
    //Restaura el estado del objeto a partir de un estado guardado previamente.
    void RestoreState(object state);
}
