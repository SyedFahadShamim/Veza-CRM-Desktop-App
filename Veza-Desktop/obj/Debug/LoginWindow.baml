   M S B A M L   `   `   `  ����G  CVeza-Desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Veza_DesktopVeza_Desktop  5   	       Veza_Desktop.LoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Veza_Desktop     5   	     � Title$  Login Window��5      $	��700��6#   $	��400��60   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $	��17* �6   .+�5   
   �� $��1��6   $��0��6    $��Assets\Image\ITSLogo.png��6-   $��96��6O   $��58��6Z   $��Center=�6f   $	��Top=�6�   $��152,68,145,0q�6�   6�   �� 5   
   -   5         ��Name$ label��5   -   $��1��6   $��0��6    $
��Chat��6<      
CustomFont#��[  6K   $��SemiBold"�6o   $��28(�6�   $
��Left=�6�   $��186,105,0,30q�6�   $��Stretch=�6�   $��85��6�    J� RenderTransformOrigin$ 0.328,0.385&�6�   
���   �z�5      V� 5      .~�5      �� �� 5      �� 5       �� Angle$ -0.423Q�6&   T� 5      5      5!      �� 5"   
   $��1��6   $��1��6    $��LOGIN��6-   $M��GC:\Users\Usman Paracha\Desktop\Project\CRM-GUI\CRM-GUI\Font\akaDora.ttf)�6=   $��Medium"�6�   $��28(�6�   $��145.2,29.2,144.6,11.2q�6�   $��Stretch=�6�   $��Center=�6�   $��38��6  $	��103��6  � 5$   
   $��1��6   $��2��6   $	��280��6,   $��Stretch=�69   
�������$��	0,0,0,100q�6h   $��2��6{   .+�5%      �� $��Center=�6   $��Center=�64   $��0,0,0,10q�6Q   $	��270��6c   .+�5&      �� -   5        $��Username��5&      
 � Tag$ Username��6*   $��Center=�69   $��Center=�6V   $��32��6q   $	��300��6}   $��36,-95,36,-220q�6�   $��1,1,1,1q�6�   .��6�   ��5'      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5(       �� 6   6sys&clr-namespace:System;assembly=mscorlib     6     �� 
TargetType" ��6S    �� 	Resources 5)        CueBannerBrush	 j� 
AlignmentX
 j� 
AlignmentY j� Stretch !� Visual��%�   &	       !� 5*   "   $
	 Left=�6E   $
 Center=�6W   $
 None=�6k    5+   &   �� 5,   *   $�� ��60   
�������6;   5.   #   5/       �� Triggers 50      P� 51   "    P� Property � �� �� Empty P� Value# Z 6:   . �52   &   ��  �� Property � �� �� Value# [ 6C   53   #   P� 54   "    � �� 6:   Z� . �55   &   ��  � ��# [ 6C   56   #   P� 57   "    �@J�IsKeyboardFocused . . �58   &   ��  � ��
 �����6C   59   #   5;      .��5<   %   5=      �� 5@      -   5        $ Login��5@      $	��123��6%   $��35��61   
���   �$M��GC:\Users\Usman Paracha\Desktop\Project\CRM-GUI\CRM-GUI\Font\akaDora.ttf)�6Q   $��20(�6�   $��Normal"�6�   $��
0,200,0,40q�6�   
�������$��LOGIN��6�   $��Center=�6  $��Center=�61  5A      G� 5C      -   5        $��Password��5C      $
��Left=�6+   $��
46,114,0,0q�6F   $	��Top=�6Z   $��32��6r   $	��301��6~   �� 5D      $��Enter your User name��6   $
��Left=�63   $��	42,20,0,0q�6N   $	��Top=�6a   $	��130��6y   �� 5E      $��Enter your Password��6   $
��Left=�62   $��	42,86,0,0q�6M   $	��Top=�6`   $	��130��6x   5H      5J      5K      