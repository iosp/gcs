   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  E AGCSArduino, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GCSArduino.EnumsGCSArduino.Enums M IManagedMavlinkMsgs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?2clr-namespace:MMAVLink;assembly=ManagedMavlinkMsgsMMAVLink ;"clr-namespace:GCSArduino.ConverterGCSArduino.Converter Z VESRI.ArcGIS.Client, Version=10.2.0.0, Culture=neutral, PublicKeyToken=29c6dd6e8553d944S<clr-namespace:ESRI.ArcGIS.Client;assembly=ESRI.ArcGIS.ClientESRI.ArcGIS.Client )   #GCSArduino.View.ListWaypointControl   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     6sys&clr-namespace:System;assembly=mscorlib     0enumsclr-namespace:GCSArduino.Enums     GmmavLink2clr-namespace:MMAVLink;assembly=ManagedMavlinkMsgs     8	converter"clr-namespace:GCSArduino.Converter     Oclient<clr-namespace:ESRI.ArcGIS.Client;assembly=ESRI.ArcGIS.Client       � 	Resources  2  ,GCSArduino.Converter.SecondToMinuteConverter   SecondToMinuteConverter5 /GCSArduino.Converter.MissionMavCommandConverter  MissionMavCommandConverter S� 
MethodName S� 
ObjectType   System.Enum 
 MavCmd S� MethodParameters M� TypeName��%e   &	        &	      &	 
         S� $ 	GetValues��"   M� $ enums:MissionMavCmd��.���  � RowDefinitions �� $
��Auto ��� $	��200 �.+��� $��0�� �� Orientation$ 
Horizontal=�$
��Left=�$y�	Collapsed=�.+��� $	��Del��$��30��$��40������ *DeleteWaypointCommand+�� $��V��$��30��$��40���� $��V��$��30��$��40���� $��1��
������ $��LeftToRight=����� *TaskNav.NavPath+ �� SelectedItem �� *SelectedWaypoint+ �� Mode$ TwoWay=�  	 �� 
TargetType
 �� Property �� Value��%�   '��       '�� S     �� "	 ��.��� 
 � ��
 �   ��� 
 � ��
 ������� 
 � ��$ 20(��� "	 ��.��� 
 � ��$
 Auto���� 
 � ��
 �   ����� "	 ��.��� 
 � ��
 ����� �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ��� 
 � ��
 �   �P�  �@��
IsSelected . . ��� 
 � ��
 �   � �� View �� .P��� $��ID�� �� DisplayMemberBinding �� *Id+��  �� Width$	 120��$��Command�� �� CellTemplate �� .R���  ��Name$ ComboBox��$
��Auto��$	��100��$	��100������  �� Source# [  �� SelectedValue �� *	Command+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� 	Converter# [ �� $��Latitude�� �� *
Latitude+ �� StringFormat$ {0:0.######}���� $��	Longitude�� �� *	Longitude+$ {0:0.######}���� $��Altitude�� �� .R��� $��50������ *
Altitude+�� $
��Time�� �� .R��� $��50������ *Param1+�� $��Param2�� �� .R��� $��50������ *Param2+�� $��Param3�� �� .R��� $��50������ *Param3+�� $	��Yaw�� �� .R��� $��50������ *Param4+