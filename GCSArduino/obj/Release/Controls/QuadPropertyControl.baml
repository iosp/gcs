   M S B A M L   `   `   `  ���� E  AGCSArduino, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:GCSArduino.ControlsGCSArduino.Controls  -    'GCSArduino.Controls.QuadPropertyControl   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     5Control!clr-namespace:GCSArduino.Controls     .���   � RowDefinitions  �� $
��Auto ��� $��* ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �.+���  �� Orientation$ 
Horizontal=�$��1��.+�(   "GCSArduino.Controls.SignalStrength  �� �� $ 
Horizontal=�$��2��.+�� $��2��$��50��$��22�� � Orientation$ 
Horizontal=� � IsIndeterminate .  $��3,0,3,0q��� �� $ 
Horizontal=�$��3��.+��� $��3,0,3,0q����� *UsedCapacity+���� *UsedCapacityColor+�� $��mA��$��3,0,3,0q����� *UsedCapacityColor+�� �� $ 
Horizontal=�$��4��.+��� $
��GPS:��$��7��$��3,0,3,0q����� *SatsInUseColor+�� -    ��Name$	 gps��$��8��$��3,0,3,0q����� *	SatsInUse+���� *SatsInUseColor+�� �� $ 
Horizontal=�$��5��.+��� $	��RC:��$��9��$��3,0,3,0q����� *RcQualityColor+�� $��10��$��3,0,3,0q����� *	RcQuality+���� *RcQualityColor+�� �� $��1��$��10��
���   ��� $��2��$��10��$ 
Horizontal=�$
��Left=�.+��� $��Start / Stop��$��50��$��80��$��5q��� $��
Arm/Disarm��$��50��$��80��$��5q��� $��Landing��$��50��$��80��$��5q��� $	��RTL��$��50��$��80��$��5q��� $��Loiter��$��50��$��80��$��5q�