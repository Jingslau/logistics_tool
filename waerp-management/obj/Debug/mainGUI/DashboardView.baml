   M S B A M L   `   `   `  ����V  RWaerp Inventory Management, Version=0.9.1.15, Culture=neutral, PublicKeyToken=null=#clr-namespace:waerp_management.mainwaerp_management.main  5      )    #waerp_management.main.mainDashboard   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:waerp_management.main     5      _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5        N� Title$  mainDashboard��5	       N� 
Background �#9E9E9E   MaterialDesignFont N� 
FontFamily# �   5       N� 
Foreground
 �   �$��13(�5      $��Regular"�5       � 	Resources 5        QuantityCell �� 
TargetType �� Property �� Value  SingleColDataGrid  GridCellPad  LeftGridHeaderStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader  CenterGridHeaderStyle��%  &	       &	 �     &	     &	 �    &	 �    �� 5   
   " ��6%   .�5      ��  �@��TextAlignment$ Center=�6.   �� 5       � ��$ Center=�62   5      �� 5   
   " ��6*   .�5      ��  � ��$ Center=�62   �� 5       � ��$
 Wrap=�6-   �� 5       � ��$ 10,0,0,0q�6'   5      �� 5   
   " ��6$   .�5      ��  � ��$ Center=�62   �� 5       � ��$ 	10,10,0,0q�6'   5      �� 5    
   "  6,   .�5!      ��  � ��$
 Left=�6;   �� 5"       � ��
 ���� 6+   �� 5#       � �� �#4444446+   �� 5$       � ��$ 15,0,0,0q�6'   �� 5%       � ��$ Medium"�6+   �� 5&       � ��$ 13(�6)   �� 5'       �@�Cursor$
 Hand��6'   5(      �� 5)   
   "  6.   .�5*      ��  � ��$ Center=�6;   �� 5+       � ��
 ���� 6+   �� 5,       � �� �#4444446+   �� 5-       � ��$ Medium"�6+   �� 5.       � ��$ 13(�6)   �� 5/       �@�Cursor$
 Hand��6'   50      52      .��53      �   GridBackgroundMainFrame#��[ 6    � ColumnDefinitions 54   
   �� 55      $��3* �6   �� 56      $��1* �6   	 � RowDefinitions	 58   
   �� 59      $��* �6   �� 5:      $��* �6   �� 5;      $��6* �6   .+�5>   
   �� $��0��5?      $��0��5@      $��	20,10,0,0q�5A      
 �� Orientation$
 Vertical=�5B      .+�5C      �� $��0��6   $��20(�6%   Wilkommen zurück!6F   �� 5D      -   5         ��Name$ fullname��5E      $��1��5F      $��20(�5G      $��Light"�5H      ��5I      �� *
Fullname+5J      �� 5K   
   -   5        $��0��5L      $��1��5M      $��50��5N      $��50��5O      $��0,0,30,0q�5P      $��Right=�5Q      $��Bottom=�5R       / +MaterialDesignFloatingActionMiniLightButton#��[ 5T       � ToolTip$ Artikelsuche/scannen��5U      .��5V      '  !MaterialDesignThemes.Wpf.PackIcon  $��25��5W      $��25��5X         Kind$ DatabaseSearch=�5Y      5Z      �� 5`   
   -   5         ��Name$ ReturnSelectedItemBtn��5a      $��1��5b      $��1��5c      $��30��5d      $	��200��5e      $��10q�5f      $��Right=�5g      $��Bottom=�5h      +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 10��5i      
���   �  NumInputButton#��[ 5l      .��5m      �� $��Stretch=�5n      $��Center=�5o      $
 
Horizontal=�5p      .+�5q        $��0,0,5,0q�5r      $��Center=�5s      $ TrayArrowDown=�5t      �� 5u      $
��Wrap=�6   Artikel zurückgeben5w      5x      5y      �� 5z   
   $��1��5{      $��0��5|      $��	20,0,0,10q�5}      $��Bottom=�5~      $��18(�5      $��Normal"�5�      $��..:: Letzte Entnahmen��5�      &   System.Windows.Controls.DataGrid  5�   
   -   5          Name$ dashboardRents��5�      $��2��5�      $��2��5�      $��
20,0,10,20q�5�       � ContextMenu 5�      �� 5�      .@�5�      m� -   5        $��Artikelnummer kopieren��5�   5   m� 5�      -   5        $��Beschreibung kopieren��5�   7   m� 5�      -   5        $��Zeile kopieren��5�   /   5�         	CellStyle 5�      �� 5�      *  $System.Windows.Controls.DataGridCell"  6   .�5�      ��  � ��$ 0q�68   �� 5�       �@�FocusVisualStyle 69   Z� �� 5�       � ��$ 14(�61   �� 5�       � ��$ 50��62   �� 5�       � ��$ 50��62    �� Triggers 5�      P� 5�       P� Property �@ 
IsSelected P� Value . . �5�      ��  � �� �#A94C426;   �� 5�       � ��
 �����6;   5�      5�         Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5�      1	  +System.Windows.Controls.DataGridBoundColumn 	  ElementStyle# [ 5�         Header$ Artikelnummer��5�         
IsReadOnly .  	  Binding 5�      ��  �� Path$ 
item_ident�5�        5�      $ 2* 5�      # [ 5�      $ Bezeichnung��5�       .  5�      �� $ item_description�5�        5�      $ 2* 5�      # [ 5�      $ Bezeichnung 2��5�       .  5�      �� $ item_description_2�5�        5�      $ * 5�      # [ 5�      $ Benutzer��5�       .  5�      �� $ employee�5�        5�      $ * 5�      # [ 5�      $ Menge��5�         HeaderStyle# [ 5�       .  5�      �� $ rent_quantity�5�        5�      $ * 5�      # [ 5�      $ Datum��5�       .  5�      �� $ 	createdAt�5�      5�      5�      5�      