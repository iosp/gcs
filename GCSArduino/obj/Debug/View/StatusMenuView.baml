   M S B A M L   `   `   `  ����E  AGCSArduino, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:GCSArduino.ControlsGCSArduino.Controls  5      ;"clr-namespace:GCSArduino.ConverterGCSArduino.Converter  5      ;"clr-namespace:GCSArduino.ViewModelGCSArduino.ViewModel  6C   9!clr-namespace:GCSArduino.BehaviorGCSArduino.Behavior  5      $    GCSArduino.View.StatusMenuView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5Control!clr-namespace:GCSArduino.Controls     5      8	converter"clr-namespace:GCSArduino.Converter     5      8	viewModel"clr-namespace:GCSArduino.ViewModel     6C   6behavior!clr-namespace:GCSArduino.Behavior     5        � DataContext  5	   !   ��    Locator �� Source# [   �� Path$  StatusMenuControlViewModel� � 	Resources 5      2   ,GCSArduino.Converter.GpsStatusColorConverter  GpsStatusColorConverter0  *GCSArduino.Converter.BatteryColorConverter  BatteryColorConverter0   *GCSArduino.Converter.EnumToStringConverter  EnumToStringConverter)   #GCSArduino.Converter.DebugConverter  DebugConverter,   &GCSArduino.Converter.QuadModeConverter  QuadModeConverter��%�   &	       &	      &	 &     &	 9     &	 L       5   
   6O     5   
   6J     5   
   6J     5   
   6<     5   
   6C   5      .��5      � 
���   �$��30��6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��LeftToRight=�6.   $
��Left=�6K   .+�5      (   "GCSArduino.Controls.SignalStrength  $��3,0,3,0q�6V     6%   �� *
QuadStatus+ �� Mode$ TwoWay=�6i   �� 5      $��40��6}   $��40��6�     MenuSizeText#��[ 6  $��2��6+  ��6   �� *QuadStatus.GpsStatus+ �� 	Converter# [ $ TwoWay=���6�   �� *QuadStatus.GpsStatus+# [ $ TwoWay=�6>  �� 5      $��\��6   #��[ 6�   ��6!   �� *QuadStatus.GpsStatus+# [ $ TwoWay=�6�   �� 5      $��40��6?   $��40��6M   #��[ 6�   ��6   �� *QuadStatus.Satellites+��6\   �� *QuadStatus.GpsStatus+# [ $ TwoWay=�6�   �� 5      #��[ 6�   $��3,0,20,0q�6�   ��6   �� *QuadStatus.Hdop+ �� StringFormat$ Hd ({0})����6P   �� *QuadStatus.GpsStatus+# [ $ TwoWay=�6�   �� 5      $	��100��6g   #��[ 6s   ��6   �� *QuadStatus.Mode+# [ 6�   5      �� 5"   
   $ 
Horizontal=�6   $��LeftToRight=�6.   $��Center=�6K   .+�5#      �� #��[ 6I   
������ ��6   �� *DistanceFromTarget+$ TwoWay=�6�   �� 5$      $��-��6   #��[ 6!   
������ 6b   �� 5%      #��[ 6C   
������ ��6   �� *NextWaypoint+$ TwoWay=�6�   5&      �� 5)   
   $ 
Horizontal=�6   $��RightToLeft=�6.   $��Right=�6J   .+�5*      �� -   5         ��Name$
 Time��5*      $	��100��6&   $	��100��65   #��[ 6D   $��3,0,3,0q�6j   � 5+      $	��100��6�   	 � Orientation$	 
Horizontal=�6�   
 � IsIndeterminate
 .  $��3,0,3,0q�6�     6   �� # [  $ VehiclesSource� � Value 6h   �� *+)VehicleSelected.QuadStatus.PercentBattery+$ TwoWay=�6  �� 5,      $��V��6   #��[ 6!   $��3,0,3,0q�6�   ��6H   �� *QuadStatus.PercentBattery+# [ $ TwoWay=�6�   �� 5-      $��60��6P   
������ #��[ 6�   $��3,0,3,0q�6�   ��6   �� *QuadStatus.BatteryVoltage+$ TwoWay=���6u   �� *QuadStatus.PercentBattery+# [ 6  �� 5.      $��A��6   #��[ 6!   $��3,0,3,0q�6�   ��6H   �� *QuadStatus.PercentBattery+# [ $ TwoWay=�6�   �� 5/      $��60��6P   
������ #��[ 6�   $��3,0,3,0q�6�   ��6   �� *QuadStatus.BatteryCurrent+$ TwoWay=���6u   �� *QuadStatus.PercentBattery+# [ 6  50      53      54      