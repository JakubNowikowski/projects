   M S B A M L   `   `   `  ����G  CWeekPlanner2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:WeekPlanner2WeekPlanner2  5   	       WeekPlanner2.AddWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:WeekPlanner2     5   	     � Title$  	AddWindow��5   	   $	��500��6   $	��800��6(    � WindowStartupLocation$ CenterScreen=�64   
�������.��5
      �� $��20q�6   .+�5   
   �� $
��0 10q�6   $��Choose day:��6"   $
��Bold"�65   � 5   
   -   5         �Name$ gridDays��5       � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   �� 5      $��* �6#   �� 5      $��* �6#   �� 5      $��* �6#   �� 5      $��* �6#   �� 5      $��* �6#   .+�5      	� -   5         	�Name$ radioButtonMon��5   *   $��0��6   $��Monday��6B   	� 5      -   5        $ radioButtonTue��5   *   $��1��6   $��Tuesday��6B   	� 5      -   5        $ radioButtonWed��5   *   $��2��6   $��	Wednesday��6B   	� 5      -   5        $ radioButtonThu��5   *   $��3��6   $��Thursday��6B   	� 5      -   5        $ radioButtonFri��5   *   $��4��6   $��Friday��6B   	� 5       -   5        $ radioButtonSat��5    *   $��5��6   $��Saturday��6B   	� 5!      -   5        $ radioButtonSun��5!   *   $��6��6   $��Sunday��6B   5#      �� 5'   
   $
��0 10q�6   $��Insert activity:��6"   $
��Bold"�6:   �� 5)   
   -	   5         ��Name$ textBoxActivity��5)       �� TextWrapping$
 Wrap=�6+   $��10q�6?   .��6L   5+   
   �� 5/   
   $
��0 10q�6   $��	Set time:��6"   $
��Bold"�63   � 51   
    52      �� 53      $��* �6#   �� 54      $��* �6#   �� 55      $��* �6#   �� 56      $	��25* �6#   .+�5:      �� -
   5        $ textBoxHours��5:      $��20��6,   $��0 0 0 0q�67   $��0��6H   6X   �� 5;      $
��10 0q�6   $��:��6&   $
��Bold"�6/   $��1��6A   �� 5<      -   5        $ textBoxMinutes��5<      $��20��6.   $��0 0 0 0q�69   $��2��6J   6Z   5>      �� 5B   
   $
��0 10q�6   $��Choose importance scale:��6"   $
��Bold"�6B   	� 5D   
   -   5        $ radioButtonVeryImportant��5D      $��Very important��68   $��0 0 0 10q�6Q   	� 5E   
   -   5        $ radioButtonImportant��5E      $��	Important��64   $��0 0 0 10q�6H   	� 5F   
   -   5        $  radioButtonSlightImportant��5F      $��Slightly important��6:   $��0 0 0 10q�6W   �� 5H   
   -   5         ��Name$ btn_Add��5H   8      
MainButton#��[  6   $	��Add��6I   6W   5J      5K      