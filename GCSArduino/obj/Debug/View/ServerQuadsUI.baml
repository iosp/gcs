   M S B A M L   `   `   `  ����E  AGCSArduino, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:GCSArduino.ViewGCSArduino.View  5   K   #    GCSArduino.View.ServerQuadsUI   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .viewclr-namespace:GCSArduino.View     6K   .��5      � ��5	   
   �� 5
        �� EndPoint$  0.5,1&�6"    � MappingMode$ RelativeToBoundingBox=�63    �� 
StartPoint$ 0.5,0&�6W   .��5      �  � Color$ Black��6    � Offset$ 0Q�6-   � 5      $ 	#FF474747��6   $ 1Q�61   � 5      $ 	#FF3B3B3B��6   $ 0.468Q�61   � 5      $ 	#FF515151��6   $ 0.644Q�61   5       � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   6.   �� 5      $	��33* �6   6-   �� 5      $	��33* �6   6-   �� 5      $	��33* �6   6-   �� 5      $
��Auto �6   6.   .+�5   
      GCSArduino.View.MenuView  $��0��6   $��0��6)   $��10��66   6N   !   GCSArduino.View.EsriMapView  5   
   $��0��6   $��1��6+   $��5��68   6N   #   GCSArduino.View.LayersControl  5   
   $��0��6   $��1��6-   $��5��6:   $y�Visible=�6N   $��Right=�6c   $��	0,30,50,0q�6   $	��Top=�6�   "   GCSArduino.View.ToolsControl  5    
   $��0��6   $��1��6,   $��5��69   $y�Visible=�6M   $��Right=�6b   $��	0,60,50,0q�6~   $	��Top=�6�   $   GCSArduino.View.ToolActionMenu  5!   
   $��0��6   $��1��6/   $��5��6<   $
��Left=�6P   $	��Top=�6k   %   GCSArduino.View.QuadsStatusView  5"   
   $��0��6   $��1��6/   $��5��6<   $y�Visible=�6P   $��Right=�6e   $��	0,90,50,0q�6�   $	��Top=�6�   &    GCSArduino.View.ListWaypointView  5#   
   $��0��6    $��1��60   $��5��6=   $y�Visible=�6Q   $��Right=�6f   $��
0,120,50,0q�6�   $	��Top=�6�   %   GCSArduino.View.MaintenanceView  5%   
   $��0��6    $��1��60   $��5��6=   $y�Visible=�6Q   $��Right=�6f   $��
0,150,50,0q�6�   $	��Top=�6�   5&      5'      