   M S B A M L   `   `   `  ����E  AGCSArduino, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:GCSArduino.ControlsGCSArduino.Controls  5      1clr-namespace:GCSArduino.ViewGCSArduino.View  6G   3clr-namespace:GCSArduino.EnumsGCSArduino.Enums  5      A =Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;*clr-namespace:Common.Enums;assembly=CommonCommon.Enums 6;   ;"clr-namespace:GCSArduino.ConverterGCSArduino.Converter  5      3clr-namespace:GCSArduino.ModelGCSArduino.Model  6C   ;"clr-namespace:GCSArduino.ResourcesGCSArduino.Resources  5
      [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 5      "    GCSArduino.View.ClientQuadUI   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <wpfWindowsTest!clr-namespace:GCSArduino.Controls     5      .viewclr-namespace:GCSArduino.View     6G   0enumsclr-namespace:GCSArduino.Enums     5      =enums2*clr-namespace:Common.Enums;assembly=Common     6;   8	converter"clr-namespace:GCSArduino.Converter     5      0modelclr-namespace:GCSArduino.Model     6C   8	resources"clr-namespace:GCSArduino.Resources     5
      Owf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms     5        � DataContext  5	       ��    Locator �� Source# [   �� Path$ ClientQuadUIViewModel� � 	Resources 5      8   2GCSArduino.Converter.SelectedVehicleToArmConverter ! SelectedVehicleToArmConverter(   "GCSArduino.Converter.ModeConverter  ModeConverter��%:   &	       &	        5   
   6Z     5   
   6:   5      .��5      � 
���   � � RowDefinitions 5   
   �� 5      $��30 �6   6*   �� 5      $��* �6   6)   �� 5      $��* �6   6)   �� 5      $
��Auto �6   6,    � ColumnDefinitions 5   
   �� 5      $	��70* �6   6-   �� 5      $	��30* �6   6-   .+�5   
   $   GCSArduino.View.StatusMenuView  $��0��6   $��0��6/   $��2��6<     6P   �� *VehicleSelected+6z   &    GCSArduino.Controls.VideoControl  5    
   $��1��6&   $��1��63      Port$
 2222��6C   6Q   !   GCSArduino.View.EsriMapView  5,   
   $��1��6   $��2��6(   6<   .   (GCSArduino.Controls.CreateMissionControl  5/   
   $��1��6/   $��2��6<   $
��Left=�6M   $��20q�6h   6v   /   )GCSArduino.Controls.ArduCopterModeControl  51   
   $y�	Collapsed=�60   $��1��6G   $��3��6X   6h   � 53   
   $��3��6   $��2��6    54      �� 55      $��* �6#   6/   �� 56      $
��Auto �6#   62   .+�58      +   %GCSArduino.Controls.InstrumentControl  $��0��6/   $��Center=�6?     6\   �� *VehicleSelected+6�   �� 59       �� Orientation$ Vertical=�6   $��1��60   $��Right=�6@   .+�5:      �� $ 
Horizontal=�6   .+�5;      �� $��Mission��6    $y�	Collapsed=�6/     TextBlockStyle#��[ 6F   $��5,0,20,0q�6n   $��80��6�   6�   �� 5<      $��2��6   $��0��6*   $��Start��6:     ButtonWhiteStyle#���  6M   "	   GCSArduino.Enums.SoftkeyEnum 	  StartMission#��Z 6�   ��6x   �� *MissionCommands+6�   �� 5=      $��2��6   $��1��6*   $
��Stop��6:   #���  6M   	 	  StopMission#��Z	 6�   ��6x   �� *MissionCommands+6�   �� 5>      $��2��6   $��2��6*   $��Restart��6:   #���  6M   
 	  RestartMission#��Z
 6�   ��6x   �� *MissionCommands+6�   	� 5?      $��0��6"   $��0��62   $
��Land��6?   "
  Common.Enums.VehicleModeEnum 
  Land#��Z 6    ToggleButtonGreenStyle#��[ 6P   d� 	IsChecked 6Q   �� *!VehicleSelected.QuadStatus.Mode+ �� 	Converter# [  �� ConverterParameter# Z ��6�   �� *QuadModeCommand+6�  5@      �� 5A      $ 
Horizontal=�6   .+�5B      �� $
��Mode��6   $y�	Collapsed=�6(   #��[ 6?   $��5,0,20,0q�6g   $��80��6y   6�   	� 5C      $��0��6   $��0��6.   $
��Auto��6;    
  Auto#��Z 6  #��[ 6K   6M   �� *!VehicleSelected.QuadStatus.Mode+# [ # Z ��6�   �� *QuadModeCommand+6}  	� 5D      $��0��6   $��0��6.   $��Guidede��6;    
  Guided#��Z 6  #��[ 6K   6M   �� *!VehicleSelected.QuadStatus.Mode+# [ # Z ��6�   �� *QuadModeCommand+6}  	� 5E      $��0��6   $��0��6.   $��Loitere��6;    
  Loiter#��Z 6  #��[ 6K   6M   �� *!VehicleSelected.QuadStatus.Mode+# [ # Z ��6�   �� *QuadModeCommand+6}  	� 5F      $��0��6   $��0��6.   $	��RTL��6;   
 
  RTL#��Z 6  #��[ 6K   6M   �� *!VehicleSelected.QuadStatus.Mode+# [ # Z ��6�   �� *QuadModeCommand+6}  5H      �� 5I      $ 
Horizontal=�6   .+�5J      *   $GCSArduino.Resources.ArmToggleButton  $��0��6,   $��0��69     ArmToggleButtonStyle#���  6�    	  	ArmDisArm#��Z 6     	ArmStatus 6J   �� *" VehicleSelected.QuadStatus.Armed+# [  �� Mode$ TwoWay=���6�   �� *MissionCommands+6X  d� 5K      $��Joystick��6   $��0��63   $��0��6C     ToggleButtonOnOffStyle#��[ 6}    6T   �� *IsCheckedJoystick+6�   �� 5L      $��3��6   $��0��6&   $��Take Off��66   #���  6L    	  TaskOff#��Z 6�   ��6w   �� *MissionCommands+6�   �� 5M      $��JRestart��6   #���  6.   ��6Y   �� *JoystickRestartCommands+6�   5N      5O      5P      5Q      5R      