// This is an Editor script. DO NOT MOVE.
// It should be in </Assets/Editor>


// Credit to Herman Tulleken 
// http://answers.unity3d.com/questions/12187/import-settings.html

using UnityEditor;

public class FBX_Scale_Fix : AssetPostprocessor
{
    public void OnPreprocessModel()
    {
        ModelImporter modelImporter = (ModelImporter) assetImporter;                    
        modelImporter.globalScale = 1;  // Scale to factor of 1        
    }   
}