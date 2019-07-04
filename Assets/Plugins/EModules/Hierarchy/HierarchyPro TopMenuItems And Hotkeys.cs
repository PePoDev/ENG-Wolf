#if UNITY_EDITOR
using UnityEditor;

namespace EModules {
class HierarchyPro_TopMenuItems_And_Hotkeys {
    internal const int P = 2000;
    
    [MenuItem( "Window/Hierarchy Plugin/Toggle HyperGraph State %#x", false, P + 45 )]
    public static void hipergrapgh() {   HierarchyExtensions.Utilities.TopMenuItems.hipergrapgh();}
    
    [MenuItem( "Window/Hierarchy Plugin/Toggle HyperGraph State %#x", true, P + 45 )]
    public static bool hipergrapghvalidate() {   return  HierarchyExtensions.Utilities.TopMenuItems.hipergrapghvalidate();}
    
    #if UNITY_2018_3_OR_NEWER
    [MenuItem( "Window/Hierarchy Plugin/Selection Backward %#z", false, P + 26 )]
    #else
    [MenuItem( "Window/Hierarchy Plugin/Selection Backward %#z", false, P + 6 )]
    #endif
    public static void MoveSelPrev_2(  ) {HierarchyExtensions.Utilities.TopMenuItems.MoveSelPrev_2( );}
    
    #if UNITY_2018_3_OR_NEWER
    [MenuItem( "Window/Hierarchy Plugin/Selection Forward %#y", false, P + 27 )]
    #else
    [MenuItem( "Window/Hierarchy Plugin/Selection Forward %#y", false, P + 7 )]
    #endif
    public static void MoveSelNext_2(  ) {HierarchyExtensions.Utilities.TopMenuItems.MoveSelNext_2( );}
    
    [MenuItem( "Window/Hierarchy Plugin/Toggle Lock State &#l", false, P + 85 )]
    public static void ToggleFreeze( ) {   HierarchyExtensions.Utilities.TopMenuItems.ToggleFreeze();}
    
    [MenuItem( "Window/Hierarchy Plugin/Unlock All &%#l", false, P + 89 )]
    public static void UnclockALl( ) {   HierarchyExtensions.Utilities.TopMenuItems.UnlockAll();}
}
}
#endif
