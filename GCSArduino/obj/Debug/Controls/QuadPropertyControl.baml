   M S B A M L   `   `   `  ����E  AGCSArduino, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:GCSArduino.ControlsGCSArduino.Controls  5      -    'GCSArduino.Controls.QuadPropertyControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5Control!clr-namespace:GCSArduino.Controls     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)   �� 5      $
��Auto �6   6,    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��1��6.   .+�5      (   "GCSArduino.Controls.SignalStrength  �� 5      5      �� 5   
   $ 
Horizontal=�6   $��2��6.   .+�5      � $��2��6   $��50��6+   $��22��67    � Orientation$ 
Horizontal=�6C    � IsIndeterminate .  $��3,0,3,0q�6t   6�   �� 5       5!      �� 5"   
   $ 
Horizontal=�6   $��3��6.   .+�5#      �� $��3,0,3,0q�6`   ��6   �� *UsedCapacity+��67   �� *UsedCapacityColor+6s   �� 5$      $��mA��6   $��3,0,3,0q�6L   ��6#   �� *UsedCapacityColor+6_   �� 5%      5&      �� 5(   
   $ 
Horizontal=�6   $��4��6.   .+�5)      �� $
��GPS:��6   $��7��6&   $��3,0,3,0q�66   ��6G   �� *SatsInUseColor+6o   �� 5*      -   5         ��Name$	 gps��5*      $��8��6A   $��3,0,3,0q�6x   ��6&   �� *	SatsInUse+��6R   �� *SatsInUseColor+6�   �� 5+      5,      �� 5-   
   $ 
Horizontal=�6   $��5��6.   .+�5.      �� $	��RC:��6   $��9��6(   $��3,0,3,0q�6^   ��68   �� *RcQualityColor+6q   �� 5/      $��10��6]   $��3,0,3,0q�6o   ��6   �� *	RcQuality+��67   �� *RcQualityColor+6�   �� 50      51      �� 53   
   $��1��6   $��10��6   
���   �6I   �� 54   
   $��2��6   $��10��6"   $ 
Horizontal=�67   $
��Left=�6P   .+�55      �� $��Start / Stop��6   $��50��6,   $��80��68   $��5q�6C   6P   �� 56      $��
Arm/Disarm��6   $��50��6*   $��80��66   $��5q�6A   6N   �� 57      $��Landing��6   $��50��6'   $��80��63   $��5q�6>   6K   �� 58      $	��RTL��6   $��50��6#   $��80��6/   $��5q�6:   6G   �� 59      $��Loiter��6   $��50��6&   $��80��62   $��5q�6=   6J   5;      5<      5=      