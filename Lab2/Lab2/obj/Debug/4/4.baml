   M S B A M L   `   `   `  ����?  ;Lab2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:Lab2._4Lab2._4  5   	       
Lab2._4._4   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   'localclr-namespace:Lab2._4     5   	     � Title$  
TextEditor��5   	   $	��383��6   $	��600��6)    � 	Resources 5         LinearPanelBrush �� 
StartPoint �� EndPoint � Color � Offset��%�   &	        �� 5	   
   $	 0,0&�5
      $	 1,0&�6/   .��5      � $ Yellow��6   $ 0Q�6*   � 5      $ 	LightBlue��6   $	 0.5Q�6-   � 5      $ Yellow��6   $ 1Q�6*   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��0��6   ���	#FFCBBEBE$��2q�5   )    �� CornerRadius$ 10��6=   $��5,5,5,0q�5      #��[  5      ._�5      ��  �� Orientation$ 
Horizontal=�6   $��5q�62   .+�5      �� 	 � ToolTip$	 Відкрити��6   $
��Open��6,   .��5      �� $��images/open.png��6   $��32��65   $��32��6@   5       �� 5!      $	 Очистити��6      Lab2._4.CustomCommands
   Clear#��Z
 6,   .��5"      �� $��images/clear.png��6   $��32��66   $��32��6A   5#      �� 5$      $	 Копіювати��6   $
��Copy��6-   ��6<   ��  �� ElementName$ txtDocument��.��5%      �� $��images/copy.png��6   $��32��65   $��32��6@   5&      �� 5'      $	 Вставити��6   $��Paste��6,   ��6<   �� $ txtDocument��.��5(      �� $��images/paste.png��6   $��32��66   $��32��6A   5)      5*      5+      �� 5.   
   $��1��6   ���	#FFCBBEBE$��2q�5/   )   $ 10��6=   $��5,10,5,5q�50      #��[  51      ._�52      � $��5q�6    53      �� 54      �� 55      $
��Auto �6$   .+�58      �� -   5         ��Name$ txtDocument��58      $��0��59       �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�5;       �� TextWrapping$
 Wrap=�5<      $��5q�5>      ��5=      �� $ 
fontSlider�� �� Path$ Value� �� UpdateSourceTrigger$ PropertyChanged=�.��5?   >   TEXT5A      ]� 5C      $��1��6   ��.  .+�5D      �� $ ��Розмір шрифту:��6   $
��Left=�65   $��5q�6K   �� 5E      -   5         ��Name$ 
fontSlider��5E      $
��Left=�5F      $��4q�5G      $	��100��5H       � Minimum$ 12Q�5I       � Maximum$	 100Q�5J       � Value$ 24Q�5K      �� 5L      $��OK��6   $
��Save��5M      $��Right=�5N      $��5q�5O      ��5P      Z� 5Q      5R      5S      5T      5U      