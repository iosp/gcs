   M S B A M L   `   `   `  ���� E  AInterfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:Interfaces;assembly=Interfaces
Interfaces  E AGCSArduino, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:GCSArduino.ViewGCSArduino.View A =Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;*clr-namespace:Common.Enums;assembly=CommonCommon.Enums $   GCSArduino.View.ToolActionMenu   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     C
interfaces,clr-namespace:Interfaces;assembly=Interfaces     .viewclr-namespace:GCSArduino.View     <enums*clr-namespace:Common.Enums;assembly=Common     $	��107��$	��130��  � DataContext  ��    Locator �� Source# [   �� Path$ QuadToolActionViewModel����� *
MarginView+ � 	Resources ��%    .��� -    �Name$ 
LayoutRoot��y��� *VisibilityView+.+��� -    ��Name$ tool1��$��0,0.001,0,-0.001q����	#FF202F36 �� CornerRadius$ 1��._�o�   komunaMapMenu#���  ��Z� ��Z� .@�m� -    m�Name$ dynamicMenuItems��$��בחר משימה��
�������  komunaMenuItem2#���  $��0,3,0,0q�$	��Top=����� *Tasks+����  �� DataType   Interfaces.ITask"  .R�m� 	 m� IsCheckable	 .  $��RightToLeft=����� *
TaskName+
 m� Command
 ��  �� RelativeSource �� *FindAncestor+ �� AncestorType$  GCSArduino.View.ToolActionMenu"  $% DataContext.SelectedTaskCommand� m� CommandParameter �� m� $	��0,3q�$��ניהול משימה��
�������#���  .@�m� $	��0,3q�$��טען משימה��
�������	 .  #���  $	��Top=�$y�	Collapsed=�$ LoadMission��
 �� *CommandState+m� $	��0,3q�$��
המראה��
�������	 . #���  $	��Top=�$ TakeOff��
 �� *CommandState+m� $	��0,3q�$��התחל��
�������	 . #���  $	��Top=�$ Start��
 �� *CommandState+m� $	��0,3q�$��
השתהה��
�������	 . #���  $	��Top=�$ Pause��
 �� *CommandState+m� $	��0,3q�$��
נחיתה��
�������	 . #���  $	��Top=�$
 Land��
 �� *CommandState+m� $	��0,3q�$��
מצלמה��
�������	 . #���  $	��Top=�$ 
OpenCamera��
 �� *CommandState+m� $	��0,3q�$��חזור_הביתה��
�������	 . #���  $	��Top=�$ CameHome��
 �� *CommandState+m� $	��0,3q�$��
שליטה��
�������	 . #���  $	��Top=�$ ControlRequest�� m� 	IsChecked �� *IsJoystickChecked+
 �� *CommandState+m� $	��0,3q�$
��Mode��
�������#���  .@�m� $	��0,3q�$��	Stabilize��$��RightToLeft=�
�������	 .  #���  $	��Top=�"  Common.Enums.VehicleModeEnum   	Stabilize# Z 
 �� *CommandsActionMode+m� $	��0,3q�$
��Auto��$��RightToLeft=�
�������	 .  #���  $	��Top=�   Auto# Z 
 �� *CommandsActionMode+m� $	��0,3q�$��Loiter��$��RightToLeft=�
�������	 .  #���  $	��Top=�   Loiter# Z 
 �� *CommandsActionMode+m� $	��0,3q�$��Guided��$��RightToLeft=�
�������	 .  #���  $	��Top=�   Guided# Z 
 �� *CommandsActionMode+m� $	��0,3q�$��AltHold��$��RightToLeft=�
�������	 .  #���  $	��Top=�   AltHold# Z 
 �� *CommandsActionMode+