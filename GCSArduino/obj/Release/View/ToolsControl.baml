   M S B A M L   `   `   `  ���� c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  h dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35�bclr-namespace:Microsoft.Expression.Interactivity.Layout;assembly=Microsoft.Expression.Interactions)Microsoft.Expression.Interactivity.Layout  
GCSArduino"   GCSArduino.View.ToolsControl   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     qilbclr-namespace:Microsoft.Expression.Interactivity.Layout;assembly=Microsoft.Expression.Interactions       � DataContext  ��    Locator �� Source# [   �� Path$ ToolsControlViewModel� � 	Resources   closeToolBox g� 	BeginTime �� 
TargetName �� TargetProperty R� KeyTime  System.Windows.Visibility   	Collapsed	 R� Value&   System.Windows.VerticalAlignment

   Top(  "System.Windows.HorizontalAlignment   Right  openToolBox   Visible   Stretch   Stretch��%�  &	       &	 P    �� .��T� $ 00:00:00X�$ border��$ (UIElement.Visibility)�..�i� $ 00:00:00.1000000��#	 Z T� $ 00:00:00X�$ tools��$* $(FrameworkElement.VerticalAlignment)�..�i� $ 00:00:00.1000000��#	 Z
 T� $ 00:00:00X�$ tools��$, &(FrameworkElement.HorizontalAlignment)�..�i� $ 00:00:00.1000000��#	 Z �� .��T� $ 00:00:00X�$ border��$ (UIElement.Visibility)�..�i� $ 00:00:00.1000000��#	 Z T� $ 00:00:00X�$ tools��$, &(FrameworkElement.HorizontalAlignment)�..�i� $ 00:00:00.1000000��#	 Z T� $ 00:00:00X�$ tools��$* $(FrameworkElement.VerticalAlignment)�..�i� $ 00:00:00.1000000��#	 Z  � Triggers :�  :� RoutedEvent$ ToggleButton.Unchecked�� :� 
SourceName$ minus��.[��� -    ��Name$" closeToolBox_BeginStoryboard��#��[ :� $ ToggleButton.Checked��$ minus��.[��� -   $! openToolBox_BeginStoryboard��#��[ .��� -    �Name$ 
LayoutRoot��.+��� -    ��Name$ tools��$��0,0,0,-0.001q����	#FF516D7A$��0q� �� CornerRadius$ 2��.   (System.Windows.Interactivity.Interaction   	Behaviors H  BMicrosoft.Expression.Interactivity.Layout.MouseDragElementBehavior  ._��� $��0q� �� Orientation$ Vertical=�.+�� $��Right=�$	��0,2q�.+��� $	��5,0q�$��Verdana)�$��12(�
�������$��כלים��$
��Wrap=�d� $��0,2,0,0q�  KomunaPinToggleButton#���   � ToolTip$ נעץ במקום��$��10��$��10��$��ToggleButton��d� -    d�Name$ minus��$��0,2,4,0q�  komunaMinimizeToggleButton#���  $ מזער��$��10��$��10��$��ToggleButton���� -   $ border��$��3,0,3,3q����	#FF374A53$��0q�$ 2��$y�	Collapsed=�._��� $��4,4,0,4q� J� RenderTransformOrigin$ 0.5,0.5&�.+�� -    �Name$ map_controlers��$��Right=�$��0,2,3,2q�.+��� -    ��Name$	 LOS��$��Right=�$��0q�  LOS#���  $��Center=�$��24��$��24��$��CheckBox��$	 LOS������ *
CommandLos+ d� 	IsChecked �� *LosIsChecked+�� -	   $ ormap��$��Right=�$��0q�  OrianMap#���  $��Center=�$��24��$��24��$��CheckBox��$! מפת אוריינטציה�� �� *OrmapIsChecked+�� -
   $ raster��$��Right=�$��0,0,25,0q�  RasterOnMap#���  $��Center=�$��25��$��25��$��CheckBox��$ שכבת תמונות������ *CommandRasterOnMap+ �� *RasterIsChecked+� -   $ brt_Copy��$	��Top=�$��50��$��23��$��0,0,11,0q�.+��� -    ��Name$ MapBrightnessSlide2��  MapBrightnessSlider#���  $
��Left=�$��50��$
��Auto�� � LargeChange$ 5Q�$ בהירות מפה��  � Maximum$  32Q�! � Minimum$	! -32Q�" � SmallChange$" 1Q�# �� TickPlacement$
# Both=�$ 0.5,0.5&�$��Center=�$ � Value$ �� *BrightnessValue+% �� UpdateSourceTrigger$% PropertyChanged=�� $��Center=�$	��161��.+�F� & �� Stretch$
& Fill=����	#FF2A383F$��0q�$	��Top=�$��1���������p� �6�9K�@�p� 	F� $
& Fill=����	#FF516D7A$��0,1,0,0q�$	��Top=�$��1���������p� �6�9K�@�p� 	� $��Right=�$��0,2,3,2q�$	��Top=�' � Orientation$' 
Horizontal=�.+�	� -   ( 	�Name$( 	zoomToAll��$��Right=�$��0q� 	 ZoomToAllRadioButton#��� 	 $��Center=�$��25��$��25��$��RadioButton��$ הצג הכל������ *CommandZoomOut+ �� *ZoomToAllIsChecked+	� -   $( ZoomOut��$��Right=�$��0q� 
 ZoomOutRadioButton#��� 
 $��Center=�$��25��$��25��$��RadioButton��$ הקטן תצוגה������ *CommandZoomOut+ �� *ZoomOutIsChecked+	� -   $( ZoomIn��$��Right=�$��0,0,18,0q�  ZoomInRadioButton#���  $��Center=�$��25��$��25��$��RadioButton��$ הגדל תצוגה������ *CommandZoomIn+ �� *ZoomInIsChecked+� -   $ zoomSLD_Copy��$��51.334��$��0,0,18,0q�$��23��.+��� -   $ MapZoomSlide2��  
SpringZoom#���  $
��Auto��) �� TickFrequency$) 1Q�$! 0Q�$	  100Q�$ הגדלה/הקטנה��$��50��$��Center=�$��Right=�$��0q���Z� $ �� *
ZoomSlider+$% PropertyChanged=�� $��Center=�$	��161��.+�F� $
& Fill=����	#FF2A383F$��0q�$	��Top=�$��1���������p� �6�9K�@�p� 	F� $
& Fill=����	#FF516D7A$��0,1,0,0q�$	��Top=�$��1���������p� �6�9K�@�p� 	� $��Right=�$��0,2,3,2q�.+�	� -   $
( Hand��$��Right=�$��0q�  PanRadioButton#���  $��Center=�$��25��$��25��$��RadioButton��$ הזז מפה������ *CommandHand+ �� *HandIsChecked+	� -   $( Ruler��$��Right=�$��0q�  RulerRadioButton#���  $��Center=�$��25��$��25��$��RadioButton��$ מדוד מרחק������ *CommandRuler+ �� *RulerIsChecked+� -   $ 
unit_Copy2��$��0,0,8,0q�$��Center=�$
��Left=�.+��� -   * ��Name$* CoordinateSys2��$+ �� IsSynchronizedWithCurrentItem$
+ TrueY����	#FFFFFFFF$
��Left=�  
UMTcombo01#���  , �� IsDropDownOpen, .  $ קנ"מ��$��0q�$��12(�$��Verdana)�.@��� -   - ��Name$
- UTM2�����	#FFFFFFFF$��1:50K��  UMTcomboBoxItem01#���  $��Verdana)���Z� �� -   $
- GEO2�����	#FFFFFFFF$��1:10K��#���  $��Verdana)�. �� 
IsSelected. . ��Z� � -   $ 
unit_Copy3��$��0,0,5,0q�$��Center=�$
��Left=�.+��� -   $* CoordinateSys3��$
+ TrueY����	#FFFFFFFF$
��Left=�#���  , .  $ סוג נ.צ��$��0q�$��12(�$��Verdana)�.@��� -   $
- UTM3�����	#FFFFFFFF$	��UTM��#���  $��Verdana)���Z� �� -   $
- GEO3�����	#FFFFFFFF$	��GEO��#���  $��Verdana)�. . ��Z� � $��Center=�$	��161��.+�F� $
& Fill=����	#FF2A383F$��0q�$	��Top=�$��1���������p� �6�9K�@�p� 	F� $
& Fill=����	#FF516D7A$��0,1,0,0q�$	��Top=�$��1���������p� �6�9K�@�p� 	� $��Right=�$��0,2,3,0q�.+�	� -   $
( Rect��$��Right=�$��0,0,1,0q�  RectRadioButton#���  $��Center=�$��25��$��25��$��RadioButton��$
��Rect������ *CommandPaint+ �� *RectIsChecked+	� -   $( Circle��$��Right=�$��0,0,1,0q�  CircleRadioButton#���  $��Center=�$��25��$��25��$��RadioButton��$��Circle������ *CommandPaint+ �� *CircleIsChecked+	� -   $
( Area��$��Right=�$��0,0,1,0q�  HotAreaRadioButton#���  $��Center=�$��25��$��25��$��RadioButton��$ פוליגון סגור��$
��Area������ *CommandPaint+ �� *AreaIsChecked+	� -   $( Polyline��$��Right=�$��0,0,1,0q�  PolilineRadioButton#���  $��Center=�$��25��$��25��$��RadioButton��$ פוליגון פתוח��$��Polyline������ *CommandPaint+ �� *PolylineIsChecked+	� -    $( Point��$��Right=�$��0,0,6,0q�  RadioButtonPoint#���  $��Center=�$��25��$��25��$��RadioButton��$ נקודת עניין��$��Point������ *CommandPaint+ �� *PointIsChecked+