//////// Custom Tree-IGenericMenu Example ////////
/*
    To create a hotkey you can use the following special characters: % (ctrl on Windows, cmd on macOS), # (shift), & (alt).
    To create a special hotkey you can use "MySubItem/MyMenuItem %LEFT" "MySubItem/MyMenuItem %HOME" "MySubItem/MyMenuItem #ENDER".
    You can configure and choose special windows for this hotkeys in the plugin settings.
*/

#if UNITY_EDITOR

using System.Linq;
using System.Collections.Generic;
using EModules.EModulesInternal;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;


/////////////////////////////////////////////////////MENU ITEM TEMPLATE///////////////////////////////////////////////////////////////////////////////
/*

    class MyMenu : HierarchyExtensions.IGenericMenu
    {
        public string Name { get { return "MySubItem/MyMenuItem %k"; } }
        public int PositionInMenu { get { return 0; } }

        public bool IsEnable(GameObject clickedObject) { return true; }
        public bool NeedExcludeFromMenu(GameObject clickedObject) { return false; } // or 'return clickedObject.GetComponent<MyComponent>() == null'

        public void OnClick(GameObject[] affectedObjectsArray)
        {
            throw new System.NotImplementedException();
        }
    }

*/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


namespace Hierarchy_Examples {



#region ITEM 100-101 - Group/UnGroup

class MyMenu_GroupFirst : HierarchyExtensions.IGenericMenu {
    public bool IsEnable( GameObject clickedObject ) { return true; }
    public bool NeedExcludeFromMenu( GameObject clickedObject ) { return false; }
    
    public int PositionInMenu { get { return 100; } }
    public string Name { get { return "Group (First Center) %g"; } }
    
    
    public void OnClick( GameObject[] affectedObjectsArray )
    {   var onlytop = MyMenu_Utils.GetOnlyTopObjects(affectedObjectsArray).OrderBy(go => go.transform.GetSiblingIndex()).ToArray();
        if ( onlytop.Length == 0 ) return;
        
        var groupParent = onlytop[0].transform.parent;
        var groupSiblingIndex = onlytop[0].transform.GetSiblingIndex();
        
        var groupRoot = new GameObject("GROUP " + onlytop[0].name);
        groupRoot.transform.SetParent( groupParent, false );
        groupRoot.transform.localScale = Vector3.one;
        groupRoot.transform.SetSiblingIndex( groupSiblingIndex );
        //********************************//
        groupRoot.transform.position = onlytop[0].transform.position;
        groupRoot.transform.rotation = onlytop[0].transform.rotation;
        //********************************//
        
        MyMenu_Utils.AssignUniqueName( groupRoot ); // name
        if ( groupRoot.GetComponentsInParent<Canvas>( true ).Length != 0 )     // canvas
        {   var rect = groupRoot.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            groupRoot.AddComponent<CanvasRenderer>();
        }
        
        Undo.RegisterCreatedObjectUndo( groupRoot, groupRoot.name );
        
        foreach ( var gameObject in onlytop )
        {   Undo.SetTransformParent( gameObject.transform, groupRoot.transform, groupRoot.name );
        }
        
        HierarchyExtensions.Utilities.SetExpanded( groupRoot.GetInstanceID(), true );
        
        Selection.objects = onlytop.ToArray();
        //Selection.objects = new[] { groubObject };
    }
    
}
class MyMenu_GroupWorld : HierarchyExtensions.IGenericMenu {
    public bool IsEnable( GameObject clickedObject ) { return true; }
    public bool NeedExcludeFromMenu( GameObject clickedObject ) { return false; }
    
    public int PositionInMenu { get { return 101; } }
    public string Name { get { return "Group (World Center) %#g"; } }
    
    
    public void OnClick( GameObject[] affectedObjectsArray )
    {   var onlytop = MyMenu_Utils.GetOnlyTopObjects(affectedObjectsArray).OrderBy(go => go.transform.GetSiblingIndex()).ToArray();
        if ( onlytop.Length == 0 ) return;
        
        var groupParent = onlytop[0].transform.parent;
        var groupSiblingIndex = onlytop[0].transform.GetSiblingIndex();
        
        var groupRoot = new GameObject("GROUP " + onlytop[0].name);
        groupRoot.transform.SetParent( groupParent, false );
        groupRoot.transform.localScale = Vector3.one;
        groupRoot.transform.SetSiblingIndex( groupSiblingIndex );
        //********************************//
        //********************************//
        
        MyMenu_Utils.AssignUniqueName( groupRoot ); // name
        if ( groupRoot.GetComponentsInParent<Canvas>( true ).Length != 0 )     // canvas
        {   var rect = groupRoot.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            groupRoot.AddComponent<CanvasRenderer>();
        }
        
        Undo.RegisterCreatedObjectUndo( groupRoot, groupRoot.name );
        
        foreach ( var gameObject in onlytop )
        {   Undo.SetTransformParent( gameObject.transform, groupRoot.transform, groupRoot.name );
        }
        
        HierarchyExtensions.Utilities.SetExpanded( groupRoot.GetInstanceID(), true );
        
        Selection.objects = onlytop.ToArray();
        //Selection.objects = new[] { groubObject };
    }
    
}
class MyMenu_GroupAverage : HierarchyExtensions.IGenericMenu {
    public bool IsEnable( GameObject clickedObject ) { return true; }
    public bool NeedExcludeFromMenu( GameObject clickedObject ) { return false; }
    
    public int PositionInMenu { get { return 102; } }
    public string Name { get { return "Group (Average Center)"; } }
    
    
    public void OnClick( GameObject[] affectedObjectsArray )
    {   var onlytop = MyMenu_Utils.GetOnlyTopObjects(affectedObjectsArray).OrderBy(go => go.transform.GetSiblingIndex()).ToArray();
        if ( onlytop.Length == 0 ) return;
        
        var groupParent = onlytop[0].transform.parent;
        var groupSiblingIndex = onlytop[0].transform.GetSiblingIndex();
        
        var groupRoot = new GameObject("GROUP " + onlytop[0].name);
        groupRoot.transform.SetParent( groupParent, false );
        groupRoot.transform.localScale = Vector3.one;
        groupRoot.transform.SetSiblingIndex( groupSiblingIndex );
        //********************************//
        Vector3 center = Vector3.zero;
        Vector3 rot = Vector3.zero;
        foreach ( var item in onlytop )
        {   center += item.transform.position;
            rot += item.transform.eulerAngles;
        }
        center /= onlytop.Length;
        rot /= onlytop.Length;
        groupRoot.transform.position = center;
        groupRoot.transform.eulerAngles = rot;
        //********************************//
        
        MyMenu_Utils.AssignUniqueName( groupRoot ); // name
        if ( groupRoot.GetComponentsInParent<Canvas>( true ).Length != 0 )     // canvas
        {   var rect = groupRoot.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            groupRoot.AddComponent<CanvasRenderer>();
        }
        
        Undo.RegisterCreatedObjectUndo( groupRoot, groupRoot.name );
        
        foreach ( var gameObject in onlytop )
        {   Undo.SetTransformParent( gameObject.transform, groupRoot.transform, groupRoot.name );
        }
        
        HierarchyExtensions.Utilities.SetExpanded( groupRoot.GetInstanceID(), true );
        
        Selection.objects = onlytop.ToArray();
        //Selection.objects = new[] { groubObject };
    }
    
}

class MyMenu_UnGroup : HierarchyExtensions.IGenericMenu {
    public bool IsEnable( GameObject clickedObject ) { return clickedObject.transform.childCount != 0; }
    public bool NeedExcludeFromMenu( GameObject clickedObject ) { return false; }
    
    public int PositionInMenu { get { return 103; } }
    public string Name { get { return "UnGroup"; } }
    
    
    public void OnClick( GameObject[] affectedObjectsArray )
    {   var ungroupedObjects = new List<GameObject>();
        var onlytop = MyMenu_Utils.GetOnlyTopObjects(affectedObjectsArray);
        foreach ( var ungroupedRoot in onlytop )
        {   var ungroupSiblinkIndex = ungroupedRoot.transform.GetSiblingIndex();
            var ungroupParent = ungroupedRoot.transform.parent;
            var undoName = ungroupedRoot.name;
            for ( int i = ungroupedRoot.transform.childCount - 1 ; i >= 0 ; i-- )
            {   var o = ungroupedRoot.transform.GetChild(i);
                Undo.SetTransformParent( o.transform, ungroupParent, "Remove " + undoName );
                
                if ( !Application.isPlaying ) Undo.RegisterFullObjectHierarchyUndo( o, "Remove " + undoName );
                o.SetSiblingIndex( ungroupSiblinkIndex );
                if ( !Application.isPlaying ) EditorUtility.SetDirty( o );
                
                ungroupedObjects.Add( o.gameObject );
            }
            Undo.DestroyObjectImmediate( ungroupedRoot );
        }
        Selection.objects = ungroupedObjects.ToArray();
    }
    
}

#endregion





#region ITEM 200-203 - Sibling

class MyMenu_Sibling0 : HierarchyExtensions.IGenericMenu {
    public bool IsEnable( GameObject clickedObject ) { return true; }
    public bool NeedExcludeFromMenu( GameObject clickedObject ) { return false; }
    public int PositionInMenu { get { return 200; } }
    public string Name { get { return "Sibling/Set Previous Sibling Index %'"; } }
    public void OnClick( GameObject[] affectedObjectsArray )
    {   var obs = Selection.gameObjects.Select(g => g.transform).ToArray();
        if ( obs.Length == 0 ) return;
        obs = obs.OrderBy( o => o.GetSiblingIndex() ).ToArray();
        List<Transform> moveBack = new List<Transform>();
        foreach ( var item in obs.Select( o => new { sib = o.GetSiblingIndex(), transform = o } ).ToArray() )
        {   var sib = item.sib - 1;
            Undo.SetTransformParent( item.transform, item.transform.parent, "Set Previous Sibling Index" );
            if ( sib < 0 ) moveBack.Add( item.transform );
            item.transform.SetSiblingIndex( sib );
        }
        foreach ( var transform in moveBack )
        {   transform.SetAsFirstSibling();
        }
    }
}
class MyMenu_Sibling1 : HierarchyExtensions.IGenericMenu {
    public bool IsEnable( GameObject clickedObject ) { return true; }
    public bool NeedExcludeFromMenu( GameObject clickedObject ) { return false; }
    public int PositionInMenu { get { return 201; } }
    public string Name { get { return "Sibling/Set Next Sibling Index %/"; } }
    public void OnClick( GameObject[] affectedObjectsArray )
    {   var obs = Selection.gameObjects.Select(g => g.transform).ToArray();
        if ( obs.Length == 0 ) return;
        obs = obs.OrderByDescending( o => o.GetSiblingIndex() ).ToArray();
        List<Transform> moveBack = new List<Transform>();
        foreach ( var item in obs.Select( o => new { sib = o.GetSiblingIndex(), transform = o } ).ToArray() )
        {   var sib = item.sib + 1;
            Undo.SetTransformParent( item.transform, item.transform.parent, "Set Next Sibling Index" );
            var nned = sib;
            item.transform.SetSiblingIndex( sib );
            if ( nned != item.transform.GetSiblingIndex() ) moveBack.Add( item.transform );
        }
        foreach ( var transform in moveBack )
        {   transform.SetAsLastSibling();
        }
    }
}
class MyMenu_Sibling2 : HierarchyExtensions.IGenericMenu {
    public bool IsEnable( GameObject clickedObject ) { return true; }
    public bool NeedExcludeFromMenu( GameObject clickedObject ) { return false; }
    public int PositionInMenu { get { return 202; } }
    public string Name { get { return "Sibling/Set As First Sibling %["; } }
    public void OnClick( GameObject[] affectedObjectsArray )
    {   var obs = Selection.gameObjects.Select(g => g.transform).ToArray();
        if ( obs.Length == 0 ) return;
        obs = obs.OrderByDescending( o => o.GetSiblingIndex() ).ToArray();
        foreach ( var item in obs )
        {   Undo.SetTransformParent( item, item.parent, "Set As First Sibling" );
            item.SetAsFirstSibling();
        }
    }
}
class MyMenu_Sibling3 : HierarchyExtensions.IGenericMenu {
    public bool IsEnable( GameObject clickedObject ) { return true; }
    public bool NeedExcludeFromMenu( GameObject clickedObject ) { return false; }
    public int PositionInMenu { get { return 203; } }
    public string Name { get { return "Sibling/Set As Last Sibling %]"; } }
    public void OnClick( GameObject[] affectedObjectsArray )
    {   var obs = Selection.gameObjects.Select(g => g.transform).ToArray();
        if ( obs.Length == 0 ) return;
        obs = obs.OrderBy( o => o.GetSiblingIndex() ).ToArray();
        foreach ( var item in obs )
        {   Undo.SetTransformParent( item, item.parent, "Set As Last Sibling" );
            item.SetAsLastSibling();
        }
    }
}



class MyMenu_ParentClear : HierarchyExtensions.IGenericMenu {
    public bool IsEnable( GameObject clickedObject ) { return true; }
    public bool NeedExcludeFromMenu( GameObject clickedObject ) { return false; }
    public int PositionInMenu { get { return 204; } }
    public string Name { get { return "Move To Parent &%["; } }
    public void OnClick( GameObject[] affectedObjectsArray )
    {   var obs = Selection.gameObjects.Select(g => g.transform).ToArray();
        if ( obs.Length == 0 ) return;
        obs = obs.OrderBy( o => o.GetSiblingIndex() ).ToArray();
        foreach ( var item in obs )
        {   if ( !item.parent )
            {   Undo.SetTransformParent( item, item.parent, "Move To Parent" );
                item.SetAsLastSibling();
            }
            else
            {   Undo.SetTransformParent( item, item.parent.parent, "Move To Parent" );
                item.SetAsLastSibling();
            }
        }
    }
}
#endregion





#region ITEM 500 - DuplicateNextToObject

class MyMenu_DuplicateNextToObject : HierarchyExtensions.IGenericMenu {
    public bool IsEnable( GameObject clickedObject ) { return true; }
    public bool NeedExcludeFromMenu( GameObject clickedObject ) { return false; }
    
    public int PositionInMenu { get { return 500; } }
    public string Name { get { return "Duplicate Next To Object %#d"; } }
    
    
    public void OnClick( GameObject[] affectedObjectsArray )
    {
    
        var onlytop = MyMenu_Utils.GetOnlyTopObjects(affectedObjectsArray).OrderByDescending(o => o.transform.GetSiblingIndex());
        
        List<GameObject> clonedObjects = new List<GameObject>();
        foreach ( var gameObject in onlytop )
        {   var oldSib = gameObject.transform.GetSiblingIndex();
            Selection.objects = new[] { gameObject };
            HierarchyExtensions.Utilities.DuplicateSelection();
            var clonedObject = Selection.activeGameObject;
            MyMenu_Utils.AssignUniqueName( clonedObject );
            clonedObject.transform.SetSiblingIndex( oldSib + 1 );
            clonedObjects.Add( clonedObject );
        }
        
        Selection.objects = clonedObjects.ToArray();
        
    }
}

#endregion




#region ITEM 750 - Rename
class MyMenu_Rename : HierarchyExtensions.IGenericMenu {
    public bool IsEnable( GameObject clickedObject ) { return true; }
    public bool NeedExcludeFromMenu( GameObject clickedObject ) { return false; }
    public int PositionInMenu { get { return 750; } }
    public string Name { get { return "Multy Renamer"; } }
    public void OnClick( GameObject[] affectedObjectsArray )
    {
    
        CustomModule.SHOW_StringInput( "Find:", EditorPrefs.GetString( "EModules/MultyRenamer/Find", "" ), ( find ) =>
        {   if ( string.IsNullOrEmpty( find ) ) return;
            EditorPrefs.SetString( "EModules/MultyRenamer/Find", find );
            CustomModule.SHOW_StringInput( "Replace:", EditorPrefs.GetString( "EModules/MultyRenamer/Replace", "" ), ( replace ) =>
            {   EditorPrefs.SetString( "EModules/MultyRenamer/Replace", replace );
                foreach ( var item in affectedObjectsArray )
                {   if ( !item ) continue;
                    Undo.RecordObject( item, "Multy Renamer" );
                    item.name = item.name.Replace( find, replace );
                    EditorUtility.SetDirty( item );
                }
            } );
        } );
    }
}
#endregion






#region ITEM 1000-1001 - ExpandSelecdedObject/CollapseSelecdedObject

class MyMenu_ExpandSelecdedObject : HierarchyExtensions.IGenericMenu {
    public bool IsEnable( GameObject clickedObject ) { return clickedObject.transform.childCount != 0; }
    public bool NeedExcludeFromMenu( GameObject clickedObject ) { return false; }
    
    public int PositionInMenu { get { return 1000; } }
    public string Name { get { return "Expand Selection"; } }
    
    
    public void OnClick( GameObject[] affectedObjectsArray )
    {   foreach ( var result in affectedObjectsArray.Select( o => o.GetInstanceID() ) )
            HierarchyExtensions.Utilities.SetExpandedRecursive( result, true );
    }
    
}


class MyMenu_CollapseSelecdedObject : HierarchyExtensions.IGenericMenu {
    public bool IsEnable( GameObject clickedObject ) { return clickedObject.transform.childCount != 0; }
    public bool NeedExcludeFromMenu( GameObject clickedObject ) { return false; }
    
    public int PositionInMenu { get { return 1001; } }
    public string Name { get { return "Collapse Selection"; } }
    
    
    public void OnClick( GameObject[] affectedObjectsArray )
    {   foreach ( var result in MyMenu_Utils.GetOnlyTopObjects( affectedObjectsArray ).Select( o => o.GetInstanceID() ) )
            HierarchyExtensions.Utilities.SetExpandedRecursive( result, false );
    }
    
}

#endregion



#region ITEM 2000-2001 - ReverseChildrenOrder/SelectOnlyTopObjects/SelectAllChildren

class MyMenu_ReverseChildrenOrder : HierarchyExtensions.IGenericMenu {
    public bool IsEnable( GameObject clickedObject ) { return clickedObject.transform.childCount > 0; }
    public bool NeedExcludeFromMenu( GameObject clickedObject ) { return false; }
    
    public int PositionInMenu { get { return 2000; } }
    public string Name { get { return "Reverse Children Order"; } }
    
    
    public void OnClick( GameObject[] affectedObjectsArray )
    {   foreach ( var gameObject in MyMenu_Utils.GetOnlyTopObjects( affectedObjectsArray ) )
        {   var T = gameObject.transform;
            for ( int i = 0 ; i < gameObject.transform.childCount ; i++ )
            {   Undo.SetTransformParent( T.GetChild( i ), T.GetChild( i ).transform.parent, "Reverse Children Order" );
                T.GetChild( i ).SetAsFirstSibling();
            }
        }
    }
    
}

class MyMenu_SelectOnlyTopObjects : HierarchyExtensions.IGenericMenu {
    public bool IsEnable( GameObject clickedObject ) { return Selection.gameObjects.Length >= 2; }
    public bool NeedExcludeFromMenu( GameObject clickedObject ) { return false; }
    
    public int PositionInMenu { get { return 2001; } }
    public string Name { get { return "Select Only Top Objects"; } }
    
    
    public void OnClick( GameObject[] affectedObjectsArray )
    {   Selection.objects = MyMenu_Utils.GetOnlyTopObjects( affectedObjectsArray );
    }
    
}


class MyMenu_SelectAllChildren : HierarchyExtensions.IGenericMenu {
    public bool IsEnable( GameObject clickedObject ) { return clickedObject.transform.childCount != 0; }
    public bool NeedExcludeFromMenu( GameObject clickedObject ) { return false; }
    
    public int PositionInMenu { get { return 2002; } }
    public string Name { get { return "Select All Children"; } }
    
    
    public void OnClick( GameObject[] affectedObjectsArray )
    {   Selection.objects = affectedObjectsArray.SelectMany( s => s.GetComponentsInChildren<Transform>( true ) ).Select( s => s.gameObject ).ToArray();
    }
    
}

#endregion






#region - Utils

static class MyMenu_Utils {
    public static void AssignUniqueName( GameObject o )
    {
    
        var usedNames = new SortedDictionary<string, string>();
        var childList = o.transform.parent
                        ? new Transform[o.transform.parent.childCount].Select((t, i) => o.transform.parent.GetChild(i))
                        : o.scene.GetRootGameObjects().Select(go => go.transform);
                        
        foreach ( var child in childList.Where( child => child != o.transform ) )
        {   if ( !usedNames.ContainsKey( child.name ) ) usedNames.Add( child.name, child.name );
        }// existing names
        
        if ( !usedNames.ContainsKey( o.name ) ) return;
        
        
        
        var number = 1;
        var name = o.name;
        
        var leftBracket = name.IndexOf('(');
        var rightBracket = name.IndexOf(')');
        
        if ( leftBracket != -1 && rightBracket != -1 && rightBracket - leftBracket > 1 )
        {   int parseResult;
            if ( int.TryParse( name.Substring( leftBracket + 1, rightBracket - leftBracket - 1 ), out parseResult ) )
            {   number = parseResult + 1;
                name = name.Remove( leftBracket );
            }
        }// previous value
        
        
        
        name = name.TrimEnd();
        while ( usedNames.ContainsKey( name + " (" + number + ")" ) ) ++number;
        o.name = name + " (" + number + ")"; //result
        
    }
    
    public static GameObject[] GetOnlyTopObjects( GameObject[] affectedObjectsArray )
    {   var converted = affectedObjectsArray.Select(a => new { a, par = a.GetComponentsInParent<Transform>( true ).Where(p => p != a.transform) } );
        return
            converted.Where( c => c.par.Count( p => affectedObjectsArray.Contains( p.gameObject ) ) == 0 ).
            Select( g => g.a ).ToArray();
    }
}

#endregion



}//namespace

#endif