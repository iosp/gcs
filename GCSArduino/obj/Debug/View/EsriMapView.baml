   M S B A M L   `   `   `  ����E  AGCSArduino, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:GCSArduino.BehaviorGCSArduino.Behavior  5      Z VESRI.ArcGIS.Client, Version=10.2.0.0, Culture=neutral, PublicKeyToken=29c6dd6e8553d944S<clr-namespace:ESRI.ArcGIS.Client;assembly=ESRI.ArcGIS.ClientESRI.ArcGIS.Client 5	      9!clr-namespace:GCSArduino.ControlsGCSArduino.Controls  5
      !    GCSArduino.View.EsriMapView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      0behavior!clr-namespace:GCSArduino.Behavior  5      Iclient<clr-namespace:ESRI.ArcGIS.Client;assembly=ESRI.ArcGIS.Client  5	      )b!clr-namespace:GCSArduino.Controls  5
        � DataContext  5      ��    Locator �� Source# [   �� Path$ MapViewModel�.��5      +   %GCSArduino.Controls.BorderDropAndDrop  .  (System.Windows.Interactivity.Interaction   	Behaviors 5   
   6   0GCSArduino.Behavior.FrameworkElementDragBehavior  5      6   0GCSArduino.Behavior.FrameworkElementDropBehavior  5      ._�5   
   � .+�5   
     ESRI.ArcGIS.Client.Map     UseAcceleratedDisplay .     
WrapAround .    IsLogoVisible .   5      /   )GCSArduino.Behavior.MapTileSourceBehavior  5      	   ID$	 Map��61      Opacity$ 1Q�6T   	   Path	 6:   �� *
FilePath+6T   0   *GCSArduino.Behavior.MapItemsSourceBehavior  5      	
   ID$
 Entities��5         XPropertyPath$ 	XLocation��6#      YPropertyPath$ 	YLocation��6=      HeadingPath$ Heading��6X      SelectedPropertyPath$ 
IsSelected��6n     TestpictureMarker   Symbol# [ 5         ItemsSource 5      �� *EntitysItemsSource+7   1GCSArduino.Behavior.MapGraphicItemsSourceBehavior  5       	   ID$ Shapes��5!        
LineSymbol   Symbol# [ 6      SymbolPropertyPath$ Symbol��6D      GeometryPropertyPath$ Geometry��6`      EditGeometryAction 5    9   �� *EditGeometryAction+ �� Mode$ TwoWay=�   ShapeSelected 5!   �   �� *SelectedShape+$ TwoWay=�   ItemsSource 5"      �� *ShapesItemsSource+$ TwoWay=�)	   #GCSArduino.Behavior.MapDrawBehavior	  5$        DrawLineSymbol 	  
LineSymbol# [ 6+    	  NewTaskShapeId 6X   �� *NewTaskShapeId+$ TwoWay=� 	  ItemsSource 5%   ,   �� *ShapesItemsSource+$ TwoWay=� 	  DrawMode 5&   ,   �� *
DrawMode+$ TwoWay=�6Y   7
   1GCSArduino.Behavior.MapSelectedItemSourceBehavior
  5'       
  XPropertyPath$ 	XLocation��6:    
  YPropertyPath$ 	YLocation��6T     QuadPictureMarkerSymbol 
  Symbol# [ 5(   :     
  SelectedItem  5)   :   �� *SelectedEntity+$ TwoWay=�6q   5,      5-      5.      5/      