   M S B A M L   `   `   `  ���� c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  h dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35�bclr-namespace:Microsoft.Expression.Interactivity.Layout;assembly=Microsoft.Expression.Interactions)Microsoft.Expression.Interactivity.Layout E AGCSArduino, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:GCSArduino.ResourcesGCSArduino.Resources 3clr-namespace:GCSArduino.EnumsGCSArduino.Enums A =Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;*clr-namespace:Common.Enums;assembly=CommonCommon.Enums %   GCSArduino.View.QuadsStatusView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     qilbclr-namespace:Microsoft.Expression.Interactivity.Layout;assembly=Microsoft.Expression.Interactions     8	resources"clr-namespace:GCSArduino.Resources     0enumsclr-namespace:GCSArduino.Enums     =enums2*clr-namespace:Common.Enums;assembly=Common       � DataContext  ��    Locator �� Source# [   �� Path$ QuadsStatusViewModel� � 	Resources   closeToolBox g� 	BeginTime �� 
TargetName �� TargetProperty R� KeyTime  System.Windows.Visibility   	Collapsed	 R� Value&   System.Windows.VerticalAlignment

   Top(  "System.Windows.HorizontalAlignment   Right  openToolBox   Visible   Stretch   Stretch��%�  &	       &	 R    �� .��T� $ 00:00:00X�$ border��$ (UIElement.Visibility)�..�i� $ 00:00:00.1000000��#	 Z T� $ 00:00:00X�$ Status��$* $(FrameworkElement.VerticalAlignment)�..�i� $ 00:00:00.1000000��#	 Z
 T� $ 00:00:00X�$ Status��$, &(FrameworkElement.HorizontalAlignment)�..�i� $ 00:00:00.1000000��#	 Z �� .��T� $ 00:00:00X�$ border��$ (UIElement.Visibility)�..�i� $ 00:00:00.1000000��#	 Z T� $ 00:00:00X�$ Status��$, &(FrameworkElement.HorizontalAlignment)�..�i� $ 00:00:00.1000000��#	 Z T� $ 00:00:00X�$ Status��$* $(FrameworkElement.VerticalAlignment)�..�i� $ 00:00:00.1000000��#	 Z  � Triggers :�  :� RoutedEvent$ ToggleButton.Unchecked�� :� 
SourceName$ minus_status��.[��� -    ��Name$" closeToolBox_BeginStoryboard��#��[ :� $ ToggleButton.Checked��$ minus_status��.[��� -   $! openToolBox_BeginStoryboard��#��[ .��� -    �Name$ 
LayoutRoot��.+��� -    ��Name$ Status��$��0,0,0,-0.001q����	#FF516D7A$��0q� �� CornerRadius$ 2��.   (System.Windows.Interactivity.Interaction   	Behaviors H  BMicrosoft.Expression.Interactivity.Layout.MouseDragElementBehavior  ._��� $��0q� �� Orientation$ Vertical=�.+�� $��Right=�$	��0,2q�$��20��.+��� $	��5,0q�$��Verdana)�$��12(�
�������$��Status��$
��Wrap=�d� $��0,2,0,0q�  KomunaPinToggleButton#���   � ToolTip$ נעץ במקום��$��10��$��10��$��ToggleButton��d� -    d�Name$ minus_status��$��0,2,4,0q�  komunaMinimizeToggleButton#���  $ מזער��$��10��$��10��$��ToggleButton���� -   $ border��$��3,0,3,3q����	#FF374A53$��0q�$ 2��$y�	Collapsed=�._��  � RowDefinitions �� $��* ��� $��* �.+��� $��0��
������ ���� *
Vehicles+ �� SelectedItem �� *VehicleSelected+ �� Mode$ TwoWay=�  �� 
TargetType �� Property �� Value��%�   '��       '�� S     �� " ��.���  � ��
 �   ���  � ��
 �������  � ��$ 20(��� " ��.���  � ��$ 10����  � ��
 �   ����� " ��.���  � ��
 �����  �� Triggers  P� ! P� Property! �@J�IsMouseOver" P� Value" . . ���  � ��
 �   �P� ! �@��
IsSelected" . . ���  � ��
 �   �# �� View# �� .P��� $��	SelectAll��$ �� Width$	$ 100��% �� HeaderTemplate% �� .R��� $ 
Horizontal=�.+��� $��Center=�$��Center=�$��0,0,5,0q�& d� 	IsChecked& �� # [  $$ QuadsStatusViewModel.SelectAll�'   Triggers' /   )System.Windows.Interactivity.EventTrigger  (   	EventName$( Checked��.   (System.Windows.Interactivity.TriggerBase)   Actions.) 6   0System.Windows.Interactivity.InvokeCommandAction  *   Command* �� �� $��Selected��$��Center=�$��Center=�$��0,0,5,0q�+ �� CellTemplate+ �� .R��� & �� *
IsSelected+$ TwoWay=�, �� UpdateSourceTrigger$, PropertyChanged=��� $
��Name��$	$ 120��- �� DisplayMemberBinding- �� *Name+�� $��Altitude��$$ 80��- �� *
Altitude+�� $��Battery��$$ 80��- �� *QuadStatus.BatteryVoltage+�� $	$ 120��$
��Task��+ �� .R��� . ��Name$. ComboBox��$
��Auto��$	��100��$	��100��/ �� DisplayMemberPath$/ TaskID������ # [  $  QuadsStatusViewModel.Tasks� �� *Task+'   $( SelectionChanged��.)   * �� # [  $, &QuadsStatusViewModel.SelectTaskCommand�0   CommandParameter0 �� �� $��WP��$$ 80��- �� *NextWaypoint+�� $��Dis WP��$$ 80��- �� *DistanceFromTarget+�� $��Time WP��$$ 90��- �� *TimeInWaypoint+�� $
��Mode��$$ 90��- �� *QuadStatus.Mode+�� $��MStatus��$$ 90��- �� *IsActivateMission+�� $��1��$ Vertical=�.+��� $ 
Horizontal=�.+��� $��0��$��0��$��Take Off��  ButtonWhiteStyle#��[ ���� *TakeOffCommand+�� $��80��$��50��
�������
������ $��20(�1 �� TextAlignment$1 Center=�$��Center=����� *
AltTakeOff+�� $
��Left=�$	��Top=�$��Mode Commands��$��80��$	��520��
�������$��16(�.���� $ 
Horizontal=�.+��� $��0��$��0��$	��Arm��#��[ ���� *ArmModeCommand+�� $��0��$��0��$
��Auto��"	  Common.Enums.VehicleModeEnum2 	  Auto#��Z2 #��[ ���� *QuadModeCommand+�� $��0��$��0��$	��RTL��
3 	  RTL#��Z3 #��[ ���� *QuadModeCommand+�� $��0��$��0��$
��Land��4 	  Land#��Z4 #��[ ���� *QuadModeCommand+�� $��0��$��0��$��Dis Arm��#��[ ���� *DisArmModeCommand+�� $
��Left=�$	��Top=�$��Mission Commands��$��80��$	��620��
�������$��16(�.���� $ 
Horizontal=�.+��� $��Mission��$y�	Collapsed=�  TextBlockStyle#��[ $��5,0,20,0q�$��80���� $��2��$��0��$
��Send��#���  ���� *SendMissionCommands+�� $��2��$��0��$��Start��#���  "
  GCSArduino.Enums.SoftkeyEnum5 
  StartMission#��Z5 ���� *MissionCommands+�� $��2��$��1��$
��Stop��#���  6 
  StopMission#��Z6 ���� *MissionCommands+�� $��2��$��2��$��Restart��#���  7 
  RestartMission#��Z7 ���� *MissionCommands+�� $��2��$��2��$��Next WP��#���  ���� *SetNextWaypointCommand+�� $��JRestart��#���  ���� *JoystickRestartCommands+