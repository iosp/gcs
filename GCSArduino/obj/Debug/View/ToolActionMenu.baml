   M S B A M L   `   `   `  ����E  AInterfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:Interfaces;assembly=Interfaces
Interfaces  5      E AGCSArduino, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:GCSArduino.ViewGCSArduino.View 6N   A =Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;*clr-namespace:Common.Enums;assembly=CommonCommon.Enums 5      $   GCSArduino.View.ToolActionMenu   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      C
interfaces,clr-namespace:Interfaces;assembly=Interfaces     5      .viewclr-namespace:GCSArduino.View     6N   <enums*clr-namespace:Common.Enums;assembly=Common     5      $	��107��5
      $	��130��6     � DataContext  5      ��    Locator �� Source# [   �� Path$ QuadToolActionViewModel���5	      �� *
MarginView+ � 	Resources 5      ��%    5      .��5      � -   5         �Name$ 
LayoutRoot��5      y�6   �� *VisibilityView+.+�5   
   �� -   5         ��Name$ tool1��5      $��0,0.001,0,-0.001q�6    ���	#FF202F36 �� CornerRadius$ 1��6Q   ._�5      o�   komunaMapMenu#���  6   ��6;   Z� ��6Q   Z� .@�5      m� -   5         m�Name$ dynamicMenuItems��5      $��בחר משימה��65   
�������  komunaMenuItem2#���  6[   $��0,3,0,0q�6�   $	��Top=�6�   ��6�   �� *Tasks+��5      �� 5       �� DataType   Interfaces.ITask"  6'   .R�5      m� 	 m� IsCheckable	 .  $��RightToLeft=�6Y   ��6'   �� *
TaskName+
 m� Command
 6u   ��  �� RelativeSource �� *FindAncestor+ �� AncestorType$  GCSArduino.View.ToolActionMenu"  $% DataContext.SelectedTaskCommand� m� CommandParameter 6  �� 5      5      5      m� 5      $	��0,3q�6   $��ניהול משימה��6)   
�������#���  6Q   .@�5      m� $	��0,3q�6!   $��טען משימה��61   
�������	 .  #���  6l   $	��Top=�6�   $y�	Collapsed=�6�   $ LoadMission��6�   
 6�   �� *CommandState+6�   m� 5      $	��0,3q�6!   $��
המראה��61   
�������	 . #���  6l   $	��Top=�6�   $ TakeOff��6�   
 6�   �� *CommandState+6�   m� 5      $	��0,3q�6!   $��התחל��61   
�������	 . #���  6k   $	��Top=�6�   $ Start��6�   
 6�   �� *CommandState+6�   m� 5      $	��0,3q�6!   $��
השתהה��61   
�������	 . #���  6k   $	��Top=�6�   $ Pause��6�   
 6�   �� *CommandState+6�   m� 5      $	��0,3q�6!   $��
נחיתה��61   
�������	 . #���  6k   $	��Top=�6�   $
 Land��6�   
 6�   �� *CommandState+6�   5       m� 5!      $	��0,3q�6   $��
מצלמה��6-   
�������	 . #���  6f   $	��Top=�6�   $ 
OpenCamera��6�   
 6�   �� *CommandState+6�   m� 5"      $	��0,3q�6   $��חזור_הביתה��6-   
�������	 . #���  6k   $	��Top=�6�   $ CameHome��6�   
 6�   �� *CommandState+6�   m� 5$      $	��0,3q�6   $��
שליטה��6-   
�������	 . #���  6�   $	��Top=�6�   $ ControlRequest��6�    m� 	IsChecked 6?   �� *IsJoystickChecked+
 6�   �� *CommandState+6�   m� 5%      $	��0,3q�6   $
��Mode��6)   
�������#���  6J   .@�5&      m� $	��0,3q�6!   $��	Stabilize��61   $��RightToLeft=�6E   
�������	 .  #���  6�   $	��Top=�6�   "  Common.Enums.VehicleModeEnum   	Stabilize# Z 6�   
 6�   �� *CommandsActionMode+6�   m� 5'      $	��0,3q�6!   $
��Auto��61   $��RightToLeft=�6E   
�������	 .  #���  6�   $	��Top=�6�      Auto# Z 6�   
 6�   �� *CommandsActionMode+6�   m� 5(      $	��0,3q�6!   $��Loiter��61   $��RightToLeft=�6E   
�������	 .  #���  6�   $	��Top=�6�      Loiter# Z 6�   
 6�   �� *CommandsActionMode+6�   m� 5)      $	��0,3q�6!   $��Guided��61   $��RightToLeft=�6E   
�������	 .  #���  6�   $	��Top=�6�      Guided# Z 6�   
 6�   �� *CommandsActionMode+6�   m� 5*      $	��0,3q�6!   $��AltHold��61   $��RightToLeft=�6E   
�������	 .  #���  6�   $	��Top=�6�      AltHold# Z 6�   
 6�   �� *CommandsActionMode+6�   5+      5,      5-      5.      5/      