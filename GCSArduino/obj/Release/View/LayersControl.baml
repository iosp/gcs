   M S B A M L   `   `   `  ���� c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  h dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35�bclr-namespace:Microsoft.Expression.Interactivity.Layout;assembly=Microsoft.Expression.Interactions)Microsoft.Expression.Interactivity.Layout E AGCSArduino, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GCSArduino.ModelGCSArduino.Model E AInterfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:Interfaces;assembly=Interfaces
Interfaces ;"clr-namespace:GCSArduino.ConverterGCSArduino.Converter B >Mission, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null@-clr-namespace:MissionPackage;assembly=MissionMissionPackage #   GCSArduino.View.LayersControl   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	    @d2http://schemas.microsoft.com/expression/blend/2008 	    ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity 	    qilbclr-namespace:Microsoft.Expression.Interactivity.Layout;assembly=Microsoft.Expression.Interactions 	    0modelclr-namespace:GCSArduino.Model 	    C
interfaces,clr-namespace:Interfaces;assembly=Interfaces 	    8	converter"clr-namespace:GCSArduino.Converter 	    HmissionPackage-clr-namespace:MissionPackage;assembly=Mission 	      � DataContext  ��    Locator �� Source# [   �� Path$ LayerControlViewModel� � 	Resources 9  3GCSArduino.Converter.IsCheckedToVisibilityConverter " IsCheckedToVisibilityConverter  CloseLayers g� 	BeginTime �� 
TargetName �� TargetProperty R� KeyTime  System.Windows.Visibility   	Collapsed	 R� Value&   System.Windows.VerticalAlignment

   Top(  "System.Windows.HorizontalAlignment   Right  OpenLeyers_01   Stretch   Stretch   Visible  
ChildTasks �� DataType  GCSArduino.Model.Vehicle �� ItemTemplate �� ItemsSource��%\  &	       &	      &	 X    &	 �    (�� �    *, +)7    �� .��T� $ 00:00:00X�$ border1��$ (UIElement.Visibility)�..�i� $ 00:00:00.1000000��#	 Z T� $ 00:00:00X�$ layers��$* $(FrameworkElement.VerticalAlignment)�..�i� $ 00:00:00.1000000��#	 Z
 T� $ 00:00:00X�$ layers��$, &(FrameworkElement.HorizontalAlignment)�..�i� $ 00:00:00.1000000��#	 Z �� .��T� $ 00:00:00X�$ layers��$, &(FrameworkElement.HorizontalAlignment)�..�i� $ 00:00:00.1000000��#	 Z T� $ 00:00:00X�$ layers��$* $(FrameworkElement.VerticalAlignment)�..�i� $ 00:00:00.1000000��#	 Z T� $ 00:00:00X�$ border1��$ (UIElement.Visibility)�..�i� $ 00:00:00.1000000��#	 Z �� .R��� 
����������� *
TaskName+�� "  8    �� *Tasks+.R��� 
����������� *Name+ � Triggers :�  :� RoutedEvent$ ToggleButton.Unchecked�� :� 
SourceName$ 
minus_Copy��.[��� -    ��Name$! CloseLayers_BeginStoryboard��#��[ :� $ ToggleButton.Checked��$ 
minus_Copy��.[��� -   $# OpenLeyers_01_BeginStoryboard��#��[ .��� -    �Name$ 
LayoutRoot��$��RightToLeft=�.+��� -    ��Name$ layers��$��0q� �� CornerRadius$ 2������  
 blue01 �� Color# �  .   (System.Windows.Interactivity.Interaction   	Behaviors H  BMicrosoft.Expression.Interactivity.Layout.MouseDragElementBehavior  ._��� $��0q� �� Orientation$ Vertical=�.+�� $	��0,2q�$��20��.+�d� -    d�Name$ 
minus_Copy��$��0,2,4,0q�  komunaMinimizeToggleButton#���   � ToolTip$ מזער��$��10��$��10��$��ToggleButton���� $��Verdana)�$��12(�
�������$��
שכבות��$
��Wrap=�$��5,0,0,0q��� -   $ border1��$��3,1,3,3q�$��0q�$ 2��$y�	Collapsed=�����   blueBackground# �  ._��� $��4,4,0,4q�.+��� $��1��
������ ���� *Tasks+ �� SelectedItem �� *SelectedTask+y���  �� ElementName$ MissionList��$ 	IsChecked�  �� 	Converter#  [ ���� ! �� 
TargetType"! ��.��� " �� Property" � ��# �� Value
# �   �$ �� Triggers$ P� % P� Property% �@J�IsMouseOver& P� Value& . . ��� " � ��
# �   �P� % �@��
IsSelected& . . ��� " � ��
# �  ������ .R��� 
�������$��5,0,0,0q�$��12(����� *
TaskName+R� -   ' R�Name$' TreeViewQuad��$��0q�  komunaTreeViewItemHebrew#���  $	��Top=�y��� $ VehicleList��$ 	IsChecked�#  [ ���� *
Vehicles+��Z� ��Z� � $	��144��$��2,0,0,0q�.+�F� ( �� Stretch$
( Fill=����	#FF2A383F$��0q�$	��Top=�$��1���������p� �6�9K�@�p� 	F� $
( Fill=����	#FF516D7A$��0,1,0,0q�$	��Top=�$��1���������p� �6�9K�@�p� 	�� $ 
Horizontal=�$��7,0,0,0q�$	��Top=�.+�	� -   ) 	�Name$) VehicleList��$��20,0,0,0q� 	 komunaTextRadioBTN#��� 	 $��Center=�$��כלים��* d� 	IsChecked* �� *MissionOrVehicles+	� -	   $) MissionList��$��0q�#��� 	 $��Center=�$��משימות��� $��50,0,0,0q�$	��Top=�$ שמור משימה��.+��� -
   + ��Name$
+ Save��$��1,0,0,0q� 
 save#��� 
 $��16��$��16��$��Button��$ שמור משימה������ *SaveMissionCommand+�� -   $
+ Load��$��1,0,0,0q�  OpenMissions#���  $��16��$��16��$��Button��$ פתח משימה������ *OpenMissionCommand+