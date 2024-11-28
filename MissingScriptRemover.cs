#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Plugins.AhojSystem.EditorExtensions.MissingScriptRemover {
    public static class MissingScriptRemover {
        [MenuItem("AhojSystem/Missing Script Remover")]
        private static void BulkRemover() {
            var selectedObjects = Selection.gameObjects;
            if (selectedObjects.Length == 0) {
                Debug.LogWarning("オブジェクトが選択されていません");
                return;
            }

            foreach (var gameObject in selectedObjects) {
                var removeCount = RemoveMissingScriptsRecursively(gameObject);
                Debug.Log($"{gameObject.name}から{removeCount}個のMissing Scriptを削除しました");
            }

            Debug.Log("処理完了");
        }

        private static int RemoveMissingScriptsRecursively(GameObject gameObject) =>
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gameObject) + gameObject.transform
                .Cast<Transform>().Sum(child => RemoveMissingScriptsRecursively(child.gameObject));
    }
}
#endif

// Copyright (c) 2024 AhojSystem