   M S B A M L   `   `   `  ����V  RWaerp Inventory Management, Version=0.9.1.15, Culture=neutral, PublicKeyToken=null{Bclr-namespace:waerp_management.modules.OrderSystem.ManageOldOrders4waerp_management.modules.OrderSystem.ManageOldOrders  5      P    Jwaerp_management.modules.OrderSystem.ManageOldOrders.OldOrdersOverviewView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      TlocalBclr-namespace:waerp_management.modules.OrderSystem.ManageOldOrders     5      _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      ���#EEEEEE   MaterialDesignFont#���   5      
���   �$��13(�5      $��Regular"�5        � 	Resources  5        QuantityCell �� 
TargetType �� Property �� Value  SingleColDataGrid  GridCellPad  LeftGridHeaderStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader  CenterGridHeaderStyle��%�  &	       &	 �     &	 �     &	 o    &	 �    �� 5   
   " ��6%   .�5      ��  �@��TextAlignment$ Center=�6.   �� 5       � ��$ 0,10,0,0q�6'   5      �� 5   
   " ��6*   .�5      ��  � ��$ Center=�62   �� 5       � ��$ 15,0,0,0q�6'   5      �� 5   
   " ��6$   .�5      ��  � ��$ 	15,10,0,0q�6'   �� 5       � ��$
 Wrap=�6-   5      �� 5   
   "  6,   .�5      ��  � ��$
 Left=�6;   �� 5        � ��
 ���� 6+   �� 5!       � �� �#4444446+   �� 5"       � ��$ 15,0,0,0q�6'   �� 5#       � ��$ Medium"�6+   �� 5$       � ��$ 13(�6)   �� 5%       �@�Cursor$
 Hand��6'   5&      �� 5'   
   "  6.   .�5(      ��  � ��$ Center=�6;   �� 5)       � ��
 ���� 6+   �� 5*       � �� �#4444446+   �� 5+       � ��$ Medium"�6+   �� 5,       � ��$ 13(�6)   �� 5-       �@�Cursor$
 Hand��6'   5.      5/      .��50      �   GridBackgroundMainFrame#��[ 6    � RowDefinitions 51   
   �� 52      $
��auto �6   �� 53      $��3* �6    � ColumnDefinitions 56   
   �� 57      $��2* �6   �� 58      $��3* �6   .+�5:   
   � $��0��5;      $��2��5<      $��20,20,50,10q�5=       5>      �� 5?      $��* �6#   .+�5A      �� -   5         ��Name$ instructionText��5B      $��2��5C      $��20,20,20,10q�5D         PageHeadInstructionTextBlock#��[ 5E      ��Dies ist eine Übersicht über vergangene Bestellungen. Diese Bestellungen können nicht rückwirkend geändert werden. Um bereits eingebuchte Bestellungen zu verwalten, nutzen Sie bitte die Umbuchungsoption.�� 5F   �   �� 6�   VTDie Bestellnummer stellt sich zusammen aus: Jahr/Monat/Tag-[Bestellnummer des Tages]5G   g   5H      � 5N   
   $��2��5O      $��0��5P      $��
20,0,20,20q�5Q       5R      �� 5S      $��1* �6    �� 5T      $��5* �6    .+�5V      �� -   5         ��Name$ searchBoxOrder��5W      $��0��5X      $��Stretch=�5Y      $��Center=�5Z      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Bestellnummersuche��5[      	 �� AcceptsReturn	 . 
 � Cursor$

 Hand��5]      {�.   MaterialDesignFilledTextBox#��[ 5_       �� TextWrapping$
 Wrap=�5a      &   System.Windows.Controls.DataGrid  5b      -   5          Name$ 	orderData��5c      $��1��5d      ��5e      ��  �� Path$ 	orderData�   	CellStyle 5g      �� 5h      *  $System.Windows.Controls.DataGridCell"  6   .�5i      ��  � ��$ 0q�6<   �� 5j       �@�FocusVisualStyle 6=   Z� �� 5k       � ��$ 14(�65   �� 5l       � ��$ 40��66   �� 5m       � ��$ 40��66    �� Triggers 5n      P� 5o       P� Property �@ 
IsSelected P� Value . . �5p   "   ��  � �� �#A94C426?   �� 5q   "    � ��
 �����6?   5r      5u         Columns 5w      0  *System.Windows.Controls.DataGridTextColumn  5x      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5y      1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ 5{         Header$ Bestellnummer��5|         
IsReadOnly .    Binding 5z      �� $ order_ident�5}      5      5�      � 5�   
   $��2��5�      $��2��5�      $��2��5�      $��
20,0,20,20q�5�       5�      �� 5�      $��1* �6    �� 5�      $��5* �6    .+�5�      �� -   5        $ 	searchBox��5�      $��0��5�      $��Stretch=�5�      $��Center=�5�      $ Artikelsuche��5�      	 . $

 Hand��5�      {�. #��[ 5�      $
 Wrap=�5�        5�      -   5        $ dataGridItems��5�      $��1��5�      ��5�      �� $ itemData� 5�      �� 5�      "  6   .�5�      ��  � ��$ 0q�6<   �� 5�       �@�FocusVisualStyle 6=   Z� �� 5�       � ��$ 14(�65   �� 5�       � ��$	 100��66   �� 5�       � ��$	 100��66    5�      P� 5�       �@ 
IsSelected . . �5�   "   ��  � �� �#9E9E9E6?   �� 5�   "    � ��
 �����6?   5�      5�       5�        5�      $ * 5�      # [ 5�      $ Art.-Nr.��5�         HeaderStyle# [ 5�       .  5�      �� $ 
item_ident�5�        5�      $
 1.5* 5�      # [ 5�      $ Beschreibung��5�      # [ 5�       .  5�      �� $ item_description�5�        5�      $ * 5�      # [ 5�      $ Menge��5�      # [ 5�       .  5�      �� $ order_quantity_org�5�        5�      $ 2* 5�      # [ 5�      $ Datum��5�      # [ 5�       .  5�      �� $ 	createdAt�5�      5�      5�      5�      5�      