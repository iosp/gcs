   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5      h dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35�bclr-namespace:Microsoft.Expression.Interactivity.Layout;assembly=Microsoft.Expression.Interactions)Microsoft.Expression.Interactivity.Layout 5      E AGCSArduino, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GCSArduino.ModelGCSArduino.Model 5      E AInterfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:Interfaces;assembly=Interfaces
Interfaces 5	      ;"clr-namespace:GCSArduino.ConverterGCSArduino.Converter 5
      B >Mission, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null@-clr-namespace:MissionPackage;assembly=MissionMissionPackage 5      #   GCSArduino.View.LayersControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    5      @d2http://schemas.microsoft.com/expression/blend/2008 	    5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity 	    5      qilbclr-namespace:Microsoft.Expression.Interactivity.Layout;assembly=Microsoft.Expression.Interactions 	    5      0modelclr-namespace:GCSArduino.Model 	    5      C
interfaces,clr-namespace:Interfaces;assembly=Interfaces 	    5	      8	converter"clr-namespace:GCSArduino.Converter 	    5
      HmissionPackage-clr-namespace:MissionPackage;assembly=Mission 	    5        � DataContext  5      ��    Locator �� Source# [   �� Path$ LayerControlViewModel� � 	Resources 5      9  3GCSArduino.Converter.IsCheckedToVisibilityConverter " IsCheckedToVisibilityConverter  CloseLayers g� 	BeginTime �� 
TargetName �� TargetProperty R� KeyTime  System.Windows.Visibility   	Collapsed	 R� Value&   System.Windows.VerticalAlignment

   Top(  "System.Windows.HorizontalAlignment   Right  OpenLeyers_01   Stretch   Stretch   Visible  
ChildTasks �� DataType  GCSArduino.Model.Vehicle �� ItemTemplate �� ItemsSource��%#  &	       &	      &	     &	     (�� h    5+   #   *, +)7    5   
   6\   �� 5   
   .��5      T� $ 00:00:00X�6,   $ border1��6A   $ (UIElement.Visibility)�6a   ..�5      i� $ 00:00:00.1000000��6)   #	 Z 6D   5      T� 5      $ 00:00:00X�6,   $ layers��6A   $* $(FrameworkElement.VerticalAlignment)�6`   ..�5      i� $ 00:00:00.1000000��6)   #	 Z
 6D   5      T� 5      $ 00:00:00X�6,   $ layers��6A   $, &(FrameworkElement.HorizontalAlignment)�6`   ..�5      i� $ 00:00:00.1000000��6)   #	 Z 6D   5      5      �� 5   
   .��5      T� $ 00:00:00X�6,   $ layers��6A   $, &(FrameworkElement.HorizontalAlignment)�6`   ..�5      i� $ 00:00:00.1000000��6)   #	 Z 6D   5      T� 5      $ 00:00:00X�6,   $ layers��6A   $* $(FrameworkElement.VerticalAlignment)�6`   ..�5       i� $ 00:00:00.1000000��6)   #	 Z 6D   5!      T� 5"      $ 00:00:00X�6,   $ border1��6A   $ (UIElement.Visibility)�6a   ..�5#      i� $ 00:00:00.1000000��6)   #	 Z 6D   5$      5%      �� 5'   
   .R�5(      �� 
���������6   �� *
TaskName+63   5)      �� 5+   
   "  6#   8    6E   �� *Tasks+.R�5,      �� 
���������6   �� *Name+6/   5-      50       � Triggers 52      :� 53   
    :� RoutedEvent$ ToggleButton.Unchecked��6    :� 
SourceName$ 
minus_Copy��6<   .[�54      �� -   5         ��Name$! CloseLayers_BeginStoryboard��54      #��[ 6C   55      :� 56   
   $ ToggleButton.Checked��6   $ 
minus_Copy��6:   .[�57      �� -   5        $# OpenLeyers_01_BeginStoryboard��57      #��[ 6E   58      .��5;      � -   5         �Name$ 
LayoutRoot��5;      $��RightToLeft=�6   .+�5<   
   �� -   5         ��Name$ layers��5<      $��0q�6!    �� CornerRadius$ 2��65   ��5=      �� 5>       
 blue01 �� Color# �  6"   .   (System.Windows.Interactivity.Interaction   	Behaviors 5@      H  BMicrosoft.Expression.Interactivity.Layout.MouseDragElementBehavior  5A      ._�5D      �� $��0q�6    �� Orientation$ Vertical=�6$   .+�5F      � $	��0,2q�6   $��20��6)   .+�5G      d� -   5         d�Name$ 
minus_Copy��5G   #   $��0,2,4,0q�67     komunaMinimizeToggleButton#���  6H    � ToolTip$ מזער��6}   $��10��6�   $��10��6�   $��ToggleButton��6�   �� 5H      $��Verdana)�6    $��12(�65   
�������$��
שכבות��6V   $
��Wrap=�6c   $��5,0,0,0q�6w   5I      �� 5K      -   5        $ border1��5K      $��3,1,3,3q�6*   $��0q�6;   $ 2��6O   $y�	Collapsed=�6`   ��5L      �� 5M        blueBackground# �  6*   ._�5O      �� $��4,4,0,4q�6!   .+�5R      �� $��1��6"   
������ ��6H   �� *Tasks+ �� SelectedItem 6f   �� *SelectedTask+y�6�   ��  �� ElementName$ MissionList��$ 	IsChecked�  �� 	Converter#  [ ��5S      �� 5T   "   ! �� 
TargetType"! ��6(   .�5U   &   �� " �� Property" � ��# �� Value
# �   �6C   $ �� Triggers$ 5V   &   P� 5W   *   % P� Property% �@J�IsMouseOver& P� Value& . . �5X   .   �� " � ��
# �   �6K   5Y   +   P� 5Z   *   % �@��
IsSelected& . . �5[   .   �� " � ��
# �  ��6K   5\   +   5^   #   ��5a      �� 5b   "   .R�5c   &   �� 
�������$��5,0,0,0q�6]   $��12(�6n   ��60   �� *
TaskName+6   5d   #   5f      R� 5l      -   5        ' R�Name$' TreeViewQuad��5l   #   $��0q�6�     komunaTreeViewItemHebrew#���  6�   $	��Top=�6�  y�69   �� $ VehicleList��$ 	IsChecked�#  [ ��6�   �� *
Vehicles+��6   Z� ��66  Z� 6�  � 5o      $	��144��6   $��2,0,0,0q�6+   .+�5p      F� ( �� Stretch$
( Fill=�6#   ���	#FF2A383F$��0q�6E   $	��Top=�6P   $��1��6h   �������p� �6�9K�@�p� 	6s   F� 5q      $
( Fill=�6#   ���	#FF516D7A$��0,1,0,0q�6E   $	��Top=�6V   $��1��6n   �������p� �6�9K�@�p� 	6y   5r      �� 5t      $ 
Horizontal=�6%   $��7,0,0,0q�6>   $	��Top=�6O   .+�5u      	� -   5        ) 	�Name$) VehicleList��5u   *   $��20,0,0,0q�6@    	 komunaTextRadioBTN#��� 	 6R   $��Center=�6   $��כלים��6�   * d� 	IsChecked* 6�   �� *MissionOrVehicles+	� 5v      -	   5        $) MissionList��5v   *   $��0q�6@   #��� 	 6K   $��Center=�6x   $��משימות��6�   � 5w      $��50,0,0,0q�6)   $	��Top=�6;   $ שמור משימה��6S   .+�5x   "   �� -
   5        + ��Name$
+ Save��5x   )   $��1,0,0,0q�68    
 save#��� 
 6I   $��16��6h   $��16��6s   $��Button��6   $ שמור משימה��6�   ��6�   �� *SaveMissionCommand+�� 5y   "   -   5        $
+ Load��5y   )   $��1,0,0,0q�68     OpenMissions#���  6I   $��16��6p   $��16��6{   $��Button��6�   $ פתח משימה��6�   ��6�   �� *OpenMissionCommand+5z      5{      5|      5}      5~      5      5�      5�      