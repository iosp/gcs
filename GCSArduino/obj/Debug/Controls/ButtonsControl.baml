   M S B A M L   `   `   `  ����E  AGCSArduino, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GCSArduino.EnumsGCSArduino.Enums  5   K   ;"clr-namespace:GCSArduino.ResourcesGCSArduino.Resources  5      ;"clr-namespace:GCSArduino.ConverterGCSArduino.Converter  6C   A =Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;*clr-namespace:Common.Enums;assembly=CommonCommon.Enums 5      (    "GCSArduino.Controls.ButtonsControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0enumsclr-namespace:GCSArduino.Enums     6K   8	resources"clr-namespace:GCSArduino.Resources     5      8	converter"clr-namespace:GCSArduino.Converter     6C   =enums2*clr-namespace:Common.Enums;assembly=Common     5        � 	Resources  5
      8   2GCSArduino.Converter.SelectedVehicleToArmConverter !  SelectedVehicleToArmConverter(   "GCSArduino.Converter.ModeConverter  ModeConverter��%:   &	        &	        5   
   6Z     5   
   6:   5      .��5      � y�6   �� *
Visibility+ � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   6.   .+�5   
   *   $GCSArduino.Resources.ArmToggleButton  $��0��6$   $��0��61     ArmToggleButtonStyle#���  6�   "   GCSArduino.Enums.SoftkeyEnum   	ArmDisArm#��Z 6     	ArmStatus 6B   �� *" VehicleSelected.QuadStatus.Armed+ �� 	Converter# [   �� Mode$ TwoWay=���6�   �� *ButtonCommands+6O  �� 5   
   $��0��6   $��2��6   $��Set Home��6.     ButtonWhiteStyle#���  6A      SetHome#��Z 6�   ��6l   �� *ButtonCommands+6�   	� 5   
   $��0��6   $��1��6#   $��	Stabilize��64   "  Common.Enums.VehicleModeEnum   	Stabilize#��Z 6    ToggleButtonGreenStyle#��[ 6M  	 d� 	IsChecked	 6I   �� *!VehicleSelected.QuadStatus.Mode+# [ 
 �� ConverterParameter#
 Z ��6�   �� *QuadModeCommand+6  	� 5    
   $��1��6   $��1��6#   $��Loiter��63      Loiter#��Z 6	  #��[ 6E  	 6G   �� *!VehicleSelected.QuadStatus.Mode+# [ #
 Z ��6�   �� *QuadModeCommand+6w  	� 5!   
   $��1��6   $��2��6#   $	��RTL��63   
   RTL#��Z 6  #��[ 6?  	 6G   �� *!VehicleSelected.QuadStatus.Mode+# [ #
 Z ��6�   �� *QuadModeCommand+6q  	� 5"   
   $��1��6   $��0��6#   $
��Auto��63      Auto#��Z 6  #��[ 6A  	 6G   �� *!VehicleSelected.QuadStatus.Mode+# [ #
 Z ��6�   �� *QuadModeCommand+6s  �� 5%   
   $��2��6   $��0��6   $��Start��6.   #���  6A      StartMission#��Z 6�   ��6l   �� *ButtonCommands+6�   �� 5&   
   $��2��6   $��1��6   $
��Stop��6.   #���  6A      StopMission#��Z 6�   ��6l   �� *ButtonCommands+6�   �� 5'   
   $��2��6   $��2��6   $��Restart��6.   #���  6A      RestartMission#��Z 6�   ��6l   �� *ButtonCommands+6�   �� 5)   
   $��3��6   $��0��6   $��Take Off��6.   #���  6D      TaskOff#��Z 6�   ��6o   �� *ButtonCommands+6�   �� 5*   
   $��3��6   $��1��6   $��Sensor2��6.   #���  6C      Reserve#��Z 6�   ��6n   �� *ButtonCommands+6�   �� 5+   
   $��3��6   $��2��6   $��Sensor3��6.   #���  6C   #��Z 6�   ��6n   �� *ButtonCommands+6�   5.      5/      