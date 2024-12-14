using System.IO;
using System.IO.Compression;
using UnityEditor;
using UnityEngine;

public class GenerateZip : MonoBehaviour
{
    [MenuItem("Tools/Generate ZIP for Submission")]
    public static void CreateZip()
    {
        // Ruta del proyecto y del archivo ZIP
        string projectPath = Application.dataPath + "/.."; // Carpeta raíz del proyecto
        string desktopPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), "Andrade_Paredes_Roberto_SpaceShooter.zip");

        // Comprimir el proyecto
        try
        {
            // Verificar si el archivo ZIP ya existe en el escritorio
            if (File.Exists(desktopPath))
            {
                Debug.Log("El archivo ZIP ya existe en el escritorio. Eliminando...");
                File.Delete(desktopPath); // Eliminar el archivo existente
            }

            // Crear el archivo ZIP
            ZipFile.CreateFromDirectory(projectPath, desktopPath, System.IO.Compression.CompressionLevel.Optimal, true);
            Debug.Log("ZIP creado con éxito en el escritorio: " + desktopPath);

            // Abrir la carpeta donde se generó el ZIP
            EditorUtility.RevealInFinder(desktopPath);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error al crear el ZIP: " + ex.Message);
        }
    }
}
