   M S B A M L   `   `   `  ����D  @ToPayList, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:ToPayList.Controller.ConvertersToPayList.Controller.Converters  5      $    ToPayList.View.EditTransaction   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      D
converters-clr-namespace:ToPayList.Controller.Converters     5        N� Title$  EditTransaction��5	       � 	Resources 5
      /   )ToPayList.Controller.Converters.IntToBool   	inttobool��%   &	          5   
   6   5      .��5      � .+�5   
   �� .+�5      �� 	Descricao6#   �� 5      -   5         ��Name$ txtDescricao��5      $��16(�6L   
��� � ���6,   ��  �� Path$ 	Descricao�6Z   �� 5      Data6   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ txtDate��5         SelectedDate 6*   �� $
 Data� �� StringFormat$ {0:dd/MM/yyyy}���� 5      Valor6   �� 5      -   5        $ txtValor��5      $��10(�6D   
���������6(   �� $ Valor�6R   �� 5      -   5         ��Name$ chkPago��5       d� 	IsChecked 6(   �� $
 Pago�	 �� 	Converter#	 [  .��6n   Pago6t   �� 5      Comprovante6%   �� 5      -   5        $ txtComprovante��5      �� 5      -   5        
 ��Name$
 btnAddComprovante��5      .��6P   Anexar comprovante6d   �� 5      -   5        $
 btnDelComprovante��5      .��6P   Excluir comprovante6e   �� 5      $��Center=�6   -----------6B   �� 5      -   5        $
 btnSalvarTransacao��5      .��6R   Salvar6Z   �� 5      -	   5        $
 btnCancelarTransacao��5      .��6V   
Cancelar6`   �� 5      -
   5        $
 btnExcluirTransacao��5      .��6T   	Excluir6]   5      5       5!      