// TODO; 自動割り振りがあるときにビルドするとエラーになるので、
// 一旦コメントアウトしておく

/*using System;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.AddressableAssets;

namespace EditorSupport
{
    public class AutoSubmitAddressable : AssetPostprocessor
    {

        private enum eAddressType
        {
            Chara,
            Window,
            GameObj,
        }
        
        private static readonly string TargetPaths = @"Assets/Prefabs/GameObj/";
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="importedAssets">インポートされたもの</param>
        /// <param name="deletedAssets">削除されたもの</param>
        /// <param name="movedAssets">移動されたもの</param>
        /// <param name="movedFromAssetPaths">わからんから調べる</param>
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            var dirty = false;
            
            dirty |= CharaChangeEntry(importedAssets, false);
            dirty |= CharaChangeEntry(deletedAssets, true);
            dirty |= CharaChangeEntry(movedAssets, false);
        
            if (dirty)
            {
                AssetDatabase.SaveAssets();
            }
        }

        private static bool CharaChangeEntry(string[] paths, bool delete)
        {
            // Addressableをスクリプトから制御するときに必要なクラス
            var settings = AddressableAssetSettingsDefaultObject.Settings;
            // Addressableのグループ取得
            // var groups = settings.groups;
            var dirty = false;
       
            foreach (var path in paths)
            {
                var pathMatch = Regex.Match(path, @"\d+/\d/");
                var nameMatch = Regex.Match(path, @"Chara\d+");
                
                // アドレス対象外のものであれば割り振らない
                if (nameMatch.Value.Equals(string.Empty))
                    continue;
                
                // 読み込みたいパスを引数に入れてそれと合うか検索
                if (!path.StartsWith(TargetPaths + pathMatch.Value))
                    continue;
            
                // フォルダの存在確認
                if (AssetDatabase.IsValidFolder(path))
                    continue;

                // 一致部分を消して文字列として登録
                var assetPath = path.Replace(TargetPaths, "");
                
                var targetGroup = settings.DefaultGroup;
                
                // DirectorySeparatorChar = \
                // AltDirectorySeparatorChar = /
                // でもPathクラスではどっち使ってもいいよとのことらしい
                // TODO:グループ分けるときに使用 一旦コメントアウト
                *//*var rootIndex = assetPath.IndexOf(System.IO.Path.DirectorySeparatorChar, StringComparison.Ordinal);
                if (rootIndex >= 0)
                {
                    var rootPath = assetPath.Substring(0, rootIndex);
                    var groupIndex = groups.FindIndex(g => g.Name == rootPath);
                    if (groupIndex >= 0)
                    {
                        targetGroup = groups[groupIndex];
                    }
                    else
                    {
                        var groupTemplate = settings.GetGroupTemplateObject(0) as AddressableAssetGroupTemplate;
                        targetGroup = settings.CreateGroup(rootPath, false, false, false, null, groupTemplate.GetTypes());
                    }
                
                    assetPath = assetPath.Substring(rootIndex + 1);
                }*//*

                var guid = AssetDatabase.AssetPathToGUID(path);
                if (delete)
                {
                    settings.RemoveAssetEntry(guid);
                }
                else
                {
                    var entry = settings.CreateOrMoveEntry(guid, targetGroup);
                    if (entry.address == nameMatch.Value)
                        continue;
                
                    entry.address = nameMatch.Value;
                }

                dirty = true;
            }

            return dirty;
        }
        
        
    }
}
*/