   M S B A M L   `   `   `  ����D  @ToPayList, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:ToPayList.Controller.ConvertersToPayList.Controller.Converters  5          ToPayList.View.lista   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      D
converters-clr-namespace:ToPayList.Controller.Converters     5      @d2http://schemas.microsoft.com/expression/blend/2008     5   $   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6a     N� Title$  lista��6    � 	Resources 5      4   .ToPayList.Controller.Converters.DatetimeToDate 	  dttod/   )ToPayList.Controller.Converters.IntToBool  	inttobool  ItemTemplate �� 
StartPoint �� EndPoint � Offset � Color � ColumnDefinitions � RowDefinitions �� StringFormat  SelectedTemplate	 ��Name
 d� 	IsChecked �� Path �� 	Converter �� 
TargetType  ContainerStyle �� Property �� Value �� Triggers P� Property P� Value��%�  &	        &	      &	 &     &	     7  &	 �    7  7    5   
   6$     5   
   6   �� 5
   
   .R�5      � ��5      �� 5      $	 0,0&�6*   $	 0,1&�6;   .��5      � $ 0Q�6'   $ White��62   6B   � 5      $ 1Q�6'   $ Silver��62   6C   5       5      �� 5      $��10 �6'   64   �� 5      $��* �6'   63   �� 5      $��10 �6'   64   �� 5      $��* �6'   63    5      �� 5      $��* �6$   61   �� 5      $��* �6$   61   .+�5      �� $��0��6   $��1��6)   $��3��69   $��16(�6h   
��� � ���6M   �� *	Descricao+6v   �� 5      $��1��6   $��1��6)   $��10(�6n   
���������69   �� *Data+$ {0:MM/dd/yyyy}��6|   �� 5      $��1��6   $��3��6)   $��10(�6P   
���������69   �� *Valor+6^   5      5       �� 5"   
   .R�5#      � ��5$      �� 5%      $	 0,0&�6*   $	 0,1&�6;   .��5&      � $ 0Q�6'   $ 	LightBlue��62   6F   � 5'      $ 1Q�6'   $ 	#FF0077CC��62   6F   5(       5*      �� 5+      $��10 �6'   64   �� 5,      $
��Auto �6'   66   �� 5-      $��* �6'   63   �� 5.      $��10 �6'   64   �� 5/      $��* �6'   63    51      �� 52      $��* �6$   61   �� 53      $��* �6$   61   .+�55      �� $	 chkItem��6d   $��0��6   $��1��6(   $��2��68   $��Center=�6I   
 6u   �� $
 Pago�8   6�   �� 56      $��0��6   $��2��6)   $��3��69   $��16(�6m   
��� � ���6M   �� $ 	Descricao�6{   �� 57      $��1��6   $��2��6)   $��10(�6s   
���������69   �� $
 Data�$ {0:MM/dd/yyyy}��6�   �� 58      $��1��6   $��4��6)   $��10(�6U   
���������69   �� $ Valor�6c   59      5:      �� 5<   
   " ��6   .�5=      ��  � ��8   60    5>      P� 5?       �@��
IsSelected . . �5@      ��  � ��8  68   5A      5C      5D      .��5E      �� .+�5F   
   �� -   5         ��Name$ btnNovo��5F      .��68   	Adicionar6C   �� 5G   
   -   5         ��Name$ lstToPay��5G      $��Stretch=�6$   #��[ 5I      ��5H      �� 5K      5L      5M      