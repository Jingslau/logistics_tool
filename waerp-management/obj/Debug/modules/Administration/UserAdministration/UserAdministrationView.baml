   M S B A M L   `   `   `  ����V  RWaerp Inventory Management, Version=0.9.1.15, Culture=neutral, PublicKeyToken=null�Lclr-namespace:waerp_management.application.Administration.UserAdministration>waerp_management.application.Administration.UserAdministration  5      [    Uwaerp_management.application.Administration.UserAdministration.UserAdministrationView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ^localLclr-namespace:waerp_management.application.Administration.UserAdministration     5      _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      ���#EEEEEE   MaterialDesignFont#���   5      
���   �$��13(�5      $��Regular"�5        � 	Resources  5        QuantityCell �� 
TargetType �� Property �� Value  SingleColDataGrid  GridCellPad  LeftGridHeaderStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader  CenterGridHeaderStyle��%A  &	       &	 �     &	 !    &	 �    &	 �    �� 5   
   " ��6%   .�5      ��  �@��TextAlignment$ Center=�6.   �� 5       � ��$ 0,0,0,0q�6'   �� 5       � ��$ Center=�62   5      �� 5   
   " ��6*   .�5      ��  � ��$ Center=�62   �� 5       � ��$ 15,0,0,0q�6'   5      �� 5   
   " ��6$   .�5      ��  � ��$ 15,0,0,0q�6'   �� 5       � ��$
 Wrap=�6-   �� 5       � ��$ Center=�62   5      �� 5    
   "  6,   .�5!      ��  � ��$
 Left=�6;   �� 5"       � ��
 ���� 6+   �� 5#       � �� �#4444446+   �� 5$       � ��$ 15,0,0,0q�6'   �� 5%       � ��$ Medium"�6+   �� 5&       � ��$ 13(�6)   �� 5'       �@�Cursor$
 Hand��6'   5(      �� 5)   
   "  6.   .�5*      ��  � ��$ Center=�6;   �� 5+       � ��
 ���� 6+   �� 5,       � �� �#4444446+   �� 5-       � ��$ Medium"�6+   �� 5.       � ��$ 13(�6)   �� 5/       �@�Cursor$
 Hand��6'   50      51      .��52      �   GridBackgroundMainFrame#��[ 6    � RowDefinitions 53   
   �� 54      $��* �6   �� 55      $��5* �6   .+�57   
   � $��0��58      $��	50,0,50,0q�59      $��Center=�5:       � ColumnDefinitions 5;      �� 5<      $��3* �6#   �� 5=      $��* �6#   .+�5?      �� $��0��5@      $��Stretch=�5A      $��16(�5B      $��Light"�5C       �� TextAlignment$
 Left=�5D      $
��Wrap=�5E      jhUm einen Mitarbeitern zu bearbeiten oder löschen, klicken Sie auf den jeweiligen Button in der Tabelle.�� 5F   y   A?Gelöschte Mitarbeitern können nicht wiederhergestellt werden!5G   P   �� 5H      -   5         ��Name$ 
AddUserBtn��5I      $��1��5J      $	��170��5K      $��Right=�5L      $��Mitarbeiter anlegen��5N        NormalButton#��[ 5O      5Q      � 5R   
   $��1��6    5S      �� 5T      $��* �6    �� 5U      $��7* �6    .+�5W      �� -   5         ��Name$ 	searchBox��5X      $��0��5Y      $��	50,0,50,0q�5Z      $��Stretch=�5[      $��Center=�5\      )  #MaterialDesignThemes.Wpf.HintAssist	   Hint$	 Suche��5]      
 �� AcceptsReturn
 .  � Cursor$
 Hand��5_      {�.   MaterialDesignFilledTextBox#��[ 5a       �� TextWrapping$ NoWrap=�5c      &   System.Windows.Controls.DataGrid  5d      -   5          Name$ UserDataItems��5e      $��1��5f      $��50,20,50,20q�5g      ��5h      ��  �� Path$ itemData�   	CellStyle 5k      �� 5l      *  $System.Windows.Controls.DataGridCell"  6   .�5m      ��  � ��$ 0q�6<   �� 5n       �@�FocusVisualStyle 6=   Z� �� 5o       � ��$ 14(�65   �� 5p       � ��$ 80��66   �� 5q       � ��$ 80��66    �� Triggers 5r      P� 5s       P� Property �@ 
IsSelected P� Value . . �5t   "   ��  � �� �#A94C426?   �� 5u   "    � ��
 �����6?   5v      5z         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5�      1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ 5�         Header$ Mitarbeiter ID��5�         HeaderStyle# [ 5�         
IsReadOnly .    Binding 5�      �� $ 
user_ident�5�        5�      $ 2* 5�      # [ 5�      $ Benutzername��5�      # [ 5�       .  5�      �� $ username�5�        5�      $ 2* 5�      # [ 5�      $ Vorname��5�      # [ 5�       .  5�      �� $
 name�5�        5�      $ 2* 5�      # [ 5�      $ Nachname��5�      # [ 5�       .  5�      �� $ surname�5�        5�      $ 2* 5�      # [ 5�      $ E-Mail��5�      # [ 5�       .  5�      �� $ email�5�        5�      $ * 5�      # [ 5�      $ Berechtigung��5�      # [ 5�       .  5�      �� $ 	role_name�5�      4	  .System.Windows.Controls.DataGridTemplateColumn	  5�      $ * 5�      $ 
Bearbeiten��5�      # [ 5�       .  	  CellTemplate 5�      �� 5�      .R�5�   "   �� $ EditUser��5�   %   -   5        $��40��5�   %   $��40��5�   %   ���#4265A9
������ $��0q�5�   %   
������� +	 'MaterialDesignFloatingActionLightButton#��[	 5�   %   .��5�   &   '
  !MaterialDesignThemes.Wpf.PackIcon
   
  Kind$ PencilOutline=�6>   5�   #   5�      5�      	  5�      $ * 5�      $ Löschen��5�      # [ 5�       .  5�      �� 5�      .R�5�   "   �� $ 
DeleteUser��5�   %   -   5        $��40��5�   %   $��40��5�   %   ���#A94C42
������ $��0q�5�   %   
�������#��[	 5�   %   .��5�   &   
  $ DeleteOutline=�6>   5�   #   5�      5�      5�      5�      5�      5�      