   M S B A M L   `   `   `  ����A  =Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;*clr-namespace:Common.Enums;assembly=CommonCommon.Enums  5      E AGCSArduino, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:GCSArduino.ConverterGCSArduino.Converter 5      /   )GCSArduino.Controls.ArduCopterModeControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <enums*clr-namespace:Common.Enums;assembly=Common     5      8	converter"clr-namespace:GCSArduino.Converter     5        � 	Resources  5
      (  "GCSArduino.Converter.ModeConverter   ModeConverter��%   &	          5   
   6:   5      .��5      � y�6   �� *
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
   	� $��0��6   $��0��6&   $��	Stabilize��63   "   Common.Enums.VehicleModeEnum   	Stabilize#��Z 6    ToggleButtonGreenStyle#��[ 6J   d� 	IsChecked 6H   �� *!VehicleSelected.QuadStatus.Mode+ �� 	Converter# [   �� ConverterParameter# Z ��6�   �� *QuadModeCommand+6|  	� 5   
   $��1��6   $��0��6&   $
��Acro��63      Acro#��Z 6  #��[ 6J   6H   �� *!VehicleSelected.QuadStatus.Mode+# [  # Z ��6�   �� *QuadModeCommand+6|  	� 5   
   $��2��6   $��0��6&   $��AltHold��63      AltHold#��Z 6  #��[ 6J   6H   �� *!VehicleSelected.QuadStatus.Mode+# [  # Z ��6�   �� *QuadModeCommand+6}  	� 5   
   $��0��6   $��1��6&   $
��Auto��63   	   Auto#��Z	 6  #��[ 6J   6H   �� *!VehicleSelected.QuadStatus.Mode+# [  # Z	 ��6�   �� *QuadModeCommand+6|  	� 5    
   $��1��6   $��1��6&   $��Guided��63   
   Guided#��Z
 6  #��[ 6J   6H   �� *!VehicleSelected.QuadStatus.Mode+# [  # Z
 ��6�   �� *QuadModeCommand+6|  	� 5!   
   $��2��6   $��1��6&   $��Loiter��63      Loiter#��Z 6  #��[ 6J   6H   �� *!VehicleSelected.QuadStatus.Mode+# [  # Z ��6�   �� *QuadModeCommand+6|  	� 5"   
   $��0��6   $��2��6&   $	��RTL��63   
   RTL#��Z 6  #��[ 6J   6H   �� *!VehicleSelected.QuadStatus.Mode+# [  # Z ��6�   �� *QuadModeCommand+6|  	� 5#   
   $��1��6   $��2��6&   $��Circle��63      Circle#��Z 6  #��[ 6J   6H   �� *!VehicleSelected.QuadStatus.Mode+# [  # Z ��6�   �� *QuadModeCommand+6|  	� 5$   
   $��2��6   $��2��6&   $
��Land��63      Land#��Z 6  #��[ 6J   6H   �� *!VehicleSelected.QuadStatus.Mode+# [  # Z ��6�   �� *QuadModeCommand+6|  	� 5%   
   $��0��6   $��3��6&   $��OfLoiter��63      OfLoiter#��Z 6  #��[ 6J   6H   �� *!VehicleSelected.QuadStatus.Mode+# [  # Z ��6�   �� *QuadModeCommand+6|  	� 5&   
   $��1��6   $��3��6&   $��Drift��63      Drift#��Z 6  #��[ 6J   6H   �� *!VehicleSelected.QuadStatus.Mode+# [  # Z ��6�   �� *QuadModeCommand+6|  	� 5'   
   $��2��6   $��3��6&   $��Sport��63      Sport#��Z 6  #��[ 6J   6H   �� *!VehicleSelected.QuadStatus.Mode+# [  # Z ��6�   �� *QuadModeCommand+6|  5)      5*      