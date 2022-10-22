#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorUtils
{
    public static bool DoesAssetHavePreview(Object asset)
    {
        if (asset != null)
        {
            var assetAsGameobject = asset as GameObject;
            if (assetAsGameobject != null)
            {
                var renderer = assetAsGameobject.GetComponentInChildren<Renderer>();
                if (renderer != null)
                {
                    return true;
                }

                var imageComponent = assetAsGameobject.GetComponentInChildren<UnityEngine.UI.Image>();
                if (imageComponent != null && imageComponent.sprite != null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static Texture2D GetAssetPreview(Object asset)
    {
        if (asset is Texture2D)
        {
            return asset as Texture2D;
        }
        Texture2D assetPreview = AssetPreview.GetAssetPreview(asset);
        if (assetPreview == null)
        {
            GameObject assetAsGameobject = asset as GameObject;
            if (assetAsGameobject != null)
            {
                Renderer renderer = assetAsGameobject.GetComponentInChildren<Renderer>();
                if (renderer != null)
                {
                    if (renderer is SkinnedMeshRenderer)
                    {
                        var skinnedMeshRenderer = renderer as SkinnedMeshRenderer;
                        if (skinnedMeshRenderer != null && skinnedMeshRenderer.sharedMesh != null)
                        {
                            var sourceAssetPath = AssetDatabase.GetAssetPath(skinnedMeshRenderer.sharedMesh);
                            assetPreview = AssetPreview.GetAssetPreview(AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(sourceAssetPath));
                        }
                    }
                    else if (renderer is MeshRenderer)
                    {
                        var meshFilter = renderer.GetComponent<MeshFilter>();
                        if (meshFilter != null && meshFilter.sharedMesh != null)
                        {
                            var sourceAssetPath = AssetDatabase.GetAssetPath(meshFilter.sharedMesh);
                            assetPreview = AssetPreview.GetAssetPreview(AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(sourceAssetPath));
                        }
                    }
                }

                if (assetPreview == null)
                {
                    var imageComponent = assetAsGameobject.GetComponentInChildren<UnityEngine.UI.Image>();
                    Sprite sprite = null;
                    if (imageComponent != null)
                    {
                        sprite = imageComponent.sprite;
                    }
                    if (sprite == null)
                    {
                        SpriteRenderer spriteComponent = assetAsGameobject.GetComponentInChildren<SpriteRenderer>();
                        if (spriteComponent != null)
                        {
                            sprite = spriteComponent.sprite;
                        }
                    }
                    if (sprite != null)
                    {
                        assetPreview = sprite.texture;
                    }
                }
            }
        }
        return assetPreview;
    }

    public static List<Object> GetAllAssetsOfType(System.Type type)
    {
        if (!typeof(Object).IsAssignableFrom(type))
        {
            Debug.LogError("'GetAllAssetsOfType' can only be performed for type that is 'Object'");
            return null;
        }

        var assets = new List<Object>();
        string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", type.Name));
        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            var asset = AssetDatabase.LoadAssetAtPath(assetPath, type);
            if (asset != null)
            {
                assets.Add(asset);
            }
        }
        return assets;
    }

    public static List<T> GetAllAssetsOfType<T>() where T : Object
    {
        return GetAllAssetsOfType<T>(new string[] { "Assets" });
    }

    public static List<T> GetAllAssetsOfType<T>(string[] searchInFolders) where T : Object
    {
        var assets = new List<T>();
        string[] guids = AssetDatabase.FindAssets($"t:{(typeof(T)).Name}", searchInFolders);
        var pathsOfAssetsWithMultipleSubAssets = new HashSet<string>();
        for (int i = 0; i < guids.Length; i++)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if (asset == null)
            {
                continue;
            }
            if (!AssetDatabase.IsMainAsset(asset))
            {
                pathsOfAssetsWithMultipleSubAssets.Add(assetPath);
                continue;
            }
            if (asset != null)
            {
                assets.Add(asset);
            }
        }

        foreach (var complexAssetPath in pathsOfAssetsWithMultipleSubAssets)
        {
            var allAssetsAtPath = AssetDatabase.LoadAllAssetsAtPath(complexAssetPath);
            foreach (var asset in allAssetsAtPath)
            {
                if (asset is T assetAsT)
                {
                    assets.Add(assetAsT);
                }
            }
        }
        return assets;
    }

    public static void EnsureFieldIsNotNullSearchInChildren<T>(GameObject gameObject, ref T component) where T : Component
    {
        if (component == null)
        {
            component = gameObject.GetComponentInChildren<T>();
            if (component != null)
            {
                EditorUtility.SetDirty(gameObject);
            }
        }
    }

    public static void EnsureFieldIsNotNullSearchInParent<T>(GameObject gameObject, ref T component) where T : Component
    {
        if (component == null)
        {
            component = gameObject.GetComponentInParent<T>();
            if (component != null)
            {
                EditorUtility.SetDirty(gameObject);
            }
        }
    }

    public static bool IsObjectSelfSelected(GameObject gameObject) => IsObjectSelfSelected(gameObject.GetInstanceID());

    public static bool IsObjectSelfSelected(int gameObjectID)
    {
        if (Selection.gameObjects!=null)
        {
            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                if (Selection.gameObjects[i].GetInstanceID() == gameObjectID)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
#endif
